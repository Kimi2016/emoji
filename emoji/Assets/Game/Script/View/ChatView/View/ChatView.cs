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
* Filename: ChatView
* Created:  2016/1/20 16:46:18
* Author:   HaYaShi ToShiTaKa
* Purpose:  聊天框脚本
* ==============================================================================
*/
using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ChatView : UIBase {

	private UIInput _iptChat;
	private GameObject _btnChatSend;
	private GameObject _btnTextInput;
	private GameObject _btnVoiceInput;
	private GameObject _btnEmoji;
	private UIWidget _chatWidget;
	private GameObject _chatPanel;
	private UITable _tableChatPanel;

	private UIGrid _gridEmojiPanel; //表情gird
	private GameObject _textTemplate; // 文字模板
	private GameObject _voiceOn;
	private GameObject _emojiList;
	private GameObject _emojiTemplate;
	private MicroPhoneInput _microPhoneInput;
	private List<Transform> _menuList;

	private GridItemList<EmojiListItem> _emojiItems;
	private TableItemList<ChatInfoItem> _chatInfoItems;

	private bool _isVoicePressed;
	private int _recordKey;
	private string _playName;
	private List<string> _emojiNames;

	#region virtual 重写函数
	void Awake() {

		_chatPanel = transform.Find("all_bf").gameObject;
		_chatWidget = transform.Find("all_bf/xinxikuang/Container").GetComponent<UIWidget>();
		_iptChat = transform.Find("all_bf/xinxikuang/Container/InputBox").GetComponent<UIInput>();
		_btnChatSend = transform.Find("all_bf/xinxikuang/Container/button_send").gameObject;
		_btnEmoji = transform.Find("all_bf/xinxikuang/Container/button_emoji").gameObject;
		_btnTextInput = transform.Find("all_bf/text_button").gameObject;
		_btnVoiceInput = transform.Find("all_bf/voice_button ").gameObject;
		_gridEmojiPanel = transform.Find("all_bf/xinxikuang/Container/emoji_list/emojis/Grid").GetComponent<UIGrid>();
		_tableChatPanel = transform.Find("all_bf/xinxikuang/Container/Scroll View/Table").GetComponent<UITable>();

		_textTemplate = transform.Find("all_bf/xinxikuang/Container/Scroll View/text_template").gameObject;
		_microPhoneInput = _chatPanel.GetComponent<MicroPhoneInput>();
		_voiceOn = transform.Find("all_bf/voice_button /buttom_on").gameObject;
		_emojiList = transform.Find("all_bf/xinxikuang/Container/emoji_list").gameObject;
		_emojiTemplate = transform.Find("all_bf/xinxikuang/Container/emoji_list/emojis/emoji_template").gameObject;
		_textTemplate.SetActive(false);
		_emojiTemplate.SetActive(false);
		_emojiList.SetActive(false);

		_emojiNames = new List<string>();
		_menuList = new List<Transform> { _emojiList.transform, _btnEmoji.transform };

		_emojiNames.Add("000_2");
		_emojiNames.Add("001_1");

		_playName = "王五";

		#region 注册点击函数
		RegistUIButton(_btnChatSend, ChatSendClick);
		RegistUIButton(_btnEmoji, EmojiClick);
		RegistOnPress(_btnVoiceInput, VoiceInputPress);
		#endregion
	}

	void Start() {
		_isVoicePressed = false;
		RefreshChatPanel();
		_gridEmojiPanel.CreateScrollView<EmojiListItem>(_emojiTemplate, _emojiNames, _emojiItems, this);

	}
	public bool GetMouseButtonDown(int button, List<Transform> parents) {
		if (Input.GetMouseButtonDown(button)) {
			if (!UICamera.isOverUI) return true;
			if (Input.touchCount > 0 && UICamera.Raycast(Input.GetTouch(0).position)) return false;

			Ray ray = UICamera.mainCamera.ScreenPointToRay(Input.mousePosition);
			RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity, 1 << LayerMask.NameToLayer("UI"));
			for (int i = 0; i < hits.Length; i++) {
				if (CheckHasParent(hits[i].collider.transform, parents)) return false;
			}
			return true;
		}

		return false;
	}
	private bool CheckHasParent(Transform current, List<Transform> parents) {
		if (current == null) {
			return false;
		}
		for (int i = 0; i < parents.Count; i++) {
			if (current == parents[i]) {
				return true;
			}
		}
		return CheckHasParent(current.parent, parents);
	}
	void Update() {
		if (GetMouseButtonDown(0, _menuList)) {
			_emojiList.SetActive(false);
		}
	}
	#endregion

	#region refresh
	private void RefreshChatPanel() {
		List<ChatData> chatDataList = ChatDataManager.GetInstance().chatDataList;
		_tableChatPanel.CreateScrollView<ChatInfoItem>(_textTemplate, chatDataList, _chatInfoItems, this);
	}
	#endregion

	#region fill
	public void FillEmoji(EmojiListItem fillItem, object data) {
		string spriteName = data.ToString();
		fillItem.spItem.spriteName = spriteName;
		RegistUIButton(fillItem.gameObject, (go) => {
			if (spriteName.Length > 3) {
				_iptChat.value = String.Format("{0}#{1}", _iptChat.value, spriteName.Substring(0, 3));
			}
		});
	}
	private void StopAudioAnimation(UISpriteAnimation spriteAnimation) {
		spriteAnimation.ResetToBeginning();
		spriteAnimation.Pause();
		_microPhoneInput.ResetAudio();
	}
	#endregion

	#region click
	private void EmojiClick(GameObject go) {
		if (_emojiList.activeSelf) {
			_emojiList.SetActive(false);
		}
		else {
			_emojiList.SetActive(true);
		}
	}
	private void ChatSendClick(GameObject sender) {
		_emojiList.SetActive(false);
		if (_iptChat.value == "" || _iptChat.value == string.Empty) {
			return;
		}
		string text = _iptChat.value;
		ChatData data = new ChatData(_playName, text, enumChatType.CHAT_TYPE_SYSTEM, enumSysInfoType.INFO_TYPE_MSG, null, 0);
		ChatDataManager.GetInstance().AddChatData(data);
		RefreshChatPanel();
		_iptChat.isSelected = false;

		_iptChat.value = "";
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
			_recordKey = TimeOutUtil.getInstance().SchedulerCSFun(() => { }, 0, 0.1f);
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
		float time = TimeOutUtil.getInstance().UnSchedulerCSFun(_recordKey);

		ChatData data = new ChatData(_playName, "#999", enumChatType.CHAT_TYPE_SYSTEM, enumSysInfoType.INFO_TYPE_MSG, voiceData, 0);
		ChatDataManager.GetInstance().AddChatData(data);
		RefreshChatPanel();

		_isVoicePressed = false;
		_voiceOn.SetActive(false);
	}
	#endregion

	#region public
	public void AddTextToChatPanel() {
		RefreshChatPanel();
	}
	#endregion
}
