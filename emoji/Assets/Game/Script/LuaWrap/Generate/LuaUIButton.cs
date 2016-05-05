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
* Filename: LuaUIButton
* Created:  5/4/2016 11:45:56 AM
* Author:   HaYaShi ToShiTaKa and tolua#
* Purpose:  UIButton的lua导出类,本类由插件自动生成
* ==============================================================================
*/
namespace LuaInterface {
    using System.Collections.Generic;
    using System;
    using System.Collections;

    public class LuaUIButton {
        public static void Register(IntPtr L) {
            int oldTop = LuaDLL.lua_gettop(L);
            LuaDLL.lua_getglobal(L, "UIButton");

            if (LuaDLL.lua_isnil(L, -1)) {
                LuaDLL.lua_pop(L, 1);
                LuaDLL.lua_newtable(L);
                LuaDLL.lua_setglobal(L, "UIButton");
                LuaDLL.lua_getglobal(L, "UIButton");
            }

            LuaDLL.lua_pushstdcallcfunction(L, LuaUIButton.SetState, "SetState");
            LuaDLL.lua_pushcsharpproperty(L, "isEnabled", LuaUIButton.get_isEnabled, LuaUIButton.set_isEnabled);
            LuaDLL.lua_pushcsharpproperty(L, "normalSprite", LuaUIButton.get_normalSprite, LuaUIButton.set_normalSprite);
            LuaDLL.lua_pushcsharpproperty(L, "normalSprite2D", LuaUIButton.get_normalSprite2D, LuaUIButton.set_normalSprite2D);
            LuaDLL.lua_pushcsharpproperty(L, "current", LuaUIButton.get_current, LuaUIButton.set_current);
            LuaDLL.lua_pushcsharpproperty(L, "dragHighlight", LuaUIButton.get_dragHighlight, LuaUIButton.set_dragHighlight);
            LuaDLL.lua_pushcsharpproperty(L, "hoverSprite", LuaUIButton.get_hoverSprite, LuaUIButton.set_hoverSprite);
            LuaDLL.lua_pushcsharpproperty(L, "pressedSprite", LuaUIButton.get_pressedSprite, LuaUIButton.set_pressedSprite);
            LuaDLL.lua_pushcsharpproperty(L, "disabledSprite", LuaUIButton.get_disabledSprite, LuaUIButton.set_disabledSprite);
            LuaDLL.lua_pushcsharpproperty(L, "hoverSprite2D", LuaUIButton.get_hoverSprite2D, LuaUIButton.set_hoverSprite2D);
            LuaDLL.lua_pushcsharpproperty(L, "pressedSprite2D", LuaUIButton.get_pressedSprite2D, LuaUIButton.set_pressedSprite2D);
            LuaDLL.lua_pushcsharpproperty(L, "disabledSprite2D", LuaUIButton.get_disabledSprite2D, LuaUIButton.set_disabledSprite2D);
            LuaDLL.lua_pushcsharpproperty(L, "pixelSnap", LuaUIButton.get_pixelSnap, LuaUIButton.set_pixelSnap);
            LuaDLL.lua_pushcsharpproperty(L, "onClick", LuaUIButton.get_onClick, LuaUIButton.set_onClick);

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
            LuaDLL.lua_getglobal(L, "UIButtonColor");
            if (LuaDLL.lua_isnil(L, -1)) {
                LuaDLL.lua_pop(L, 1);
                LuaDLL.lua_newtable(L);
                LuaDLL.lua_setglobal(L, "UIButtonColor");
                LuaDLL.lua_getglobal(L, "UIButtonColor");
                LuaDLL.lua_setmetatable(L, -2);
            }
            else {
                LuaDLL.lua_setmetatable(L, -2);
            }

            LuaDLL.lua_settop(L, oldTop);
            LuaStatic.AddTypeDict(typeof(UIButton));

        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int SetState(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 3) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UIButton obj = LuaStatic.GetObj(L, 1) as UIButton;
            UIButtonColor.State arg1 = (UIButtonColor.State)(double)(LuaStatic.GetObj(L, 2));
            Boolean arg2 = (Boolean)LuaStatic.GetObj(L, 3);
            obj.SetState(arg1,arg2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_isEnabled(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UIButton obj = LuaStatic.GetObj(L, 1) as UIButton;
            LuaDLL.lua_pushboolean(L, obj.isEnabled);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_isEnabled(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UIButton obj = LuaStatic.GetObj(L, 1) as UIButton;
            
            obj.isEnabled = (Boolean)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_normalSprite(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UIButton obj = LuaStatic.GetObj(L, 1) as UIButton;
            LuaDLL.lua_pushstring(L, obj.normalSprite);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_normalSprite(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UIButton obj = LuaStatic.GetObj(L, 1) as UIButton;
            
            obj.normalSprite = (String)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_normalSprite2D(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UIButton obj = LuaStatic.GetObj(L, 1) as UIButton;
            LuaStatic.addGameObject2Lua(L, obj.normalSprite2D, "Sprite");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_normalSprite2D(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UIButton obj = LuaStatic.GetObj(L, 1) as UIButton;
            
            obj.normalSprite2D = (UnityEngine.Sprite)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_current(IntPtr L) {
            int result = 1;

            LuaStatic.addGameObject2Lua(L, UIButton.current, "UIButton");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_current(IntPtr L) {
            int result = 1;

            UIButton.current = (UIButton)LuaStatic.GetObj(L, 1);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_dragHighlight(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UIButton obj = LuaStatic.GetObj(L, 1) as UIButton;
            LuaDLL.lua_pushboolean(L, obj.dragHighlight);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_dragHighlight(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UIButton obj = LuaStatic.GetObj(L, 1) as UIButton;
            
            obj.dragHighlight = (Boolean)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_hoverSprite(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UIButton obj = LuaStatic.GetObj(L, 1) as UIButton;
            LuaDLL.lua_pushstring(L, obj.hoverSprite);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_hoverSprite(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UIButton obj = LuaStatic.GetObj(L, 1) as UIButton;
            
            obj.hoverSprite = (String)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_pressedSprite(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UIButton obj = LuaStatic.GetObj(L, 1) as UIButton;
            LuaDLL.lua_pushstring(L, obj.pressedSprite);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_pressedSprite(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UIButton obj = LuaStatic.GetObj(L, 1) as UIButton;
            
            obj.pressedSprite = (String)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_disabledSprite(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UIButton obj = LuaStatic.GetObj(L, 1) as UIButton;
            LuaDLL.lua_pushstring(L, obj.disabledSprite);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_disabledSprite(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UIButton obj = LuaStatic.GetObj(L, 1) as UIButton;
            
            obj.disabledSprite = (String)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_hoverSprite2D(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UIButton obj = LuaStatic.GetObj(L, 1) as UIButton;
            LuaStatic.addGameObject2Lua(L, obj.hoverSprite2D, "Sprite");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_hoverSprite2D(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UIButton obj = LuaStatic.GetObj(L, 1) as UIButton;
            
            obj.hoverSprite2D = (UnityEngine.Sprite)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_pressedSprite2D(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UIButton obj = LuaStatic.GetObj(L, 1) as UIButton;
            LuaStatic.addGameObject2Lua(L, obj.pressedSprite2D, "Sprite");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_pressedSprite2D(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UIButton obj = LuaStatic.GetObj(L, 1) as UIButton;
            
            obj.pressedSprite2D = (UnityEngine.Sprite)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_disabledSprite2D(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UIButton obj = LuaStatic.GetObj(L, 1) as UIButton;
            LuaStatic.addGameObject2Lua(L, obj.disabledSprite2D, "Sprite");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_disabledSprite2D(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UIButton obj = LuaStatic.GetObj(L, 1) as UIButton;
            
            obj.disabledSprite2D = (UnityEngine.Sprite)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_pixelSnap(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UIButton obj = LuaStatic.GetObj(L, 1) as UIButton;
            LuaDLL.lua_pushboolean(L, obj.pixelSnap);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_pixelSnap(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UIButton obj = LuaStatic.GetObj(L, 1) as UIButton;
            
            obj.pixelSnap = (Boolean)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_onClick(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UIButton obj = LuaStatic.GetObj(L, 1) as UIButton;
            LuaStatic.addGameObject2Lua(L, obj.onClick, "List`1");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_onClick(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UIButton obj = LuaStatic.GetObj(L, 1) as UIButton;
            
            obj.onClick = (List<EventDelegate>)LuaStatic.GetObj(L, 2);
            return result;
        }
    }

}