using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


/// <summary>
/// 这个类实现脚本的Logger接口，脚本编译时的信息会从Log输出出来
/// </summary>
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

public class ScriptMgr
{
    /// <summary>
    /// ScriptMgr用单例模式，主要是为了提供C#Light Env的初始化
    /// </summary>
    public static ScriptMgr Instance
    {
        get
        {
            if (g_this == null)
                g_this = new ScriptMgr();
            return g_this;

        }
    }
    #region forInstance
    static ScriptMgr g_this;
    public CSLE.CLS_Environment env
    {
        get;
        private set;
    }
    private ScriptMgr()
    {
        env = new CSLE.CLS_Environment(new ScriptLogger());
        env.logger.Log("C#LightEvil Inited.Ver=" + env.version);

        RegTypes();
    }
    #endregion


    /// <summary>
    /// 这里注册脚本有权访问的类型，大部分类型用RegHelper_Type提供即可
    /// </summary>
    void RegTypes()
    {
        //大部分类型用RegHelper_Type提供即可
        env.RegType(new CSLE.RegHelper_Type(typeof(Vector2)));
        env.RegType(new CSLE.RegHelper_Type(typeof(Vector3)));
        env.RegType(new CSLE.RegHelper_Type(typeof(Vector4)));
        env.RegType(new CSLE.RegHelper_Type(typeof(Time)));

        env.RegType(new CSLE.RegHelper_Type(typeof(Debug)));
        env.RegType(new CSLE.RegHelper_Type(typeof(GameObject)));
        env.RegType(new CSLE.RegHelper_Type(typeof(Component)));
        env.RegType(new CSLE.RegHelper_Type(typeof(UnityEngine.Object)));
        env.RegType(new CSLE.RegHelper_Type(typeof(Transform)));
        env.RegType(new CSLE.RegHelper_Type(typeof(UIEventListener)));
        //对于AOT环境，比如IOS，get set不能用RegHelper直接提供，就用AOTExt里面提供的对应类替换
        env.RegType(new CSLE.RegHelper_Type(typeof(int[]), "int[]"));//数组要独立注册
        env.RegType(new CSLE.RegHelper_Type(typeof(List<int>), "List<int>"));//模板类要独立注册



        //每一种回调类型要独立注册
        //env.RegDeleType(new CSLE.RegHelper_DeleAction("Action")); //unity 用的dotnet 2.0 没有Action
        //env.RegDeleType(new CSLE.RegHelper_DeleAction<int>("Action<int>")); ;


        env.RegType(new CSLE.RegHelper_Type(typeof(StateMgr)));
        env.RegType(new CSLE.RegHelper_Type(typeof(Rect)));
        env.RegType(new CSLE.RegHelper_Type(typeof(ScriptInstanceState)));
        env.RegType(new CSLE.RegHelper_Type(typeof(PrimitiveType)));
        env.RegType(new CSLE.RegHelper_DeleAction<GameObject>(typeof(UIEventListener.VoidDelegate), "UIEventListener.VoidDelegate"));

    }

    public bool projectLoaded
    {
        get;
        private set;
    }
    public void LoadProject()
    {
        if (projectLoaded) return;
        try
        {
            string[] files = System.IO.Directory.GetFiles(Application.streamingAssetsPath, "*.cs", System.IO.SearchOption.AllDirectories);
            Dictionary<string, IList<CSLE.Token>> project = new Dictionary<string, IList<CSLE.Token>>();
            foreach (var v in files)
            {
                var tokens = env.tokenParser.Parse(System.IO.File.ReadAllText(v));
                project.Add(v, tokens);
            }
            env.Project_Compiler(project, true);
            projectLoaded = true;
        }
        catch (Exception err)
        {

            Debug.LogError("编译脚本项目失败，请检查" + err.ToString());
        }
    }
    public void Execute(string code)
    {
        var content = env.CreateContent();


        try
        {
            var tokens = env.ParserToken(code);
            var expr = env.Expr_CompilerToken(tokens);
            expr.ComputeValue(content);
        }
        catch (Exception err)
        {
            var dumpv = content.DumpValue();
            var dumps = content.DumpStack(null);
            var dumpSys = err.ToString();
            Debug.LogError(dumpv + dumps + dumpSys);
        }
    }
}

