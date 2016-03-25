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
* Filename: MainUIExpSlider
* Created:  2016/1/10 13:55:48
* Author:   HaYaShi ToShiTaKa
* Purpose:  经验条
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using UnityEngine;
public class ChatUIExpSlider : UIBase {

	private UISlider _sldExpBar;

	#region virtual 重写函数
	public override void OnLoadedUI(bool close3dTouch, object args) {
		base.OnLoadedUI(close3dTouch, args);

		_sldExpBar = transform.Find("Ex_di_hero").GetComponent<UISlider>();
	}

	public override void OnOpenUI(object args) {
		base.OnOpenUI(args);
		RefreshExpBar();
	}

	public override void OnCloseUI() {
		base.OnCloseUI();
	}

	public override void OnFreeUI() {
		base.OnFreeUI();
	}
	#endregion
	public void RefreshExpBar() {
		_sldExpBar.value = 0;
	}

}