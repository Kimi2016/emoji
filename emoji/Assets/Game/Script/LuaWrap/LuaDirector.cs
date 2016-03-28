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
* Filename: LuaDirector
* Created:  2016/3/28 19:09:51
* Author:   HaYaShi ToShiTaKa
* Purpose:  Director的lua导出类
* ==============================================================================
*/
namespace LuaInterface {
	using System;
	using System.Collections.Generic;
	using UnityEngine;
	public class LuaDirector {
		public static void Register(IntPtr L) {
			int oldTop = LuaDLL.lua_gettop(L);

			LuaDLL.lua_newtable(L);
			//functions
			LuaDLL.lua_pushstdcallcfunction(L, new LuaCSFunction(LuaDirector.OpenView));
			LuaDLL.lua_setfield(L, -2, "OpenView");
			LuaDLL.lua_pushstdcallcfunction(L, new LuaCSFunction(LuaDirector.GetInstance));
			LuaDLL.lua_setfield(L, -2, "GetInstance");
			LuaDLL.lua_pushstdcallcfunction(L, new LuaCSFunction(LuaDirector.Log));
			LuaDLL.lua_setfield(L, -2, "Log");

			//property
			LuaDLL.lua_pushstdcallcfunction(L, new LuaCSFunction(LuaDirector.SetValue));
			LuaDLL.lua_setfield(L, -2, "SetValue");
			LuaDLL.lua_pushstdcallcfunction(L, new LuaCSFunction(LuaDirector.GetValue));
			LuaDLL.lua_setfield(L, -2, "GetValue");

			//mata table method
			LuaDLL.lua_getglobal(L, "readIndex");
			LuaDLL.lua_pushvalue(L, -1);
			LuaDLL.lua_setfield(L, -3, "__index");
			LuaDLL.lua_pop(L, 1);

			LuaDLL.lua_getglobal(L, "writeIndex");
			LuaDLL.lua_pushvalue(L, -1);
			LuaDLL.lua_setfield(L, -3, "__newindex");
			LuaDLL.lua_pop(L, 1);

			LuaDLL.lua_pushstdcallcfunction(L, new LuaCSFunction(LuaStatic.GameObjectGC));
			LuaDLL.lua_setfield(L, -2, "__gc");

			//set table
			LuaDLL.lua_pushvalue(L, -1);
			LuaDLL.lua_setglobal(L, "mtDirector");

			LuaDLL.lua_settop(L, oldTop);
		}
		public static void SetLuaClass(LuaState luaState) {
			string text = Resources.Load<TextAsset>(GameConst.LuaClass.DIRECTORY_URL).text;
			luaState.DoString(text);
		}
		[MonoPInvokeCallback(typeof(LuaCSFunction))]
		public static int GetInstance(IntPtr L) {
			int result = 1;
			LuaStatic.addGameObject2Lua(L, Director.GetInstance(), "mtDirector");
			return result;
		}
		[MonoPInvokeCallback(typeof(LuaCSFunction))]
		public static int SetValue(IntPtr L) {
			int result = 1;
			Director director = LuaStatic.GetObj(L, 1) as Director;
			if (director == null) {
				LuaStatic.traceback(L, "nullobj call");
				LuaDLL.lua_error(L);
				return result;
			}
			director.value = (int)LuaDLL.lua_tonumber(L, 2);
			return result;
		}
		[MonoPInvokeCallback(typeof(LuaCSFunction))]
		public static int GetValue(IntPtr L) {
			int result = 1;
			Director director = LuaStatic.GetObj(L, 1) as Director;
			if (director == null) {
				LuaStatic.traceback(L, "nullobj call");
				LuaDLL.lua_error(L);
				return result;
			}
			LuaDLL.lua_pushnumber(L, director.value);
			return result;
		}

		[MonoPInvokeCallback(typeof(LuaCSFunction))]
		public static int OpenView(IntPtr L) {
			int result = 1;
			Director director = LuaStatic.GetObj(L, 1) as Director;
			if (director == null) {
				LuaStatic.traceback(L, "nullobj call");
				LuaDLL.lua_error(L);
				return result;
			}
			EnumUIName uiName = (EnumUIName)LuaDLL.lua_tonumber(L, 2);
			director.uiManager.OpenView(uiName);
			return result;
		}
		[MonoPInvokeCallback(typeof(LuaCSFunction))]
		public static int Log(IntPtr L) {
			int result = 1;
			Director director = LuaStatic.GetObj(L, 1) as Director;
			if (director == null) {
				LuaStatic.traceback(L, "nullobj call");
				LuaDLL.lua_error(L);
				return result;
			}
			director.LogTest();
			return result;
		}
	}
}