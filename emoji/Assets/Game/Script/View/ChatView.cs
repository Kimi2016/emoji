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

public class ChatView : MonoBehaviour {

	private UIInput _iptChat;
	private GameObject _btnChatSend;
	private GameObject _btnTextInput;
	private GameObject _btnVoiceInput;
	private UIGrid _gridChatPanel;	//聊天grid
	private GameObject _textTemplate; // 文字模板

	protected void RegistUIButton(GameObject button, UIEventListener.VoidDelegate action) {
		UIEventListener listener = UIEventListener.Get(button);
		listener.onClick += action;
	}

	#region virtual 重写函数
	void Awake() {

		_iptChat = transform.Find("Anchor_Bottom_Left/all_bf/xinxikuang/InputBox").GetComponent<UIInput>();
		_btnChatSend = transform.Find("Anchor_Bottom_Left/all_bf/xinxikuang/InputBox/button_send").gameObject;
		_btnTextInput = transform.Find("Anchor_Bottom_Left/all_bf/text_button").gameObject;
		_btnVoiceInput = transform.Find("Anchor_Bottom_Left/all_bf/voice_button ").gameObject;
		_gridChatPanel = transform.Find("Anchor_Bottom_Left/all_bf/xinxikuang/Scroll View/Grid").GetComponent<UIGrid>();
		_textTemplate = transform.Find("Anchor_Bottom_Left/all_bf/xinxikuang/Scroll View/text_template").gameObject;

		_textTemplate.SetActive(false);
		#region 注册点击函数
		RegistUIButton(_btnChatSend, ChatSendClick);
		RegistUIButton(_btnTextInput, TextInputClick);
		RegistUIButton(_btnVoiceInput, VoiceInputtClick);
		#endregion
	}

	void Start() {
		_gridChatPanel.AddScrollViewChild(_textTemplate, @"[王五]:我们都#000好孩子#001", FillText);
		//_gridChatPanel.AddScrollViewChild(_textTemplate, @"[王五]:我们都#000好孩子#001", FillText);
	}

	private void FillText(Transform fillItem, object data) {
		string text = fillItem.GetComponent<UIEmoji>().CalculateExpression(data.ToString());
		if (text != string.Empty || text != "") {
			_gridChatPanel.AddScrollViewChild(_textTemplate, text, FillText);
		}
	}

	#endregion

	#region click
	private void ChatSendClick(GameObject sender) {
		if (_iptChat.value == "" || _iptChat.value == string.Empty) {
			return;
		}
		_gridChatPanel.AddScrollViewChild(_textTemplate, "[王五]:" + _iptChat.value, FillText);
		_iptChat.isSelected = false;
		_iptChat.value = "";
	}
	private void VoiceInputtClick(GameObject sender) {
	}
	private void TextInputClick(GameObject sender) {
		_iptChat.isSelected = true;
	}
	#endregion
}
