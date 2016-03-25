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
* Filename: ChatTaskItem
* Created:  2016/3/14 16:25:19
* Author:   HaYaShi ToShiTaKa
* Purpose:  聊天的任务按钮
* ==============================================================================
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ChatTaskItem : GridBaseItem {
	public UILabel lblTaskName;
	public override void FindItem() {
		base.FindItem();
		lblTaskName = transform.Find("name").GetComponent<UILabel>();
	}
	public override void FillItem(IList datas, int index) {
		base.FillItem(datas, index);
	}
}