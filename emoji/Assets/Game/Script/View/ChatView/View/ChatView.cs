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
* Created:  2016/1/12 18:23:36
* Author:   HaYaShi ToShiTaKa
* Purpose:  聊天与经验条的界面
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using UnityEngine;

public class ChatView : UIBase {

	/*Anchor_Bottom_Left 聊天面板*/
	private Transform _anchorBottomLeft;

	/*Anchor_Bottom 经验进度条*/
	private Transform _anchorBottom;

	/*Anchor_Left 聊天详细框*/
	private Transform _chatDetail;

	private string playName;
	public TweenPosition _detailTween;
	private ChatUIChatPanel _chatUIChatPanel;
	private ChatDetailView _chatDetailView;

	#region property
	public ChatUIChatPanel chatUIChatPanel {
		get { return _chatUIChatPanel; }
	}
	public ChatDetailView chatDetailView {
		get { return _chatDetailView; }
	}
	#endregion

	#region virtual 重写函数
	public override void OnLoadedUI(bool close3dTouch, object args) {
		base.OnLoadedUI(close3dTouch, args);
		_anchorBottomLeft = transform.Find("Anchor_Bottom_Left");
		_anchorBottom = transform.Find("Anchor_Bottom");
		_chatDetail = transform.Find("Anchor_Left/chat_back");
		_detailTween = _chatDetail.GetComponent<TweenPosition>();

		_detailTween.SetOnFinished(() => {
			if (_detailTween.direction == AnimationOrTween.Direction.Forward) {
				_anchorBottomLeft.gameObject.SetActive(false);
			}
		});
		RegistUIBase<ChatUIExpSlider>(_anchorBottom);
		RegisterEvent(EnumEventDispathcer.ChatViewRefreshPanel,RefreshChatPanel);
		RegisterEvent(EnumEventDispathcer.ChatViewRefreshHistory, RefreshHistory);

		_chatUIChatPanel = RegistUIBase<ChatUIChatPanel>(_anchorBottomLeft);
		_chatDetailView = RegistUIBase<ChatDetailView>(_chatDetail);
		_chatDetail.gameObject.SetActive(false);
		playName = "王五";
	}
	#endregion

	public void OnSendButtonClicked(UIInput iptChat,enumChatType chatType,string text){
		if (iptChat.value == "" || iptChat.value == string.Empty) {
			return;
		}
		if (iptChat.value.Length > ChatDataManager.MAX_TEXT_LENGTH) {
			return;
		}
		ChatDataManager.GetInstance().AddHistory(text);
		SendMessage(text, chatType);
		iptChat.isSelected = false;
		iptChat.value = "";
	}
	public void SendMessage(string text, enumChatType chatType) {
		ChatData data = new ChatData(playName, text, chatType, enumSysInfoType.INFO_TYPE_CHAT, null, 0, 1);
		ChatDataManager.GetInstance().AddChatData(data);
	}
	public void RefreshChatPanel(object args) {
		_chatDetailView.AddTextToChatPanel();
		_chatUIChatPanel.AddTextToChatPanel();
	}
	public void RefreshHistory(object args) {
		_chatDetailView.GetUIBase<ChatUIPopMenu>().RefreshHistory();
	}
	private bool CheckTextIsBeginAt(string text) {
		bool result = false;
		do {
			if (text.Length == 0) break;
			if (text[0] == '@') {
				result = true;
			}
		} while (false);

		return result;
	}
}