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
* Filename: LuaComponent
* Created:  5/4/2016 11:45:55 AM
* Author:   HaYaShi ToShiTaKa and tolua#
* Purpose:  Component的lua导出类,本类由插件自动生成
* ==============================================================================
*/
namespace LuaInterface {
    using System.Collections.Generic;
    using System;
    using System.Collections;

    public class LuaComponent {
        public static void Register(IntPtr L) {
            int oldTop = LuaDLL.lua_gettop(L);
            LuaDLL.lua_getglobal(L, "Component");

            if (LuaDLL.lua_isnil(L, -1)) {
                LuaDLL.lua_pop(L, 1);
                LuaDLL.lua_newtable(L);
                LuaDLL.lua_setglobal(L, "Component");
                LuaDLL.lua_getglobal(L, "Component");
            }

            LuaDLL.lua_pushstdcallcfunction(L, LuaComponent.New, "New");
            LuaDLL.lua_pushstdcallcfunction(L, LuaComponent.GetComponent, "GetComponent");
            LuaDLL.lua_pushstdcallcfunction(L, LuaComponent.GetComponentInChildren, "GetComponentInChildren");
            LuaDLL.lua_pushstdcallcfunction(L, LuaComponent.GetComponentsInChildren, "GetComponentsInChildren");
            LuaDLL.lua_pushstdcallcfunction(L, LuaComponent.GetComponentInParent, "GetComponentInParent");
            LuaDLL.lua_pushstdcallcfunction(L, LuaComponent.GetComponentsInParent, "GetComponentsInParent");
            LuaDLL.lua_pushstdcallcfunction(L, LuaComponent.GetComponents, "GetComponents");
            LuaDLL.lua_pushstdcallcfunction(L, LuaComponent.CompareTag, "CompareTag");
            LuaDLL.lua_pushstdcallcfunction(L, LuaComponent.SendMessageUpwards, "SendMessageUpwards");
            LuaDLL.lua_pushstdcallcfunction(L, LuaComponent.SendMessage, "SendMessage");
            LuaDLL.lua_pushstdcallcfunction(L, LuaComponent.BroadcastMessage, "BroadcastMessage");
            LuaDLL.lua_pushcsharpproperty(L, "transform", LuaComponent.get_transform, null);
            LuaDLL.lua_pushcsharpproperty(L, "gameObject", LuaComponent.get_gameObject, null);
            LuaDLL.lua_pushcsharpproperty(L, "tag", LuaComponent.get_tag, LuaComponent.set_tag);

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
            LuaDLL.lua_getglobal(L, "Object");
            if (LuaDLL.lua_isnil(L, -1)) {
                LuaDLL.lua_pop(L, 1);
                LuaDLL.lua_newtable(L);
                LuaDLL.lua_setglobal(L, "Object");
                LuaDLL.lua_getglobal(L, "Object");
                LuaDLL.lua_setmetatable(L, -2);
            }
            else {
                LuaDLL.lua_setmetatable(L, -2);
            }

            LuaDLL.lua_settop(L, oldTop);
            LuaStatic.AddTypeDict(typeof(UnityEngine.Component));

        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int New(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            LuaStatic.addGameObject2Lua(L, new UnityEngine.Component(), "Component");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int GetComponent(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(Type), 2)) {
                UnityEngine.Component obj = LuaStatic.GetObj(L, 1) as UnityEngine.Component;
                object type1 = LuaStatic.GetObj(L, 2);
                Type arg1 = LuaStatic.GetType(type1);
                LuaStatic.addGameObject2Lua(L, obj.GetComponent(arg1), (string)type1);

                return result;
            }
            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(String), 2)) {
                UnityEngine.Component obj = LuaStatic.GetObj(L, 1) as UnityEngine.Component;
                String arg1 = (String)LuaStatic.GetObj(L, 2);
                LuaStatic.addGameObject2Lua(L, obj.GetComponent(arg1), "Component");

                return result;
            }
            LuaStatic.traceback(L, "count not enough");
            LuaDLL.lua_error(L);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int GetComponentInChildren(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Component obj = LuaStatic.GetObj(L, 1) as UnityEngine.Component;
            object type1 = LuaStatic.GetObj(L, 2);
            Type arg1 = LuaStatic.GetType(type1);
            LuaStatic.addGameObject2Lua(L, obj.GetComponentInChildren(arg1), (string)type1);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int GetComponentsInChildren(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(Type), 2)) {
                UnityEngine.Component obj = LuaStatic.GetObj(L, 1) as UnityEngine.Component;
                object type1 = LuaStatic.GetObj(L, 2);
                Type arg1 = LuaStatic.GetType(type1);
                UnityEngine.Component[] objs = obj.GetComponentsInChildren(arg1);
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
            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(Type), 2) &&
                LuaStatic.CheckType(L, typeof(Boolean), 3)) {
                UnityEngine.Component obj = LuaStatic.GetObj(L, 1) as UnityEngine.Component;
                object type1 = LuaStatic.GetObj(L, 2);
                Type arg1 = LuaStatic.GetType(type1);
                Boolean arg2 = (Boolean)LuaStatic.GetObj(L, 3);
                UnityEngine.Component[] objs = obj.GetComponentsInChildren(arg1, arg2);
                LuaDLL.lua_newtable(L);
                int num2 = 0;
                foreach (var item in objs) {
                    LuaStatic.addGameObject2Lua(L, item, "Component");
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
        public static int GetComponentInParent(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Component obj = LuaStatic.GetObj(L, 1) as UnityEngine.Component;
            object type1 = LuaStatic.GetObj(L, 2);
            Type arg1 = LuaStatic.GetType(type1);
            LuaStatic.addGameObject2Lua(L, obj.GetComponentInParent(arg1), (string)type1);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int GetComponentsInParent(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(Type), 2)) {
                UnityEngine.Component obj = LuaStatic.GetObj(L, 1) as UnityEngine.Component;
                object type1 = LuaStatic.GetObj(L, 2);
                Type arg1 = LuaStatic.GetType(type1);
                UnityEngine.Component[] objs = obj.GetComponentsInParent(arg1);
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
            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(Type), 2) &&
                LuaStatic.CheckType(L, typeof(Boolean), 3)) {
                UnityEngine.Component obj = LuaStatic.GetObj(L, 1) as UnityEngine.Component;
                object type1 = LuaStatic.GetObj(L, 2);
                Type arg1 = LuaStatic.GetType(type1);
                Boolean arg2 = (Boolean)LuaStatic.GetObj(L, 3);
                UnityEngine.Component[] objs = obj.GetComponentsInParent(arg1, arg2);
                LuaDLL.lua_newtable(L);
                int num2 = 0;
                foreach (var item in objs) {
                    LuaStatic.addGameObject2Lua(L, item, "Component");
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
        public static int GetComponents(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(Type), 2)) {
                UnityEngine.Component obj = LuaStatic.GetObj(L, 1) as UnityEngine.Component;
                object type1 = LuaStatic.GetObj(L, 2);
                Type arg1 = LuaStatic.GetType(type1);
                UnityEngine.Component[] objs = obj.GetComponents(arg1);
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
            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(Type), 2) &&
                LuaStatic.CheckType(L, typeof(List<UnityEngine.Component>), 3)) {
                UnityEngine.Component obj = LuaStatic.GetObj(L, 1) as UnityEngine.Component;
                object type1 = LuaStatic.GetObj(L, 2);
                Type arg1 = LuaStatic.GetType(type1);
                List<UnityEngine.Component> arg2 = (List<UnityEngine.Component>)LuaStatic.GetObj(L, 3);
                obj.GetComponents(arg1, arg2);

                return result;
            }
            LuaStatic.traceback(L, "count not enough");
            LuaDLL.lua_error(L);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int CompareTag(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Component obj = LuaStatic.GetObj(L, 1) as UnityEngine.Component;
            String arg1 = (String)LuaStatic.GetObj(L, 2);
            LuaDLL.lua_pushboolean(L, obj.CompareTag(arg1));
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int SendMessageUpwards(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 4 &&
                LuaStatic.CheckType(L, typeof(String), 2) &&
                LuaStatic.CheckType(L, typeof(Object), 3) &&
                LuaStatic.CheckType(L, typeof(UnityEngine.SendMessageOptions), 4)) {
                UnityEngine.Component obj = LuaStatic.GetObj(L, 1) as UnityEngine.Component;
                String arg1 = (String)LuaStatic.GetObj(L, 2);
                Object arg2 = (Object)LuaStatic.GetObj(L, 3);
                UnityEngine.SendMessageOptions arg3 = (UnityEngine.SendMessageOptions)(double)(LuaStatic.GetObj(L, 4));
                obj.SendMessageUpwards(arg1, arg2, arg3);

                return result;
            }
            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(String), 2) &&
                LuaStatic.CheckType(L, typeof(Object), 3)) {
                UnityEngine.Component obj = LuaStatic.GetObj(L, 1) as UnityEngine.Component;
                String arg1 = (String)LuaStatic.GetObj(L, 2);
                Object arg2 = (Object)LuaStatic.GetObj(L, 3);
                obj.SendMessageUpwards(arg1, arg2);

                return result;
            }
            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(String), 2)) {
                UnityEngine.Component obj = LuaStatic.GetObj(L, 1) as UnityEngine.Component;
                String arg1 = (String)LuaStatic.GetObj(L, 2);
                obj.SendMessageUpwards(arg1);

                return result;
            }
            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(String), 2) &&
                LuaStatic.CheckType(L, typeof(UnityEngine.SendMessageOptions), 3)) {
                UnityEngine.Component obj = LuaStatic.GetObj(L, 1) as UnityEngine.Component;
                String arg1 = (String)LuaStatic.GetObj(L, 2);
                UnityEngine.SendMessageOptions arg2 = (UnityEngine.SendMessageOptions)(double)(LuaStatic.GetObj(L, 3));
                obj.SendMessageUpwards(arg1, arg2);

                return result;
            }
            LuaStatic.traceback(L, "count not enough");
            LuaDLL.lua_error(L);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int SendMessage(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 4 &&
                LuaStatic.CheckType(L, typeof(String), 2) &&
                LuaStatic.CheckType(L, typeof(Object), 3) &&
                LuaStatic.CheckType(L, typeof(UnityEngine.SendMessageOptions), 4)) {
                UnityEngine.Component obj = LuaStatic.GetObj(L, 1) as UnityEngine.Component;
                String arg1 = (String)LuaStatic.GetObj(L, 2);
                Object arg2 = (Object)LuaStatic.GetObj(L, 3);
                UnityEngine.SendMessageOptions arg3 = (UnityEngine.SendMessageOptions)(double)(LuaStatic.GetObj(L, 4));
                obj.SendMessage(arg1, arg2, arg3);

                return result;
            }
            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(String), 2) &&
                LuaStatic.CheckType(L, typeof(Object), 3)) {
                UnityEngine.Component obj = LuaStatic.GetObj(L, 1) as UnityEngine.Component;
                String arg1 = (String)LuaStatic.GetObj(L, 2);
                Object arg2 = (Object)LuaStatic.GetObj(L, 3);
                obj.SendMessage(arg1, arg2);

                return result;
            }
            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(String), 2)) {
                UnityEngine.Component obj = LuaStatic.GetObj(L, 1) as UnityEngine.Component;
                String arg1 = (String)LuaStatic.GetObj(L, 2);
                obj.SendMessage(arg1);

                return result;
            }
            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(String), 2) &&
                LuaStatic.CheckType(L, typeof(UnityEngine.SendMessageOptions), 3)) {
                UnityEngine.Component obj = LuaStatic.GetObj(L, 1) as UnityEngine.Component;
                String arg1 = (String)LuaStatic.GetObj(L, 2);
                UnityEngine.SendMessageOptions arg2 = (UnityEngine.SendMessageOptions)(double)(LuaStatic.GetObj(L, 3));
                obj.SendMessage(arg1, arg2);

                return result;
            }
            LuaStatic.traceback(L, "count not enough");
            LuaDLL.lua_error(L);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int BroadcastMessage(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 4 &&
                LuaStatic.CheckType(L, typeof(String), 2) &&
                LuaStatic.CheckType(L, typeof(Object), 3) &&
                LuaStatic.CheckType(L, typeof(UnityEngine.SendMessageOptions), 4)) {
                UnityEngine.Component obj = LuaStatic.GetObj(L, 1) as UnityEngine.Component;
                String arg1 = (String)LuaStatic.GetObj(L, 2);
                Object arg2 = (Object)LuaStatic.GetObj(L, 3);
                UnityEngine.SendMessageOptions arg3 = (UnityEngine.SendMessageOptions)(double)(LuaStatic.GetObj(L, 4));
                obj.BroadcastMessage(arg1, arg2, arg3);

                return result;
            }
            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(String), 2) &&
                LuaStatic.CheckType(L, typeof(Object), 3)) {
                UnityEngine.Component obj = LuaStatic.GetObj(L, 1) as UnityEngine.Component;
                String arg1 = (String)LuaStatic.GetObj(L, 2);
                Object arg2 = (Object)LuaStatic.GetObj(L, 3);
                obj.BroadcastMessage(arg1, arg2);

                return result;
            }
            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(String), 2)) {
                UnityEngine.Component obj = LuaStatic.GetObj(L, 1) as UnityEngine.Component;
                String arg1 = (String)LuaStatic.GetObj(L, 2);
                obj.BroadcastMessage(arg1);

                return result;
            }
            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(String), 2) &&
                LuaStatic.CheckType(L, typeof(UnityEngine.SendMessageOptions), 3)) {
                UnityEngine.Component obj = LuaStatic.GetObj(L, 1) as UnityEngine.Component;
                String arg1 = (String)LuaStatic.GetObj(L, 2);
                UnityEngine.SendMessageOptions arg2 = (UnityEngine.SendMessageOptions)(double)(LuaStatic.GetObj(L, 3));
                obj.BroadcastMessage(arg1, arg2);

                return result;
            }
            LuaStatic.traceback(L, "count not enough");
            LuaDLL.lua_error(L);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_transform(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Component obj = LuaStatic.GetObj(L, 1) as UnityEngine.Component;
            LuaStatic.addGameObject2Lua(L, obj.transform, "Transform");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_gameObject(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Component obj = LuaStatic.GetObj(L, 1) as UnityEngine.Component;
            LuaStatic.addGameObject2Lua(L, obj.gameObject, "GameObject");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_tag(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Component obj = LuaStatic.GetObj(L, 1) as UnityEngine.Component;
            LuaDLL.lua_pushstring(L, obj.tag);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_tag(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Component obj = LuaStatic.GetObj(L, 1) as UnityEngine.Component;
            
            obj.tag = (String)LuaStatic.GetObj(L, 2);
            return result;
        }
    }

}