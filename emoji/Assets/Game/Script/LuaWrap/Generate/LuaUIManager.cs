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
* Created:  4/6/2016 10:55:34 PM
* Author:   HaYaShi ToShiTaKa and tolua#
* Purpose:  UIManager的lua导出类,本类由插件自动生成
* ==============================================================================
*/
namespace LuaInterface {
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class LuaUIManager {

        public static void Register(IntPtr L) {
            int oldTop = LuaDLL.lua_gettop(L);

            LuaDLL.lua_newtable(L);
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

            LuaDLL.lua_pushvalue(L, -1);
            LuaDLL.lua_setglobal(L, "UIManager");

            LuaDLL.lua_settop(L, oldTop);
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

            if (count == 2&& LuaStatic.CheckType(L, typeof(EnumUIName), 2)) {
                    UIManager obj = LuaStatic.GetObj(L, 1) as UIManager;
                    EnumUIName arg1 = (EnumUIName)Convert.ToInt32(LuaStatic.GetObj(L, 2));
                    obj.OpenView(arg1);

                    return result;
                }
            if (count == 3&& LuaStatic.CheckType(L, typeof(EnumUIName), 2)&& LuaStatic.CheckType(L, typeof(System.Object), 3)) {
                    UIManager obj = LuaStatic.GetObj(L, 1) as UIManager;
                    EnumUIName arg1 = (EnumUIName)Convert.ToInt32(LuaStatic.GetObj(L, 2));
                    System.Object arg2 = (System.Object)LuaStatic.GetObj(L, 3);
                    obj.OpenView(arg1,arg2);

                    return result;
                }
            if (count == 4&& LuaStatic.CheckType(L, typeof(EnumUIName), 2)&& LuaStatic.CheckType(L, typeof(System.Object), 3)&& LuaStatic.CheckType(L, typeof(System.Boolean), 4)) {
                    UIManager obj = LuaStatic.GetObj(L, 1) as UIManager;
                    EnumUIName arg1 = (EnumUIName)Convert.ToInt32(LuaStatic.GetObj(L, 2));
                    System.Object arg2 = (System.Object)LuaStatic.GetObj(L, 3);
                    System.Boolean arg3 = (System.Boolean)LuaStatic.GetObj(L, 4);
                    obj.OpenView(arg1,arg2,arg3);

                    return result;
                }
            LuaStatic.traceback(L, "count not enough");
            LuaDLL.lua_error(L);                            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int CloseView(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 2&& LuaStatic.CheckType(L, typeof(EnumUIName), 2)) {
                    UIManager obj = LuaStatic.GetObj(L, 1) as UIManager;
                    EnumUIName arg1 = (EnumUIName)Convert.ToInt32(LuaStatic.GetObj(L, 2));
                    obj.CloseView(arg1);

                    return result;
                }
            if (count == 3&& LuaStatic.CheckType(L, typeof(EnumUIName), 2)&& LuaStatic.CheckType(L, typeof(System.Boolean), 3)) {
                    UIManager obj = LuaStatic.GetObj(L, 1) as UIManager;
                    EnumUIName arg1 = (EnumUIName)Convert.ToInt32(LuaStatic.GetObj(L, 2));
                    System.Boolean arg2 = (System.Boolean)LuaStatic.GetObj(L, 3);
                    obj.CloseView(arg1,arg2);

                    return result;
                }
            LuaStatic.traceback(L, "count not enough");
            LuaDLL.lua_error(L);                            return result;
        }
    }

}