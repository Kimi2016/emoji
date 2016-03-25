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
* Filename: Main
* Created:  2016/3/24 16:19:04
* Author:   HaYaShi ToShiTaKa
* Purpose:  游戏主循环入口
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using UnityEngine;
public class Director : MonoBehaviour {
	private static Director mInstance;
	public static Director GetInstance() {
		if (mInstance == null) {
			mInstance = Camera.main.gameObject.AddComponent<Director>();
		}
		return mInstance;
	}

	#region include some game manager
	private Scheduler mScheduler;
	public Scheduler scheduler {
		get {
			return mScheduler;
		}
	}
	private UIManager mUIManager;
	public UIManager uiManager {
		get {
			return mUIManager;
		}
	}
	private EventDispatcher mEventDispatcher;
	public EventDispatcher eventDispatcher {
		get {
			return mEventDispatcher;
		}
	}
	#endregion

	void Start() {
		mInstance = this;
		mScheduler = Scheduler.MakeInstance();
		mUIManager = UIManager.MakeInstance();
		mEventDispatcher = EventDispatcher.MakeInstance();
		mUIManager.OpenView(EnumUIName.ChatView);
	}
	void Update() { 
		
	}
}