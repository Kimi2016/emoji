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
* Filename: LuaUIManager
* Created:  5/6/2016 3:57:08 PM
* Author:   HaYaShi ToShiTaKa and tolua#
* Purpose:  UIManager的lua导出类,本类由插件自动生成
* ==============================================================================
*/
namespace LuaInterface {
    using System.Collections.Generic;
    using System;
    using System.Collections;

    public class LuaUIManager {
        public static void Register(IntPtr L) {
            int oldTop = LuaDLL.lua_gettop(L);
            LuaDLL.lua_getglobal(L, "UIManager");

            if (LuaDLL.lua_isnil(L, -1)) {
                LuaDLL.lua_pop(L, 1);
                LuaDLL.lua_newtable(L);
                LuaDLL.lua_setglobal(L, "UIManager");
                LuaDLL.lua_getglobal(L, "UIManager");
            }

            LuaDLL.lua_pushstdcallcfunction(L, LuaUIManager.MakeInstance, "MakeInstance");
            LuaDLL.lua_pushstdcallcfunction(L, LuaUIManager.OpenView, "OpenView");
            LuaDLL.lua_pushstdcallcfunction(L, LuaUIManager.CloseView, "CloseView");

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

            LuaDLL.lua_settop(L, oldTop);
            LuaStatic.AddTypeDict(typeof(UIManager));

        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int MakeInstance(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            LuaStatic.addGameObject2Lua(L, UIManager.MakeInstance(), "UIManager");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int OpenView(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(EnumUIName), 2)) {
                UIManager obj = LuaStatic.GetObj(L, 1) as UIManager;
                EnumUIName arg1 = (EnumUIName)(double)(LuaStatic.GetObj(L, 2));
                obj.OpenView(arg1);

                return result;
            }
            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(EnumUIName), 2) &&
                LuaStatic.CheckType(L, typeof(Object), 3)) {
                UIManager obj = LuaStatic.GetObj(L, 1) as UIManager;
                EnumUIName arg1 = (EnumUIName)(double)(LuaStatic.GetObj(L, 2));
                Object arg2 = (Object)LuaStatic.GetObj(L, 3);
                obj.OpenView(arg1, arg2);

                return result;
            }
            if (count == 4 &&
                LuaStatic.CheckType(L, typeof(EnumUIName), 2) &&
                LuaStatic.CheckType(L, typeof(Object), 3) &&
                LuaStatic.CheckType(L, typeof(Boolean), 4)) {
                UIManager obj = LuaStatic.GetObj(L, 1) as UIManager;
                EnumUIName arg1 = (EnumUIName)(double)(LuaStatic.GetObj(L, 2));
                Object arg2 = (Object)LuaStatic.GetObj(L, 3);
                Boolean arg3 = (Boolean)LuaStatic.GetObj(L, 4);
                obj.OpenView(arg1, arg2, arg3);

                return result;
            }
            LuaStatic.traceback(L, "count not enough");
            LuaDLL.lua_error(L);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int CloseView(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(EnumUIName), 2)) {
                UIManager obj = LuaStatic.GetObj(L, 1) as UIManager;
                EnumUIName arg1 = (EnumUIName)(double)(LuaStatic.GetObj(L, 2));
                obj.CloseView(arg1);

                return result;
            }
            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(EnumUIName), 2) &&
                LuaStatic.CheckType(L, typeof(Boolean), 3)) {
                UIManager obj = LuaStatic.GetObj(L, 1) as UIManager;
                EnumUIName arg1 = (EnumUIName)(double)(LuaStatic.GetObj(L, 2));
                Boolean arg2 = (Boolean)LuaStatic.GetObj(L, 3);
                obj.CloseView(arg1, arg2);

                return result;
            }
            LuaStatic.traceback(L, "count not enough");
            LuaDLL.lua_error(L);
            return result;
        }
    }

}