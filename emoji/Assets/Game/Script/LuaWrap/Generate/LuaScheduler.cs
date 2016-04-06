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
* Filename: LuaScheduler
* Created:  4/6/2016 10:55:34 PM
* Author:   HaYaShi ToShiTaKa and tolua#
* Purpose:  Scheduler的lua导出类,本类由插件自动生成
* ==============================================================================
*/
namespace LuaInterface {
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class LuaScheduler {

        public static void Register(IntPtr L) {
            int oldTop = LuaDLL.lua_gettop(L);

            LuaDLL.lua_newtable(L);
            LuaDLL.lua_pushstdcallcfunction(L, LuaScheduler.MakeInstance, "MakeInstance");
            LuaDLL.lua_pushstdcallcfunction(L, LuaScheduler.SchedulerCSFun, "SchedulerCSFun");
            LuaDLL.lua_pushstdcallcfunction(L, LuaScheduler.UnSchedulerCSFun, "UnSchedulerCSFun");
            LuaDLL.lua_pushstdcallcfunction(L, LuaScheduler.SetTimeOut, "SetTimeOut");
            LuaDLL.lua_pushstdcallcfunction(L, LuaScheduler.ExecuteCoroutine, "ExecuteCoroutine");

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
            LuaDLL.lua_setglobal(L, "Scheduler");

            LuaDLL.lua_settop(L, oldTop);
                    }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int MakeInstance(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            LuaStatic.addGameObject2Lua(L, Scheduler.MakeInstance(), "Scheduler");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int SchedulerCSFun(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 5&& LuaStatic.CheckType(L, typeof(System.Action), 2)&& LuaStatic.CheckType(L, typeof(System.Single), 3)&& LuaStatic.CheckType(L, typeof(System.Single), 4)&& LuaStatic.CheckType(L, typeof(System.Object), 5)) {
                    Scheduler obj = LuaStatic.GetObj(L, 1) as Scheduler;
                    System.Action arg1 = (System.Action)LuaStatic.GetObj(L, 2);
                    System.Single arg2 = (System.Single)Convert.ToInt32(LuaStatic.GetObj(L, 3));
                    System.Single arg3 = (System.Single)Convert.ToInt32(LuaStatic.GetObj(L, 4));
                    System.Object arg4 = (System.Object)LuaStatic.GetObj(L, 5);
                    LuaDLL.lua_pushnumber(L, obj.SchedulerCSFun(arg1,arg2,arg3,arg4));

                    return result;
                }
            if (count == 4&& LuaStatic.CheckType(L, typeof(System.Action), 2)&& LuaStatic.CheckType(L, typeof(System.Single), 3)&& LuaStatic.CheckType(L, typeof(System.Single), 4)) {
                    Scheduler obj = LuaStatic.GetObj(L, 1) as Scheduler;
                    System.Action arg1 = (System.Action)LuaStatic.GetObj(L, 2);
                    System.Single arg2 = (System.Single)Convert.ToInt32(LuaStatic.GetObj(L, 3));
                    System.Single arg3 = (System.Single)Convert.ToInt32(LuaStatic.GetObj(L, 4));
                    LuaDLL.lua_pushnumber(L, obj.SchedulerCSFun(arg1,arg2,arg3));

                    return result;
                }
            LuaStatic.traceback(L, "count not enough");
            LuaDLL.lua_error(L);                            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int UnSchedulerCSFun(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            Scheduler obj = LuaStatic.GetObj(L, 1) as Scheduler;
                System.Int32 arg1 = (System.Int32)Convert.ToInt32(LuaStatic.GetObj(L, 2));
            LuaDLL.lua_pushnumber(L, obj.UnSchedulerCSFun(arg1));
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int SetTimeOut(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 3) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            Scheduler obj = LuaStatic.GetObj(L, 1) as Scheduler;
                System.Single arg1 = (System.Single)Convert.ToInt32(LuaStatic.GetObj(L, 2));
                System.Action arg2 = (System.Action)LuaStatic.GetObj(L, 3);
            obj.SetTimeOut(arg1,arg2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int ExecuteCoroutine(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            Scheduler obj = LuaStatic.GetObj(L, 1) as Scheduler;
                System.Collections.IEnumerator arg1 = (System.Collections.IEnumerator)LuaStatic.GetObj(L, 2);
            obj.ExecuteCoroutine(arg1);
            return result;
        }
    }

}