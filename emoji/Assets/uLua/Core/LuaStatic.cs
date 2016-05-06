namespace LuaInterface {
    using System;
    using System.IO;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Reflection;
    using System.Threading;
    using System.Text;
    using UnityEngine;
    using System.Runtime.InteropServices;

    public class LuaStatic {

        #region member
        public static readonly Dictionary<long, object> objs = new Dictionary<long, object>();
        public static readonly Dictionary<object, int> objBack = new Dictionary<object, int>();
        public static readonly Dictionary<string, Type> ExportTypeDict = new Dictionary<string, Type>();
        public static int objsRefId = 0;
        public const string CACHE_CSHARP_OBJECT_TABLE = "objectCache";
        #endregion

        #region public
        public static void AddTypeDict(Type type) {
            if (ExportTypeDict.ContainsKey(type.Name)) {
                return;
            }
            ExportTypeDict[type.Name] = type;
        }
        public static Type GetType(object typeData) {
            Type result;
            string typeName = typeData.ToString();
            if (ExportTypeDict.ContainsKey(typeName)) {
                result = ExportTypeDict[typeName];
            }
            else {
                result = Type.GetType(typeName);
                AddTypeDict(result);
            }
            return result;
        }
        public static object[] PopValues(IntPtr luaState, int oldTop) {
            int newTop = LuaDLL.lua_gettop(luaState);
            if (oldTop >= newTop) {
                return null;
            }
            else {
                ArrayList returnValues = new ArrayList();
                for (int i = oldTop + 1; i <= newTop; i++) {
                    returnValues.Add(GetObj(luaState, i));
                }
                LuaDLL.lua_settop(luaState, oldTop);
                return returnValues.ToArray();
            }
        }
        public static void PushValue(IntPtr luaState, object o) {
            Type t = o.GetType();
            if (t == typeof(int) || t == typeof(int).MakeByRefType()) {
                LuaDLL.lua_pushnumber(luaState, Convert.ToDouble(o));
            }
            else if (t == typeof(bool) || t == typeof(bool).MakeByRefType()) {
                LuaDLL.lua_pushboolean(luaState, (bool)o);
            }
            else if (t == typeof(string) || t == typeof(string).MakeByRefType()) {
                LuaDLL.lua_pushstring(luaState, (string)o);
            }
            else if (t == typeof(IntPtr)) {
                LuaDLL.lua_pushlightuserdata(luaState, (IntPtr)o);
            }
            else if (t.IsPrimitive && !t.IsEnum) {
                LuaDLL.lua_pushnumber(luaState, Convert.ToDouble(o));
            }
            else if (t.IsArray) {
                IEnumerable objs = (IEnumerable)o;
                LuaDLL.lua_newtable(luaState);
                int num2 = 0;
                foreach (var item in objs) {
                    PushValue(luaState, item);
                    LuaDLL.lua_pushnumber(luaState, (double)(++num2));
                    LuaDLL.lua_insert(luaState, -2);
                    LuaDLL.lua_settable(luaState, -3);
                }
            }
            else if (t == typeof(Rect)) {
                LuaDLL.lua_pushrect(luaState, (Rect)o);
            }
            else if (t == typeof(Vector2)) {
                LuaDLL.lua_pushvector2(luaState, (Vector2)o);
            }
            else if (t == typeof(Vector3)) {
                LuaDLL.lua_pushvector3(luaState, (Vector3)o);
            }
            else if (t == typeof(Vector4)) {
                LuaDLL.lua_pushvector4(luaState, (Vector4)o);
            }
            else {
                LuaStatic.addGameObject2Lua(luaState, o, t.Name);
            }
        }
        public static object GetObj(IntPtr luaState, int index) {
            switch (LuaDLL.lua_type(luaState, index)) {
                case LuaTypes.LUA_TNIL:
                    return null;
                case LuaTypes.LUA_TBOOLEAN:
                    return LuaDLL.lua_toboolean(luaState, index);
                case LuaTypes.LUA_TLIGHTUSERDATA:
                    return LuaDLL.lua_touserdata(luaState, index);
                case LuaTypes.LUA_TNUMBER:
                    return LuaDLL.lua_tonumber(luaState, index);
                case LuaTypes.LUA_TSTRING:
                    return LuaDLL.lua_tostring(luaState, index);
                case LuaTypes.LUA_TTABLE:
                    return ToStruct(luaState, index);
                case LuaTypes.LUA_TUSERDATA: {
                        object obj2;
                        IntPtr ptr = LuaDLL.lua_touserdata(luaState, index);
                        if (!LuaStatic.objs.TryGetValue(ptr.ToInt64(), out obj2)) {
                            traceback(luaState, "GetObj: obj not exist");
                            LuaDLL.lua_error(luaState);
                            return null;
                        }
                        return obj2;
                    }
            }
            traceback(luaState, "GetObj: wrong value type: " + LuaDLL.luaL_typename(luaState, index));
            LuaDLL.lua_error(luaState);
            return null;
        }
        public static bool CheckType(IntPtr L, Type t, int pos) {
            //默认都可以转 object
            if (t == typeof(object)) {
                return true;
            }
            LuaTypes luaType = LuaDLL.lua_type(L, pos);
            switch (luaType) {
                case LuaTypes.LUA_TNUMBER:
                    return t.IsPrimitive || t.IsEnum;
                case LuaTypes.LUA_TSTRING:
                    return t == typeof(string) || t == typeof(byte[]) || t == typeof(char[]) || t == typeof(Type);
                case LuaTypes.LUA_TUSERDATA:
                    IntPtr ptr = LuaDLL.lua_touserdata(L, pos);
                    return t == typeof(object) || objs.ContainsKey(ptr.ToInt64());
                case LuaTypes.LUA_TBOOLEAN:
                    return t == typeof(bool);
                case LuaTypes.LUA_TTABLE:
                    return lua_isusertable(L, t, pos);
                case LuaTypes.LUA_TFUNCTION:
                    return true;
                //return t == typeof(LuaFunction);
                case LuaTypes.LUA_TLIGHTUSERDATA:
                    return t == typeof(IntPtr);
                case LuaTypes.LUA_TNIL:
                    return t == null || t.IsEnum || !t.IsValueType;
                default:
                    break;
            }
            traceback(L, "undefined type to check" + LuaDLL.luaL_typename(L, pos));
            return false;
        }
        public static int addGameObject2Lua(IntPtr L, object obj, string metatable) {
            if (obj == null) {
                LuaDLL.lua_pushnil(L);
                return 1;
            }
            if (objBack.ContainsKey(obj)) {
                int index = objBack[obj];
                return GetUserDataFromCache(L, index);
            }
            IntPtr ptr = LuaDLL.lua_newuserdata(L, 1);
            CacheUserData(L, LuaDLL.lua_gettop(L));

            Marshal.WriteByte(ptr, 1);
            long ptrKey = ptr.ToInt64();
            objs[ptrKey] = obj;
            objBack[obj] = objsRefId++;
            LuaDLL.lua_getglobal(L, metatable);
            if (LuaDLL.lua_isnil(L, -1)) {
                LuaDLL.lua_pop(L, 1);
            }
            else {
                LuaDLL.lua_setmetatable(L, -2);
            }
            return 1;
        }
        public static int GameObjectGC(IntPtr L) {
            RemoveGameObjectOnGC(LuaDLL.lua_touserdata(L, 1));
            return 0;
        }
        public static int traceback(IntPtr L, string err = "Lua traceback:") {
            LuaDLL.lua_getglobal(L, "debug");
            LuaDLL.lua_getfield(L, -1, "traceback");
            LuaDLL.lua_remove(L, -2);
            LuaDLL.lua_pushstring(L, err);
            LuaDLL.lua_pushnumber(L, 1.0);
            LuaDLL.lua_call(L, 2, 1);
            return 1;
        }
        public static void print(object message) {
            Debug.Log("[LUA]: " + message.ToString());
        }
        #endregion

        #region cache
        private static int GetUserDataFromCache(IntPtr L, int index) {
            int result = 1;
            LuaDLL.lua_getfield(L, LuaIndexes.LUA_REGISTRYINDEX, CACHE_CSHARP_OBJECT_TABLE);
            LuaDLL.lua_pushnumber(L, index);
            LuaDLL.lua_gettable(L, -2);
            return result;
        }
        private static int CacheUserData(IntPtr L, int lo) {
            int result = 1;
            LuaDLL.lua_getfield(L, LuaIndexes.LUA_REGISTRYINDEX, CACHE_CSHARP_OBJECT_TABLE);
            LuaDLL.lua_pushnumber(L, objsRefId);
            LuaDLL.lua_pushvalue(L, lo);
            LuaDLL.lua_settable(L, -3);
            LuaDLL.lua_pop(L, 1);
            return result;
        }
        #endregion

        #region value type
        private static bool lua_isusertable(IntPtr L, Type t, int pos) {
            if (t.IsArray) {
                return true;
            }
            else if (t.IsValueType) {
                if (t == typeof(Rect)) {
                    return LuaDLL.lua_metatableequal(L, pos, "Rect");
                }
                else if (t == typeof(Vector2)) {
                    return LuaDLL.lua_metatableequal(L, pos, "Vector2");
                }
                else if (t == typeof(Vector3)) {
                    return LuaDLL.lua_metatableequal(L, pos, "Vector3");
                }
                else if (t == typeof(Vector4)) {
                    return LuaDLL.lua_metatableequal(L, pos, "Vector4");
                }
            }
            return false;
        }
        private static object ToStruct(IntPtr luaState, int index) {
            if (LuaDLL.lua_metatableequal(luaState, index, "Vector3")) {
                return LuaDLL.lua_tovector3(luaState, index);
            }
            if (LuaDLL.lua_metatableequal(luaState, index, "Vector2")) {
                return LuaDLL.lua_tovector2(luaState, index);
            }
            if (LuaDLL.lua_metatableequal(luaState, index, "Vector4")) {
                return LuaDLL.lua_tovector4(luaState, index);
            }
            if (LuaDLL.lua_metatableequal(luaState, index, "Rect")) {
                return LuaDLL.lua_torect(luaState, index);
            }
            return null;
        }
        #endregion

        #region gc
        private static void RemoveGameObjectOnGC(IntPtr idptr) {
            RemoveGameObject(idptr);
        }
        private static void RemoveGameObject(IntPtr idptr) {
            long key = idptr.ToInt64();
            if (objs.ContainsKey(key)) {
                objBack.Remove(objs[key]);
                objs.Remove(key);
            }
        }
        #endregion
    }
}
