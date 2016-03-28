namespace LuaInterface
{
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
	
	public class LuaStatic
	{
		public static readonly Dictionary<long, object> objs = new Dictionary<long, object>();
		public static readonly Dictionary<object, int> objBack = new Dictionary<object, int>();
		public static int objsRefId = 0;
		public const string ObjectCacheTable = "objectCache";
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
		private static int GetUserDataFromCache(IntPtr L, int index) {
			int result = 1;
			LuaDLL.lua_getfield(L, LuaIndexes.LUA_REGISTRYINDEX, ObjectCacheTable);
			LuaDLL.lua_pushnumber(L, index);
			LuaDLL.lua_gettable(L, -2);
			return result;
		}
		private static int CacheUserData(IntPtr L, int lo) {
			int result = 1;
			LuaDLL.lua_getfield(L, LuaIndexes.LUA_REGISTRYINDEX, ObjectCacheTable);
			LuaDLL.lua_pushnumber(L, objsRefId);
			LuaDLL.lua_pushvalue(L, lo);
			LuaDLL.lua_settable(L, -3);
			LuaDLL.lua_pop(L, 1);
			return result;
		}
		public static int GameObjectGC(IntPtr L) {
			RemoveGameObjectOnGC(LuaDLL.lua_touserdata(L, 1));
			return 0;
		}
		public static void RemoveGameObjectOnGC(IntPtr idptr) {
			RemoveGameObject(idptr);
		}
		public static void RemoveGameObject(IntPtr idptr) {
			long key = idptr.ToInt64();
			if (objs.ContainsKey(key)) {
				objs.Remove(key);
				objBack.Remove(objs[key]);
			}
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

		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
		public static int panic(IntPtr L)
		{
			string reason = String.Format("unprotected error in call to Lua API ({0})", LuaDLL.lua_tostring(L, -1));
			throw new LuaException(reason);
		}
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
		public static int error_traceback(IntPtr L)
		{
			LuaDLL.lua_getglobal(L,"debug");
			LuaDLL.lua_getfield(L,-1,"traceback");
			LuaDLL.lua_pushvalue(L,1);
			LuaDLL.lua_pushnumber(L,2);
			LuaDLL.lua_call (L,2,1);
			return 1;
		}
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
		public static int print(IntPtr L)
		{
			// For each argument we'll 'tostring' it
			int n = LuaDLL.lua_gettop(L);
			string s = String.Empty;
			
			LuaDLL.lua_getglobal(L, "tostring");
			
			for( int i = 1; i <= n; i++ ) 
			{
				LuaDLL.lua_pushvalue(L, -1);  /* function to be called */
				LuaDLL.lua_pushvalue(L, i);   /* value to print */
				LuaDLL.lua_call(L, 1, 1);
				s += LuaDLL.lua_tostring(L, -1);
				
				if( i > 1 ) 
				{
					s += "\t";
				}
				
				LuaDLL.lua_pop(L, 1);  /* pop result */
				
				Debug.Log("LUA: " + s);
			}
			return 0;
		}
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
		public static int loader(IntPtr L)
		{
			// Get script to load
			string fileName = String.Empty;
			fileName = LuaDLL.lua_tostring(L, 1);
			fileName = fileName.Replace('.', '/');
			fileName += ".lua";
			
			// Load with Unity3D resources
			TextAsset file = (TextAsset)Resources.Load(fileName);
			if( file == null )
			{
				return 0;
			}
			
			LuaDLL.luaL_loadbuffer(L, file.text, Encoding.UTF8.GetByteCount(file.text), fileName);
			
			return 1;
		}
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
		public static int dofile(IntPtr L)
		{
			// Get script to load
			string fileName = String.Empty;
			fileName = LuaDLL.lua_tostring(L, 1);
			fileName.Replace('.', '/');
			fileName += ".lua";
			
			int n = LuaDLL.lua_gettop(L);
			
			// Load with Unity3D resources
			TextAsset file = (TextAsset)Resources.Load(fileName);
			if( file == null )
			{
				return LuaDLL.lua_gettop(L) - n;
			}
			
			if( LuaDLL.luaL_loadbuffer(L, file.text, Encoding.UTF8.GetByteCount(file.text), fileName) == 0 )
			{
				LuaDLL.lua_call(L, 0, LuaDLL.LUA_MULTRET);
			}
			
			return LuaDLL.lua_gettop(L) - n;
		}
		
		[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
		public static int loadfile(IntPtr L)
		{
			return loader(L);
		}
		
		public static string init_luanet =
			@"local metatable = {}
            local rawget = rawget
            local import_type = luanet.import_type
            local load_assembly = luanet.load_assembly
            luanet.error, luanet.type = error, type
            -- Lookup a .NET identifier component.
            function metatable:__index(key) -- key is e.g. 'Form'
            -- Get the fully-qualified name, e.g. 'System.Windows.Forms.Form'
            local fqn = rawget(self,'.fqn')
            fqn = ((fqn and fqn .. '.') or '') .. key

            -- Try to find either a luanet function or a CLR type
            local obj = rawget(luanet,key) or import_type(fqn)

            -- If key is neither a luanet function or a CLR type, then it is simply
            -- an identifier component.
            if obj == nil then
                -- It might be an assembly, so we load it too.
                    pcall(load_assembly,fqn)
                    obj = { ['.fqn'] = fqn }
            setmetatable(obj, metatable)
            end

            -- Cache this lookup
            rawset(self, key, obj)
            return obj
            end

            -- A non-type has been called; e.g. foo = System.Foo()
            function metatable:__call(...)
            error('No such type: ' .. rawget(self,'.fqn'), 2)
            end

            -- This is the root of the .NET namespace
            luanet['.fqn'] = false
            setmetatable(luanet, metatable)

            -- Preload the mscorlib assembly
            luanet.load_assembly('mscorlib')";
	}
}
