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
* Filename: LuaTransform
* Created:  5/4/2016 11:45:55 AM
* Author:   HaYaShi ToShiTaKa and tolua#
* Purpose:  Transform的lua导出类,本类由插件自动生成
* ==============================================================================
*/
namespace LuaInterface {
    using System.Collections.Generic;
    using System;
    using System.Collections;

    public class LuaTransform {
        public static void Register(IntPtr L) {
            int oldTop = LuaDLL.lua_gettop(L);
            LuaDLL.lua_getglobal(L, "Transform");

            if (LuaDLL.lua_isnil(L, -1)) {
                LuaDLL.lua_pop(L, 1);
                LuaDLL.lua_newtable(L);
                LuaDLL.lua_setglobal(L, "Transform");
                LuaDLL.lua_getglobal(L, "Transform");
            }

            LuaDLL.lua_pushstdcallcfunction(L, LuaTransform.SetParent, "SetParent");
            LuaDLL.lua_pushstdcallcfunction(L, LuaTransform.Translate, "Translate");
            LuaDLL.lua_pushstdcallcfunction(L, LuaTransform.Rotate, "Rotate");
            LuaDLL.lua_pushstdcallcfunction(L, LuaTransform.RotateAround, "RotateAround");
            LuaDLL.lua_pushstdcallcfunction(L, LuaTransform.LookAt, "LookAt");
            LuaDLL.lua_pushstdcallcfunction(L, LuaTransform.TransformDirection, "TransformDirection");
            LuaDLL.lua_pushstdcallcfunction(L, LuaTransform.InverseTransformDirection, "InverseTransformDirection");
            LuaDLL.lua_pushstdcallcfunction(L, LuaTransform.TransformVector, "TransformVector");
            LuaDLL.lua_pushstdcallcfunction(L, LuaTransform.InverseTransformVector, "InverseTransformVector");
            LuaDLL.lua_pushstdcallcfunction(L, LuaTransform.TransformPoint, "TransformPoint");
            LuaDLL.lua_pushstdcallcfunction(L, LuaTransform.InverseTransformPoint, "InverseTransformPoint");
            LuaDLL.lua_pushstdcallcfunction(L, LuaTransform.DetachChildren, "DetachChildren");
            LuaDLL.lua_pushstdcallcfunction(L, LuaTransform.SetAsFirstSibling, "SetAsFirstSibling");
            LuaDLL.lua_pushstdcallcfunction(L, LuaTransform.SetAsLastSibling, "SetAsLastSibling");
            LuaDLL.lua_pushstdcallcfunction(L, LuaTransform.SetSiblingIndex, "SetSiblingIndex");
            LuaDLL.lua_pushstdcallcfunction(L, LuaTransform.GetSiblingIndex, "GetSiblingIndex");
            LuaDLL.lua_pushstdcallcfunction(L, LuaTransform.Find, "Find");
            LuaDLL.lua_pushstdcallcfunction(L, LuaTransform.IsChildOf, "IsChildOf");
            LuaDLL.lua_pushstdcallcfunction(L, LuaTransform.FindChild, "FindChild");
            LuaDLL.lua_pushstdcallcfunction(L, LuaTransform.GetEnumerator, "GetEnumerator");
            LuaDLL.lua_pushstdcallcfunction(L, LuaTransform.GetChild, "GetChild");
            LuaDLL.lua_pushcsharpproperty(L, "position", LuaTransform.get_position, LuaTransform.set_position);
            LuaDLL.lua_pushcsharpproperty(L, "localPosition", LuaTransform.get_localPosition, LuaTransform.set_localPosition);
            LuaDLL.lua_pushcsharpproperty(L, "eulerAngles", LuaTransform.get_eulerAngles, LuaTransform.set_eulerAngles);
            LuaDLL.lua_pushcsharpproperty(L, "localEulerAngles", LuaTransform.get_localEulerAngles, LuaTransform.set_localEulerAngles);
            LuaDLL.lua_pushcsharpproperty(L, "right", LuaTransform.get_right, LuaTransform.set_right);
            LuaDLL.lua_pushcsharpproperty(L, "up", LuaTransform.get_up, LuaTransform.set_up);
            LuaDLL.lua_pushcsharpproperty(L, "forward", LuaTransform.get_forward, LuaTransform.set_forward);
            LuaDLL.lua_pushcsharpproperty(L, "rotation", LuaTransform.get_rotation, LuaTransform.set_rotation);
            LuaDLL.lua_pushcsharpproperty(L, "localRotation", LuaTransform.get_localRotation, LuaTransform.set_localRotation);
            LuaDLL.lua_pushcsharpproperty(L, "localScale", LuaTransform.get_localScale, LuaTransform.set_localScale);
            LuaDLL.lua_pushcsharpproperty(L, "parent", LuaTransform.get_parent, LuaTransform.set_parent);
            LuaDLL.lua_pushcsharpproperty(L, "worldToLocalMatrix", LuaTransform.get_worldToLocalMatrix, null);
            LuaDLL.lua_pushcsharpproperty(L, "localToWorldMatrix", LuaTransform.get_localToWorldMatrix, null);
            LuaDLL.lua_pushcsharpproperty(L, "root", LuaTransform.get_root, null);
            LuaDLL.lua_pushcsharpproperty(L, "childCount", LuaTransform.get_childCount, null);
            LuaDLL.lua_pushcsharpproperty(L, "lossyScale", LuaTransform.get_lossyScale, null);
            LuaDLL.lua_pushcsharpproperty(L, "hasChanged", LuaTransform.get_hasChanged, LuaTransform.set_hasChanged);

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
            LuaDLL.lua_getglobal(L, "Component");
            if (LuaDLL.lua_isnil(L, -1)) {
                LuaDLL.lua_pop(L, 1);
                LuaDLL.lua_newtable(L);
                LuaDLL.lua_setglobal(L, "Component");
                LuaDLL.lua_getglobal(L, "Component");
                LuaDLL.lua_setmetatable(L, -2);
            }
            else {
                LuaDLL.lua_setmetatable(L, -2);
            }

            LuaDLL.lua_settop(L, oldTop);
            LuaStatic.AddTypeDict(typeof(UnityEngine.Transform));

        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int SetParent(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Transform), 2)) {
                UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
                UnityEngine.Transform arg1 = (UnityEngine.Transform)LuaStatic.GetObj(L, 2);
                obj.SetParent(arg1);

                return result;
            }
            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Transform), 2) &&
                LuaStatic.CheckType(L, typeof(Boolean), 3)) {
                UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
                UnityEngine.Transform arg1 = (UnityEngine.Transform)LuaStatic.GetObj(L, 2);
                Boolean arg2 = (Boolean)LuaStatic.GetObj(L, 3);
                obj.SetParent(arg1, arg2);

                return result;
            }
            LuaStatic.traceback(L, "count not enough");
            LuaDLL.lua_error(L);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int Translate(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Vector3), 2)) {
                UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
                UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaStatic.GetObj(L, 2);
                obj.Translate(arg1);

                return result;
            }
            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Vector3), 2) &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Space), 3)) {
                UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
                UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaStatic.GetObj(L, 2);
                UnityEngine.Space arg2 = (UnityEngine.Space)(double)(LuaStatic.GetObj(L, 3));
                obj.Translate(arg1, arg2);

                return result;
            }
            if (count == 4 &&
                LuaStatic.CheckType(L, typeof(Single), 2) &&
                LuaStatic.CheckType(L, typeof(Single), 3) &&
                LuaStatic.CheckType(L, typeof(Single), 4)) {
                UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
                Single arg1 = (Single)(double)(LuaStatic.GetObj(L, 2));
                Single arg2 = (Single)(double)(LuaStatic.GetObj(L, 3));
                Single arg3 = (Single)(double)(LuaStatic.GetObj(L, 4));
                obj.Translate(arg1, arg2, arg3);

                return result;
            }
            if (count == 5 &&
                LuaStatic.CheckType(L, typeof(Single), 2) &&
                LuaStatic.CheckType(L, typeof(Single), 3) &&
                LuaStatic.CheckType(L, typeof(Single), 4) &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Space), 5)) {
                UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
                Single arg1 = (Single)(double)(LuaStatic.GetObj(L, 2));
                Single arg2 = (Single)(double)(LuaStatic.GetObj(L, 3));
                Single arg3 = (Single)(double)(LuaStatic.GetObj(L, 4));
                UnityEngine.Space arg4 = (UnityEngine.Space)(double)(LuaStatic.GetObj(L, 5));
                obj.Translate(arg1, arg2, arg3, arg4);

                return result;
            }
            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Vector3), 2) &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Transform), 3)) {
                UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
                UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaStatic.GetObj(L, 2);
                UnityEngine.Transform arg2 = (UnityEngine.Transform)LuaStatic.GetObj(L, 3);
                obj.Translate(arg1, arg2);

                return result;
            }
            if (count == 5 &&
                LuaStatic.CheckType(L, typeof(Single), 2) &&
                LuaStatic.CheckType(L, typeof(Single), 3) &&
                LuaStatic.CheckType(L, typeof(Single), 4) &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Transform), 5)) {
                UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
                Single arg1 = (Single)(double)(LuaStatic.GetObj(L, 2));
                Single arg2 = (Single)(double)(LuaStatic.GetObj(L, 3));
                Single arg3 = (Single)(double)(LuaStatic.GetObj(L, 4));
                UnityEngine.Transform arg4 = (UnityEngine.Transform)LuaStatic.GetObj(L, 5);
                obj.Translate(arg1, arg2, arg3, arg4);

                return result;
            }
            LuaStatic.traceback(L, "count not enough");
            LuaDLL.lua_error(L);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int Rotate(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Vector3), 2)) {
                UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
                UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaStatic.GetObj(L, 2);
                obj.Rotate(arg1);

                return result;
            }
            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Vector3), 2) &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Space), 3)) {
                UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
                UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaStatic.GetObj(L, 2);
                UnityEngine.Space arg2 = (UnityEngine.Space)(double)(LuaStatic.GetObj(L, 3));
                obj.Rotate(arg1, arg2);

                return result;
            }
            if (count == 4 &&
                LuaStatic.CheckType(L, typeof(Single), 2) &&
                LuaStatic.CheckType(L, typeof(Single), 3) &&
                LuaStatic.CheckType(L, typeof(Single), 4)) {
                UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
                Single arg1 = (Single)(double)(LuaStatic.GetObj(L, 2));
                Single arg2 = (Single)(double)(LuaStatic.GetObj(L, 3));
                Single arg3 = (Single)(double)(LuaStatic.GetObj(L, 4));
                obj.Rotate(arg1, arg2, arg3);

                return result;
            }
            if (count == 5 &&
                LuaStatic.CheckType(L, typeof(Single), 2) &&
                LuaStatic.CheckType(L, typeof(Single), 3) &&
                LuaStatic.CheckType(L, typeof(Single), 4) &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Space), 5)) {
                UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
                Single arg1 = (Single)(double)(LuaStatic.GetObj(L, 2));
                Single arg2 = (Single)(double)(LuaStatic.GetObj(L, 3));
                Single arg3 = (Single)(double)(LuaStatic.GetObj(L, 4));
                UnityEngine.Space arg4 = (UnityEngine.Space)(double)(LuaStatic.GetObj(L, 5));
                obj.Rotate(arg1, arg2, arg3, arg4);

                return result;
            }
            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Vector3), 2) &&
                LuaStatic.CheckType(L, typeof(Single), 3)) {
                UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
                UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaStatic.GetObj(L, 2);
                Single arg2 = (Single)(double)(LuaStatic.GetObj(L, 3));
                obj.Rotate(arg1, arg2);

                return result;
            }
            if (count == 4 &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Vector3), 2) &&
                LuaStatic.CheckType(L, typeof(Single), 3) &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Space), 4)) {
                UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
                UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaStatic.GetObj(L, 2);
                Single arg2 = (Single)(double)(LuaStatic.GetObj(L, 3));
                UnityEngine.Space arg3 = (UnityEngine.Space)(double)(LuaStatic.GetObj(L, 4));
                obj.Rotate(arg1, arg2, arg3);

                return result;
            }
            LuaStatic.traceback(L, "count not enough");
            LuaDLL.lua_error(L);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int RotateAround(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 4) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaStatic.GetObj(L, 2);
            UnityEngine.Vector3 arg2 = (UnityEngine.Vector3)LuaStatic.GetObj(L, 3);
            Single arg3 = (Single)(double)(LuaStatic.GetObj(L, 4));
            obj.RotateAround(arg1,arg2,arg3);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int LookAt(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Transform), 2)) {
                UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
                UnityEngine.Transform arg1 = (UnityEngine.Transform)LuaStatic.GetObj(L, 2);
                obj.LookAt(arg1);

                return result;
            }
            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Transform), 2) &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Vector3), 3)) {
                UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
                UnityEngine.Transform arg1 = (UnityEngine.Transform)LuaStatic.GetObj(L, 2);
                UnityEngine.Vector3 arg2 = (UnityEngine.Vector3)LuaStatic.GetObj(L, 3);
                obj.LookAt(arg1, arg2);

                return result;
            }
            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Vector3), 2) &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Vector3), 3)) {
                UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
                UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaStatic.GetObj(L, 2);
                UnityEngine.Vector3 arg2 = (UnityEngine.Vector3)LuaStatic.GetObj(L, 3);
                obj.LookAt(arg1, arg2);

                return result;
            }
            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Vector3), 2)) {
                UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
                UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaStatic.GetObj(L, 2);
                obj.LookAt(arg1);

                return result;
            }
            LuaStatic.traceback(L, "count not enough");
            LuaDLL.lua_error(L);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int TransformDirection(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Vector3), 2)) {
                UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
                UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaStatic.GetObj(L, 2);
                LuaDLL.lua_pushvector3(L, obj.TransformDirection(arg1));

                return result;
            }
            if (count == 4 &&
                LuaStatic.CheckType(L, typeof(Single), 2) &&
                LuaStatic.CheckType(L, typeof(Single), 3) &&
                LuaStatic.CheckType(L, typeof(Single), 4)) {
                UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
                Single arg1 = (Single)(double)(LuaStatic.GetObj(L, 2));
                Single arg2 = (Single)(double)(LuaStatic.GetObj(L, 3));
                Single arg3 = (Single)(double)(LuaStatic.GetObj(L, 4));
                LuaDLL.lua_pushvector3(L, obj.TransformDirection(arg1, arg2, arg3));

                return result;
            }
            LuaStatic.traceback(L, "count not enough");
            LuaDLL.lua_error(L);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int InverseTransformDirection(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Vector3), 2)) {
                UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
                UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaStatic.GetObj(L, 2);
                LuaDLL.lua_pushvector3(L, obj.InverseTransformDirection(arg1));

                return result;
            }
            if (count == 4 &&
                LuaStatic.CheckType(L, typeof(Single), 2) &&
                LuaStatic.CheckType(L, typeof(Single), 3) &&
                LuaStatic.CheckType(L, typeof(Single), 4)) {
                UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
                Single arg1 = (Single)(double)(LuaStatic.GetObj(L, 2));
                Single arg2 = (Single)(double)(LuaStatic.GetObj(L, 3));
                Single arg3 = (Single)(double)(LuaStatic.GetObj(L, 4));
                LuaDLL.lua_pushvector3(L, obj.InverseTransformDirection(arg1, arg2, arg3));

                return result;
            }
            LuaStatic.traceback(L, "count not enough");
            LuaDLL.lua_error(L);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int TransformVector(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Vector3), 2)) {
                UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
                UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaStatic.GetObj(L, 2);
                LuaDLL.lua_pushvector3(L, obj.TransformVector(arg1));

                return result;
            }
            if (count == 4 &&
                LuaStatic.CheckType(L, typeof(Single), 2) &&
                LuaStatic.CheckType(L, typeof(Single), 3) &&
                LuaStatic.CheckType(L, typeof(Single), 4)) {
                UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
                Single arg1 = (Single)(double)(LuaStatic.GetObj(L, 2));
                Single arg2 = (Single)(double)(LuaStatic.GetObj(L, 3));
                Single arg3 = (Single)(double)(LuaStatic.GetObj(L, 4));
                LuaDLL.lua_pushvector3(L, obj.TransformVector(arg1, arg2, arg3));

                return result;
            }
            LuaStatic.traceback(L, "count not enough");
            LuaDLL.lua_error(L);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int InverseTransformVector(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Vector3), 2)) {
                UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
                UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaStatic.GetObj(L, 2);
                LuaDLL.lua_pushvector3(L, obj.InverseTransformVector(arg1));

                return result;
            }
            if (count == 4 &&
                LuaStatic.CheckType(L, typeof(Single), 2) &&
                LuaStatic.CheckType(L, typeof(Single), 3) &&
                LuaStatic.CheckType(L, typeof(Single), 4)) {
                UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
                Single arg1 = (Single)(double)(LuaStatic.GetObj(L, 2));
                Single arg2 = (Single)(double)(LuaStatic.GetObj(L, 3));
                Single arg3 = (Single)(double)(LuaStatic.GetObj(L, 4));
                LuaDLL.lua_pushvector3(L, obj.InverseTransformVector(arg1, arg2, arg3));

                return result;
            }
            LuaStatic.traceback(L, "count not enough");
            LuaDLL.lua_error(L);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int TransformPoint(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Vector3), 2)) {
                UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
                UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaStatic.GetObj(L, 2);
                LuaDLL.lua_pushvector3(L, obj.TransformPoint(arg1));

                return result;
            }
            if (count == 4 &&
                LuaStatic.CheckType(L, typeof(Single), 2) &&
                LuaStatic.CheckType(L, typeof(Single), 3) &&
                LuaStatic.CheckType(L, typeof(Single), 4)) {
                UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
                Single arg1 = (Single)(double)(LuaStatic.GetObj(L, 2));
                Single arg2 = (Single)(double)(LuaStatic.GetObj(L, 3));
                Single arg3 = (Single)(double)(LuaStatic.GetObj(L, 4));
                LuaDLL.lua_pushvector3(L, obj.TransformPoint(arg1, arg2, arg3));

                return result;
            }
            LuaStatic.traceback(L, "count not enough");
            LuaDLL.lua_error(L);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int InverseTransformPoint(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Vector3), 2)) {
                UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
                UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaStatic.GetObj(L, 2);
                LuaDLL.lua_pushvector3(L, obj.InverseTransformPoint(arg1));

                return result;
            }
            if (count == 4 &&
                LuaStatic.CheckType(L, typeof(Single), 2) &&
                LuaStatic.CheckType(L, typeof(Single), 3) &&
                LuaStatic.CheckType(L, typeof(Single), 4)) {
                UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
                Single arg1 = (Single)(double)(LuaStatic.GetObj(L, 2));
                Single arg2 = (Single)(double)(LuaStatic.GetObj(L, 3));
                Single arg3 = (Single)(double)(LuaStatic.GetObj(L, 4));
                LuaDLL.lua_pushvector3(L, obj.InverseTransformPoint(arg1, arg2, arg3));

                return result;
            }
            LuaStatic.traceback(L, "count not enough");
            LuaDLL.lua_error(L);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int DetachChildren(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            obj.DetachChildren();
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int SetAsFirstSibling(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            obj.SetAsFirstSibling();
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int SetAsLastSibling(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            obj.SetAsLastSibling();
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int SetSiblingIndex(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            Int32 arg1 = (Int32)(double)(LuaStatic.GetObj(L, 2));
            obj.SetSiblingIndex(arg1);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int GetSiblingIndex(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            LuaDLL.lua_pushnumber(L, obj.GetSiblingIndex());
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int Find(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            String arg1 = (String)LuaStatic.GetObj(L, 2);
            LuaStatic.addGameObject2Lua(L, obj.Find(arg1), "Transform");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int IsChildOf(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            UnityEngine.Transform arg1 = (UnityEngine.Transform)LuaStatic.GetObj(L, 2);
            LuaDLL.lua_pushboolean(L, obj.IsChildOf(arg1));
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int FindChild(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            String arg1 = (String)LuaStatic.GetObj(L, 2);
            LuaStatic.addGameObject2Lua(L, obj.FindChild(arg1), "Transform");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int GetEnumerator(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            LuaStatic.addGameObject2Lua(L, obj.GetEnumerator(), "IEnumerator");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int GetChild(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            Int32 arg1 = (Int32)(double)(LuaStatic.GetObj(L, 2));
            LuaStatic.addGameObject2Lua(L, obj.GetChild(arg1), "Transform");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_position(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            LuaDLL.lua_pushvector3(L, obj.position);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_position(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            
            obj.position = (UnityEngine.Vector3)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_localPosition(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            LuaDLL.lua_pushvector3(L, obj.localPosition);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_localPosition(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            
            obj.localPosition = (UnityEngine.Vector3)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_eulerAngles(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            LuaDLL.lua_pushvector3(L, obj.eulerAngles);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_eulerAngles(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            
            obj.eulerAngles = (UnityEngine.Vector3)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_localEulerAngles(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            LuaDLL.lua_pushvector3(L, obj.localEulerAngles);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_localEulerAngles(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            
            obj.localEulerAngles = (UnityEngine.Vector3)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_right(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            LuaDLL.lua_pushvector3(L, obj.right);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_right(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            
            obj.right = (UnityEngine.Vector3)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_up(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            LuaDLL.lua_pushvector3(L, obj.up);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_up(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            
            obj.up = (UnityEngine.Vector3)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_forward(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            LuaDLL.lua_pushvector3(L, obj.forward);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_forward(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            
            obj.forward = (UnityEngine.Vector3)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_rotation(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            LuaStatic.addGameObject2Lua(L, obj.rotation, "Quaternion");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_rotation(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            
            obj.rotation = (UnityEngine.Quaternion)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_localRotation(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            LuaStatic.addGameObject2Lua(L, obj.localRotation, "Quaternion");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_localRotation(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            
            obj.localRotation = (UnityEngine.Quaternion)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_localScale(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            LuaDLL.lua_pushvector3(L, obj.localScale);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_localScale(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            
            obj.localScale = (UnityEngine.Vector3)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_parent(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            LuaStatic.addGameObject2Lua(L, obj.parent, "Transform");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_parent(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            
            obj.parent = (UnityEngine.Transform)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_worldToLocalMatrix(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            LuaStatic.addGameObject2Lua(L, obj.worldToLocalMatrix, "Matrix4x4");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_localToWorldMatrix(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            LuaStatic.addGameObject2Lua(L, obj.localToWorldMatrix, "Matrix4x4");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_root(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            LuaStatic.addGameObject2Lua(L, obj.root, "Transform");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_childCount(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            LuaDLL.lua_pushnumber(L, obj.childCount);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_lossyScale(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            LuaDLL.lua_pushvector3(L, obj.lossyScale);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_hasChanged(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            LuaDLL.lua_pushboolean(L, obj.hasChanged);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_hasChanged(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UnityEngine.Transform obj = LuaStatic.GetObj(L, 1) as UnityEngine.Transform;
            
            obj.hasChanged = (Boolean)LuaStatic.GetObj(L, 2);
            return result;
        }
    }

}