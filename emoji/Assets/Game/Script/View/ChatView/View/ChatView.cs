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
	private UIGrid _gridChatPanel;	//聊天grid
	private UIGrid _gridEmojiPanel; //表情gird
	private GameObject _textTemplate; // 文字模板
	private GameObject _audioTemplate;
	private GameObject _voiceOn;
	private GameObject _emojiList;
	private GameObject _emojiTemplate;
	private GameObject _btnZoom;
	private MicroPhoneInput _microPhoneInput;
	private List<Transform> _menuList;

	private ScrollViewItemList<EmojiListItem> _emojiItems;
	private bool _isVoicePressed;
	private int _recordKey;
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
		_btnZoom = transform.Find("all_bf/xinxikuang/Container/zoom").gameObject;
		_gridChatPanel = transform.Find("all_bf/xinxikuang/Container/Scroll View/Grid").GetComponent<UIGrid>();
		_gridEmojiPanel = transform.Find("all_bf/xinxikuang/Container/emoji_list/emojis/Grid").GetComponent<UIGrid>();
		_textTemplate = transform.Find("all_bf/xinxikuang/Container/Scroll View/text_template").gameObject;
		_microPhoneInput = _btnVoiceInput.GetComponent<MicroPhoneInput>();
		_audioTemplate = transform.Find("all_bf/xinxikuang/Container/Scroll View/audio_template").gameObject;
		_voiceOn = transform.Find("all_bf/voice_button /buttom_on").gameObject;
		_emojiList = transform.Find("all_bf/xinxikuang/Container/emoji_list").gameObject;
		_emojiTemplate = transform.Find("all_bf/xinxikuang/Container/emoji_list/emojis/emoji_template").gameObject;
		_audioTemplate.SetActive(false);
		_textTemplate.SetActive(false);
		_emojiTemplate.SetActive(false);
		_emojiList.SetActive(false);

		_emojiNames = new List<string>();
		_menuList = new List<Transform> { _emojiList.transform, _btnEmoji.transform };

		for (int i = 0; i < 10000; i++) {
			_emojiNames.Add("000_2");
		}

		#region 注册点击函数
		RegistUIButton(_btnChatSend, ChatSendClick);
		RegistUIButton(_btnTextInput, TextInputClick);
		RegistUIButton(_btnEmoji, EmojiClick);
		RegistOnPress(_btnVoiceInput, VoiceInputClick);

		Vector3 taskPanelDest = new Vector3(_chatPanel.transform.localPosition.x, _chatPanel.transform.localPosition.y - _chatWidget.height, 0);
		Vector3 finalRotation = new Vector3(0, 0, 0);

		RegistUIButton(_btnZoom, (go) => {
			float duration = 0.3f;
			Vector3 tmpPos = _chatPanel.transform.localPosition;
			Vector3 currotation = _btnZoom.transform.localRotation.eulerAngles;
			TweenPosition tp = TweenPosition.Begin(_chatPanel, duration, taskPanelDest);
			taskPanelDest = tmpPos;
			_btnZoom.transform.localRotation = Quaternion.Euler(finalRotation);
			finalRotation = currotation;
		});
		#endregion
	}

	void Start() {
		_isVoicePressed = false;
		_gridChatPanel.AddScrollViewChild(_textTemplate, @"[王五]:我们都#000好孩子#001", FillText);
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
	private void FillText(Transform fillItem, object data) {
		string text = fillItem.GetComponent<UIEmoji>().CalculateExpression(data.ToString());
		if (text != string.Empty || text != "") {
			_gridChatPanel.AddScrollViewChild(_textTemplate, text, FillText);
		}
	}
	public void FillEmoji(EmojiListItem fillItem, object data) {
		string spriteName = data.ToString();
		fillItem.spItem.spriteName = spriteName;
		RegistUIButton(fillItem.gameObject, (go) => {
			if (spriteName.Length > 3) {
				_iptChat.value = String.Format("{0}#{1}", _iptChat.value, spriteName.Substring(0, 3));
			}
		});
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
	private void VoiceInputClick(GameObject go, bool state) {
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
	private void EmojiClick(GameObject go) {
		if (_emojiList.activeSelf) {
			_emojiList.SetActive(false);
		}
		else {
			_emojiList.SetActive(true);
		}
	}	
	#endregion
}
