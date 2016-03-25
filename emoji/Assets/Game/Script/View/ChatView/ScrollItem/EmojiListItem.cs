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
* Filename: EmojiListItem
* Created:  2016/2/2 13:20:16
* Author:   HaYaShi ToShiTaKa
* Purpose:  对话表情控件
* ==============================================================================
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiListItem : GridBaseItem {
	public UISprite spItem;
	public override void FindItem() {
		base.FindItem();
		spItem = transform.Find("face").GetComponent<UISprite>();
	}
	public override void FillItem(IList datas, int index) {
		base.FillItem(datas, index);
		string text = datas[index].ToString();
		spItem.spriteName = text;
		text = "#" + datas[index].ToString().Substring(0,3);
		UnRegistUIButton(spItem.gameObject);
		GetBaseUI<ChatDetailView>().FillEmoji(this, text);
	}
}