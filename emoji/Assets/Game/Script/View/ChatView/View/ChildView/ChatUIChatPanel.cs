/*
               #########                       
              ############                     
              #############                    
             ##  ###########                   
            ###  ###### #####                  
            ### #######   ####                 
           ###  ########## ####                
          ####  ########### ####               
         ####   ###########  #####             
        #####   ### ########   #####           
       #####   ###   ########   ######         
      ######   ###  ###########   ######       
     ######   #### ##############  ######      
    #######  #####################  ######     
    #######  ######################  ######    
   #######  ###### #################  ######   
   #######  ###### ###### #########   ######   
   #######    ##  ######   ######     ######   
   #######        ######    #####     #####    
    ######        #####     #####     ####     
     #####        ####      #####     ###      
      #####       ###        ###      #        
        ###       ###        ###              
         ##       ###        ###               
__________#_______####_______####______________

                我们的未来没有BUG              
* ==============================================================================
* Filename: MainUIChatPanel
* Created:  2016/1/10 13:05:32
* Author:   HaYaShi ToShiTaKa
* Purpose:  聊天面板
* ==============================================================================
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatUIChatPanel : UIBase {
	public GameObject _btnTextInput;
	private GameObject _btnVoiceInput;
	private UIWidget _chatWidget;
	private GameObject _chatPanel;
	private UIPanel _chatScrollPanel;
	private UIScrollView _chatScrollView;
	private UITable _tableChatPanel;

	private GameObject _lockMessage;
	private UILabel _labelLock;

	private GameObject _textTemplate; // 文字模板
	private GameObject _voiceOn;
	private MicroPhoneInput _microPhoneInput;

	private GridItemList<EmojiListItem> _emojiItems;
	private TableItemList<ChatInfoItem> _chatInfoItems;

	private Vector3 _tablePosition;
	private Vector3 _scrollPosition;
	private Vector2 _panelOffset;

	private bool _isVoicePressed;
	private int _recordKey;
	private string _playName;
	private int _lockMessgeNum;
	private List<ChatData> _chatDataList;
	#region virtual 重写函数
	public override void OnLoadedUI(bool close3dTouch, object args) {
		base.OnLoadedUI(close3dTouch, args);

		_chatPanel = transform.Find("all_bf").gameObject;
		_chatWidget = transform.Find("all_bf/xinxikuang/Container").GetComponent<UIWidget>();
		_btnTextInput = transform.Find("all_bf/text_button").gameObject;
		_btnVoiceInput = transform.Find("all_bf/voice_button ").gameObject;
		_chatScrollPanel = transform.Find("all_bf/xinxikuang/Container/Scroll View").GetComponent<UIPanel>();
		_chatScrollView = transform.Find("all_bf/xinxikuang/Container/Scroll View").GetComponent<UIScrollView>();
		_tableChatPanel = transform.Find("all_bf/xinxikuang/Container/Scroll View/Table").GetComponent<UITable>();
		_lockMessage = transform.Find("all_bf/xinxikuang/Container/Panel/Message").gameObject;
		_labelLock = transform.Find("all_bf/xinxikuang/Container/Panel/Message/Label").GetComponent<UILabel>();

		_textTemplate = transform.Find("all_bf/xinxikuang/Container/Scroll View/text_template").gameObject;
		_microPhoneInput = _chatPanel.GetComponent<MicroPhoneInput>();
		_voiceOn = transform.Find("all_bf/voice_button /buttom_on").gameObject;
		_textTemplate.SetActive(false);

		_chatInfoItems = new TableItemList<ChatInfoItem>(null, this);

		_playName = "王五";

		_tablePosition = _tableChatPanel.transform.localPosition;
		_scrollPosition = _chatScrollView.transform.localPosition;
		_panelOffset = _chatScrollPanel.clipOffset;
		_chatDataList = ChatDataManager.GetInstance().chatDataList;

		HideLockMessage();
		_chatScrollView.onMomentumMove = () => {
			if (CheckChatPanelBottom() && _lockMessgeNum > 0) {
				RefreshChatPanel();
				HideLockMessage();
			}
		};

		#region 注册点击函数
		RegistOnPress(_btnVoiceInput, VoiceInputPress);
		RegistUIButton(_lockMessage, LockClick);
		RegistUIButton(_btnTextInput, (go) => {
			GetParentUI<ChatView>().chatDetailView.RefreshOpen();
		});
		#endregion
	}
	public override void OnOpenUI(object args) {
		base.OnOpenUI(args);
		RefreshChatPanel();
	}
	public override void OnCloseUI() {
		base.OnCloseUI();
	}
	public override void OnFreeUI() {
		base.OnFreeUI();
	}
	#endregion

	#region refresh
	private bool CheckChatPanelBottom() {
		return _chatScrollView.transform.localPosition.y >= _scrollPosition.y;
	}
	public void RefreshChatPanel() {
		if (gameObject.activeSelf) {
			if (_chatScrollView.transform.localPosition != _scrollPosition || !_chatInfoItems.RefreshTable()) {
				InitRefreshChatPanel();
			}
			if (_chatScrollPanel.onClipMove != null) {
				_chatScrollPanel.onClipMove(_chatScrollPanel);
			}
		}
	}
	private void InitRefreshChatPanel() {
		_chatScrollView.transform.localPosition = _scrollPosition;
		_chatScrollPanel.clipOffset = _panelOffset;
		_tableChatPanel.transform.DestroyChildren();
		_tableChatPanel.transform.localPosition = _tablePosition;
		_tableChatPanel.CreateScrollView<ChatInfoItem>(_textTemplate, _chatDataList, _chatInfoItems, this);
		HideLockMessage();
	}
	private void HideLockMessage() {
		_lockMessage.SetActive(false);
		_lockMessgeNum = 0;
	}
	private void ShowLockMessge() {
		if (gameObject.activeSelf) {
			_lockMessgeNum++;
			_labelLock.text = string.Format("你有{0}条新信息", _lockMessgeNum);
			_lockMessage.SetActive(true);
		}
	}
	#endregion

	#region fill
	public void FillEmoji(EmojiListItem fillItem, object data) {
		string spriteName = data.ToString();
		fillItem.spItem.spriteName = spriteName;
		RegistUIButton(fillItem.gameObject, (go) => {
		});
	}
	private void StopAudioAnimation(UISpriteAnimation spriteAnimation) {
		spriteAnimation.ResetToBeginning();
		spriteAnimation.Pause();
		_microPhoneInput.ResetAudio();
	}
	#endregion

	#region click
	private void LockClick(GameObject go) {
		InitRefreshChatPanel();
	}
	private void VoiceInputPress(GameObject go, bool state) {
		if (state) {
			if (_isVoicePressed) return;
			_isVoicePressed = true;
			_voiceOn.SetActive(true);
			if (_microPhoneInput.isNoDevice) return;
			_microPhoneInput.StartRecord();
			_microPhoneInput.onRecordTimeOut = (waveData) => {
				SendVoice(waveData);
			};
			_recordKey = Director.GetInstance().scheduler.SchedulerCSFun(() => { }, 0, 0.1f);
		}
		else {
			if (_microPhoneInput.isNoDevice) {
				_isVoicePressed = false;
				_voiceOn.SetActive(false);
				return;
			}
			if (!_isVoicePressed) return;
			SendVoice(_microPhoneInput.StopRecord());
		}
	}
	private void SendVoice(Byte[] voiceData) {
		float time = Director.GetInstance().scheduler.UnSchedulerCSFun(_recordKey);

		ChatData data = new ChatData(_playName, "#999", enumChatType.CHANEL_SYSTEM, enumSysInfoType.INFO_TYPE_CHAT, voiceData, 0, 1);
		ChatDataManager.GetInstance().AddChatData(data);
		RefreshChatPanel();

		_isVoicePressed = false;
		_voiceOn.SetActive(false);
	}
	#endregion

	#region public
	public void AddTextToChatPanel() {
		if (!CheckChatPanelBottom()) {
			ShowLockMessge();
			return;
		}
		RefreshChatPanel();
	}
	#endregion
}