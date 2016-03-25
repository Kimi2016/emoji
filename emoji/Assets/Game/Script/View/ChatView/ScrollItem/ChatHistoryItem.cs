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
* Filename: ChatHistoryItem
* Created:  2016/3/15 18:01:48
* Author:   HaYaShi ToShiTaKa
* Purpose:  
* ==============================================================================
*/
using System;
using System.Collections;
using System.Collections.Generic;

public class ChatHistoryItem : GridBaseItem {
	public UILabel labelText;
	public override void FindItem() {
		base.FindItem();
		labelText = transform.Find("name").GetComponent<UILabel>();
	}
	public override void FillItem(IList datas, int index) {
		base.FillItem(datas, index);
		string text = datas[datas.Count - 1 - index].ToString();
		labelText.text = text;
		GetBaseUI<ChatDetailView>().FillHistoryTalk(this, text);
	}
}