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
* Filename: LuaResources
* Created:  5/6/2016 3:57:08 PM
* Author:   HaYaShi ToShiTaKa and tolua#
* Purpose:  Resources的lua导出类,本类由插件自动生成
* ==============================================================================
*/
namespace LuaInterface {
    using System.Collections.Generic;
    using System;
    using System.Collections;

    public class LuaResources {
        public static void Register(IntPtr L) {
            int oldTop = LuaDLL.lua_gettop(L);
            LuaDLL.lua_getglobal(L, "Resources");

            if (LuaDLL.lua_isnil(L, -1)) {
                LuaDLL.lua_pop(L, 1);
                LuaDLL.lua_newtable(L);
                LuaDLL.lua_setglobal(L, "Resources");
                LuaDLL.lua_getglobal(L, "Resources");
            }

            LuaDLL.lua_pushstdcallcfunction(L, LuaResources.New, "New");
            LuaDLL.lua_pushstdcallcfunction(L, LuaResources.FindObjectsOfTypeAll, "FindObjectsOfTypeAll");
            LuaDLL.lua_pushstdcallcfunction(L, LuaResources.Load, "Load");
            LuaDLL.lua_pushstdcallcfunction(L, LuaResources.LoadAsync, "LoadAsync");
            LuaDLL.lua_pushstdcallcfunction(L, LuaResources.LoadAll, "LoadAll");
            LuaDLL.lua_pushstdcallcfunction(L, LuaResources.GetBuiltinResource, "GetBuiltinResource");
            LuaDLL.lua_pushstdcallcfunction(L, LuaResources.UnloadAsset, "UnloadAsset");
            LuaDLL.lua_pushstdcallcfunction(L, LuaResources.UnloadUnusedAssets, "UnloadUnusedAssets");

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
            LuaStatic.AddTypeDict(typeof(UnityEngine.Resources));

        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int New(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            LuaStatic.addGameObject2Lua(L, new UnityEngine.Resources(), "Resources");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int FindObjectsOfTypeAll(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            object type1 = LuaStatic.GetObj(L, 2);
            Type arg1 = LuaStatic.GetType(type1);
            IEnumerable objs = (IEnumerable)UnityEngine.Resources.FindObjectsOfTypeAll(arg1);
            LuaDLL.lua_newtable(L);
            int num2 = 0;
            foreach (var item in objs) {
                LuaStatic.addGameObject2Lua(L, (UnityEngine.Object)item, (string)type1);
                LuaDLL.lua_pushnumber(L, (double)(++num2));
                LuaDLL.lua_insert(L, -2);
                LuaDLL.lua_settable(L, -3);
            }
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int Load(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(String), 2)) {
                String arg1 = (String)LuaStatic.GetObj(L, 2);
                LuaStatic.addGameObject2Lua(L, UnityEngine.Resources.Load(arg1), "Object");

                return result;
            }
            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(String), 2) &&
                LuaStatic.CheckType(L, typeof(Type), 3)) {
                String arg1 = (String)LuaStatic.GetObj(L, 2);
                object type2 = LuaStatic.GetObj(L, 3);
                Type arg2 = LuaStatic.GetType(type2);
                LuaStatic.addGameObject2Lua(L, UnityEngine.Resources.Load(arg1, arg2), (string)type2);

                return result;
            }
            LuaStatic.traceback(L, "count not enough");
            LuaDLL.lua_error(L);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int LoadAsync(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(String), 2)) {
                String arg1 = (String)LuaStatic.GetObj(L, 2);
                LuaStatic.addGameObject2Lua(L, UnityEngine.Resources.LoadAsync(arg1), "ResourceRequest");

                return result;
            }
            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(String), 2) &&
                LuaStatic.CheckType(L, typeof(Type), 3)) {
                String arg1 = (String)LuaStatic.GetObj(L, 2);
                object type2 = LuaStatic.GetObj(L, 3);
                Type arg2 = LuaStatic.GetType(type2);
                LuaStatic.addGameObject2Lua(L, UnityEngine.Resources.LoadAsync(arg1, arg2), (string)type2);

                return result;
            }
            LuaStatic.traceback(L, "count not enough");
            LuaDLL.lua_error(L);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int LoadAll(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(String), 2) &&
                LuaStatic.CheckType(L, typeof(Type), 3)) {
                String arg1 = (String)LuaStatic.GetObj(L, 2);
                object type2 = LuaStatic.GetObj(L, 3);
                Type arg2 = LuaStatic.GetType(type2);
                IEnumerable objs = (IEnumerable)UnityEngine.Resources.LoadAll(arg1, arg2);
                LuaDLL.lua_newtable(L);
                int num2 = 0;
                foreach (var item in objs) {
                    LuaStatic.addGameObject2Lua(L, (UnityEngine.Object)item, (string)type2);
                    LuaDLL.lua_pushnumber(L, (double)(++num2));
                    LuaDLL.lua_insert(L, -2);
                    LuaDLL.lua_settable(L, -3);
                }

                return result;
            }
            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(String), 2)) {
                String arg1 = (String)LuaStatic.GetObj(L, 2);
                IEnumerable objs = (IEnumerable)UnityEngine.Resources.LoadAll(arg1);
                LuaDLL.lua_newtable(L);
                int num2 = 0;
                foreach (var item in objs) {
                    LuaStatic.addGameObject2Lua(L, (UnityEngine.Object)item, "Object");
                    LuaDLL.lua_pushnumber(L, (double)(++num2));
                    LuaDLL.lua_insert(L, -2);
                    LuaDLL.lua_settable(L, -3);
                }

                return result;
            }
            LuaStatic.traceback(L, "count not enough");
            LuaDLL.lua_error(L);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int GetBuiltinResource(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            object type1 = LuaStatic.GetObj(L, 2);
            Type arg1 = LuaStatic.GetType(type1);
            String arg2 = (String)LuaStatic.GetObj(L, 3);
            LuaStatic.addGameObject2Lua(L, UnityEngine.Resources.GetBuiltinResource(arg1,arg2), "Object");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int UnloadAsset(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            UnityEngine.Object arg1 = (UnityEngine.Object)LuaStatic.GetObj(L, 2);
            UnityEngine.Resources.UnloadAsset(arg1);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int UnloadUnusedAssets(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            LuaStatic.addGameObject2Lua(L, UnityEngine.Resources.UnloadUnusedAssets(), "AsyncOperation");
            return result;
        }
    }

}