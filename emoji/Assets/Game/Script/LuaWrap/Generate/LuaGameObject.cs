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
* Filename: LuaGameObject
* Created:  5/6/2016 3:57:07 PM
* Author:   HaYaShi ToShiTaKa and tolua#
* Purpose:  GameObject的lua导出类,本类由插件自动生成
* ==============================================================================
*/
namespace LuaInterface {
    using System.Collections.Generic;
    using System;
    using System.Collections;

    public class LuaGameObject {
        public static void Register(IntPtr L) {
            int oldTop = LuaDLL.lua_gettop(L);
            LuaDLL.lua_getglobal(L, "GameObject");

            if (LuaDLL.lua_isnil(L, -1)) {
                LuaDLL.lua_pop(L, 1);
                LuaDLL.lua_newtable(L);
                LuaDLL.lua_setglobal(L, "GameObject");
                LuaDLL.lua_getglobal(L, "GameObject");
            }

            LuaDLL.lua_pushstdcallcfunction(L, LuaGameObject.New, "New");
            LuaDLL.lua_pushstdcallcfunction(L, LuaGameObject.CreatePrimitive, "CreatePrimitive");
            LuaDLL.lua_pushstdcallcfunction(L, LuaGameObject.GetComponent, "GetComponent");
            LuaDLL.lua_pushstdcallcfunction(L, LuaGameObject.GetComponentInChildren, "GetComponentInChildren");
            LuaDLL.lua_pushstdcallcfunction(L, LuaGameObject.GetComponentInParent, "GetComponentInParent");
            LuaDLL.lua_pushstdcallcfunction(L, LuaGameObject.GetComponents, "GetComponents");
            LuaDLL.lua_pushstdcallcfunction(L, LuaGameObject.GetComponentsInChildren, "GetComponentsInChildren");
            LuaDLL.lua_pushstdcallcfunction(L, LuaGameObject.GetComponentsInParent, "GetComponentsInParent");
            LuaDLL.lua_pushstdcallcfunction(L, LuaGameObject.SetActive, "SetActive");
            LuaDLL.lua_pushstdcallcfunction(L, LuaGameObject.CompareTag, "CompareTag");
            LuaDLL.lua_pushstdcallcfunction(L, LuaGameObject.FindGameObjectWithTag, "FindGameObjectWithTag");
            LuaDLL.lua_pushstdcallcfunction(L, LuaGameObject.FindWithTag, "FindWithTag");
            LuaDLL.lua_pushstdcallcfunction(L, LuaGameObject.FindGameObjectsWithTag, "FindGameObjectsWithTag");
            LuaDLL.lua_pushstdcallcfunction(L, LuaGameObject.SendMessageUpwards, "SendMessageUpwards");
            LuaDLL.lua_pushstdcallcfunction(L, LuaGameObject.SendMessage, "SendMessage");
            LuaDLL.lua_pushstdcallcfunction(L, LuaGameObject.BroadcastMessage, "BroadcastMessage");
            LuaDLL.lua_pushstdcallcfunction(L, LuaGameObject.AddComponent, "AddComponent");
            LuaDLL.lua_pushstdcallcfunction(L, LuaGameObject.Find, "Find");
            LuaDLL.lua_pushcsharpproperty(L, "transform", LuaGameObject.get_transform, null);
            LuaDLL.lua_pushcsharpproperty(L, "layer", LuaGameObject.get_layer, LuaGameObject.set_layer);
            LuaDLL.lua_pushcsharpproperty(L, "activeSelf", LuaGameObject.get_activeSelf, null);
            LuaDLL.lua_pushcsharpproperty(L, "activeInHierarchy", LuaGameObject.get_activeInHierarchy, null);
            LuaDLL.lua_pushcsharpproperty(L, "isStatic", LuaGameObject.get_isStatic, LuaGameObject.set_isStatic);
            LuaDLL.lua_pushcsharpproperty(L, "tag", LuaGameObject.get_tag, LuaGameObject.set_tag);
            LuaDLL.lua_pushcsharpproperty(L, "gameObject", LuaGameObject.get_gameObject, null);

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
            LuaStatic.AddTypeDict(typeof(UnityEngine.GameObject));

        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int New(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(String), 2)) {
                String arg1 = (String)LuaStatic.GetObj(L, 2);
                LuaStatic.addGameObject2Lua(L, new UnityEngine.GameObject(arg1), "GameObject");

                return result;
            }
            if (count == 1) {
                LuaStatic.addGameObject2Lua(L, new UnityEngine.GameObject(), "GameObject");

                return result;
            }
            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(String), 2) &&
                LuaStatic.CheckType(L, typeof(Type[]), 3)) {
                String arg1 = (String)LuaStatic.GetObj(L, 2);
                Type[] arg2 = (Type[])LuaStatic.GetObj(L, 3);
                LuaStatic.addGameObject2Lua(L, new UnityEngine.GameObject(arg1, arg2), "GameObject");

                return result;
            }
            LuaStatic.traceback(L, "count not enough");
            LuaDLL.lua_error(L);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int CreatePrimitive(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            UnityEngine.PrimitiveType arg1 = (UnityEngine.PrimitiveType)(double)(LuaStatic.GetObj(L, 2));
            LuaStatic.addGameObject2Lua(L, UnityEngine.GameObject.CreatePrimitive(arg1), "GameObject");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int GetComponent(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(Type), 2)) {
                UnityEngine.GameObject obj = LuaStatic.GetObj(L, 1) as UnityEngine.GameObject;
                object type1 = LuaStatic.GetObj(L, 2);
                Type arg1 = LuaStatic.GetType(type1);
                LuaStatic.addGameObject2Lua(L, obj.GetComponent(arg1), (string)type1);

                return result;
            }
            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(String), 2)) {
                UnityEngine.GameObject obj = LuaStatic.GetObj(L, 1) as UnityEngine.GameObject;
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
            UnityEngine.GameObject obj = LuaStatic.GetObj(L, 1) as UnityEngine.GameObject;
            object type1 = LuaStatic.GetObj(L, 2);
            Type arg1 = LuaStatic.GetType(type1);
            LuaStatic.addGameObject2Lua(L, obj.GetComponentInChildren(arg1), (string)type1);
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
            UnityEngine.GameObject obj = LuaStatic.GetObj(L, 1) as UnityEngine.GameObject;
            object type1 = LuaStatic.GetObj(L, 2);
            Type arg1 = LuaStatic.GetType(type1);
            LuaStatic.addGameObject2Lua(L, obj.GetComponentInParent(arg1), (string)type1);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int GetComponents(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(Type), 2)) {
                UnityEngine.GameObject obj = LuaStatic.GetObj(L, 1) as UnityEngine.GameObject;
                object type1 = LuaStatic.GetObj(L, 2);
                Type arg1 = LuaStatic.GetType(type1);
                IEnumerable objs = (IEnumerable)obj.GetComponents(arg1);
                LuaDLL.lua_newtable(L);
                int num2 = 0;
                foreach (var item in objs) {
                    LuaStatic.addGameObject2Lua(L, (UnityEngine.Component)item, (string)type1);
                    LuaDLL.lua_pushnumber(L, (double)(++num2));
                    LuaDLL.lua_insert(L, -2);
                    LuaDLL.lua_settable(L, -3);
                }

                return result;
            }
            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(Type), 2) &&
                LuaStatic.CheckType(L, typeof(List<UnityEngine.Component>), 3)) {
                UnityEngine.GameObject obj = LuaStatic.GetObj(L, 1) as UnityEngine.GameObject;
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
        public static int GetComponentsInChildren(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(Type), 2)) {
                UnityEngine.GameObject obj = LuaStatic.GetObj(L, 1) as UnityEngine.GameObject;
                object type1 = LuaStatic.GetObj(L, 2);
                Type arg1 = LuaStatic.GetType(type1);
                IEnumerable objs = (IEnumerable)obj.GetComponentsInChildren(arg1);
                LuaDLL.lua_newtable(L);
                int num2 = 0;
                foreach (var item in objs) {
                    LuaStatic.addGameObject2Lua(L, (UnityEngine.Component)item, (string)type1);
                    LuaDLL.lua_pushnumber(L, (double)(++num2));
                    LuaDLL.lua_insert(L, -2);
                    LuaDLL.lua_settable(L, -3);
                }

                return result;
            }
            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(Type), 2) &&
                LuaStatic.CheckType(L, typeof(Boolean), 3)) {
                UnityEngine.GameObject obj = LuaStatic.GetObj(L, 1) as UnityEngine.GameObject;
                object type1 = LuaStatic.GetObj(L, 2);
                Type arg1 = LuaStatic.GetType(type1);
                Boolean arg2 = (Boolean)LuaStatic.GetObj(L, 3);
                IEnumerable objs = (IEnumerable)obj.GetComponentsInChildren(arg1, arg2);
                LuaDLL.lua_newtable(L);
                int num2 = 0;
                foreach (var item in objs) {
                    LuaStatic.addGameObject2Lua(L, (UnityEngine.Component)item, "Component");
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
        public static int GetComponentsInParent(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(Type), 2)) {
                UnityEngine.GameObject obj = LuaStatic.GetObj(L, 1) as UnityEngine.GameObject;
                object type1 = LuaStatic.GetObj(L, 2);
                Type arg1 = LuaStatic.GetType(type1);
                IEnumerable objs = (IEnumerable)obj.GetComponentsInParent(arg1);
                LuaDLL.lua_newtable(L);
                int num2 = 0;
                foreach (var item in objs) {
                    LuaStatic.addGameObject2Lua(L, (UnityEngine.Component)item, (string)type1);
                    LuaDLL.lua_pushnumber(L, (double)(++num2));
                    LuaDLL.lua_insert(L, -2);
                    LuaDLL.lua_settable(L, -3);
                }

                return result;
            }
            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(Type), 2) &&
                LuaStatic.CheckType(L, typeof(Boolean), 3)) {
                UnityEngine.GameObject obj = LuaStatic.GetObj(L, 1) as UnityEngine.GameObject;
                object type1 = LuaStatic.GetObj(L, 2);
                Type arg1 = LuaStatic.GetType(type1);
                Boolean arg2 = (Boolean)LuaStatic.GetObj(L, 3);
                IEnumerable objs = (IEnumerable)obj.GetComponentsInParent(arg1, arg2);
                LuaDLL.lua_newtable(L);
                int num2 = 0;
                foreach (var item in objs) {
                    LuaStatic.addGameObject2Lua(L, (UnityEngine.Component)item, "Component");
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
        public static int SetActive(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.GameObject obj = LuaStatic.GetObj(L, 1) as UnityEngine.GameObject;
            Boolean arg1 = (Boolean)LuaStatic.GetObj(L, 2);
            obj.SetActive(arg1);
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
            UnityEngine.GameObject obj = LuaStatic.GetObj(L, 1) as UnityEngine.GameObject;
            String arg1 = (String)LuaStatic.GetObj(L, 2);
            LuaDLL.lua_pushboolean(L, obj.CompareTag(arg1));
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int FindGameObjectWithTag(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            String arg1 = (String)LuaStatic.GetObj(L, 2);
            LuaStatic.addGameObject2Lua(L, UnityEngine.GameObject.FindGameObjectWithTag(arg1), "GameObject");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int FindWithTag(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            String arg1 = (String)LuaStatic.GetObj(L, 2);
            LuaStatic.addGameObject2Lua(L, UnityEngine.GameObject.FindWithTag(arg1), "GameObject");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int FindGameObjectsWithTag(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            String arg1 = (String)LuaStatic.GetObj(L, 2);
            IEnumerable objs = (IEnumerable)UnityEngine.GameObject.FindGameObjectsWithTag(arg1);
            LuaDLL.lua_newtable(L);
            int num2 = 0;
            foreach (var item in objs) {
                LuaStatic.addGameObject2Lua(L, (UnityEngine.GameObject)item, "GameObject");
                LuaDLL.lua_pushnumber(L, (double)(++num2));
                LuaDLL.lua_insert(L, -2);
                LuaDLL.lua_settable(L, -3);
            }
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
                UnityEngine.GameObject obj = LuaStatic.GetObj(L, 1) as UnityEngine.GameObject;
                String arg1 = (String)LuaStatic.GetObj(L, 2);
                Object arg2 = (Object)LuaStatic.GetObj(L, 3);
                UnityEngine.SendMessageOptions arg3 = (UnityEngine.SendMessageOptions)(double)(LuaStatic.GetObj(L, 4));
                obj.SendMessageUpwards(arg1, arg2, arg3);

                return result;
            }
            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(String), 2) &&
                LuaStatic.CheckType(L, typeof(Object), 3)) {
                UnityEngine.GameObject obj = LuaStatic.GetObj(L, 1) as UnityEngine.GameObject;
                String arg1 = (String)LuaStatic.GetObj(L, 2);
                Object arg2 = (Object)LuaStatic.GetObj(L, 3);
                obj.SendMessageUpwards(arg1, arg2);

                return result;
            }
            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(String), 2)) {
                UnityEngine.GameObject obj = LuaStatic.GetObj(L, 1) as UnityEngine.GameObject;
                String arg1 = (String)LuaStatic.GetObj(L, 2);
                obj.SendMessageUpwards(arg1);

                return result;
            }
            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(String), 2) &&
                LuaStatic.CheckType(L, typeof(UnityEngine.SendMessageOptions), 3)) {
                UnityEngine.GameObject obj = LuaStatic.GetObj(L, 1) as UnityEngine.GameObject;
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
                UnityEngine.GameObject obj = LuaStatic.GetObj(L, 1) as UnityEngine.GameObject;
                String arg1 = (String)LuaStatic.GetObj(L, 2);
                Object arg2 = (Object)LuaStatic.GetObj(L, 3);
                UnityEngine.SendMessageOptions arg3 = (UnityEngine.SendMessageOptions)(double)(LuaStatic.GetObj(L, 4));
                obj.SendMessage(arg1, arg2, arg3);

                return result;
            }
            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(String), 2) &&
                LuaStatic.CheckType(L, typeof(Object), 3)) {
                UnityEngine.GameObject obj = LuaStatic.GetObj(L, 1) as UnityEngine.GameObject;
                String arg1 = (String)LuaStatic.GetObj(L, 2);
                Object arg2 = (Object)LuaStatic.GetObj(L, 3);
                obj.SendMessage(arg1, arg2);

                return result;
            }
            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(String), 2)) {
                UnityEngine.GameObject obj = LuaStatic.GetObj(L, 1) as UnityEngine.GameObject;
                String arg1 = (String)LuaStatic.GetObj(L, 2);
                obj.SendMessage(arg1);

                return result;
            }
            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(String), 2) &&
                LuaStatic.CheckType(L, typeof(UnityEngine.SendMessageOptions), 3)) {
                UnityEngine.GameObject obj = LuaStatic.GetObj(L, 1) as UnityEngine.GameObject;
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
                UnityEngine.GameObject obj = LuaStatic.GetObj(L, 1) as UnityEngine.GameObject;
                String arg1 = (String)LuaStatic.GetObj(L, 2);
                Object arg2 = (Object)LuaStatic.GetObj(L, 3);
                UnityEngine.SendMessageOptions arg3 = (UnityEngine.SendMessageOptions)(double)(LuaStatic.GetObj(L, 4));
                obj.BroadcastMessage(arg1, arg2, arg3);

                return result;
            }
            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(String), 2) &&
                LuaStatic.CheckType(L, typeof(Object), 3)) {
                UnityEngine.GameObject obj = LuaStatic.GetObj(L, 1) as UnityEngine.GameObject;
                String arg1 = (String)LuaStatic.GetObj(L, 2);
                Object arg2 = (Object)LuaStatic.GetObj(L, 3);
                obj.BroadcastMessage(arg1, arg2);

                return result;
            }
            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(String), 2)) {
                UnityEngine.GameObject obj = LuaStatic.GetObj(L, 1) as UnityEngine.GameObject;
                String arg1 = (String)LuaStatic.GetObj(L, 2);
                obj.BroadcastMessage(arg1);

                return result;
            }
            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(String), 2) &&
                LuaStatic.CheckType(L, typeof(UnityEngine.SendMessageOptions), 3)) {
                UnityEngine.GameObject obj = LuaStatic.GetObj(L, 1) as UnityEngine.GameObject;
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
        public static int AddComponent(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.GameObject obj = LuaStatic.GetObj(L, 1) as UnityEngine.GameObject;
            object type1 = LuaStatic.GetObj(L, 2);
            Type arg1 = LuaStatic.GetType(type1);
            LuaStatic.addGameObject2Lua(L, obj.AddComponent(arg1), (string)type1);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int Find(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            String arg1 = (String)LuaStatic.GetObj(L, 2);
            LuaStatic.addGameObject2Lua(L, UnityEngine.GameObject.Find(arg1), "GameObject");
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
            UnityEngine.GameObject obj = LuaStatic.GetObj(L, 1) as UnityEngine.GameObject;
            LuaStatic.addGameObject2Lua(L, obj.transform, "Transform");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_layer(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.GameObject obj = LuaStatic.GetObj(L, 1) as UnityEngine.GameObject;
            LuaDLL.lua_pushnumber(L, obj.layer);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_layer(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.GameObject obj = LuaStatic.GetObj(L, 1) as UnityEngine.GameObject;
            
            obj.layer = (Int32)(double)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_activeSelf(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.GameObject obj = LuaStatic.GetObj(L, 1) as UnityEngine.GameObject;
            LuaDLL.lua_pushboolean(L, obj.activeSelf);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_activeInHierarchy(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.GameObject obj = LuaStatic.GetObj(L, 1) as UnityEngine.GameObject;
            LuaDLL.lua_pushboolean(L, obj.activeInHierarchy);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_isStatic(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.GameObject obj = LuaStatic.GetObj(L, 1) as UnityEngine.GameObject;
            LuaDLL.lua_pushboolean(L, obj.isStatic);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_isStatic(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.GameObject obj = LuaStatic.GetObj(L, 1) as UnityEngine.GameObject;
            
            obj.isStatic = (Boolean)LuaStatic.GetObj(L, 2);
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
            UnityEngine.GameObject obj = LuaStatic.GetObj(L, 1) as UnityEngine.GameObject;
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
            UnityEngine.GameObject obj = LuaStatic.GetObj(L, 1) as UnityEngine.GameObject;
            
            obj.tag = (String)LuaStatic.GetObj(L, 2);
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
            UnityEngine.GameObject obj = LuaStatic.GetObj(L, 1) as UnityEngine.GameObject;
            LuaStatic.addGameObject2Lua(L, obj.gameObject, "GameObject");
            return result;
        }
    }

}