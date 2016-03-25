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
* Filename: ChatHonorItem
* Created:  2016/3/16 16:17:04
* Author:   HaYaShi ToShiTaKa
* Purpose:  
* ==============================================================================
*/
using System;
using System.Collections;
using System.Collections.Generic;

public class ChatHonorItem : GridBaseItem {
	public UILabel lableName;
	public override void FindItem() {
		base.FindItem();
		lableName = transform.Find("name").GetComponent<UILabel>();
	}
	public override void FillItem(IList datas, int index) {
		base.FillItem(datas, index);
	}
}