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
        ###       ###        ###              )
         ##       ###        ###               
__________#_______####_______####______________

                我们的未来没有BUG              
* ==============================================================================
* Filename: LuaApiMaker
* Created:  2016/4/5 12:38:24
* Author:   HaYaShi ToShiTaKa
* Purpose:  
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using LuaInterface;
using System.Text;

static public class LuaApiMaker {

    #region member
    public static string saveDir = Application.dataPath + "/Game/Script/LuaWrap/Generate/";
    static BindingFlags binding = BindingFlags.Public | BindingFlags.Static | BindingFlags.IgnoreCase;
    static List<Type> typeList = new List<Type> { 
            typeof(UnityEngine.Object),
            typeof(Transform),
            typeof(GameObject),
            typeof(Component),
            typeof(MonoBehaviour),
            typeof(Resources),
            typeof(UIButton),
            typeof(UILabel),
            typeof(Director),
            typeof(Scheduler),
            typeof(UIManager),
    };

    static List<string> usingList = new List<string> {
			"System.Collections.Generic","System","System.Collections",
		};
    static Dictionary<string, int> nameCounter = new Dictionary<string, int>();

    static List<ConstructorInfo> constructs = new List<ConstructorInfo>();
    static List<MethodInfo> methods = new List<MethodInfo>();
    static List<PropertyInfo> propertys = new List<PropertyInfo>();
    static List<FieldInfo> fields = new List<FieldInfo>();

    static string argsText;
    #endregion

    #region gen wrap

    [MenuItem("Lua/Gen Lua Wrap Files", false, 1)]
    public static void GenerateClassWraps() {
        if (EditorApplication.isCompiling) {
            EditorUtility.DisplayDialog("警告", "请等待编辑器完成编译在执行此功能", "确定");
            return;
        }

        if (!File.Exists(saveDir)) {
            Directory.CreateDirectory(saveDir);
        }
        BindingFlags bindType = BindingFlags.DeclaredOnly |
            BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public;

        foreach (Type type in typeList) {

            string name = type.Name;

            StringBuilder sb = new StringBuilder();

            constructs.Clear();
            methods.Clear();
            propertys.Clear();
            fields.Clear();
            nameCounter.Clear();
            if (!type.IsValueType) {
                constructs.AddRange(type.GetConstructors(bindType));
            }
            methods.AddRange(type.GetMethods(bindType));
            propertys.AddRange(type.GetProperties(bindType));
            fields.AddRange(type.GetFields(bindType));

            for (int i = constructs.Count - 1; i >= 0; --i) {
                //扔掉 unity3d 废弃的函数                
                if (IsObsolete(constructs[i])) {
                    constructs.RemoveAt(i);
                }
                if (typeof(MonoBehaviour).IsAssignableFrom(type)) {
                    constructs.RemoveAt(i);
                }
            }
            for (int i = methods.Count - 1; i >= 0; --i) {
                //去掉操作符函数
                if (methods[i].Name.Contains("op_") || methods[i].Name.Contains("add_") || methods[i].Name.Contains("remove_")
                    || methods[i].Name.Contains("set_") || methods[i].Name.Contains("get_") || methods[i].IsGenericMethod
                    ) {
                    methods.RemoveAt(i);
                    continue;
                }

                //扔掉 unity3d 废弃的函数                
                if (IsObsolete(methods[i])) {
                    methods.RemoveAt(i);
                }
            }
            for (int i = propertys.Count - 1; i >= 0; --i) {
                //扔掉 unity3d 废弃的函数                
                if (IsObsolete(propertys[i])) {
                    propertys.RemoveAt(i);
                }
            }
            for (int i = fields.Count - 1; i >= 0; --i) {
                //扔掉 unity3d 废弃的函数                
                if (IsObsolete(fields[i])) {
                    fields.RemoveAt(i);
                }
            }

            sb.AppendFormat("    public class Lua{0}", name);
            sb.Append(" {");
            sb.Append("\r\n");

            GenRegister(sb, type);
            nameCounter.Clear();

            if (!type.IsValueType) {
                GenConstructs(sb);
            }
            
            GenFunctions(sb);
            GenPropertys(sb);
            GenFields(sb);

            sb.Append("    }\r\n");
            SaveFile(saveDir + "Lua" + name + ".cs", sb, name);

            EditorApplication.isPlaying = false;
            Debug.Log("Generate lua binding files over");
            AssetDatabase.Refresh();
        }
        GenRegiserClass();
        EditorApplication.isPlaying = false;
        AssetDatabase.Refresh();
    }

    #region gen list
    static void GenConstructs(StringBuilder sb) {
        for (int i = 0; i < constructs.Count; i++) {
            ConstructorInfo c = constructs[i];
            GenConstruct(c, sb);
            break;
        }
    }
    static void GenFunctions(StringBuilder sb) {
        for (int i = 0; i < methods.Count; i++) {
            MethodInfo m = methods[i];
            int count = 1;
            if (m.IsGenericMethod) {
                continue;
            }

            if (!nameCounter.TryGetValue(m.Name, out count)) {
                nameCounter[m.Name] = 1;
            }
            else {
                nameCounter[m.Name] = count + 1;
            }
            if (nameCounter[m.Name] > 1) {
                continue;
            }
            GenFunction(m, sb);
        }
    }
    static void GenPropertys(StringBuilder sb) {
        for (int i = 0; i < propertys.Count; i++) {
            PropertyInfo p = propertys[i];
            GenProperty(p, sb);
        }
    }
    static void GenFields(StringBuilder sb) {
        for (int i = 0; i < fields.Count; i++) {
            FieldInfo f = fields[i];
            GenField(f, sb);
        }
    }
    #endregion

    static void SaveFile(string file, StringBuilder sb, string name) {
        var utf8WithoutBom = new System.Text.UTF8Encoding(false);
        using (StreamWriter textWriter = new StreamWriter(file, false, utf8WithoutBom)) {
            StringBuilder usb = new StringBuilder();
            usb.AppendFormat(@"/*
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
* Filename: Lua{0}
* Created:  {1}
* Author:   HaYaShi ToShiTaKa and tolua#
* Purpose:  {0}的lua导出类,本类由插件自动生成
* ==============================================================================
*/
", name, DateTime.Now.ToString());

            usb.Append("namespace LuaInterface {\r\n");

            foreach (string str in usingList) {
                usb.AppendFormat("    using {0};\r\n", str);
            }
            usb.Append("\r\n");

            textWriter.Write(usb.ToString());
            textWriter.Write(sb.ToString());
            textWriter.Write("\r\n}");
            textWriter.Flush();
            textWriter.Close();
        }
    }
    static void GenRegiserClass() {
        var utf8WithoutBom = new System.Text.UTF8Encoding(false);
        using (StreamWriter textWriter = new StreamWriter(saveDir + "LuaRegister.cs", false, utf8WithoutBom)) {
            StringBuilder usb = new StringBuilder();
            usb.AppendFormat(@"/*
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
* Filename: LuaRegister
* Created:  {0}
* Author:   HaYaShi ToShiTaKa
* Purpose:  Lua导出类注册的地方,本类由插件自动生成
* ==============================================================================
*/
", DateTime.Now.ToString());

            usb.Append("namespace LuaInterface {\r\n");

            foreach (string str in usingList) {
                usb.AppendFormat("    using {0};\r\n", str);
            }
            usb.Append("\r\n");
            StringBuilder sb = new StringBuilder();
            sb.Append(@"
    public class LuaRegister {
        public static void Register(IntPtr L) {
");
            for (int i = 0; i < typeList.Count; i++) {
                sb.AppendFormat("            Lua{0}.Register(L);\r\n", typeList[i].Name);
            }
            sb.Append("        }\r\n");
            textWriter.Write(usb.ToString());
            textWriter.Write(sb.ToString());
            textWriter.Write("    }\r\n}");
            textWriter.Flush();
            textWriter.Close();
        }
    }

    #region gen functions
    static void GenConstruct(ConstructorInfo c, StringBuilder sb) {
        AppendFunctionHead("New", sb);

        ParameterInfo[] paramInfos = c.GetParameters();
        argsText = c.ReflectedType.Name + "." + c.Name + "(";
        CheckArgsCount(c, sb);
        AppendFunctionEnd(sb);
    }
    static void GenProperty(PropertyInfo p, StringBuilder sb) {

        //先注册get方法
        MethodInfo m = p.GetGetMethod();
        AppendFunctionHead(m, sb);
        string text = m.ReflectedType.Name + "." + m.Name + "(";
        string name = "";
        int count = m.IsStatic ? 0 : 1;
        if (m.IsStatic) {
            text = TrimNameSpace(p.ReflectedType) + ".";
        }
        else {
            text = "obj.";
        }
        if (count > 0) {
            text += p.Name;
            CheckArgsCount(m, count, sb, true);
        }

        text += ";\r\n";
        sb.Append("\r\n            ");

        text = text.Remove(text.Length - 3, 3);
        GenPushStr(m.ReturnType, text, sb, null);
        AppendFunctionEnd(sb);

        //注册set方法
        m = p.GetSetMethod();
        if (m == null) return;
        AppendFunctionHead(m, sb);
        ParameterInfo[] paramInfos = m.GetParameters();
        text = m.ReflectedType.Name + ".";
        count = m.IsStatic ? 1 : 2;
        if (m.IsStatic) {
            text = "\r\n            " + TrimNameSpace(m.ReturnType) + ".";
        }
        else {
            text = "\r\n            " + "obj.";
        }
        name = string.Format("({0})", TrimNameSpace(paramInfos[0].ParameterType));

        if (paramInfos[0].ParameterType.IsPrimitive && paramInfos[0].ParameterType != typeof(bool)) {
            name += "(double)";
        }
        if (count > 0) {
            if (!m.IsStatic) {
                CheckArgsCount(m, count, sb, true);
                text += string.Format("{0} = {1}LuaStatic.GetObj(L, {2});\r\n", p.Name, name, 2);
            }
            else {
                text += string.Format("{0} = {1}LuaStatic.GetObj(L, {2});\r\n", p.Name, name, 1);
            }
        }

        sb.Append("\r\n            ");
        sb.Append(text);

        AppendFunctionEnd(sb);
    }
    static void GenField(FieldInfo p, StringBuilder sb) {

        //先注册get方法
        AppendFunctionHead("get_" + p.Name, sb);
        string text = string.Empty;
        int count = p.IsStatic ? 0 : 1;
        if (p.IsStatic) {
            text = p.ReflectedType.Name + "." + p.Name;
        }
        else {
            text = "obj.";
        }
        if (count > 0) {
            text += p.Name;
            CheckArgsCount(TrimNameSpace(p.ReflectedType), count, sb, true);
        }
        text += ";\r\n";
        sb.Append("\r\n            ");

        text = text.Remove(text.Length - 3, 3);
        GenPushStr(p.FieldType, text, sb, null);
        AppendFunctionEnd(sb);

        //注册set方法
        AppendFunctionHead("set_" + p.Name, sb);
        text = TrimNameSpace(p.ReflectedType) + "." + p.Name;
        count = p.IsStatic ? 1 : 2;
        if (p.IsStatic) {
            text = TrimNameSpace(p.ReflectedType) + ".";
        }
        else {
            text = "\r\n            " + "obj.";
        }
        string name = string.Format("({0})", TrimNameSpace(p.FieldType));

        if (p.FieldType.IsPrimitive && p.FieldType != typeof(bool)) {
            name += "(double)";
        }
        if (count > 0) {
            if (!p.IsStatic) {
                CheckArgsCount(TrimNameSpace(p.ReflectedType), count, sb, true);
                text += string.Format("{0} = {1}LuaStatic.GetObj(L, {2});\r\n", p.Name, name, 2);
            }
            else {
                text += string.Format("{0} = {1}LuaStatic.GetObj(L, {2});\r\n", p.Name, name, 1);
            }

        }

        sb.Append("\r\n            ");
        sb.Append(text);

        AppendFunctionEnd(sb);
    }
    static void GenFunction(MethodInfo m, StringBuilder sb) {
        AppendFunctionHead(m, sb);

        ParameterInfo[] paramInfos = m.GetParameters();
        argsText = m.ReflectedType.Name + "." + m.Name + "(";
        CheckArgsCount(m, sb);
        AppendFunctionEnd(sb);
    }

    #region append
    static void AppendFunctionHead(MemberInfo mb, StringBuilder sb) {
        AppendFunctionHead(mb.Name, sb);
    }
    static void AppendFunctionHead(string name, StringBuilder sb) {
        sb.Append("\r\n        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]\r\n");
        sb.AppendFormat("        public static int {0}(IntPtr L)", name);
        sb.Append(" {\r\n            int result = 1;\r\n");
    }
    static void AppendFunctionEnd(StringBuilder sb) {
        sb.Replace("\'", "\"");
        sb.Append("            return result;\r\n");
        sb.Append("        }\r\n");
    }
    static void CheckArgsCount(MemberInfo info, int count, StringBuilder sb, bool isProperty) {
        CheckArgsCount(TrimNameSpace(info.ReflectedType), count, sb, isProperty);
    }
    static void CheckArgsCount(string classType, int count, StringBuilder sb, bool isProperty) {
        sb.Append("            int count = LuaDLL.lua_gettop(L);\r\n");
        sb.AppendFormat(@"
            if (count != {0}){2}
            {1} obj = LuaStatic.GetObj(L, 1) as {1};", count, classType, @" {
                LuaStatic.traceback(L, 'count not enough');
                LuaDLL.lua_error(L);
                return result;
            }");
    }
    static void CheckArgsCount(MethodInfo info, StringBuilder sb) {
        List<MethodInfo> methodList = new List<MethodInfo>();
        ParameterInfo[] paramInfos;
        foreach (var method in methods) {
            if (info.Name == method.Name) {
                methodList.Add(method);
            }
        }
        int count;
        bool isReturn;
        sb.Append("            int count = LuaDLL.lua_gettop(L);\r\n");
        if (methodList.Count <= 1) {
            isReturn = info.ReturnType != typeof(void);
            paramInfos = info.GetParameters();
            count = paramInfos.Length + 1;
            if (!info.IsStatic) {
                sb.AppendFormat(@"
            if (count != {0}){2}
            {1} obj = LuaStatic.GetObj(L, 1) as {1};", count, TrimNameSpace(info.ReflectedType), @" {
                LuaStatic.traceback(L, 'count not enough');
                LuaDLL.lua_error(L);
                return result;
            }");
                argsText = "obj." + info.Name + "(";
            }
            else {
                argsText = TrimNameSpace(info.ReflectedType) + "." + info.Name + "(";
            }
            for (int i = 0; i < paramInfos.Length; i++) {
                AppendArgs("\r\n            ", sb, paramInfos, i);
                if (i != 0) {
                    argsText += ",";
                }
                if (paramInfos[i].IsOut) {
                    argsText += "out ";
                }
                else if (paramInfos[i].IsIn) {
                    argsText += "in ";
                }
                argsText += string.Format("arg{0}", i + 1);
            }
            sb.Append("\r\n            ");
            argsText += ");\r\n";
            if (isReturn) {
                argsText = argsText.Remove(argsText.Length - 3, 3);
                GenPushStr(info.ReturnType, argsText, sb, GenTypeObjectNameString(paramInfos));
            }
            else {
                sb.Append(argsText);
            }
            for (int i = 0; i < paramInfos.Length; i++) {
                if (paramInfos[i].IsOut) {
                    sb.Append("            ");
                    GenPushStr(paramInfos[i].ParameterType, string.Format("arg{0}", i + 1), sb, null);
                }
            }
        }
        else {
            for (int i = 0; i < methodList.Count; i++) {
                isReturn = methodList[i].ReturnType != typeof(void);
                paramInfos = methodList[i].GetParameters();
                count = paramInfos.Length + 1;
                string condition = "count == " + count;
                for (int j = 0; j < paramInfos.Length; j++) {
                    condition += " &&\r\n                LuaStatic.CheckType(L, typeof(" + TrimNameSpace(paramInfos[j].ParameterType) + "), " + (j + 2).ToString() + ")";
                }
                sb.AppendFormat("\r\n            if ({0})", condition);
                sb.Append(" {");
                if (!info.IsStatic) {
                    sb.AppendFormat("\r\n                {0} obj = LuaStatic.GetObj(L, 1) as {0};", TrimNameSpace(info.ReflectedType));
                    argsText = "obj." + info.Name + "(";
                }
                else {
                    argsText = TrimNameSpace(info.ReflectedType) + "." + info.Name + "(";
                }
                for (int j = 0; j < paramInfos.Length; j++) {
                    AppendArgs("\r\n                ", sb, paramInfos, j);
                    if (j != 0) {
                        argsText += ", ";
                    }
                    if (paramInfos[j].IsOut) {
                        argsText += "out ";
                    }
                    else if (paramInfos[j].IsIn) {
                        argsText += "in ";
                    }
                    argsText += string.Format("arg{0}", j + 1);
                }
                argsText += ");\r\n";
                sb.Append("\r\n                ");
                if (isReturn) {
                    argsText = argsText.Remove(argsText.Length - 3, 3);
                    GenPushStr(info.ReturnType, argsText, sb, true, GenTypeObjectNameString(paramInfos));
                }
                else {
                    sb.Append(argsText);
                }
                for (int j = 0; j < paramInfos.Length; j++) {
                    if (paramInfos[j].IsOut) {
                        sb.Append("                ");
                        GenPushStr(paramInfos[i].ParameterType, string.Format("arg{0}", j + 1), sb, true, null);
                    }
                }
                sb.Append("\r\n                return result;");
                sb.Append("\r\n            }");

                if (i == methodList.Count - 1) {
                    sb.Append("\r\n            LuaStatic.traceback(L, 'count not enough');");
                    sb.Append("\r\n            LuaDLL.lua_error(L);\r\n");
                }
            }
        }
    }
    static void CheckArgsCount(ConstructorInfo info, StringBuilder sb) {
        ParameterInfo[] paramInfos;
        int argCount;
        bool isReturn = true;
        sb.Append("            int count = LuaDLL.lua_gettop(L);\r\n");
        if (constructs.Count <= 1) {
            paramInfos = info.GetParameters();
            argCount = paramInfos.Length;
            argsText = "new " + TrimNameSpace(info.ReflectedType) + "(";

            for (int i = 0; i < paramInfos.Length; i++) {
                AppendArgs("\r\n            ", sb, paramInfos, i);
                if (i != 0) {
                    argsText += ",";
                }
                argsText += string.Format("arg{0}", i + 1);
            }
            sb.Append("\r\n            ");
            argsText += ");\r\n";
            if (isReturn) {
                argsText = argsText.Remove(argsText.Length - 3, 3);
                GenPushStr(info.ReflectedType, argsText, sb, GenTypeObjectNameString(paramInfos));
            }

        }
        else {
            for (int i = 0; i < constructs.Count; i++) {
                paramInfos = constructs[i].GetParameters();
                argCount = paramInfos.Length;
                string condition = "count == " + (argCount + 1);
                for (int j = 0; j < paramInfos.Length; j++) {
                    condition += " &&\r\n                LuaStatic.CheckType(L, typeof(" + TrimNameSpace(paramInfos[j].ParameterType) + "), " + (j + 2).ToString() + ")";
                }
                sb.AppendFormat("\r\n            if ({0})", condition);
                sb.Append(" {");
                argsText = "new " + TrimNameSpace(info.ReflectedType) + "(";

                for (int j = 0; j < paramInfos.Length; j++) {
                    AppendArgs("\r\n                ", sb, paramInfos, j);
                    if (j != 0) {
                        argsText += ", ";
                    }
                    argsText += string.Format("arg{0}", j + 1);
                }
                argsText += ");\r\n";
                sb.Append("\r\n                ");
                if (isReturn) {
                    argsText = argsText.Remove(argsText.Length - 3, 3);
                    GenPushStr(info.ReflectedType, argsText, sb, true, GenTypeObjectNameString(paramInfos));
                }

                sb.Append("\r\n                return result;");
                sb.Append("\r\n            }");

                if (i == constructs.Count - 1) {
                    sb.Append("\r\n            LuaStatic.traceback(L, 'count not enough');");
                    sb.Append("\r\n            LuaDLL.lua_error(L);\r\n");
                }


            }
        }
    }
    static void AppendArgs(string space, StringBuilder sb, ParameterInfo[] paramInfos, int index) {
        Type paramType = paramInfos[index].ParameterType;
        sb.Append(space);
        if (paramType.IsPrimitive && paramType != typeof(bool) || paramType.IsEnum) {
            sb.AppendFormat("{0} arg{1} = ({0})(double)(LuaStatic.GetObj(L, {2}));", TrimNameSpace(paramInfos[index].ParameterType), index + 1, index + 2);
        }
        else if (paramType == typeof(Type)) {
            sb.AppendFormat("object type{0} = LuaStatic.GetObj(L, {1});", index + 1, index + 2);
            sb.Append(space);
            sb.AppendFormat("{0} arg{1} = LuaStatic.GetType(type{1});", TrimNameSpace(paramInfos[index].ParameterType), index + 1);
        }
        else {
            sb.AppendFormat("{0} arg{1} = ({0})LuaStatic.GetObj(L, {2});", TrimNameSpace(paramInfos[index].ParameterType), index + 1, index + 2);
        }

    }
    static string GenTypeObjectNameString(ParameterInfo[] paramInfos) {
        if (paramInfos.Length <= 0) {
            return null;
        }
        if (paramInfos[paramInfos.Length - 1].ParameterType != typeof(Type)) {
            return null;
        }
        return string.Format("(string)type{0}", paramInfos.Length);
    }
    static bool CheckParamsIsRef(ParameterInfo[] param) {
        for (int i = 0; i < param.Length; i++) {
            if (param[i].IsOut || param[i].IsIn) {
                return true;
            }
        }
        return false;
    }
    #endregion

    static void GenPushStr(Type t, string arg, StringBuilder sb, string typeName) {
        GenPushStr(t, arg, sb, false, typeName);
    }
    static void GenPushStr(Type t, string arg, StringBuilder sb, bool isOver,string typeName) {
        if (t == typeof(int) || t == typeof(int).MakeByRefType()) {
            sb.AppendFormat("LuaDLL.lua_pushnumber(L, {0});\r\n", arg);
        }
        else if (t == typeof(bool) || t == typeof(bool).MakeByRefType()) {
            sb.AppendFormat("LuaDLL.lua_pushboolean(L, {0});\r\n", arg);
        }
        else if (t == typeof(string) || t == typeof(string).MakeByRefType()) {
            sb.AppendFormat("LuaDLL.lua_pushstring(L, {0});\r\n", arg);
        }
        else if (t == typeof(IntPtr)) {
            sb.AppendFormat("LuaDLL.lua_pushlightuserdata(L, {0});\r\n", arg);
        }
        else if (t.IsPrimitive && !t.IsEnum) {
            sb.AppendFormat("LuaDLL.lua_pushnumber(L, {0});\r\n", arg);
        }
        else if (t.IsArray) {
            string space = isOver ? "                " : "            ";
            sb.AppendFormat("IEnumerable objs = (IEnumerable){0};\r\n", arg);
            sb.Append(space + "LuaDLL.lua_newtable(L);\r\n");
            sb.Append(space + "int num2 = 0;\r\n");
            sb.Append(space + "foreach (var item in objs) {\r\n");
            sb.Append(space + "    ");
            GenPushStr(t.GetElementType(), string.Format("({0})item", TrimNameSpace(t.GetElementType())), sb, isOver, typeName);
            sb.Append(space + "    LuaDLL.lua_pushnumber(L, (double)(++num2));\r\n");
            sb.Append(space + "    LuaDLL.lua_insert(L, -2);\r\n");
            sb.Append(space + "    LuaDLL.lua_settable(L, -3);\r\n");
            sb.Append(space + "}\r\n");
        }
        else if (t == typeof(Rect)) {
            sb.AppendFormat("LuaDLL.lua_pushrect(L, {0});\r\n", arg);
        }
        else if (t == typeof(Vector2)) {
            sb.AppendFormat("LuaDLL.lua_pushvector2(L, {0});\r\n", arg);
        }
        else if (t == typeof(Vector3)) {
            sb.AppendFormat("LuaDLL.lua_pushvector3(L, {0});\r\n", arg);
        }
        else if (t == typeof(Vector4)) {
            sb.AppendFormat("LuaDLL.lua_pushvector4(L, {0});\r\n", arg);
        }
        else {
            sb.AppendFormat("LuaStatic.addGameObject2Lua(L, {0}, {1});\r\n", arg, string.IsNullOrEmpty(typeName) ? string.Format("'{0}'",t.Name) : typeName);
        }
    }

    #endregion

    #region gen regist function
    static void GenRegister(StringBuilder sb, Type classType) {
        nameCounter.Clear();
        AppendRegisterHead(sb, classType.Name);
        for (int i = 0; i < constructs.Count; i++) {
            RegisterConstruct(constructs[i], sb);
            break;
        }
        for (int i = 0; i < methods.Count; i++) {
            RegisterFunction(methods[i], sb);
        }
        for (int i = 0; i < propertys.Count; i++) {
            RegisterProperty(propertys[i], sb);
        }
        for (int i = 0; i < fields.Count; i++) {
            RegisterField(fields[i], sb);
        }

        AppendRegisterEnd(sb, classType);
    }
    static void RegisterConstruct(ConstructorInfo c, StringBuilder sb) {
        sb.AppendFormat("            LuaDLL.lua_pushstdcallcfunction(L, {0}.{1}, '{1}');\r\n", "Lua" + c.ReflectedType.Name, "New");
    }
    static void RegisterFunction(MethodInfo m, StringBuilder sb) {
        int count = 0;
        if (!nameCounter.TryGetValue(m.Name, out count)) {
            nameCounter[m.Name] = 1;
        }
        else {
            nameCounter[m.Name] = count + 1;
        }
        if (nameCounter[m.Name] > 1) {
            return;
        }
        sb.AppendFormat("            LuaDLL.lua_pushstdcallcfunction(L, {0}.{1}, '{1}');\r\n", "Lua" + m.ReflectedType.Name, m.Name);
    }
    static void RegisterProperty(PropertyInfo p, StringBuilder sb) {
        string getMethod = "Lua" + p.ReflectedType.Name + "." + p.GetGetMethod().Name;
        string setMethod = p.GetSetMethod() == null ? "null" : "Lua" + p.ReflectedType.Name + "." + p.GetSetMethod().Name;
        sb.AppendFormat("            LuaDLL.lua_pushcsharpproperty(L, '{0}', {1}, {2});\r\n", p.Name, getMethod, setMethod);
    }
    static void RegisterField(FieldInfo f, StringBuilder sb) {
        string getMethod = "Lua" + f.ReflectedType.Name + ".get_" + f.Name;
        string setMethod = "Lua" + f.ReflectedType.Name + ".set_" + f.Name;
        sb.AppendFormat("            LuaDLL.lua_pushcsharpproperty(L, '{0}', {1}, {2});\r\n", f.Name, getMethod, setMethod);
    }
    static void AppendRegisterHead(StringBuilder sb, string className) {
        sb.Append("        public static void Register(IntPtr L) {\r\n");
        sb.Append("            int oldTop = LuaDLL.lua_gettop(L);\r\n");
        sb.AppendFormat("            LuaDLL.lua_getglobal(L, '{0}');\r\n\r\n", className);
        sb.Append("            if (LuaDLL.lua_isnil(L, -1)) {\r\n");
        sb.Append("                LuaDLL.lua_pop(L, 1);\r\n");
        sb.Append("                LuaDLL.lua_newtable(L);\r\n");
        sb.AppendFormat("                LuaDLL.lua_setglobal(L, '{0}');\r\n", className);
        sb.AppendFormat("                LuaDLL.lua_getglobal(L, '{0}');\r\n", className);
        sb.Append("            }\r\n");
        sb.Append("\r\n");
    }
    static void AppendRegisterEnd(StringBuilder sb, Type classType) {
        sb.Append(@"
            LuaDLL.lua_getglobal(L, 'readIndex');
            LuaDLL.lua_pushvalue(L, -1);
            LuaDLL.lua_setfield(L, -3, '__index');
            LuaDLL.lua_pop(L, 1);

            LuaDLL.lua_getglobal(L, 'writeIndex');
            LuaDLL.lua_pushvalue(L, -1);
            LuaDLL.lua_setfield(L, -3, '__newindex');
            LuaDLL.lua_pop(L, 1);

            LuaDLL.lua_pushstdcallcfunction(L, new LuaCSFunction(LuaStatic.GameObjectGC));
            LuaDLL.lua_setfield(L, -2, '__gc');");
        Type baseType = classType.BaseType;
        if (baseType != null && classType.BaseType != typeof(object)) {
            sb.AppendFormat("\r\n            LuaDLL.lua_getglobal(L, '{0}');\r\n", baseType.Name);
            sb.Append("            if (LuaDLL.lua_isnil(L, -1)) {\r\n");
            sb.Append("                LuaDLL.lua_pop(L, 1);\r\n");
            sb.Append("                LuaDLL.lua_newtable(L);\r\n");
            sb.AppendFormat("                LuaDLL.lua_setglobal(L, '{0}');\r\n", baseType.Name);
            sb.AppendFormat("                LuaDLL.lua_getglobal(L, '{0}');\r\n", baseType.Name);
            sb.Append("                LuaDLL.lua_setmetatable(L, -2);\r\n");
            sb.Append("            }\r\n");
            sb.Append("            else {\r\n");
            sb.Append("                LuaDLL.lua_setmetatable(L, -2);\r\n");
            sb.Append("            }");
        }
        sb.Append("\r\n\r\n            LuaDLL.lua_settop(L, oldTop);\r\n");
        sb.AppendFormat("            LuaStatic.AddTypeDict(typeof({0}));\r\n", classType.FullName);
        sb.Append("\r\n        }\r\n");
    }
    #endregion

    #region string deal
    static string TrimNameSpace(Type fieldType) {
        string fullName = string.Empty;
        string tempName = string.Empty;
        Type[] types = fieldType.GetGenericArguments();
        if (types.Length <= 0) {
            fullName = fieldType.FullName;
            for (int i = 0; i < usingList.Count; i++) {
                tempName = fullName.Replace(usingList[i] + ".", "");
                if (!tempName.Contains(".")) {
                    fullName = tempName;
                }
            }
        }
        else {
            string nameSpace = string.IsNullOrEmpty(fieldType.Namespace) ? "" : fieldType.Namespace + ".";
            fullName = nameSpace + fieldType.Name.Substring(0, fieldType.Name.Length - 2) + "<";
            for (int i = 0; i < usingList.Count; i++) {
                fullName = fullName.Replace(usingList[i] + ".", "");
            }
            for (int i = 0; i < types.Length; i++) {
                if (i == types.Length - 1) {
                    fullName += TrimNameSpace(types[i]) + ">";
                    break;
                }
                fullName += TrimNameSpace(types[i]) + " ,";

            }
        }
        return fullName.Replace("+", ".").Replace("&", "");
    }
    static string GetArrayItem(Type arryType, bool isFull) {
        string result = string.Empty;
        Type[] types = arryType.GetGenericArguments();
        if (types.Length > 0) {
            arryType = types[types.Length - 1];
        }
        if (isFull) {
            result = arryType.FullName;
        }
        else {
            result = arryType.Name;
        }
        result = result.Replace("[]", "");
        result = result.Replace("+", ".");
        return result;
    }
    #endregion

    #endregion

    #region export api
    [MenuItem("Lua/Gen Lua Api", false, 2)]
    public static void ExportLuaApi() {
        List<Type> classList = new List<Type>();

        // add class here
        classList.Add(typeof(Director));
        classList.Add(typeof(GameObject));

        MethodInfo[] methods;
        MemberInfo[] fields;
        PropertyInfo[] properties;

        string path = "";
        path = EditorUtility.SaveFolderPanel("Select API Folder", "C:/Users/123/Documents/BabeLua/Completion", "");
        if (path == "") {
            return;
        }

        foreach (Type t in classList) {
            FileStream fs = new FileStream(path + "/" + t.Name + ".lua", FileMode.Create);
            var utf8WithoutBom = new System.Text.UTF8Encoding(false);
            StreamWriter sw = new StreamWriter(fs, utf8WithoutBom);

            sw.WriteLine(t.Name + " = { }\n");
            fields = t.GetFields();
            foreach (var field in fields) {
                if (IsObsolete(field)) {
                    continue;
                }
                string param = field.ToString();
                string[] expression = param.Split(" ".ToCharArray(), 2);
                string returnType = expression[0];

                returnType = GetClassName(returnType);

                WriteField(sw, returnType, t.Name, field.Name);
            }

            properties = t.GetProperties();
            foreach (var property in properties) {
                if (IsObsolete(property)) {
                    continue;
                }
                string param = property.ToString();
                string[] expression = param.Split(" ".ToCharArray(), 2);
                string returnType = expression[0];
                returnType = GetClassName(returnType);

                WriteField(sw, returnType, t.Name, property.Name);
            }

            sw.Write("\n");

            methods = t.GetMethods();

            List<string> argFlags = new List<string>();
            List<string> returnTypes = new List<string>();
            MethodInfo method;

            for (int i = 0; i < methods.Length; i++) {
                method = methods[i];
                if (IsObsolete(method)) {
                    continue;
                }
                string methodName = method.Name;

                string param = method.ToString();
                string[] expression = param.Split(" ".ToCharArray(), 2);

                string returnType = expression[0];


                returnType = GetClassName(returnType);

                string argFlag = expression[1].Split("(".ToCharArray(), 2)[1];

                argFlag = argFlag.TrimEnd(")".ToCharArray());
                argFlags.Add(argFlag);
                returnTypes.Add(returnType);

                if (method.IsPublic && !methodName.StartsWith("get_") && !methodName.StartsWith("set_")) {
                    string funString = OneFunString(argFlags.ToArray(), returnTypes.ToArray(), t.Name, method.Name);
                    if (i > 0) {
                        if (method.Name != methods[i - 1].Name) {
                            WriteFun(sw, funString);
                            argFlags.Clear();
                            returnTypes.Clear();
                        }
                    }
                    else {
                        if (method.Name != methods[i + 1].Name) {
                            WriteFun(sw, funString);
                            argFlags.Clear();
                            returnTypes.Clear();
                        }
                    }
                }

            }

            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
        }

        Debug.Log("转换完成");
    }
    public static bool IsObsolete(MemberInfo mb) {
        object[] attrs = mb.GetCustomAttributes(true);

        for (int j = 0; j < attrs.Length; j++) {
            Type t = attrs[j].GetType();

            if (t == typeof(System.ObsoleteAttribute)) // || t.ToString() == "UnityEngine.WrapperlessIcall")
            {
                return true;
            }
        }

        return false;
    }
    static void WriteField(StreamWriter sw, string returnType, string className, string funName) {
        sw.WriteLine("--- <summary>");
        sw.WriteLine("--- @ReturnCode     :" + returnType);
        sw.WriteLine("--- </summary>");
        sw.WriteLine("--- <returns type=\"" + returnType + "\"></returns>");

        sw.WriteLine(className + "." + funName + " = nil");
    }

    static string OneFunString(string[] argFlags, string[] returnTypes, string className, string funName) {
        string funString = "--- <summary>\n";

        for (int i = 0; i < argFlags.Length; i++) {
            string argFlag = argFlags[i];
            string returnType = returnTypes[i];
            if (i != argFlags.Length - 1) {
                funString = funString + string.Format("--- @ArgumentFlag   :{0}\n", argFlag);
                funString = funString + string.Format("--- @ReturnCode     :{0}\n", returnType);
                funString = funString + string.Format("--- function {0}.{1}({2}) end\n\n", className, funName, argFlag);
            }
            else {
                funString = funString + string.Format("--- @ArgumentFlag   :{0}\n", argFlag);
            }

        }

        funString = funString + string.Format("--- @ReturnCode     :{0}\n", returnTypes[returnTypes.Length - 1]);
        funString = funString + "--- </summary>\n";
        funString = funString + string.Format("--- <returns type=\"{0}\"></returns>\n", returnTypes[returnTypes.Length - 1]);
        funString = funString + string.Format("function {0}.{1}() end\n\n", className, funName);

        return funString;
    }
    static void WriteFun(StreamWriter sw, string funString) {
        sw.WriteLine(funString);
    }
    static string GetClassName(string allName) {

        string[] charArray = allName.Split(".".ToCharArray());
        string className = charArray[charArray.Length - 1];

        return className;
    }
    #endregion
}