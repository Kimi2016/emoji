//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------
using System;
using System.Collections.Generic;
using UnityEngine;

public class UIEmoji : MonoBehaviour {
	Dictionary<string, int> _animationRate;
	string space = "  ";
	void Awake() {
		_animationRate = new Dictionary<string, int>();
		_animationRate["000"] = 2;
		_animationRate["001"] = 9;
	}
	private int GetAnimationRate(string name) {
		int rate;
		if (_animationRate.TryGetValue(name, out rate)) {
			return rate;
		}
		return 2;
	}
	public string CalculateExpression(string text) {
		UILabel _lblChatInfo = transform.Find("Label").GetComponent<UILabel>();
		GameObject _faceTemplate = transform.Find("Sprite").gameObject;

		BetterList<Vector3> vets = new BetterList<Vector3>();
		BetterList<int> indexs = new BetterList<int>();
		GameObject sprite;
		UISprite spFace;
		UISpriteAnimation spaFace;
		int length = text.Length;
		int lineLength = 0;
		if (length <= 0) return "";
		_lblChatInfo.text = text;
		_lblChatInfo.UpdateNGUIText();
		NGUIText.PrintExactCharacterPositions(text, vets, indexs);

		for (int i = 0; i < length; i++) {
			// 判断是否是emoji
			if (text[i] == '#' && i + 3 < length) {
				if (Char.IsNumber(text[i + 1]) &&
					Char.IsNumber(text[i + 2]) &&
					Char.IsNumber(text[i + 3])) {
					text = text.Remove(i, 1);
					text = text.Insert(i, "一");
					_lblChatInfo.text = text;
					vets.Clear();
					indexs.Clear();
					_lblChatInfo.UpdateNGUIText();
					NGUIText.PrintExactCharacterPositions(text, vets, indexs);

					// 是就把它替换成表情图片
					string faceName = text.Substring(i + 1, 3);
					text = text.Remove(i, 4);
					text = text.Insert(i, space);

					sprite = GameObject.Instantiate(_faceTemplate);
					spFace = sprite.GetComponent<UISprite>();
					spaFace = sprite.GetComponent<UISpriteAnimation>();

					sprite.transform.parent = _faceTemplate.transform.parent;
					spFace.spriteName = faceName + "_1";
					spaFace.namePrefix = faceName + "_";
					spaFace.framesPerSecond = GetAnimationRate(faceName);

					Vector3 tmpPos;
					sprite.transform.localScale = Vector3.one;
					tmpPos = vets[2 * i];
					tmpPos.y = vets[2 * i + 1].y;

					sprite.transform.localPosition = tmpPos;

					sprite.SetActive(true);

					length = text.Length;
				}
			}
			if (vets[2 * i + 1].y != vets[1].y) {
				lineLength = i;
				break;
			}
		}
		if (lineLength == 0) {
			lineLength = text.Length;
		}
		_lblChatInfo.text = text.Substring(0, lineLength);

		return text.Substring(lineLength, text.Length - lineLength);
	}
}