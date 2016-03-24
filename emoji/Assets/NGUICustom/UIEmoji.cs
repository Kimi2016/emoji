//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------
using System;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(UILabel))]
public class UIEmoji : UIBase {
	//表情动画数据
	public Dictionary<string, AnimationVO> animationConfig = new Dictionary<string, AnimationVO> { 
		{"000",new AnimationVO(2,false,2,false)},
		{"001",new AnimationVO(6,false,2,false)},
		{"995",new AnimationVO(6,true,2,false)},//宠物经验图标
		{"996",new AnimationVO(6,true,2,false)},//钻石图标
		{"997",new AnimationVO(6,true,2,false)},//金币图标
		{"998",new AnimationVO(6,true,2,false)},//人物经验图标
		{"999",new AnimationVO(6,true,2,true)},//声音的播放
	};
	UILabel _lblChatInfo;
	ChatData chatData;
	bool isMulline = false;
	MicroPhoneInput _microPhoneInput = null;

	public string SetText(ChatData chatData) {

		this.chatData = chatData;
		_microPhoneInput = NGUITools.FindInParents<MicroPhoneInput>(gameObject);

		string text = String.Format("[00ff00]【{0}】[-]:{1}", chatData.playName, chatData.text);
		_lblChatInfo = transform.GetComponent<UILabel>();

		int length = text.Length;
		string result = "";
		bool isChangeRow = false;
		int spaceNum;
		isMulline = false;
		if (length <= 0) return result;

		string tripString = NGUIText.StripSymbols(text);

		// 先用没有富文本的文本定位表情
		tripString = ReplaceAnimation(tripString);

		// 然后在对原本的文本进行裁剪
		for (int i = 0; i < length; i++) {
			// 判断是否是emoji
			if (text[i] == '#' && i + 3 < length) {
				if (Char.IsNumber(text[i + 1]) &&
					Char.IsNumber(text[i + 2]) &&
					Char.IsNumber(text[i + 3])) {
					string faceName = text.Substring(i + 1, 3);
					AnimationVO aniConfig = GetAnimationConfigByIndex(faceName);

					if (aniConfig.isNotPlay && chatData.voiceData == null) {
						text = text.Remove(i, 4);
						length = tripString.Length;
						continue;
					}

					spaceNum = aniConfig.spaceNum;
					text = text.Remove(i, 1);
					for (int j = 0; j < spaceNum; j++) {
						text = text.Insert(i, "s");
					}
					int delta;
					for (delta = 1; delta < spaceNum; delta++) {
						isChangeRow = CheckIsChangeRow(text.Substring(0, i + delta + 1));
						if (isChangeRow) break;
					}


					text = text.Remove(i, spaceNum + 3);
					if (isChangeRow) {
						//text = text.Insert(i, "\n");
						for (; delta > 0; delta--) {
							spaceNum++;
						}
					}
					for (int j = 0; j < spaceNum; j++) {
						text = text.Insert(i, " ");
					}

					length = text.Length;
				}
			}
		}
		_lblChatInfo.text = text;
		return result;
	}
	public string SetText(string text) {
		_lblChatInfo = GetComponent<UILabel>();

		int length = text.Length;
		string result = "";
		bool isChangeRow = false;
		int spaceNum;
		isMulline = false;
		if (length <= 0) return result;

		string tripString = NGUIText.StripSymbols(text);

		// 先用没有富文本的文本定位表情
		tripString = ReplaceAnimationNotChat(tripString);

		// 然后在对原本的文本进行裁剪
		for (int i = 0; i < length; i++) {
			// 判断是否是emoji
			if (text[i] == '#' && i + 3 < length) {
				if (Char.IsNumber(text[i + 1]) &&
					Char.IsNumber(text[i + 2]) &&
					Char.IsNumber(text[i + 3])) {
					string faceName = text.Substring(i + 1, 3);
					AnimationVO aniConfig = GetAnimationConfigByIndex(faceName);

					spaceNum = aniConfig.spaceNum;
					text = text.Remove(i, 1);
					for (int j = 0; j < spaceNum; j++) {
						text = text.Insert(i, "s");
					}
					int delta;
					for (delta = 1; delta < spaceNum; delta++) {
						isChangeRow = CheckIsChangeRow(text.Substring(0, i + delta + 1));
						if (isChangeRow) break;
					}


					text = text.Remove(i, spaceNum + 3);
					if (isChangeRow) {
						//text = text.Insert(i, "\n");
						for (; delta > 0; delta--) {
							spaceNum++;
						}
					}
					for (int j = 0; j < spaceNum; j++) {
						text = text.Insert(i, " ");
					}

					length = text.Length;
				}
			}
		}
		_lblChatInfo.color = Color.white;
		_lblChatInfo.text = text;
		return result;
	}

	private string ReplaceAnimationNotChat(string tripString) {
		_lblChatInfo.text = tripString;
		int cellLength = _lblChatInfo.fontSize;
		int length = tripString.Length;

		GameObject sprite;
		UISprite spFace;
		UISpriteAnimation spaFace;
		GameObject faceTemplate = transform.Find("Sprite").gameObject;

		Transform emojiContainer = transform.Find("emojis");
		emojiContainer.DestroyChildren();

		BetterList<Vector3> vets = new BetterList<Vector3>();
		BetterList<int> indexs = new BetterList<int>();
		bool isChangeRow = false;
		int spaceNum;
		int index = 0;

		_lblChatInfo.UpdateNGUIText();
		NGUIText.PrintExactCharacterPositions(tripString, vets, indexs);

		for (int i = 0; i < length; i++) {
			// 判断是否是emoji
			if (tripString[i] == '#' && i + 3 < length) {
				if (Char.IsNumber(tripString[i + 1]) &&
					Char.IsNumber(tripString[i + 2]) &&
					Char.IsNumber(tripString[i + 3])) {
					// 是就把它替换成表情图片
					string faceName = tripString.Substring(i + 1, 3);
					AnimationVO aniConfig = GetAnimationConfigByIndex(faceName);

					tripString = tripString.Remove(i, 1);
					spaceNum = aniConfig.spaceNum;
					for (int j = 0; j < spaceNum; j++) {
						tripString = tripString.Insert(i, "s");
					}

					_lblChatInfo.text = tripString;
					vets.Clear();
					indexs.Clear();
					_lblChatInfo.UpdateNGUIText();
					NGUIText.PrintExactCharacterPositions(tripString, vets, indexs);
					index = i;
					int delta;
					for (delta = 1; delta < spaceNum; delta++) {
						isChangeRow = CheckIsChangeRow(tripString.Substring(0, i + delta + 1), ref index);
						if (isChangeRow) break;
					}

					//#000#000#00011111111111#000#000#000#000#000
					tripString = tripString.Remove(i, spaceNum + 3);
					if (isChangeRow) {
						for (; delta > 0; delta--) {
							spaceNum++;
						}
					}
					for (int j = 0; j < spaceNum; j++) {
						tripString = tripString.Insert(i, " ");
					}

					sprite = GameObject.Instantiate(faceTemplate);
					spFace = sprite.GetComponent<UISprite>();
					spaFace = sprite.GetComponent<UISpriteAnimation>();
					UISpriteData spriteData = spFace.atlas.GetSprite(faceName + "_1");
					float rate = (float)spriteData.height / (float)spriteData.width;
					spFace.GetComponent<UIWidget>().width = (int)(cellLength * spaceNum * 0.5f);
					spFace.GetComponent<UIWidget>().height = (int)(cellLength * spaceNum * 0.5f * rate);

					sprite.transform.parent = emojiContainer;
					spFace.spriteName = faceName + "_1";
					spaFace.namePrefix = faceName + "_";

					if (aniConfig != null) {
						if (aniConfig.isNotPlay) {
							spaFace.enabled = false;
							StopAudioAnimation(spaFace);
						}
						spaFace.framesPerSecond = aniConfig.rate;
					}
					else {
						Debug.LogWarning("警告，请把对应动画的配置文件填写掉");
					}
					Vector3 tmpPos;
					sprite.transform.localScale = Vector3.one;
					tmpPos = vets[2 * index];
					tmpPos.y = vets[2 * index + 1].y;

					sprite.transform.localPosition = tmpPos;

					sprite.SetActive(true);

					length = tripString.Length;
				}
			}
		}


		return tripString;
	}
	private string ReplaceAnimation(string tripString) {
		_lblChatInfo.text = tripString;
		int cellLength = _lblChatInfo.fontSize;
		int length = tripString.Length;

		GameObject sprite;
		UISprite spFace;
		UISpriteAnimation spaFace;
		GameObject faceTemplate = transform.Find("Sprite").gameObject;
		Transform emojiContainer = transform.Find("emojis");
		emojiContainer.DestroyChildren();

		BetterList<Vector3> vets = new BetterList<Vector3>();
		BetterList<int> indexs = new BetterList<int>();
		bool isChangeRow = false;
		int spaceNum;
		int index = 0;

		_lblChatInfo.UpdateNGUIText();
		NGUIText.PrintExactCharacterPositions(tripString, vets, indexs);

		for (int i = 0; i < length; i++) {
			// 判断是否是emoji
			if (tripString[i] == '#' && i + 3 < length) {
				if (Char.IsNumber(tripString[i + 1]) &&
					Char.IsNumber(tripString[i + 2]) &&
					Char.IsNumber(tripString[i + 3])) {
					// 是就把它替换成表情图片
					string faceName = tripString.Substring(i + 1, 3);
					AnimationVO aniConfig = GetAnimationConfigByIndex(faceName);

					if (aniConfig.isNotPlay && chatData.voiceData == null) {
						tripString = tripString.Remove(i, 4);
						length = tripString.Length;
						continue;
					}

					tripString = tripString.Remove(i, 1);
					spaceNum = aniConfig.spaceNum;
					for (int j = 0; j < spaceNum; j++) {
						tripString = tripString.Insert(i, "s");
					}

					_lblChatInfo.text = tripString;
					vets.Clear();
					indexs.Clear();
					_lblChatInfo.UpdateNGUIText();
					NGUIText.PrintExactCharacterPositions(tripString, vets, indexs);
					index = i;
					int delta;
					for (delta = 1; delta < spaceNum; delta++) {
						isChangeRow = CheckIsChangeRow(tripString.Substring(0, i + delta + 1), ref index);
						if (isChangeRow) break;
					}

					//#000#000#00011111111111#000#000#000#000#000
					tripString = tripString.Remove(i, spaceNum + 3);
					if (isChangeRow) {
						for (; delta > 0; delta--) {
							spaceNum++;
						}
					}
					for (int j = 0; j < spaceNum; j++) {
						tripString = tripString.Insert(i, " ");
					}

					sprite = GameObject.Instantiate(faceTemplate);
					spFace = sprite.GetComponent<UISprite>();
					spaFace = sprite.GetComponent<UISpriteAnimation>();

					spFace.GetComponent<UIWidget>().width = (int)(cellLength * spaceNum * 0.5f);
					spFace.GetComponent<UIWidget>().height = cellLength;

					sprite.transform.parent = emojiContainer;
					spFace.spriteName = faceName + "_1";
					spaFace.namePrefix = faceName + "_";

					if (aniConfig != null) {
						if (aniConfig.isNotPlay) {
							StopAudioAnimation(spaFace);
							if (aniConfig.isAudio) {
								RegistUIButton(gameObject, (go) => {
									if (chatData.voiceData == null) return;
									Byte[] wavData = SevenZipCompress.Decompress(chatData.voiceData);
									_microPhoneInput.PlayClipData(wavData);
									spaFace.Play();
									Director.GetInstance().scheduler.SetTimeOut(chatData.time, () => {
										StopAudioAnimation(spaFace);
									});
								});
							}
						}
						spaFace.framesPerSecond = aniConfig.rate;
					}
					else {
						Debug.LogWarning("警告，请把对应动画的配置文件填写掉");
					}
					Vector3 tmpPos;
					sprite.transform.localScale = Vector3.one;
					tmpPos = vets[2 * index];
					tmpPos.y = vets[2 * index + 1].y;

					sprite.transform.localPosition = tmpPos;

					sprite.SetActive(true);

					length = tripString.Length;
				}
			}
			//if (vets[2 * i + 1].y != vets[1].y) {
			//	isMulline = true;
			//	return tripString.Substring(0, i + 1);
			//}
		}


		return tripString;
	}
	private void StopAudioAnimation(UISpriteAnimation spriteAnimation) {
		spriteAnimation.ResetToBeginning();
		spriteAnimation.Pause();
		if (_microPhoneInput != null) {
			_microPhoneInput.ResetAudio();
		}
	}
	private bool CheckIsChangeRow(string text) {
		bool result = false;

		BetterList<Vector3> vets = new BetterList<Vector3>();
		BetterList<int> indexs = new BetterList<int>();

		_lblChatInfo.text = text;
		vets.Clear();
		indexs.Clear();
		_lblChatInfo.UpdateNGUIText();
		NGUIText.PrintExactCharacterPositions(text, vets, indexs);

		if (vets[indexs.size * 2 - 1].y != vets[indexs.size * 2 - 3].y) {
			result = true;
		}

		return result;
	}
	private bool CheckIsChangeRow(string text, ref int index) {
		bool result = false;

		BetterList<Vector3> vets = new BetterList<Vector3>();
		BetterList<int> indexs = new BetterList<int>();

		_lblChatInfo.text = text;
		vets.Clear();
		indexs.Clear();
		_lblChatInfo.UpdateNGUIText();
		NGUIText.PrintExactCharacterPositions(text, vets, indexs);

		if (vets[indexs.size * 2 - 1].y != vets[indexs.size * 2 - 3].y) {
			result = true;
			index = indexs.size - 1;
		}

		return result;
	}
	public AnimationVO GetAnimationConfigByIndex(string index) {
		AnimationVO result = null;
		animationConfig.TryGetValue(index, out result);
		if (result == null) {
			Debug.Log("获取配置为空，执行下去会出问题");
		}
		return result;
	}
}

public class AnimationVO {
	public readonly int rate;
	public readonly bool isNotPlay;
	public readonly int spaceNum;
	public readonly bool isAudio;
	public AnimationVO(int rate, bool isNotPlay, int spaceNum, bool isAudio) {
		this.rate = rate;
		this.isNotPlay = isNotPlay;
		this.spaceNum = spaceNum;
		this.isAudio = isAudio;
	}
}
