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
* Filename: ChatDetailItem
* Created:  2016/3/13 12:23:34
* Author:   HaYaShi ToShiTaKa
* Purpose:  聊天详细界面的聊天Item
* ==============================================================================
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ChatDetailItem : TableBaseItem {
	private UIEmoji _emoji;
	private UIWidget _widgetItemContent;
	private UISprite _widgetBunble;
	private UIWidget _widgetText;
	private UILabel _labelName;
	private UISprite _headIcon;
	Color myColor = new Color(177.0f / 255.0f, 243.0f / 255.0f, 1.0f);
	Color their = Color.white;
	private Vector3 _preNamePostion;
	private Vector3 _preTextPostion;
	private Vector3 _preHeadPostion;

	private Vector3 _selfHeadPostion;
	private Vector3 _selfTextPostion;
	public override void FindItem() {
		base.FindItem();
		_emoji = transform.Find("Label").GetComponent<UIEmoji>();
		_widgetItemContent = transform.GetComponent<UIWidget>();
		_widgetBunble = transform.Find("Label/kuang").GetComponent<UISprite>();
		_widgetText = transform.Find("Label").GetComponent<UIWidget>();
		_labelName = transform.Find("icon/name").GetComponent<UILabel>();
		_headIcon = transform.Find("icon").GetComponent<UISprite>();
		_preNamePostion = _labelName.transform.localPosition;
		_preTextPostion = _widgetText.transform.localPosition;
		_preHeadPostion = _headIcon.transform.localPosition;

		_selfHeadPostion = _preHeadPostion + new Vector3(_widgetItemContent.width - _headIcon.width, 0, 0);
		_selfTextPostion = _preTextPostion + new Vector3(-_headIcon.width, 0, 0);
	}
	public override void FillItem(IList datas, int index) {
		base.FillItem(datas, index);
		ChatData data = (ChatData)datas[datas.Count - 1 - index];

		_labelName.text = data.playName;
		int oldHeight = _widgetText.height;
		_emoji.SetChatData(data);

		int deltaHeight = _widgetText.height - oldHeight;
		_widgetBunble.height += deltaHeight;
		_widgetItemContent.height += deltaHeight;
		_widgetItemContent.ResizeCollider();
		UnRegistUIButton(_headIcon.gameObject);
		RefreshSelf();
	}
	private void RefreshSelf() {
		_widgetBunble.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
		_headIcon.transform.localPosition = new Vector3(602, 0, 0);
		_widgetBunble.color = myColor;
		_headIcon.transform.localPosition = _selfHeadPostion;
		_labelName.transform.localPosition = new Vector3(-_preNamePostion.x + _headIcon.width - _labelName.width, 0, 0);
		_widgetText.transform.localPosition = _selfTextPostion;
	}
	private void RefreshOther() {
		_widgetBunble.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
		_widgetBunble.color = their;
		_headIcon.transform.localPosition = _preHeadPostion;
		_labelName.transform.localPosition = _preNamePostion;
		_widgetText.transform.localPosition = _preTextPostion;
	}
}