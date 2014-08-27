using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
class ScriptLogger : CSLE.ICLS_Logger
{

    public void Log(string str)
    {
        UnityEngine.Debug.Log(str);
    }

    public void Log_Error(string str)
    {
        Debug.LogError(str);
    }

    public void Log_Warn(string str)
    {
        Debug.LogWarning(str);
    }
}

class Script
{
    public static CSLE.CLS_Environment env = null;

    public static void Init()
    {
        if (env == null)
        {
            env = new CSLE.CLS_Environment(new ScriptLogger());
            env.logger.Log("C#LightEvil Inited.Ver="+ env.version);
        }
    }
    public static void Reset()
    {
        env = null;
        Init();
    }
    public static void BuildProject(string path)
    {
#if UNITY_STANDALONE
        if (env == null)
            Init();
        string[] files = System.IO.Directory.GetFiles(path, "*.cs", System.IO.SearchOption.AllDirectories);
        Dictionary<string, IList<CSLE.Token>> project = new Dictionary<string, IList<CSLE.Token>>();
        foreach (var v in files)
        {
            var tokens = env.tokenParser.Parse(System.IO.File.ReadAllText(v));
            project.Add(v, tokens);
        }
        env.Project_Compiler(project, true);
#endif
    }
    public static object Eval(string script)
    {
        if (env == null)
            Init();

        var token = env.ParserToken(script);//词法分析
        var expr = env.Expr_CompilerToken(token, true);//语法分析,简单表达式，一句话
        var value = env.Expr_Execute(expr, content);//执行表达式
        if (value == null) return null;
        return value.value;
    }
    public static object Execute(string script)
    {
        var token = env.ParserToken(script);//词法分析
        var expr = env.Expr_CompilerToken(token, false);//语法分析，语法块
        var value = env.Expr_Execute(expr, content);//执行表达式
        if (value == null) return null;
        return value.value;
    }
    public static void BuildFile(string filename, string code)
    {
        var token = env.ParserToken(code);//词法分析
        env.File_CompilerToken(filename, token, false);

    }


    public static CSLE.CLS_Content content = null;
    public static void SetValue(string name, object v)
    {
        if (env == null)
            Init();
        if (content == null)
            content = env.CreateContent();
        content.DefineAndSet(name, v.GetType(), v);
    }
    public static void ClearValue()
    {
        content = null;
    }
}

