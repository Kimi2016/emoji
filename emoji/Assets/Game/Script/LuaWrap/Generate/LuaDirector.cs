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
* Created:  5/6/2016 3:57:08 PM
* Author:   HaYaShi ToShiTaKa and tolua#
* Purpose:  Director的lua导出类,本类由插件自动生成
* ==============================================================================
*/
namespace LuaInterface {
    using System.Collections.Generic;
    using System;
    using System.Collections;

    public class LuaDirector {
        public static void Register(IntPtr L) {
            int oldTop = LuaDLL.lua_gettop(L);
            LuaDLL.lua_getglobal(L, "Director");

            if (LuaDLL.lua_isnil(L, -1)) {
                LuaDLL.lua_pop(L, 1);
                LuaDLL.lua_newtable(L);
                LuaDLL.lua_setglobal(L, "Director");
                LuaDLL.lua_getglobal(L, "Director");
            }

            LuaDLL.lua_pushstdcallcfunction(L, LuaDirector.GetInstance, "GetInstance");
            LuaDLL.lua_pushstdcallcfunction(L, LuaDirector.LogTest, "LogTest");
            LuaDLL.lua_pushcsharpproperty(L, "value", LuaDirector.get_value, LuaDirector.set_value);
            LuaDLL.lua_pushcsharpproperty(L, "luaState", LuaDirector.get_luaState, null);
            LuaDLL.lua_pushcsharpproperty(L, "scheduler", LuaDirector.get_scheduler, null);
            LuaDLL.lua_pushcsharpproperty(L, "uiManager", LuaDirector.get_uiManager, null);
            LuaDLL.lua_pushcsharpproperty(L, "eventDispatcher", LuaDirector.get_eventDispatcher, null);

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
            LuaDLL.lua_getglobal(L, "MonoBehaviour");
            if (LuaDLL.lua_isnil(L, -1)) {
                LuaDLL.lua_pop(L, 1);
                LuaDLL.lua_newtable(L);
                LuaDLL.lua_setglobal(L, "MonoBehaviour");
                LuaDLL.lua_getglobal(L, "MonoBehaviour");
                LuaDLL.lua_setmetatable(L, -2);
            }
            else {
                LuaDLL.lua_setmetatable(L, -2);
            }

            LuaDLL.lua_settop(L, oldTop);
            LuaStatic.AddTypeDict(typeof(Director));

        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int GetInstance(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            LuaStatic.addGameObject2Lua(L, Director.GetInstance(), "Director");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int LogTest(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            Director obj = LuaStatic.GetObj(L, 1) as Director;
            String arg1 = (String)LuaStatic.GetObj(L, 2);
            obj.LogTest(arg1);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_value(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            Director obj = LuaStatic.GetObj(L, 1) as Director;
            LuaDLL.lua_pushnumber(L, obj.value);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_value(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            Director obj = LuaStatic.GetObj(L, 1) as Director;
            
            obj.value = (Int32)(double)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_luaState(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            Director obj = LuaStatic.GetObj(L, 1) as Director;
            LuaStatic.addGameObject2Lua(L, obj.luaState, "LuaState");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_scheduler(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            Director obj = LuaStatic.GetObj(L, 1) as Director;
            LuaStatic.addGameObject2Lua(L, obj.scheduler, "Scheduler");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_uiManager(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            Director obj = LuaStatic.GetObj(L, 1) as Director;
            LuaStatic.addGameObject2Lua(L, obj.uiManager, "UIManager");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_eventDispatcher(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            Director obj = LuaStatic.GetObj(L, 1) as Director;
            LuaStatic.addGameObject2Lua(L, obj.eventDispatcher, "EventDispatcher");
            return result;
        }
    }

}