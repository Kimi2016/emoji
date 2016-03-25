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
* Filename: ChatInfoItem
* Created:  2016/2/12 17:05:52
* Author:   HaYaShi ToShiTaKa
* Purpose:  主城UI的聊天ITEM
* ==============================================================================
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatInfoItem : TableBaseItem {
	private UIEmoji _emoji;
	private Transform _channelPanle;
	private UILabel _labelChannel;
	private Vector3 _preChannelPos;
	public override void FindItem() {
		base.FindItem();
		_emoji = gameObject.GetComponent<UIEmoji>();
		_channelPanle = transform.Find("chanel");
		_labelChannel = transform.Find("chanel/Label").GetComponent<UILabel>();
		_preChannelPos = _channelPanle.localPosition;
	}
	public override void FillItem(IList datas, int index) {
		base.FillItem(datas, index);
		ChatData data = (ChatData)datas[datas.Count - 1 - index];
		Vector3 pos = _preChannelPos;

		_labelChannel.text = NGUIText.EncodeColor(data.chatTypeName, data.chatTypeColor);
		string text = String.Format("[00ff00]    [{0}][-]:{1}", data.playName, data.text);
		float height = _emoji.SetChatData(data, text);
		pos.y -= height;
		_channelPanle.localPosition = pos;
	}
}