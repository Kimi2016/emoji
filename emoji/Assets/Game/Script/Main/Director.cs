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
using LuaInterface;
using System.Runtime.InteropServices;

public class Director : MonoBehaviour {
	private static Director mInstance;
	public static Director GetInstance() {
		if (mInstance == null) {
			mInstance = Camera.main.gameObject.AddComponent<Director>();
		}
		return mInstance;
	}
	private int mValue = 100;
	public int value {
		get {
			return mValue;
		}
		set {
			mValue = value;
		}
	}

	private LuaState mLuaState;
	public LuaState luaState {
		get {
			return mLuaState;
		}
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
	void Awake() {
		mInstance = this;
		mScheduler = Scheduler.MakeInstance();
		//mUIManager = UIManager.MakeInstance();
		mEventDispatcher = EventDispatcher.MakeInstance();
		mLuaState = new LuaState();
	}
	void Start() {
        mLuaState.DoFile("lua/preload/regist.lua");
        LuaRegister.Register(mLuaState.L);
        mLuaState.DoFile("lua/main.lua");
        object[] args = mLuaState.CallGlobalFunction("test", new object[] { "test global function" }, 1);
        if (args == null) return;
        foreach (var item in args) {
            print(item.ToString());
        }

		//args = mLuaState.CallTableFunction("testtable", "test", new object[] { "test table function" }, 1);
		//foreach (var item in args) {
		//	print(item.ToString());
		//}
	}
	void Update() { 
		
	}

	public void LogTest(string text) {
		print(text);
	}
}