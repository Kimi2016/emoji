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
* Filename: ChatPetItem
* Created:  2016/3/16 15:39:05
* Author:   HaYaShi ToShiTaKa
* Purpose:  聊天宠物图标
* ==============================================================================
*/
using System;
using System.Collections;
using System.Collections.Generic;

public class ChatPetItem : GridBaseItem {
	public UISprite headIcon;
	public UILabel lableName;
	public UILabel lableLevel;
	public UISprite battleIcon;
	public override void FindItem() {
		base.FindItem();
		headIcon = transform.Find("icon").GetComponent<UISprite>();
		lableName = transform.Find("name").GetComponent<UILabel>();
		lableLevel = transform.Find("LabelLv").GetComponent<UILabel>();
		battleIcon = transform.Find("Sprite").GetComponent<UISprite>();

	}
	public override void FillItem(IList datas, int index) {
		base.FillItem(datas, index);
	}
}