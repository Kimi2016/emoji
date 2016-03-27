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
		Register(mLuaState.L);
		mLuaState.DoString(@"
			Director.GetInstance():OpenView(0)
		");
	}
	void Update() { 
		
	}

	public static void Register(IntPtr L) {
		int oldTop = LuaDLL.lua_gettop(L);

		LuaDLL.lua_newtable(L);
		LuaDLL.lua_pushstdcallcfunction(L, new LuaCSFunction(Director.OpenView));
		LuaDLL.lua_setfield(L, -2, "OpenView");
		LuaDLL.lua_pushstdcallcfunction(L, new LuaCSFunction(Director.GetInstance));
		LuaDLL.lua_setfield(L, -2, "GetInstance");
		LuaDLL.lua_pushvalue(L, -1);
		LuaDLL.lua_setfield(L, -2, "__index");
		LuaDLL.lua_pushvalue(L, -1);
		LuaDLL.lua_setglobal(L, "Director");

		LuaDLL.lua_settop(L, oldTop);
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int GetInstance(IntPtr L) {
		int result = 1;
		LuaStatic.addGameObject2Lua(L, GetInstance(), "Director");
		return result;
	}

	[MonoPInvokeCallback(typeof(LuaCSFunction))]
	public static int OpenView(IntPtr L) {
		int result = 1;
		Director director = LuaStatic.GetObj(L, 1) as Director;
		EnumUIName uiName = (EnumUIName)LuaDLL.lua_tonumber(L, 2);
		director.uiManager.OpenView(uiName);
		return result;
	}
}