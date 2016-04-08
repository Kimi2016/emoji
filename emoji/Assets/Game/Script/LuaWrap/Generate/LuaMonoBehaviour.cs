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
* Filename: LuaMonoBehaviour
* Created:  4/8/2016 9:47:16 PM
* Author:   HaYaShi ToShiTaKa and tolua#
* Purpose:  MonoBehaviour的lua导出类,本类由插件自动生成
* ==============================================================================
*/
namespace LuaInterface {
    using System.Collections.Generic;
    using System;
    using System.Collections;

    public class LuaMonoBehaviour {
        public static void Register(IntPtr L) {
            int oldTop = LuaDLL.lua_gettop(L);
            LuaDLL.lua_getglobal(L, "MonoBehaviour");

            if (LuaDLL.lua_isnil(L, -1)) {
                LuaDLL.lua_pop(L, 1);
                LuaDLL.lua_newtable(L);
                LuaDLL.lua_setglobal(L, "MonoBehaviour");
                LuaDLL.lua_getglobal(L, "MonoBehaviour");
            }

            LuaDLL.lua_pushstdcallcfunction(L, LuaMonoBehaviour.Invoke, "Invoke");
            LuaDLL.lua_pushstdcallcfunction(L, LuaMonoBehaviour.InvokeRepeating, "InvokeRepeating");
            LuaDLL.lua_pushstdcallcfunction(L, LuaMonoBehaviour.CancelInvoke, "CancelInvoke");
            LuaDLL.lua_pushstdcallcfunction(L, LuaMonoBehaviour.IsInvoking, "IsInvoking");
            LuaDLL.lua_pushstdcallcfunction(L, LuaMonoBehaviour.StartCoroutine, "StartCoroutine");
            LuaDLL.lua_pushstdcallcfunction(L, LuaMonoBehaviour.StartCoroutine_Auto, "StartCoroutine_Auto");
            LuaDLL.lua_pushstdcallcfunction(L, LuaMonoBehaviour.StopCoroutine, "StopCoroutine");
            LuaDLL.lua_pushstdcallcfunction(L, LuaMonoBehaviour.StopAllCoroutines, "StopAllCoroutines");
            LuaDLL.lua_pushstdcallcfunction(L, LuaMonoBehaviour.print, "print");
            LuaDLL.lua_pushcsharpproperty(L, "useGUILayout", LuaMonoBehaviour.get_useGUILayout, LuaMonoBehaviour.set_useGUILayout);

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
            LuaDLL.lua_getglobal(L, "Behaviour");
            if (LuaDLL.lua_isnil(L, -1)) {
                LuaDLL.lua_pop(L, 1);
                LuaDLL.lua_newtable(L);
                LuaDLL.lua_setglobal(L, "Behaviour");
                LuaDLL.lua_getglobal(L, "Behaviour");
                LuaDLL.lua_setmetatable(L, -2);
            }
            else {
                LuaDLL.lua_setmetatable(L, -2);
            }

            LuaDLL.lua_settop(L, oldTop);

        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int Invoke(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 3) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.MonoBehaviour obj = LuaStatic.GetObj(L, 1) as UnityEngine.MonoBehaviour;
            String arg1 = (String)LuaStatic.GetObj(L, 2);
            Single arg2 = (Single)(double)(LuaStatic.GetObj(L, 3));
            obj.Invoke(arg1,arg2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int InvokeRepeating(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 4) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.MonoBehaviour obj = LuaStatic.GetObj(L, 1) as UnityEngine.MonoBehaviour;
            String arg1 = (String)LuaStatic.GetObj(L, 2);
            Single arg2 = (Single)(double)(LuaStatic.GetObj(L, 3));
            Single arg3 = (Single)(double)(LuaStatic.GetObj(L, 4));
            obj.InvokeRepeating(arg1,arg2,arg3);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int CancelInvoke(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 1) {
                UnityEngine.MonoBehaviour obj = LuaStatic.GetObj(L, 1) as UnityEngine.MonoBehaviour;
                obj.CancelInvoke();

                return result;
            }
            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(String), 2)) {
                UnityEngine.MonoBehaviour obj = LuaStatic.GetObj(L, 1) as UnityEngine.MonoBehaviour;
                String arg1 = (String)LuaStatic.GetObj(L, 2);
                obj.CancelInvoke(arg1);

                return result;
            }
            LuaStatic.traceback(L, "count not enough");
            LuaDLL.lua_error(L);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int IsInvoking(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(String), 2)) {
                UnityEngine.MonoBehaviour obj = LuaStatic.GetObj(L, 1) as UnityEngine.MonoBehaviour;
                String arg1 = (String)LuaStatic.GetObj(L, 2);
                LuaDLL.lua_pushboolean(L, obj.IsInvoking(arg1));

                return result;
            }
            if (count == 1) {
                UnityEngine.MonoBehaviour obj = LuaStatic.GetObj(L, 1) as UnityEngine.MonoBehaviour;
                LuaDLL.lua_pushboolean(L, obj.IsInvoking());

                return result;
            }
            LuaStatic.traceback(L, "count not enough");
            LuaDLL.lua_error(L);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int StartCoroutine(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(IEnumerator), 2)) {
                UnityEngine.MonoBehaviour obj = LuaStatic.GetObj(L, 1) as UnityEngine.MonoBehaviour;
                IEnumerator arg1 = (IEnumerator)LuaStatic.GetObj(L, 2);
                LuaStatic.addGameObject2Lua(L, obj.StartCoroutine(arg1), "Coroutine");

                return result;
            }
            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(String), 2) &&
                LuaStatic.CheckType(L, typeof(Object), 3)) {
                UnityEngine.MonoBehaviour obj = LuaStatic.GetObj(L, 1) as UnityEngine.MonoBehaviour;
                String arg1 = (String)LuaStatic.GetObj(L, 2);
                Object arg2 = (Object)LuaStatic.GetObj(L, 3);
                LuaStatic.addGameObject2Lua(L, obj.StartCoroutine(arg1, arg2), "Coroutine");

                return result;
            }
            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(String), 2)) {
                UnityEngine.MonoBehaviour obj = LuaStatic.GetObj(L, 1) as UnityEngine.MonoBehaviour;
                String arg1 = (String)LuaStatic.GetObj(L, 2);
                LuaStatic.addGameObject2Lua(L, obj.StartCoroutine(arg1), "Coroutine");

                return result;
            }
            LuaStatic.traceback(L, "count not enough");
            LuaDLL.lua_error(L);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int StartCoroutine_Auto(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.MonoBehaviour obj = LuaStatic.GetObj(L, 1) as UnityEngine.MonoBehaviour;
            IEnumerator arg1 = (IEnumerator)LuaStatic.GetObj(L, 2);
            LuaStatic.addGameObject2Lua(L, obj.StartCoroutine_Auto(arg1), "Coroutine");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int StopCoroutine(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(String), 2)) {
                UnityEngine.MonoBehaviour obj = LuaStatic.GetObj(L, 1) as UnityEngine.MonoBehaviour;
                String arg1 = (String)LuaStatic.GetObj(L, 2);
                obj.StopCoroutine(arg1);

                return result;
            }
            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(IEnumerator), 2)) {
                UnityEngine.MonoBehaviour obj = LuaStatic.GetObj(L, 1) as UnityEngine.MonoBehaviour;
                IEnumerator arg1 = (IEnumerator)LuaStatic.GetObj(L, 2);
                obj.StopCoroutine(arg1);

                return result;
            }
            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Coroutine), 2)) {
                UnityEngine.MonoBehaviour obj = LuaStatic.GetObj(L, 1) as UnityEngine.MonoBehaviour;
                UnityEngine.Coroutine arg1 = (UnityEngine.Coroutine)LuaStatic.GetObj(L, 2);
                obj.StopCoroutine(arg1);

                return result;
            }
            LuaStatic.traceback(L, "count not enough");
            LuaDLL.lua_error(L);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int StopAllCoroutines(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.MonoBehaviour obj = LuaStatic.GetObj(L, 1) as UnityEngine.MonoBehaviour;
            obj.StopAllCoroutines();
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int print(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            Object arg1 = (Object)LuaStatic.GetObj(L, 2);
            UnityEngine.MonoBehaviour.print(arg1);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_useGUILayout(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.MonoBehaviour obj = LuaStatic.GetObj(L, 1) as UnityEngine.MonoBehaviour;
            LuaDLL.lua_pushboolean(L, obj.useGUILayout);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_useGUILayout(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.MonoBehaviour obj = LuaStatic.GetObj(L, 1) as UnityEngine.MonoBehaviour;
            
            obj.useGUILayout = (Boolean)LuaStatic.GetObj(L, 2);
            return result;
        }
    }

}