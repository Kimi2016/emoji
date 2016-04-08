using System.Collections;
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
* Filename: ChatDetailView
* Created:  2016/3/13 11:23:36
* Author:   HaYaShi ToShiTaKa
* Purpose:  聊天与经验条的界面
* ==============================================================================
*/
using System.Collections.Generic;
using UnityEngine;

public class ChatDetailView : UIBase {

	#region member
	public GameObject _closeButton;

	private UIToggle _toggleSound;
	private GameObject _chatTemplate;
	private UITable _tableChatPanel;
	private UIScrollView _chatScrollView;
	private UIPanel _chatPanel;

	private UIGrid _gridChanel;
	private GameObject _chanelTemplate;

	private GameObject _sendButton;
	private GameObject _recordButton;
	private UIInput _inputBox;
	public UIInput inputBox {
		get { return _inputBox; }
	}
	/* 聊天弹出菜单 */
	private Transform _popMenu;
	private GameObject _lockMessage;
	private UILabel _labelLock;
	private UIPlayTween _playTweeen;
	private TableItemList<ChatDetailItem> _chatInfoItems;

	private int _currentIndex = -1;
	public int currentIndex {
		get { return _currentIndex; }
	}
	private int _lockMessgeNum = 0;
	private Vector3 _tablePosition;
	private Vector3 _scrollPosition;
	private Vector2 _panelOffset;
	private enumChatType chatType;
	private bool inRecord = false;
	public string _inputText;
	List<ChatData> chatDataList;
	private List<bool> _isAutoAudioList = new List<bool>();
	List<Transform> needIgnoreList;
	#endregion

	#region virtual
	public override void OnLoadedUI(bool close3dTouch, object args) {
		base.OnLoadedUI(close3dTouch, args);

		_closeButton = transform.Find("chat_forward/botton_close").gameObject;
		_toggleSound = transform.Find("chat_forward/sound_swith").GetComponent<UIToggle>();
		_chatTemplate = transform.Find("chat_forward/chat_scroll/chat_template").gameObject;
		_tableChatPanel = transform.Find("chat_forward/chat_scroll/Table").GetComponent<UITable>();
		_chatScrollView = transform.Find("chat_forward/chat_scroll").GetComponent<UIScrollView>();
		_chatPanel = transform.Find("chat_forward/chat_scroll").GetComponent<UIPanel>();

		_gridChanel = transform.Find("chat_forward/grid_chanel").GetComponent<UIGrid>();
		_chanelTemplate = transform.Find("chat_forward/chanel_button_template").gameObject;

		_sendButton = transform.Find("chat_forward/send_group/botton_send").gameObject;
		_recordButton = transform.Find("chat_forward/send_group/botton_v").gameObject;
		_inputBox = transform.Find("chat_forward/send_group").GetComponent<UIInput>();
		_playTweeen = transform.Find("chat_forward/send_group/botton_menu").GetComponent<UIPlayTween>();

		_popMenu = transform.Find("Panel/Pop");
		_lockMessage = transform.Find("Panel/Message").gameObject;
		_labelLock = transform.Find("Panel/Message/Label").GetComponent<UILabel>();

		chatDataList = ChatDataManager.GetInstance().chatDataList;
		_tablePosition = _tableChatPanel.transform.localPosition;
		_scrollPosition = _chatScrollView.transform.localPosition;
		_panelOffset = _chatPanel.clipOffset;
		needIgnoreList = new List<Transform> { _popMenu, _playTweeen.transform };
		_chatInfoItems = new TableItemList<ChatDetailItem>(null, this);
		chatType = enumChatType.CHANEL_CURRENT;

		HideLockMessage();
		_popMenu.gameObject.SetActive(false);
		_chanelTemplate.SetActive(false);
		_chatTemplate.SetActive(false);

		EventDelegate.Add(_inputBox.onChange, InputTextChange);
		EventDelegate.Add(_toggleSound.onChange, () => {
			_isAutoAudioList[(int)chatType] = _toggleSound.value;
		});

		_chatScrollView.onMomentumMove = () => {
			if (CheckChatPanelBottom() && _lockMessgeNum > 0) {
				RefreshChangeTap();
			}
		};
		_gridChanel.CreateScrollView(_chanelTemplate, GameConst.ChanelList, FillChanelItem);
		
		RegistUIBase<ChatUIPopMenu>(_popMenu);
		RegistUIButton(_closeButton, CloseClick);
		RegistUIButton(_sendButton, SendClick);
		RegistUIButton(_recordButton, RecordClick);
		RegistUIButton(_lockMessage, LockClick);

	}

	public override void OnOpenUI(object args) {
		base.OnOpenUI(args);
		RefreshChatPanel();
	}
	public override void OnCloseUI() {
		base.OnCloseUI();
	}
	void Update() {
		if (InputUtility.GetMouseButtonDown(0, needIgnoreList)) {
			if (_popMenu.gameObject.activeSelf) {
				_playTweeen.Play(true);
			}
		}
	}
	#endregion

	#region fill
	private void FillChanelItem(Transform fillItem, object data) {
		string title = data.ToString();
		int index = GameConst.ChanelList.IndexOf(title);
		enumChatType chatType = (enumChatType)index;
		if (chatType == enumChatType.CHANEL_CURRENT) {
			fillItem.GetComponent<UIToggle>().startsActive = true;
			this.chatType = chatType;
		}
		_isAutoAudioList.Add(false);
		fillItem.Find("xuanzhong/Label").GetComponent<UILabel>().text = title;
		fillItem.Find("weixuan/Label").GetComponent<UILabel>().text = title;

		RegistUIButton(fillItem.gameObject, (go) => {
			this.chatType = chatType;
			OnChatTapClicked(chatType);
			_toggleSound.value = _isAutoAudioList[index];
		});
	}
	public void FillEmoji(EmojiListItem emojiItem, string datas) {
		RegistUIButton(emojiItem.spItem.gameObject, (go) => {
			_inputBox.value += datas;

		});
	}
	public void FillHistoryTalk(ChatHistoryItem talkItem, string datas) {
		RegistUIButton(talkItem.gameObject, (go) => {
			SetText(datas);
		});
	}
	public void FillPetItem(ChatPetItem petItem, string text) {
		UrlTextClick(text, petItem.gameObject);
	}
	public void FillTaskItem(ChatTaskItem taskItem, string text) {
		UrlTextClick(text, taskItem.gameObject);
	}
	public void FillHonorItem(ChatHonorItem honorItem, string text) {
		UrlTextClick(text, honorItem.gameObject);
	}
	private void InputTextChange() {
		string text = _inputBox.value;
		if (!string.IsNullOrEmpty(_inputText)) {
			string stripText = NGUIText.StripSymbols(_inputText);
			if (text.Contains(stripText)) {
				text = text.Replace(stripText, _inputText);
			}
		}
		_inputText = text;
	}
	#endregion

	#region click
	private void LockClick(GameObject go) {
		RefreshChangeTap();
	}
	private void UrlTextClick(string text, GameObject sender) {
		RegistUIButton(sender, (go) => {
			SetText(text);
		});
	}
	private void SetText(string text) {
		//string preText = _inputText;
		_inputBox.value = NGUIText.StripSymbols(text);
		_inputText = text;
	}
	public void CloseClick(GameObject sender) {
		_popMenu.gameObject.SetActive(false);
		GetParentUI<ChatView>().chatUIChatPanel.gameObject.SetActive(true);
		GetParentUI<ChatView>().chatUIChatPanel.RefreshChatPanel();
	}
	public void SendClick(GameObject sender) {
		ChatView parentUI = GetParentUI<ChatView>();
		parentUI.OnSendButtonClicked(_inputBox, chatType, _inputText);
	}
	void RecordClick(GameObject sender) {
	}
	#endregion

	private bool CheckChatPanelBottom() {
		return _chatScrollView.transform.localPosition.y >= _scrollPosition.y;
	}
	public void RefreshChatPanel() {
		if (gameObject.activeSelf) {
			if (!_chatInfoItems.MoveUpTable()) {
				RefreshChangeTap();
			}
		}
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
	public void RefreshOpen() {
		if (gameObject.activeSelf) {
			if (_chatScrollView.transform.localPosition != _scrollPosition || !_chatInfoItems.RefreshTable()) {
				RefreshChangeTap();
			}
		}
	}
	public void RefreshChangeTap() {
		HideLockMessage();
		_chatScrollView.transform.localPosition = _scrollPosition;
		_chatPanel.clipOffset = _panelOffset;
		_tableChatPanel.transform.DestroyChildren();
		_tableChatPanel.transform.localPosition = _tablePosition;
		_tableChatPanel.CreateScrollView<ChatDetailItem>(_chatTemplate, chatDataList, _chatInfoItems, this);
	}
	private void OnChatTapClicked(enumChatType chatType) {
		if (chatType == enumChatType.CHANEL_CURRENT) {
			chatDataList = ChatDataManager.GetInstance().chatDataList;
		}
		else {
			chatDataList = ChatDataManager.GetInstance().SortChatData(chatType);
		}
		RefreshChangeTap();
	}
	public void AddTextToChatPanel() {
		if (!CheckChatPanelBottom()) {
			ShowLockMessge();
			return;
		}
		RefreshChatPanel();
	}
}