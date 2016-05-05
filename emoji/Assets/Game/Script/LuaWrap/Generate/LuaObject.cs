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
* Filename: LuaObject
* Created:  5/4/2016 11:45:55 AM
* Author:   HaYaShi ToShiTaKa and tolua#
* Purpose:  Object的lua导出类,本类由插件自动生成
* ==============================================================================
*/
namespace LuaInterface {
    using System.Collections.Generic;
    using System;
    using System.Collections;

    public class LuaObject {
        public static void Register(IntPtr L) {
            int oldTop = LuaDLL.lua_gettop(L);
            LuaDLL.lua_getglobal(L, "Object");

            if (LuaDLL.lua_isnil(L, -1)) {
                LuaDLL.lua_pop(L, 1);
                LuaDLL.lua_newtable(L);
                LuaDLL.lua_setglobal(L, "Object");
                LuaDLL.lua_getglobal(L, "Object");
            }

            LuaDLL.lua_pushstdcallcfunction(L, LuaObject.New, "New");
            LuaDLL.lua_pushstdcallcfunction(L, LuaObject.Destroy, "Destroy");
            LuaDLL.lua_pushstdcallcfunction(L, LuaObject.DestroyImmediate, "DestroyImmediate");
            LuaDLL.lua_pushstdcallcfunction(L, LuaObject.FindObjectsOfType, "FindObjectsOfType");
            LuaDLL.lua_pushstdcallcfunction(L, LuaObject.DontDestroyOnLoad, "DontDestroyOnLoad");
            LuaDLL.lua_pushstdcallcfunction(L, LuaObject.DestroyObject, "DestroyObject");
            LuaDLL.lua_pushstdcallcfunction(L, LuaObject.ToString, "ToString");
            LuaDLL.lua_pushstdcallcfunction(L, LuaObject.Equals, "Equals");
            LuaDLL.lua_pushstdcallcfunction(L, LuaObject.GetHashCode, "GetHashCode");
            LuaDLL.lua_pushstdcallcfunction(L, LuaObject.GetInstanceID, "GetInstanceID");
            LuaDLL.lua_pushstdcallcfunction(L, LuaObject.Instantiate, "Instantiate");
            LuaDLL.lua_pushstdcallcfunction(L, LuaObject.FindObjectOfType, "FindObjectOfType");
            LuaDLL.lua_pushcsharpproperty(L, "name", LuaObject.get_name, LuaObject.set_name);
            LuaDLL.lua_pushcsharpproperty(L, "hideFlags", LuaObject.get_hideFlags, LuaObject.set_hideFlags);

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
            LuaStatic.AddTypeDict(typeof(UnityEngine.Object));

        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int New(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            LuaStatic.addGameObject2Lua(L, new UnityEngine.Object(), "Object");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int Destroy(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Object), 2) &&
                LuaStatic.CheckType(L, typeof(Single), 3)) {
                UnityEngine.Object arg1 = (UnityEngine.Object)LuaStatic.GetObj(L, 2);
                Single arg2 = (Single)(double)(LuaStatic.GetObj(L, 3));
                UnityEngine.Object.Destroy(arg1, arg2);

                return result;
            }
            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Object), 2)) {
                UnityEngine.Object arg1 = (UnityEngine.Object)LuaStatic.GetObj(L, 2);
                UnityEngine.Object.Destroy(arg1);

                return result;
            }
            LuaStatic.traceback(L, "count not enough");
            LuaDLL.lua_error(L);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int DestroyImmediate(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Object), 2) &&
                LuaStatic.CheckType(L, typeof(Boolean), 3)) {
                UnityEngine.Object arg1 = (UnityEngine.Object)LuaStatic.GetObj(L, 2);
                Boolean arg2 = (Boolean)LuaStatic.GetObj(L, 3);
                UnityEngine.Object.DestroyImmediate(arg1, arg2);

                return result;
            }
            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Object), 2)) {
                UnityEngine.Object arg1 = (UnityEngine.Object)LuaStatic.GetObj(L, 2);
                UnityEngine.Object.DestroyImmediate(arg1);

                return result;
            }
            LuaStatic.traceback(L, "count not enough");
            LuaDLL.lua_error(L);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int FindObjectsOfType(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            object type1 = LuaStatic.GetObj(L, 2);
            Type arg1 = LuaStatic.GetType(type1);
            UnityEngine.Object[] objs = UnityEngine.Object.FindObjectsOfType(arg1);
            LuaDLL.lua_newtable(L);
            int num2 = 0;
            foreach (var item in objs) {
                LuaStatic.addGameObject2Lua(L, item, (string)type1);
                LuaDLL.lua_pushnumber(L, (double)(++num2));
                LuaDLL.lua_insert(L, -2);
                LuaDLL.lua_settable(L, -3);
            }
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int DontDestroyOnLoad(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            UnityEngine.Object arg1 = (UnityEngine.Object)LuaStatic.GetObj(L, 2);
            UnityEngine.Object.DontDestroyOnLoad(arg1);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int DestroyObject(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Object), 2) &&
                LuaStatic.CheckType(L, typeof(Single), 3)) {
                UnityEngine.Object arg1 = (UnityEngine.Object)LuaStatic.GetObj(L, 2);
                Single arg2 = (Single)(double)(LuaStatic.GetObj(L, 3));
                UnityEngine.Object.DestroyObject(arg1, arg2);

                return result;
            }
            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Object), 2)) {
                UnityEngine.Object arg1 = (UnityEngine.Object)LuaStatic.GetObj(L, 2);
                UnityEngine.Object.DestroyObject(arg1);

                return result;
            }
            LuaStatic.traceback(L, "count not enough");
            LuaDLL.lua_error(L);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int ToString(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Object obj = LuaStatic.GetObj(L, 1) as UnityEngine.Object;
            LuaDLL.lua_pushstring(L, obj.ToString());
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int Equals(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Object obj = LuaStatic.GetObj(L, 1) as UnityEngine.Object;
            Object arg1 = (Object)LuaStatic.GetObj(L, 2);
            LuaDLL.lua_pushboolean(L, obj.Equals(arg1));
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int GetHashCode(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Object obj = LuaStatic.GetObj(L, 1) as UnityEngine.Object;
            LuaDLL.lua_pushnumber(L, obj.GetHashCode());
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int GetInstanceID(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Object obj = LuaStatic.GetObj(L, 1) as UnityEngine.Object;
            LuaDLL.lua_pushnumber(L, obj.GetInstanceID());
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int Instantiate(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 4 &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Object), 2) &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Vector3), 3) &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Quaternion), 4)) {
                UnityEngine.Object arg1 = (UnityEngine.Object)LuaStatic.GetObj(L, 2);
                UnityEngine.Vector3 arg2 = (UnityEngine.Vector3)LuaStatic.GetObj(L, 3);
                UnityEngine.Quaternion arg3 = (UnityEngine.Quaternion)LuaStatic.GetObj(L, 4);
                LuaStatic.addGameObject2Lua(L, UnityEngine.Object.Instantiate(arg1, arg2, arg3), "Object");

                return result;
            }
            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Object), 2) &&
                LuaStatic.CheckType(L, typeof(Type), 3)) {
                UnityEngine.Object arg1 = (UnityEngine.Object)LuaStatic.GetObj(L, 2);
                object type2 = LuaStatic.GetObj(L, 3);

                LuaStatic.addGameObject2Lua(L, UnityEngine.Object.Instantiate(arg1), (string)type2);

                return result;
            }
            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Object), 2)) {
                UnityEngine.Object arg1 = (UnityEngine.Object)LuaStatic.GetObj(L, 2);
                LuaStatic.addGameObject2Lua(L, UnityEngine.Object.Instantiate(arg1), "Object");

                return result;
            }
            LuaStatic.traceback(L, "count not enough");
            LuaDLL.lua_error(L);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int FindObjectOfType(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            object type1 = LuaStatic.GetObj(L, 2);
            Type arg1 = LuaStatic.GetType(type1);
            LuaStatic.addGameObject2Lua(L, UnityEngine.Object.FindObjectOfType(arg1), (string)type1);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_name(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Object obj = LuaStatic.GetObj(L, 1) as UnityEngine.Object;
            LuaDLL.lua_pushstring(L, obj.name);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_name(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Object obj = LuaStatic.GetObj(L, 1) as UnityEngine.Object;
            
            obj.name = (String)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_hideFlags(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Object obj = LuaStatic.GetObj(L, 1) as UnityEngine.Object;
            LuaStatic.addGameObject2Lua(L, obj.hideFlags, "HideFlags");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_hideFlags(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Object obj = LuaStatic.GetObj(L, 1) as UnityEngine.Object;
            
            obj.hideFlags = (UnityEngine.HideFlags)LuaStatic.GetObj(L, 2);
            return result;
        }
    }

}