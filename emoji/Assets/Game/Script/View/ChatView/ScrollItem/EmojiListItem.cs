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
		spItem = gameObject.GetComponent<UISprite>();
	}
	public override void FillItem(IList datas, int index) {
		base.FillItem(datas, index);
		GetBaseUI<ChatView>().FillEmoji(this, datas[index]);
		RegistUIButton(gameObject, (go) => {
			Debug.Log(index);
		});
	}
}