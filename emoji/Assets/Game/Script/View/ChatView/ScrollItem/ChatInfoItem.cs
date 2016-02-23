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

public class ChatInfoItem : TableBaseItem {
	private UIEmoji _emoji;
	public override void FindItem() {
		base.FindItem();
		_emoji = gameObject.GetComponent<UIEmoji>();
	}
	public override void FillItem(IList datas, int index) {
		base.FillItem(datas, index);
		ChatData data = (ChatData)datas[index];

		_emoji.SetText(data);
	}
}