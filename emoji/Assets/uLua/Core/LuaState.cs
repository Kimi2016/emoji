#if UNITY_IPHONE
#define __NOGEN__
#endif

namespace LuaInterface {
    using System;
    using System.IO;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Text;

    public class LuaState : IDisposable {
        public IntPtr L;

        internal LuaCSFunction tracebackFunction;
        internal LuaCSFunction panicCallback;

        // Overrides
        internal LuaCSFunction printFunction;
        internal LuaCSFunction loadfileFunction;
        internal LuaCSFunction loaderFunction;
        internal LuaCSFunction dofileFunction;

        public LuaState() {
            // Create State
            L = LuaDLL.luaL_newstate();

            // Create LuaInterface library
            LuaDLL.luaL_openlibs(L);
            LuaDLL.lua_pushstring(L, "LUAINTERFACE LOADED");
            LuaDLL.lua_pushboolean(L, true);
            LuaDLL.lua_settable(L, (int)LuaIndexes.LUA_REGISTRYINDEX);

            tracebackFunction = new LuaCSFunction(error_traceback);

            // We need to keep this in a managed reference so the delegate doesn't get garbage collected
            panicCallback = new LuaCSFunction(panic);
            LuaDLL.lua_atpanic(L, panicCallback);
            printFunction = new LuaCSFunction(print);
            LuaDLL.lua_pushstdcallcfunction(L, printFunction);
            LuaDLL.lua_setfield(L, LuaIndexes.LUA_GLOBALSINDEX, "print");

            loadfileFunction = new LuaCSFunction(loadfile);
            LuaDLL.lua_pushstdcallcfunction(L, loadfileFunction);
            LuaDLL.lua_setfield(L, LuaIndexes.LUA_GLOBALSINDEX, "loadfile");

            dofileFunction = new LuaCSFunction(dofile);
            LuaDLL.lua_pushstdcallcfunction(L, dofileFunction);
            LuaDLL.lua_setfield(L, LuaIndexes.LUA_GLOBALSINDEX, "dofile");
            LuaDLL.lua_pushstdcallcfunction(L, dofileFunction);
            LuaDLL.lua_setfield(L, LuaIndexes.LUA_GLOBALSINDEX, "require");

            LuaDLL.lua_newtable(L);
            LuaDLL.lua_newtable(L);
            LuaDLL.lua_pushstring(L, "v");
            LuaDLL.lua_setfield(L, -2, "__mode");
            LuaDLL.lua_setmetatable(L, -2);
            LuaDLL.lua_setfield(L, LuaIndexes.LUA_REGISTRYINDEX, LuaStatic.CACHE_CSHARP_OBJECT_TABLE);

            // Insert our loader FIRST
            loaderFunction = new LuaCSFunction(loader);
            LuaDLL.lua_pushstdcallcfunction(L, loaderFunction);
            int loaderFunc = LuaDLL.lua_gettop(L);

            LuaDLL.lua_getfield(L, LuaIndexes.LUA_GLOBALSINDEX, "package");
            LuaDLL.lua_getfield(L, -1, "loaders");
            int loaderTable = LuaDLL.lua_gettop(L);

            // Shift table elements right
            for (int e = LuaDLL.luaL_getn(L, loaderTable) + 1; e > 1; e--) {
                LuaDLL.lua_rawgeti(L, loaderTable, e - 1);
                LuaDLL.lua_rawseti(L, loaderTable, e);
            }
            LuaDLL.lua_pushvalue(L, loaderFunc);
            LuaDLL.lua_rawseti(L, loaderTable, 1);
            LuaDLL.lua_settop(L, 0);
        }

        public void Close() {
            if (L != IntPtr.Zero) {
                LuaDLL.lua_close(L);
            }
        }

        #region dofile
        public object[] DoString(string chunk, string chunkName) {
            int oldTop = LuaDLL.lua_gettop(L);
            byte[] data = Encoding.UTF8.GetBytes(chunk);
            if (LuaDLL.luaL_loadbuffer(L, data, data.Length, chunkName) == 0) {
                if (LuaDLL.lua_pcall(L, 0, -1, 0) == 0) {
                    return LuaStatic.PopValues(L, oldTop);
                }
                else {
                    ThrowExceptionFromError(oldTop);
                }

            }
            else {
                ThrowExceptionFromError(oldTop);
            }
            return null;            // Never reached - keeps compiler happy
        }
        public object[] DoFile(string fileName) {
            return DoFile(fileName, false);
        }
        public object[] DoFile(byte[] data, string fileName) {
            LuaDLL.lua_pushstdcallcfunction(L, tracebackFunction);
            int oldTop = LuaDLL.lua_gettop(L);

            if (LuaDLL.luaL_loadbuffer(L, data, data.Length, fileName) == 0) {
                if (LuaDLL.lua_pcall(L, 0, -1, -2) == 0) {
                    object[] results = LuaStatic.PopValues(L, oldTop);
                    LuaDLL.lua_pop(L, 1);
                    return results;
                }
                else {
                    ThrowExceptionFromError(oldTop);
                }
            }
            else {
                ThrowExceptionFromError(oldTop);
            }

            return null;            // Never reached - keeps compiler happy
        }
        public object[] DoFile(string fileName, bool isLoad) {
            byte[] data = { };
            if (isLoad) {
                // Load with Unity3D resources
                TextAsset file = (TextAsset)Resources.Load(fileName);
                if (file == null) {
                    return null;
                }
                data = file.bytes;
            }
            else {
                using (Stream s = new FileStream(Application.streamingAssetsPath + "/" + fileName, FileMode.Open, FileAccess.ReadWrite)) {
                    data = new byte[s.Length];
                    s.Read(data, 0, data.Length);
                    s.Close();
                }
            }
            return DoFile(data, fileName);
        }
        #endregion

        #region call function
        public object[] CallGlobalFunction(string functionName, object[] args) {
            return CallGlobalFunction(functionName, args, 0);
        }
        public object[] CallGlobalFunction(string functionName, object[] args, int returnNum) {
            if (!LuaDLL.lua_checkstack(L, args.Length + 6)) {
                LuaStatic.traceback(L, "Lua stack overflow");
                return null;
            }
            int oldTop = LuaDLL.lua_gettop(L);
            LuaDLL.lua_getglobal(L, functionName);

            object[] ret = CallLuaFunction(args, returnNum, false);
            LuaDLL.lua_settop(L, oldTop);
            return ret;
        }
        public object[] CallTableFunction(string tableName, string functionName, object[] args) {
            return CallTableFunction(tableName, functionName, args, 0);
        }
        public object[] CallTableFunction(string tableName, string functionName, object[] args, int returnNum) {
            if (!LuaDLL.lua_checkstack(L, args.Length + 6)) {
                LuaStatic.print("Lua stack overflow");
                return null;
            }
            int oldTop = LuaDLL.lua_gettop(L);
            LuaDLL.lua_getglobal(L, tableName);
            if (LuaDLL.lua_type(L, -1) != LuaTypes.LUA_TTABLE) {
                LuaDLL.lua_pop(L, 1);
                LuaStatic.print("Not a Lua table");
                return null;
            }
            LuaDLL.lua_getfield(L, -1, functionName);
            object[] ret = CallLuaFunction(args, returnNum, true);
            LuaDLL.lua_settop(L, oldTop);
            return ret;
        }
        private object[] CallLuaFunction(object[] args, int returnNum, bool isTable) {
            int nArgs = 0;
            int oldTop = LuaDLL.lua_gettop(L) - 1;

            if (LuaDLL.lua_type(L, -1) != LuaTypes.LUA_TFUNCTION) {
                LuaDLL.lua_pop(L, 1);
                LuaStatic.print("Not a Lua function");
                return null;
            }
            if (args != null) {
                nArgs = args.Length;
                if (isTable) {
                    LuaDLL.lua_pushvalue(L, -2);
                    nArgs++;
                }
                for (int i = 0; i < args.Length; i++) {
                    LuaStatic.PushValue(L, args[i]);
                }
            }
            int error = LuaDLL.lua_pcall(L, nArgs, returnNum, 0);
            if (error != 0) {
                LuaStatic.traceback(L);
                return null;
            }

            return LuaStatic.PopValues(L, oldTop);
        }
        #endregion

        #region lua function
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        private static int panic(IntPtr L) {
            string reason = String.Format("unprotected error in call to Lua API ({0})", LuaDLL.lua_tostring(L, -1));
            LuaStatic.traceback(L, reason);
            return 1;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        private static int error_traceback(IntPtr L) {
            LuaDLL.lua_getglobal(L, "debug");
            LuaDLL.lua_getfield(L, -1, "traceback");
            LuaDLL.lua_pushvalue(L, 1);
            LuaDLL.lua_pushnumber(L, 2);
            LuaDLL.lua_call(L, 2, 1);
            return 1;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        private static int print(IntPtr L) {
            // For each argument we'll 'tostring' it
            int n = LuaDLL.lua_gettop(L);
            string s = String.Empty;

            LuaDLL.lua_getglobal(L, "tostring");

            for (int i = 1; i <= n; i++) {
                LuaDLL.lua_pushvalue(L, -1);  /* function to be called */
                LuaDLL.lua_pushvalue(L, i);   /* value to print */
                LuaDLL.lua_call(L, 1, 1);
                s += LuaDLL.lua_tostring(L, -1);

                if (i > 1) {
                    s += "\t";
                }

                LuaDLL.lua_pop(L, 1);  /* pop result */
                LuaStatic.print(s);
            }
            return 0;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        private static int loader(IntPtr L) {
            // Get script to load
            string fileName = String.Empty;
            fileName = LuaDLL.lua_tostring(L, 1);
            fileName = fileName.Replace('.', '/');
            fileName = "lua/" + fileName + ".lua";

            // test code just window
            byte[] data = { };
            using (Stream s = new FileStream(Application.streamingAssetsPath + "/" + fileName, FileMode.Open, FileAccess.ReadWrite)) {
                data = new byte[s.Length];
                s.Read(data, 0, data.Length);
                s.Close();
            }

            LuaDLL.luaL_loadbuffer(L, data, data.Length, fileName);

            return 1;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        private static int dofile(IntPtr L) {
            // Get script to load
            string fileName = String.Empty;
            fileName = LuaDLL.lua_tostring(L, 1);
            fileName.Replace('.', '/');
            fileName = "lua/" + fileName + ".lua";

            int n = LuaDLL.lua_gettop(L);

            // test code just window
            byte[] data = { };
            using (Stream s = new FileStream(Application.streamingAssetsPath + "/" + fileName, FileMode.Open, FileAccess.ReadWrite)) {
                data = new byte[s.Length];
                s.Read(data, 0, data.Length);
                s.Close();
            }

            if (LuaDLL.luaL_loadbuffer(L, data, data.Length, fileName) == 0) {
                LuaDLL.lua_call(L, 0, LuaDLL.LUA_MULTRET);
            }

            return LuaDLL.lua_gettop(L) - n;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        private static int loadfile(IntPtr L) {
            return loader(L);
        }
        #endregion

        #region IDisposable Members
        internal void dispose(int reference) {
            if (L != IntPtr.Zero) //Fix submitted by Qingrui Li
                LuaDLL.lua_unref(L, reference);
        }
        public void Dispose() {
            Dispose(true);

            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
        }

        public virtual void Dispose(bool dispose) {
            if (dispose) {
                Close();
            }
        }

        #endregion

        internal void ThrowExceptionFromError(int oldTop) {
            object err = LuaStatic.GetObj(L, -1);
            LuaDLL.lua_settop(L, oldTop);

            // A pre-wrapped exception - just rethrow it (stack trace of InnerException will be preserved)
            Exception luaEx = err as Exception;
            if (luaEx != null) throw luaEx;

            // A non-wrapped Lua error (best interpreted as a string) - wrap it and throw it
            if (err == null) err = "Unknown Lua Error";
            throw new Exception(err.ToString());
        }
    }
}
