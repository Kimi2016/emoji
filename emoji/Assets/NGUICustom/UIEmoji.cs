//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(UILabelCustom))]

public class UIEmoji : UIBase {

	#region member
	UILabelCustom _lblChatInfo;
	ChatData chatData = null;
	MicroPhoneInput _microPhoneInput = null;
	Transform emojiContainer;
	GameObject faceTemplate;
	bool isColorSet = false;
	Color curColor;

	float cellLength;
	// 每一行的起点文字索引，以及对应的换行量
	Dictionary<int, int> rowDict = new Dictionary<int, int>();
	// 每一行的起点文字索引
	List<int> rowList = new List<int>();
	// 当前插入换行符的位置
	int lastRow;
	// 下一次要插入换行符的位置
	int nextRow;
	// 当前换行符在索引中的位置
	int rowListIndex;
	// 替换图文字的时候，插入图片的位置
	Vector3 picIndex;
	// 处于同一行的的表情
	List<GameObject> sprites;
	public int height = 0;
	public delegate void FillEmoji(string faceName);
	private bool _isUseUrl = true;
	public bool isUseUrl {
		get {
			return _isUseUrl;
		}
		set {
			UnRegistUIButton(_lblChatInfo.gameObject);
			if (value) {
				RegistUIButton(_lblChatInfo.gameObject, (go) => {
					string urlText = GetUrlAtPosition(UICamera.lastHit.point);
					if (!string.IsNullOrEmpty(urlText)) {
					}
				});
			}
			_isUseUrl = value;
		}
	}
	#endregion

	#region public
	public float SetChatData(ChatData chatData) {
		return SetChatData(chatData, chatData.text);
	}
	public float SetChatData(ChatData chatData, string text) {
		this.chatData = chatData;
		return SetText(text);
	}

	public string GetUrlAtPosition(Vector3 point) {
		return _lblChatInfo.GetUrlAtPosition(point);
	}
	public float SetText(string text) {
		if (_microPhoneInput == null) {
			_microPhoneInput = NGUITools.FindInParents<MicroPhoneInput>(gameObject);
			emojiContainer = transform.Find("emojis");
			faceTemplate = transform.Find("Sprite").gameObject;
			faceTemplate.SetActive(false);
			_lblChatInfo = transform.GetComponent<UILabelCustom>();
			if (!isColorSet) {
				curColor = _lblChatInfo.color;
				isColorSet = true;
			}
			if (_lblChatInfo.gameObject.GetComponent<BoxCollider>() == null) {
				_lblChatInfo.gameObject.AddComponent<BoxCollider>();
			}
			
		}

		emojiContainer.DestroyChildren();

		_lblChatInfo.color = Color.white;
		text = NGUIText.EncodeColor(text, curColor);
		_lblChatInfo.ProcessText();
		_lblChatInfo.UpdateNGUIText();
		cellLength = _lblChatInfo.fontSize + _lblChatInfo.floatSpacingY;

		_lblChatInfo.text = SetEmoji(text, (faceName) => {
			AnimationVO aniConfig;
			aniConfig = GameConst.GetAnimationConfigByIndex(faceName);
			//根据文本算出表情的初步坐标（如果后面添加了更高的表情需要重新调整位置）
			GameObject sprite = ReplaceEmojiWithPicture(faceName);
			Vector3 temPos;
			sprites.Add(sprite);

			//对齐同一行的表情
			float vetY = sprite.transform.localPosition.y - cellLength * (aniConfig.rowNum - 1);
			for (int i = 0; i < sprites.Count; i++) {
				aniConfig = GameConst.GetAnimationConfigByIndex(sprites[i].name);
				temPos = sprites[i].transform.localPosition;
				temPos.y = vetY + (aniConfig.rowNum - 1) * cellLength;
				sprites[i].transform.localPosition = temPos;
			}
		});
		_lblChatInfo.ResizeCollider();
		isUseUrl = _isUseUrl;
		return rowDict[rowList[0]] * cellLength;
	}
	public int GetHeight(string text) {
		_lblChatInfo.UpdateNGUIText();
		_lblChatInfo.text = SetEmoji(text, null);
		return _lblChatInfo.height;
	}
	#endregion

	#region 主要方法
	// 替换图文字
	private string SetEmoji(string text, FillEmoji fillEmoji) {
		string result = "";
		bool isChangeRow = false;
		int spaceNum = 0;
		int rowNum = 1;
		int length = text.Length;
		string faceName = "";
		if (length <= 0) return result;
		
		//每一行的sprite
		sprites = new List<GameObject>();
		// 每一行的起点文字索引，以及对应的换行量
		rowDict.Clear();
		rowList.Clear();

		// 当前插入换行符的位置
		rowListIndex = 0;
		picIndex = SetRealRowList(text, -1);

		for (int i = 0; i < length; i++) {
			isChangeRow = false;

			// 判断是否是emoji
			int lastIndex = i;
			bool isReplace = ReplaceEmojiText(ref text, ref i, ref rowNum, ref spaceNum, ref isChangeRow,ref faceName);
			if (isReplace) {
				// 图片换行
				if (isChangeRow) {
					sprites.Clear();
				}
				// 绘制图片
				if (fillEmoji != null) {
					fillEmoji(faceName);
				}
			}
			// 文字换行
			if (!isChangeRow) {
				if (i >= nextRow) {
					SetChangeRow(ref rowNum, sprites);
					rowListIndex++;
					lastRow = rowList[rowListIndex];
					nextRow = GetNextRowIndex();
					rowDict[lastRow] = 0;
				}
			}

			length = text.Length;
		}
		result = text;

		return result;
	}
	// 在空格处放上表情动画
	private GameObject ReplaceEmojiWithPicture(string faceName) {
		GameObject sprite;
		UISprite spFace;
		UISpriteAnimation spaFace;
		AnimationVO aniConfig = GameConst.GetAnimationConfigByIndex(faceName);

		sprite = GameObject.Instantiate(faceTemplate);
		spFace = sprite.GetComponent<UISprite>();
		spaFace = sprite.GetComponent<UISpriteAnimation>();

		spFace.GetComponent<UIWidget>().width = (int)(cellLength * aniConfig.spaceNum * 0.5f);
		spFace.GetComponent<UIWidget>().height = (int)cellLength * aniConfig.rowNum;

		sprite.transform.parent = emojiContainer;
		spFace.spriteName = faceName + "_1";
		spaFace.namePrefix = faceName + "_";

		if (aniConfig != null) {
			if (aniConfig.isNotPlay) {
				StopAudioAnimation(spaFace);
				if (aniConfig.isAudio) {
					RegistUIButton(gameObject, (go) => {
						if (chatData == null || chatData.voiceData == null) return;
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
		if (!aniConfig.isAudio) {
			sprite.GetComponent<Collider>().enabled = false;
		}
		Vector3 tmpPos;
		sprite.transform.localScale = Vector3.one;
		tmpPos = picIndex;
		tmpPos.y = picIndex.y + cellLength * (aniConfig.rowNum - 1);

		sprite.transform.localPosition = tmpPos;
		sprite.name = faceName;
		sprite.SetActive(true);
		spFace.ResizeCollider();

		return sprite;
	}
	// 检测图文字，发现后做处理
	private bool ReplaceEmojiText(ref string text, ref int index, ref int rowNum, ref int spaceNum, ref bool isChangeRow, ref string faceName) {
		bool result = false;
		int length = text.Length;
		int delta = 0;
		do {
			if (!CheckIsEmoji(text, index)) break;
			faceName = text.Substring(index + 1, 3);
			AnimationVO aniConfig = GameConst.GetAnimationConfigByIndex(faceName);
			if (aniConfig == null) break;
			if (CheckVoiceNotPlay(aniConfig, ref text, ref index)) break;

			result = true;
			int lastRowNum = rowNum;
			rowNum = Math.Max(rowNum, aniConfig.rowNum);

			spaceNum = aniConfig.spaceNum;

			text = text.Remove(index, 4);
			for (int j = 0; j < spaceNum; j++) {
				text = text.Insert(index, " ");
			}
			picIndex = SetRealRowList(text, index);

			for (delta = 0; delta < spaceNum; delta++) {
				isChangeRow = CheckIsChangeRowEx(index, delta);
				if (isChangeRow) break;
			}
			// 换行后补充空格
			if (isChangeRow) {
				//if (delta == 0 && !CheckIncludeEmoji(text.Substring(index, Math.Min(nextRow, text.Length) - index))) {
				//	delta = 1;
				//}

				for (int j = 0; j < delta; j++) {
					text = text.Insert(index, " ");
					index++;
				}
				lastRowNum = 1;
				rowNum = aniConfig.rowNum;
				rowListIndex++;
				picIndex = SetRealRowList(text, index);
				picIndex.x = 0;
			}

			if (lastRowNum != rowNum) {
				// 添加换行
				for (int j = 0; j <= rowNum - lastRowNum; j++) {
					if (rowDict[lastRow] < rowNum) {
						rowDict[lastRow] = rowDict[lastRow] + 1;
						text = text.Insert(lastRow, "\n");
						index++;
					}
					if (j < rowNum - lastRowNum || lastRow == 0) {
						picIndex.y = picIndex.y - cellLength;
					}

					if (j == rowNum - lastRowNum - 1 && lastRow == 0) {
						break;
					}
				}
			}

		} while (false);
		return result;
	}	
	#endregion

	#region common
	// 生成当前字符串的所有应该换行的位置
	private Vector3 SetRealRowList(string text, int picTextIndex) {
		Vector3 result;

		rowList.Clear();
		result = NGUITextCustom.GetExactCharacterRowList(text, rowList, picTextIndex);

		lastRow = rowList[rowListIndex];
		nextRow = GetNextRowIndex();
		if (!rowDict.ContainsKey(lastRow)) {
			rowDict[lastRow] = 0;
		}

		return result;
	}
	private int GetNextRowIndex() {
		int index = rowListIndex + 1;
		return index < rowList.Count ? rowList[index] : int.MaxValue;
	}
	private void SetChangeRow(ref int rowNum, List<GameObject> sprites) {
		rowNum = 1;
		sprites.Clear();
	}
	private int GetPictureIndex(string text,int index) {
		string tempText = text.Substring(0, index + 1);

		tempText = NGUIText.StripSymbols(tempText).Replace("\n","");
		return tempText.Length - 1;
	}
	private bool CheckVoiceNotPlay(AnimationVO aniConfig, ref string text, ref int index) {
		bool result = false;
		if (chatData == null) { return result; }
		if (aniConfig.isNotPlay && chatData.voiceData == null) {
			text = text.Remove(index, 4);
			index--;
			result = true;
		}
		return result;
	}
	private bool CheckIncludeEmoji(string text) {
		bool result = false;
		for (int i = 0; i < text.Length; i++) {
			if (CheckIsEmoji(text, i)) {
				result = true;
				break;
			}
		}
			return result;
	}
	private bool CheckIsEmoji(string text, int index) {
		return text[index] == '#' && index + 3 < text.Length && Char.IsNumber(text[index + 1])
			&& Char.IsNumber(text[index + 2]) && Char.IsNumber(text[index + 3]);
	}
	private bool CheckIsChangeRowEx(int index, int delta) {

		int size = index + delta;

		return size > 1 && size >= nextRow;
	}
	private void StopAudioAnimation(UISpriteAnimation spriteAnimation) {
		spriteAnimation.ResetToBeginning();
		spriteAnimation.Pause();
		if (_microPhoneInput != null) {
			_microPhoneInput.ResetAudio();
		}
	}
	#endregion

}