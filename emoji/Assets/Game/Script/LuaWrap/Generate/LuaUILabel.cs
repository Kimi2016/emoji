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
* Filename: LuaUILabel
* Created:  5/4/2016 11:45:56 AM
* Author:   HaYaShi ToShiTaKa and tolua#
* Purpose:  UILabel的lua导出类,本类由插件自动生成
* ==============================================================================
*/
namespace LuaInterface {
    using System.Collections.Generic;
    using System;
    using System.Collections;

    public class LuaUILabel {
        public static void Register(IntPtr L) {
            int oldTop = LuaDLL.lua_gettop(L);
            LuaDLL.lua_getglobal(L, "UILabel");

            if (LuaDLL.lua_isnil(L, -1)) {
                LuaDLL.lua_pop(L, 1);
                LuaDLL.lua_newtable(L);
                LuaDLL.lua_setglobal(L, "UILabel");
                LuaDLL.lua_getglobal(L, "UILabel");
            }

            LuaDLL.lua_pushstdcallcfunction(L, LuaUILabel.GetSides, "GetSides");
            LuaDLL.lua_pushstdcallcfunction(L, LuaUILabel.MarkAsChanged, "MarkAsChanged");
            LuaDLL.lua_pushstdcallcfunction(L, LuaUILabel.ProcessText, "ProcessText");
            LuaDLL.lua_pushstdcallcfunction(L, LuaUILabel.MakePixelPerfect, "MakePixelPerfect");
            LuaDLL.lua_pushstdcallcfunction(L, LuaUILabel.AssumeNaturalSize, "AssumeNaturalSize");
            LuaDLL.lua_pushstdcallcfunction(L, LuaUILabel.GetCharacterIndexAtPosition, "GetCharacterIndexAtPosition");
            LuaDLL.lua_pushstdcallcfunction(L, LuaUILabel.GetWordAtPosition, "GetWordAtPosition");
            LuaDLL.lua_pushstdcallcfunction(L, LuaUILabel.GetWordAtCharacterIndex, "GetWordAtCharacterIndex");
            LuaDLL.lua_pushstdcallcfunction(L, LuaUILabel.GetUrlAtPosition, "GetUrlAtPosition");
            LuaDLL.lua_pushstdcallcfunction(L, LuaUILabel.GetUrlAtCharacterIndex, "GetUrlAtCharacterIndex");
            LuaDLL.lua_pushstdcallcfunction(L, LuaUILabel.GetCharacterIndex, "GetCharacterIndex");
            LuaDLL.lua_pushstdcallcfunction(L, LuaUILabel.PrintOverlay, "PrintOverlay");
            LuaDLL.lua_pushstdcallcfunction(L, LuaUILabel.OnFill, "OnFill");
            LuaDLL.lua_pushstdcallcfunction(L, LuaUILabel.ApplyOffset, "ApplyOffset");
            LuaDLL.lua_pushstdcallcfunction(L, LuaUILabel.ApplyShadow, "ApplyShadow");
            LuaDLL.lua_pushstdcallcfunction(L, LuaUILabel.CalculateOffsetToFit, "CalculateOffsetToFit");
            LuaDLL.lua_pushstdcallcfunction(L, LuaUILabel.SetCurrentProgress, "SetCurrentProgress");
            LuaDLL.lua_pushstdcallcfunction(L, LuaUILabel.SetCurrentPercent, "SetCurrentPercent");
            LuaDLL.lua_pushstdcallcfunction(L, LuaUILabel.SetCurrentSelection, "SetCurrentSelection");
            LuaDLL.lua_pushstdcallcfunction(L, LuaUILabel.Wrap, "Wrap");
            LuaDLL.lua_pushstdcallcfunction(L, LuaUILabel.UpdateNGUIText, "UpdateNGUIText");
            LuaDLL.lua_pushcsharpproperty(L, "finalFontSize", LuaUILabel.get_finalFontSize, null);
            LuaDLL.lua_pushcsharpproperty(L, "isAnchoredHorizontally", LuaUILabel.get_isAnchoredHorizontally, null);
            LuaDLL.lua_pushcsharpproperty(L, "isAnchoredVertically", LuaUILabel.get_isAnchoredVertically, null);
            LuaDLL.lua_pushcsharpproperty(L, "material", LuaUILabel.get_material, LuaUILabel.set_material);
            LuaDLL.lua_pushcsharpproperty(L, "bitmapFont", LuaUILabel.get_bitmapFont, LuaUILabel.set_bitmapFont);
            LuaDLL.lua_pushcsharpproperty(L, "trueTypeFont", LuaUILabel.get_trueTypeFont, LuaUILabel.set_trueTypeFont);
            LuaDLL.lua_pushcsharpproperty(L, "ambigiousFont", LuaUILabel.get_ambigiousFont, LuaUILabel.set_ambigiousFont);
            LuaDLL.lua_pushcsharpproperty(L, "text", LuaUILabel.get_text, LuaUILabel.set_text);
            LuaDLL.lua_pushcsharpproperty(L, "defaultFontSize", LuaUILabel.get_defaultFontSize, null);
            LuaDLL.lua_pushcsharpproperty(L, "fontSize", LuaUILabel.get_fontSize, LuaUILabel.set_fontSize);
            LuaDLL.lua_pushcsharpproperty(L, "fontStyle", LuaUILabel.get_fontStyle, LuaUILabel.set_fontStyle);
            LuaDLL.lua_pushcsharpproperty(L, "alignment", LuaUILabel.get_alignment, LuaUILabel.set_alignment);
            LuaDLL.lua_pushcsharpproperty(L, "applyGradient", LuaUILabel.get_applyGradient, LuaUILabel.set_applyGradient);
            LuaDLL.lua_pushcsharpproperty(L, "gradientTop", LuaUILabel.get_gradientTop, LuaUILabel.set_gradientTop);
            LuaDLL.lua_pushcsharpproperty(L, "gradientBottom", LuaUILabel.get_gradientBottom, LuaUILabel.set_gradientBottom);
            LuaDLL.lua_pushcsharpproperty(L, "spacingX", LuaUILabel.get_spacingX, LuaUILabel.set_spacingX);
            LuaDLL.lua_pushcsharpproperty(L, "spacingY", LuaUILabel.get_spacingY, LuaUILabel.set_spacingY);
            LuaDLL.lua_pushcsharpproperty(L, "useFloatSpacing", LuaUILabel.get_useFloatSpacing, LuaUILabel.set_useFloatSpacing);
            LuaDLL.lua_pushcsharpproperty(L, "floatSpacingX", LuaUILabel.get_floatSpacingX, LuaUILabel.set_floatSpacingX);
            LuaDLL.lua_pushcsharpproperty(L, "floatSpacingY", LuaUILabel.get_floatSpacingY, LuaUILabel.set_floatSpacingY);
            LuaDLL.lua_pushcsharpproperty(L, "effectiveSpacingY", LuaUILabel.get_effectiveSpacingY, null);
            LuaDLL.lua_pushcsharpproperty(L, "effectiveSpacingX", LuaUILabel.get_effectiveSpacingX, null);
            LuaDLL.lua_pushcsharpproperty(L, "supportEncoding", LuaUILabel.get_supportEncoding, LuaUILabel.set_supportEncoding);
            LuaDLL.lua_pushcsharpproperty(L, "symbolStyle", LuaUILabel.get_symbolStyle, LuaUILabel.set_symbolStyle);
            LuaDLL.lua_pushcsharpproperty(L, "overflowMethod", LuaUILabel.get_overflowMethod, LuaUILabel.set_overflowMethod);
            LuaDLL.lua_pushcsharpproperty(L, "multiLine", LuaUILabel.get_multiLine, LuaUILabel.set_multiLine);
            LuaDLL.lua_pushcsharpproperty(L, "localCorners", LuaUILabel.get_localCorners, null);
            LuaDLL.lua_pushcsharpproperty(L, "worldCorners", LuaUILabel.get_worldCorners, null);
            LuaDLL.lua_pushcsharpproperty(L, "drawingDimensions", LuaUILabel.get_drawingDimensions, null);
            LuaDLL.lua_pushcsharpproperty(L, "maxLineCount", LuaUILabel.get_maxLineCount, LuaUILabel.set_maxLineCount);
            LuaDLL.lua_pushcsharpproperty(L, "effectStyle", LuaUILabel.get_effectStyle, LuaUILabel.set_effectStyle);
            LuaDLL.lua_pushcsharpproperty(L, "effectColor", LuaUILabel.get_effectColor, LuaUILabel.set_effectColor);
            LuaDLL.lua_pushcsharpproperty(L, "effectDistance", LuaUILabel.get_effectDistance, LuaUILabel.set_effectDistance);
            LuaDLL.lua_pushcsharpproperty(L, "processedText", LuaUILabel.get_processedText, null);
            LuaDLL.lua_pushcsharpproperty(L, "printedSize", LuaUILabel.get_printedSize, null);
            LuaDLL.lua_pushcsharpproperty(L, "localSize", LuaUILabel.get_localSize, null);
            LuaDLL.lua_pushcsharpproperty(L, "keepCrispWhenShrunk", LuaUILabel.get_keepCrispWhenShrunk, LuaUILabel.set_keepCrispWhenShrunk);

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
            LuaDLL.lua_getglobal(L, "UIWidget");
            if (LuaDLL.lua_isnil(L, -1)) {
                LuaDLL.lua_pop(L, 1);
                LuaDLL.lua_newtable(L);
                LuaDLL.lua_setglobal(L, "UIWidget");
                LuaDLL.lua_getglobal(L, "UIWidget");
                LuaDLL.lua_setmetatable(L, -2);
            }
            else {
                LuaDLL.lua_setmetatable(L, -2);
            }

            LuaDLL.lua_settop(L, oldTop);
            LuaStatic.AddTypeDict(typeof(UILabel));

        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int GetSides(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            UnityEngine.Transform arg1 = (UnityEngine.Transform)LuaStatic.GetObj(L, 2);
            UnityEngine.Vector3[] objs = obj.GetSides(arg1);
            LuaDLL.lua_newtable(L);
            int num2 = 0;
            foreach (var item in objs) {
                LuaDLL.lua_pushvector3(L, item);
                LuaDLL.lua_pushnumber(L, (double)(++num2));
                LuaDLL.lua_insert(L, -2);
                LuaDLL.lua_settable(L, -3);
            }
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int MarkAsChanged(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            obj.MarkAsChanged();
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int ProcessText(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            obj.ProcessText();
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int MakePixelPerfect(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            obj.MakePixelPerfect();
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int AssumeNaturalSize(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            obj.AssumeNaturalSize();
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int GetCharacterIndexAtPosition(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Vector3), 2) &&
                LuaStatic.CheckType(L, typeof(Boolean), 3)) {
                UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
                UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaStatic.GetObj(L, 2);
                Boolean arg2 = (Boolean)LuaStatic.GetObj(L, 3);
                LuaDLL.lua_pushnumber(L, obj.GetCharacterIndexAtPosition(arg1, arg2));

                return result;
            }
            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Vector2), 2) &&
                LuaStatic.CheckType(L, typeof(Boolean), 3)) {
                UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
                UnityEngine.Vector2 arg1 = (UnityEngine.Vector2)LuaStatic.GetObj(L, 2);
                Boolean arg2 = (Boolean)LuaStatic.GetObj(L, 3);
                LuaDLL.lua_pushnumber(L, obj.GetCharacterIndexAtPosition(arg1, arg2));

                return result;
            }
            LuaStatic.traceback(L, "count not enough");
            LuaDLL.lua_error(L);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int GetWordAtPosition(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Vector3), 2)) {
                UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
                UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaStatic.GetObj(L, 2);
                LuaDLL.lua_pushstring(L, obj.GetWordAtPosition(arg1));

                return result;
            }
            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Vector2), 2)) {
                UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
                UnityEngine.Vector2 arg1 = (UnityEngine.Vector2)LuaStatic.GetObj(L, 2);
                LuaDLL.lua_pushstring(L, obj.GetWordAtPosition(arg1));

                return result;
            }
            LuaStatic.traceback(L, "count not enough");
            LuaDLL.lua_error(L);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int GetWordAtCharacterIndex(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            Int32 arg1 = (Int32)(double)(LuaStatic.GetObj(L, 2));
            LuaDLL.lua_pushstring(L, obj.GetWordAtCharacterIndex(arg1));
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int GetUrlAtPosition(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Vector3), 2)) {
                UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
                UnityEngine.Vector3 arg1 = (UnityEngine.Vector3)LuaStatic.GetObj(L, 2);
                LuaDLL.lua_pushstring(L, obj.GetUrlAtPosition(arg1));

                return result;
            }
            if (count == 2 &&
                LuaStatic.CheckType(L, typeof(UnityEngine.Vector2), 2)) {
                UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
                UnityEngine.Vector2 arg1 = (UnityEngine.Vector2)LuaStatic.GetObj(L, 2);
                LuaDLL.lua_pushstring(L, obj.GetUrlAtPosition(arg1));

                return result;
            }
            LuaStatic.traceback(L, "count not enough");
            LuaDLL.lua_error(L);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int GetUrlAtCharacterIndex(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            Int32 arg1 = (Int32)(double)(LuaStatic.GetObj(L, 2));
            LuaDLL.lua_pushstring(L, obj.GetUrlAtCharacterIndex(arg1));
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int GetCharacterIndex(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 3) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            Int32 arg1 = (Int32)(double)(LuaStatic.GetObj(L, 2));
            UnityEngine.KeyCode arg2 = (UnityEngine.KeyCode)(double)(LuaStatic.GetObj(L, 3));
            LuaDLL.lua_pushnumber(L, obj.GetCharacterIndex(arg1,arg2));
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int PrintOverlay(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 7) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            Int32 arg1 = (Int32)(double)(LuaStatic.GetObj(L, 2));
            Int32 arg2 = (Int32)(double)(LuaStatic.GetObj(L, 3));
            UIGeometry arg3 = (UIGeometry)LuaStatic.GetObj(L, 4);
            UIGeometry arg4 = (UIGeometry)LuaStatic.GetObj(L, 5);
            UnityEngine.Color arg5 = (UnityEngine.Color)LuaStatic.GetObj(L, 6);
            UnityEngine.Color arg6 = (UnityEngine.Color)LuaStatic.GetObj(L, 7);
            obj.PrintOverlay(arg1,arg2,arg3,arg4,arg5,arg6);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int OnFill(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 4) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            BetterList<UnityEngine.Vector3> arg1 = (BetterList<UnityEngine.Vector3>)LuaStatic.GetObj(L, 2);
            BetterList<UnityEngine.Vector2> arg2 = (BetterList<UnityEngine.Vector2>)LuaStatic.GetObj(L, 3);
            BetterList<UnityEngine.Color32> arg3 = (BetterList<UnityEngine.Color32>)LuaStatic.GetObj(L, 4);
            obj.OnFill(arg1,arg2,arg3);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int ApplyOffset(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 3) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            BetterList<UnityEngine.Vector3> arg1 = (BetterList<UnityEngine.Vector3>)LuaStatic.GetObj(L, 2);
            Int32 arg2 = (Int32)(double)(LuaStatic.GetObj(L, 3));
            LuaDLL.lua_pushvector2(L, obj.ApplyOffset(arg1,arg2));
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int ApplyShadow(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 8) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            BetterList<UnityEngine.Vector3> arg1 = (BetterList<UnityEngine.Vector3>)LuaStatic.GetObj(L, 2);
            BetterList<UnityEngine.Vector2> arg2 = (BetterList<UnityEngine.Vector2>)LuaStatic.GetObj(L, 3);
            BetterList<UnityEngine.Color32> arg3 = (BetterList<UnityEngine.Color32>)LuaStatic.GetObj(L, 4);
            Int32 arg4 = (Int32)(double)(LuaStatic.GetObj(L, 5));
            Int32 arg5 = (Int32)(double)(LuaStatic.GetObj(L, 6));
            Single arg6 = (Single)(double)(LuaStatic.GetObj(L, 7));
            Single arg7 = (Single)(double)(LuaStatic.GetObj(L, 8));
            obj.ApplyShadow(arg1,arg2,arg3,arg4,arg5,arg6,arg7);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int CalculateOffsetToFit(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            String arg1 = (String)LuaStatic.GetObj(L, 2);
            LuaDLL.lua_pushnumber(L, obj.CalculateOffsetToFit(arg1));
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int SetCurrentProgress(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            obj.SetCurrentProgress();
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int SetCurrentPercent(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            obj.SetCurrentPercent();
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int SetCurrentSelection(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            obj.SetCurrentSelection();
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int Wrap(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count == 3 &&
                LuaStatic.CheckType(L, typeof(String), 2) &&
                LuaStatic.CheckType(L, typeof(String), 3)) {
                UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
                String arg1 = (String)LuaStatic.GetObj(L, 2);
                String arg2 = (String)LuaStatic.GetObj(L, 3);
                LuaDLL.lua_pushboolean(L, obj.Wrap(arg1, out arg2));
                LuaDLL.lua_pushstring(L, arg2);

                return result;
            }
            if (count == 4 &&
                LuaStatic.CheckType(L, typeof(String), 2) &&
                LuaStatic.CheckType(L, typeof(String), 3) &&
                LuaStatic.CheckType(L, typeof(Int32), 4)) {
                UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
                String arg1 = (String)LuaStatic.GetObj(L, 2);
                String arg2 = (String)LuaStatic.GetObj(L, 3);
                Int32 arg3 = (Int32)(double)(LuaStatic.GetObj(L, 4));
                LuaDLL.lua_pushboolean(L, obj.Wrap(arg1, out arg2, arg3));
                LuaDLL.lua_pushstring(L, arg2);

                return result;
            }
            LuaStatic.traceback(L, "count not enough");
            LuaDLL.lua_error(L);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int UpdateNGUIText(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            obj.UpdateNGUIText();
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_finalFontSize(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            LuaDLL.lua_pushnumber(L, obj.finalFontSize);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_isAnchoredHorizontally(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            LuaDLL.lua_pushboolean(L, obj.isAnchoredHorizontally);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_isAnchoredVertically(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            LuaDLL.lua_pushboolean(L, obj.isAnchoredVertically);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_material(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            LuaStatic.addGameObject2Lua(L, obj.material, "Material");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_material(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            
            obj.material = (UnityEngine.Material)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_bitmapFont(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            LuaStatic.addGameObject2Lua(L, obj.bitmapFont, "UIFont");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_bitmapFont(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            
            obj.bitmapFont = (UIFont)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_trueTypeFont(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            LuaStatic.addGameObject2Lua(L, obj.trueTypeFont, "Font");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_trueTypeFont(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            
            obj.trueTypeFont = (UnityEngine.Font)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_ambigiousFont(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            LuaStatic.addGameObject2Lua(L, obj.ambigiousFont, "Object");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_ambigiousFont(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            
            obj.ambigiousFont = (UnityEngine.Object)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_text(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            LuaDLL.lua_pushstring(L, obj.text);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_text(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            
            obj.text = (String)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_defaultFontSize(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            LuaDLL.lua_pushnumber(L, obj.defaultFontSize);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_fontSize(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            LuaDLL.lua_pushnumber(L, obj.fontSize);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_fontSize(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            
            obj.fontSize = (Int32)(double)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_fontStyle(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            LuaStatic.addGameObject2Lua(L, obj.fontStyle, "FontStyle");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_fontStyle(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            
            obj.fontStyle = (UnityEngine.FontStyle)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_alignment(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            LuaStatic.addGameObject2Lua(L, obj.alignment, "Alignment");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_alignment(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            
            obj.alignment = (NGUIText.Alignment)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_applyGradient(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            LuaDLL.lua_pushboolean(L, obj.applyGradient);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_applyGradient(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            
            obj.applyGradient = (Boolean)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_gradientTop(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            LuaStatic.addGameObject2Lua(L, obj.gradientTop, "Color");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_gradientTop(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            
            obj.gradientTop = (UnityEngine.Color)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_gradientBottom(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            LuaStatic.addGameObject2Lua(L, obj.gradientBottom, "Color");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_gradientBottom(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            
            obj.gradientBottom = (UnityEngine.Color)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_spacingX(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            LuaDLL.lua_pushnumber(L, obj.spacingX);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_spacingX(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            
            obj.spacingX = (Int32)(double)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_spacingY(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            LuaDLL.lua_pushnumber(L, obj.spacingY);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_spacingY(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            
            obj.spacingY = (Int32)(double)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_useFloatSpacing(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            LuaDLL.lua_pushboolean(L, obj.useFloatSpacing);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_useFloatSpacing(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            
            obj.useFloatSpacing = (Boolean)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_floatSpacingX(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            LuaDLL.lua_pushnumber(L, obj.floatSpacingX);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_floatSpacingX(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            
            obj.floatSpacingX = (Single)(double)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_floatSpacingY(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            LuaDLL.lua_pushnumber(L, obj.floatSpacingY);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_floatSpacingY(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            
            obj.floatSpacingY = (Single)(double)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_effectiveSpacingY(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            LuaDLL.lua_pushnumber(L, obj.effectiveSpacingY);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_effectiveSpacingX(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            LuaDLL.lua_pushnumber(L, obj.effectiveSpacingX);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_supportEncoding(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            LuaDLL.lua_pushboolean(L, obj.supportEncoding);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_supportEncoding(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            
            obj.supportEncoding = (Boolean)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_symbolStyle(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            LuaStatic.addGameObject2Lua(L, obj.symbolStyle, "SymbolStyle");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_symbolStyle(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            
            obj.symbolStyle = (NGUIText.SymbolStyle)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_overflowMethod(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            LuaStatic.addGameObject2Lua(L, obj.overflowMethod, "Overflow");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_overflowMethod(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            
            obj.overflowMethod = (UILabel.Overflow)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_multiLine(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            LuaDLL.lua_pushboolean(L, obj.multiLine);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_multiLine(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            
            obj.multiLine = (Boolean)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_localCorners(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            UnityEngine.Vector3[] objs = obj.localCorners;
            LuaDLL.lua_newtable(L);
            int num2 = 0;
            foreach (var item in objs) {
                LuaDLL.lua_pushvector3(L, item);
                LuaDLL.lua_pushnumber(L, (double)(++num2));
                LuaDLL.lua_insert(L, -2);
                LuaDLL.lua_settable(L, -3);
            }
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_worldCorners(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            UnityEngine.Vector3[] objs = obj.worldCorners;
            LuaDLL.lua_newtable(L);
            int num2 = 0;
            foreach (var item in objs) {
                LuaDLL.lua_pushvector3(L, item);
                LuaDLL.lua_pushnumber(L, (double)(++num2));
                LuaDLL.lua_insert(L, -2);
                LuaDLL.lua_settable(L, -3);
            }
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_drawingDimensions(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            LuaDLL.lua_pushvector4(L, obj.drawingDimensions);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_maxLineCount(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            LuaDLL.lua_pushnumber(L, obj.maxLineCount);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_maxLineCount(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            
            obj.maxLineCount = (Int32)(double)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_effectStyle(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            LuaStatic.addGameObject2Lua(L, obj.effectStyle, "Effect");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_effectStyle(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            
            obj.effectStyle = (UILabel.Effect)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_effectColor(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            LuaStatic.addGameObject2Lua(L, obj.effectColor, "Color");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_effectColor(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            
            obj.effectColor = (UnityEngine.Color)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_effectDistance(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            LuaDLL.lua_pushvector2(L, obj.effectDistance);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_effectDistance(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            
            obj.effectDistance = (UnityEngine.Vector2)LuaStatic.GetObj(L, 2);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_processedText(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            LuaDLL.lua_pushstring(L, obj.processedText);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_printedSize(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            LuaDLL.lua_pushvector2(L, obj.printedSize);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_localSize(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            LuaDLL.lua_pushvector2(L, obj.localSize);
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int get_keepCrispWhenShrunk(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 1) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            LuaStatic.addGameObject2Lua(L, obj.keepCrispWhenShrunk, "Crispness");
            return result;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int set_keepCrispWhenShrunk(IntPtr L) {
            int result = 1;
            int count = LuaDLL.lua_gettop(L);

            if (count != 2) {
                LuaStatic.traceback(L, "count not enough");
                LuaDLL.lua_error(L);
                return result;
            }
            UILabel obj = LuaStatic.GetObj(L, 1) as UILabel;
            
            obj.keepCrispWhenShrunk = (UILabel.Crispness)LuaStatic.GetObj(L, 2);
            return result;
        }
    }

}