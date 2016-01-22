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

public class ChatView : MonoBehaviour {

	private UIInput _iptChat;
	private GameObject _btnChatSend;
	private GameObject _btnTextInput;
	private GameObject _btnVoiceInput;
	private UIGrid _gridChatPanel;	//聊天grid
	private GameObject _textTemplate; // 文字模板
	private GameObject _audioTemplate;
	private GameObject _voiceOn;
	private MicroPhoneInput _microPhoneInput;
	private bool _isVoicePressed;
	private int _recordKey;
	protected void RegistUIButton(GameObject button, UIEventListener.VoidDelegate action) {
		UIEventListener listener = UIEventListener.Get(button);
		listener.onClick += action;
	}
	protected void RegistUIPress(GameObject button, UIEventListener.BoolDelegate action) {
		UIEventListener listener = UIEventListener.Get(button);
		listener.onPress += action;
	}
	#region virtual 重写函数
	void Awake() {

		_iptChat = transform.Find("Anchor_Bottom_Left/all_bf/xinxikuang/InputBox").GetComponent<UIInput>();
		_btnChatSend = transform.Find("Anchor_Bottom_Left/all_bf/xinxikuang/InputBox/button_send").gameObject;
		_btnTextInput = transform.Find("Anchor_Bottom_Left/all_bf/text_button").gameObject;
		_btnVoiceInput = transform.Find("Anchor_Bottom_Left/all_bf/voice_button ").gameObject;
		_gridChatPanel = transform.Find("Anchor_Bottom_Left/all_bf/xinxikuang/Scroll View/Grid").GetComponent<UIGrid>();
		_textTemplate = transform.Find("Anchor_Bottom_Left/all_bf/xinxikuang/Scroll View/text_template").gameObject;
		_microPhoneInput = _btnVoiceInput.GetComponent<MicroPhoneInput>();
		_audioTemplate = transform.Find("Anchor_Bottom_Left/all_bf/xinxikuang/Scroll View/audio_template").gameObject;
		_voiceOn = transform.Find("Anchor_Bottom_Left/all_bf/voice_button /buttom_on").gameObject;

		_audioTemplate.SetActive(false);
		_textTemplate.SetActive(false);

		#region 注册点击函数
		RegistUIButton(_btnChatSend, ChatSendClick);
		RegistUIButton(_btnTextInput, TextInputClick);
		RegistUIPress(_btnVoiceInput, VoiceInputtClick);
		#endregion
	}

	void Start() {
		_isVoicePressed = false;
		_gridChatPanel.AddScrollViewChild(_textTemplate, @"[王五]:我们都#000好孩子#001", FillText);

	}

	private void FillText(Transform fillItem, object data) {
		string text = fillItem.GetComponent<UIEmoji>().CalculateExpression(data.ToString());
		if (text != string.Empty || text != "") {
			_gridChatPanel.AddScrollViewChild(_textTemplate, text, FillText);
		}
	}

	private void FillAudio(Transform fillItem, object data) {
		GameObject audioButton = fillItem.Find("audio_sprite").gameObject;
		UISpriteAnimation spriteAnimation = audioButton.GetComponent<UISpriteAnimation>();
		StopAudioAnimation(spriteAnimation);

		RegistUIButton(audioButton, (go) => {
			Hashtable hh = data as Hashtable;
			Byte[] wavData = SevenZipCompress.Decompress(hh["data"] as Byte[]);
			_microPhoneInput.PlayClipData(wavData);
			spriteAnimation.Play();
			TimeOutUtil.getInstance().setTimeOut((float)hh["time"], () => {
				StopAudioAnimation(spriteAnimation);
			});
		});
	}
	private void StopAudioAnimation(UISpriteAnimation spriteAnimation) {
		spriteAnimation.ResetToBeginning();
		spriteAnimation.Pause();
		_microPhoneInput.ResetAudio();
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
	private void TextInputClick(GameObject sender) {
		_iptChat.isSelected = true;
	}
	private void VoiceInputtClick(GameObject go, bool state) {
		if (state) {
			if (_isVoicePressed) return;
			_isVoicePressed = true;
			_voiceOn.SetActive(true);
			_microPhoneInput.StartRecord();
			_microPhoneInput.onRecordTimeOut = (waveData) => {
				SendVoice(waveData);
			};
			_recordKey = TimeOutUtil.getInstance().SchedulerCSFun(() => { },0,0.1f);
		}
		else {
			if (!_isVoicePressed) return;
			SendVoice(_microPhoneInput.StopRecord());
		}
	}
	private void SendVoice(Byte[] data) {
		Hashtable hh = new Hashtable();
		float time = TimeOutUtil.getInstance().UnSchedulerCSFun(_recordKey);
		hh["data"] = data;
		hh["time"] = time;
		print("time is:" + time);
		_gridChatPanel.AddScrollViewChild(_audioTemplate, hh, FillAudio);
		_isVoicePressed = false;
		_voiceOn.SetActive(false);
	}
	#endregion
}
