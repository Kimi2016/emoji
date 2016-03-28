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

[Serializable] // 指示可序列化
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)] //平铺结构;Ansi字符;结构成员各按1字节对齐
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
		mUIManager = UIManager.MakeInstance();
		mEventDispatcher = EventDispatcher.MakeInstance();
		mLuaState = new LuaState();
	}
	void Start() {
		string text = Resources.Load<TextAsset>("lua/class.lua").text;
		mLuaState.DoString(text);

		LuaRegister.Register(mLuaState.L);
		LuaRegister.SetLuaClass(mLuaState);
		
		mLuaState.DoString(@"
			local directory = Director:GetInstance()
			directory:OpenView(0)
			print(directory.value)
			directory.value = 2
			print(directory.value)
			mtDirector.GetInstance():OpenView(0)
		");
	}
	void Update() { 
		
	}

	public void LogTest() {
		print("hellow world");
	}
}