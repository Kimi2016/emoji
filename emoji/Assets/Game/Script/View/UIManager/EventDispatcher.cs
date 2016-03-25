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
* Filename: EventDispatcher
* Created:  2016/3/25 10:51:56
* Author:   HaYaShi ToShiTaKa
* Purpose:  事件分发器
* ==============================================================================
*/
using System;
using System.Collections.Generic;

public class EventDispatcher {
	private static EventDispatcher mInstance;
	public static EventDispatcher MakeInstance() {
		if (mInstance == null) {
			mInstance = new EventDispatcher();
		}
		return mInstance;
	}
	private Dictionary<EnumEventDispathcer, Action<object>> mEventDict;
	
	EventDispatcher() {
		mEventDict = new Dictionary<EnumEventDispathcer, Action<object>>();
	}
	public void RegisterEvent(EnumEventDispathcer eventName, Action<object> action) {
		if (mEventDict.ContainsKey(eventName)) {
			return;
		}
		mEventDict[eventName] = action;
	}
	public void UnRegisterEvent(EnumEventDispathcer eventName) {
		if (!mEventDict.ContainsKey(eventName)) {
			return;
		}
		mEventDict.Remove(eventName);
	}
	public void Notify(EnumEventDispathcer eventName, object args) {
		Action<object> action;
		if (mEventDict.TryGetValue(eventName,out action)) {
			action(args);
		}
	}
	public void Notify(EnumEventDispathcer eventName) {
		Notify(eventName, null);
	}
}