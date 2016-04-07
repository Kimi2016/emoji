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
* Created:  4/7/2016 9:16:11 PM
* Author:   HaYaShi ToShiTaKa and tolua#
* Purpose:  Scheduler的lua导出类,本类由插件自动生成
* ==============================================================================
*/
namespace LuaInterface {
    using System.Collections.Generic;
    using System;
    using System.Collections;

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

            if (count == 5 &&
                LuaStatic.CheckType(L, typeof(Action), 2) &&
                LuaStatic.CheckType(L, typeof(Single), 3) &&
                LuaStatic.CheckType(L, typeof(Single), 4) &&
                LuaStatic.CheckType(L, typeof(Object), 5)) {
                Scheduler obj = LuaStatic.GetObj(L, 1) as Scheduler;
                Action arg1 = (Action)LuaStatic.GetObj(L, 2);
                Single arg2 = (Single)Convert.ToInt32(LuaStatic.GetObj(L, 3));
                Single arg3 = (Single)Convert.ToInt32(LuaStatic.GetObj(L, 4));
                Object arg4 = (Object)LuaStatic.GetObj(L, 5);
                LuaDLL.lua_pushnumber(L, obj.SchedulerCSFun(arg1, arg2, arg3, arg4));

                return result;
            }
            if (count == 4 &&
                LuaStatic.CheckType(L, typeof(Action), 2) &&
                LuaStatic.CheckType(L, typeof(Single), 3) &&
                LuaStatic.CheckType(L, typeof(Single), 4)) {
                Scheduler obj = LuaStatic.GetObj(L, 1) as Scheduler;
                Action arg1 = (Action)LuaStatic.GetObj(L, 2);
                Single arg2 = (Single)Convert.ToInt32(LuaStatic.GetObj(L, 3));
                Single arg3 = (Single)Convert.ToInt32(LuaStatic.GetObj(L, 4));
                LuaDLL.lua_pushnumber(L, obj.SchedulerCSFun(arg1, arg2, arg3));

                return result;
            }
            LuaStatic.traceback(L, "count not enough");
            LuaDLL.lua_error(L);
            return result;
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
            Int32 arg1 = (Int32)Convert.ToInt32(LuaStatic.GetObj(L, 2));
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
            Single arg1 = (Single)Convert.ToInt32(LuaStatic.GetObj(L, 2));
            Action arg2 = (Action)LuaStatic.GetObj(L, 3);
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
            IEnumerator arg1 = (IEnumerator)LuaStatic.GetObj(L, 2);
            obj.ExecuteCoroutine(arg1);
            return result;
        }
    }

}