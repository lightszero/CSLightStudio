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

public class ScriptEnv
{
    /// <summary>
    /// ScriptMgr用单例模式，主要是为了提供C#Light Env的初始化
    /// </summary>
    public static ScriptEnv Instance
    {
        get
        {
            if (g_this == null)
                g_this = new ScriptEnv();
            return g_this;

        }
    }
    #region forInstance
    static ScriptEnv g_this;
    public CSLE.CLS_Environment scriptEnv
    {
        get;
        private set;
    }
    private ScriptEnv()
    {
        //scriptEnv = new CSLE.CLS_Environment(new ScriptLogger());
        //scriptEnv.logger.Log("C#LightEvil Inited.Ver=" + scriptEnv.version);

        //RegTypes();
    }
    #endregion
    public void Reset(CSLE.ICLS_Logger logger)
    {
        if(logger ==null)
        {
            logger =new ScriptLogger(); 

        }
        scriptEnv = new CSLE.CLS_Environment(logger);
        RegTypes();
        projectLoaded = false;
    }

    /// <summary>
    /// 这里注册脚本有权访问的类型，大部分类型用RegHelper_Type提供即可
    /// </summary>
    void RegTypes()
    {
        //大部分类型用RegHelper_Type提供即可
        scriptEnv.RegType(new CSLE.RegHelper_Type(typeof(Vector2)));
        scriptEnv.RegType(new CSLE.RegHelper_Type(typeof(Vector3)));
        scriptEnv.RegType(new CSLE.RegHelper_Type(typeof(Vector4)));
        scriptEnv.RegType(new CSLE.RegHelper_Type(typeof(Time)));

        scriptEnv.RegType(new CSLE.RegHelper_Type(typeof(Debug)));
        scriptEnv.RegType(new CSLE.RegHelper_Type(typeof(GameObject)));
        scriptEnv.RegType(new CSLE.RegHelper_Type(typeof(Component)));
        scriptEnv.RegType(new CSLE.RegHelper_Type(typeof(UnityEngine.Object)));
        scriptEnv.RegType(new CSLE.RegHelper_Type(typeof(Transform)));
        //对于AOT环境，比如IOS，get set不能用RegHelper直接提供，就用AOTExt里面提供的对应类替换
        scriptEnv.RegType(new CSLE.RegHelper_Type(typeof(int[]), "int[]"));//数组要独立注册
        scriptEnv.RegType(new CSLE.RegHelper_Type(typeof(List<int>), "List<int>"));//模板类要独立注册



        //每一种回调类型要独立注册
        scriptEnv.RegType(new CSLE.RegHelper_DeleAction(typeof(Action),"Action")); //unity 用的dotnet 2.0 没有Action
        scriptEnv.RegType(new CSLE.RegHelper_DeleAction<int>(typeof(Action<int>), "Action<int>")); ;


        scriptEnv.RegType(new CSLE.RegHelper_Type(typeof(StateMgr)));
        scriptEnv.RegType(new CSLE.RegHelper_Type(typeof(Rect)));
        scriptEnv.RegType(new CSLE.RegHelper_Type(typeof(ScriptInstanceState)));
        scriptEnv.RegType(new CSLE.RegHelper_Type(typeof(PrimitiveType)));
        scriptEnv.RegType(new CSLE.RegHelper_Type(typeof(App)));
        scriptEnv.RegType(new CSLE.RegHelper_Type(typeof(CLComponent)));

    }

    public bool projectLoaded
    {
        get;
        private set;
    }
    public void LoadProjectBytes(string filename)
    {
        if (projectLoaded) return;
        if (Application.isEditor)
        {
            filename = System.IO.Path.GetFullPath("../bin") + "/" + filename;
        }
        try
        {
            Debug.Log("filename=" + filename);
            var bytes = System.IO.File.ReadAllBytes(filename);
            var project = scriptEnv.Project_FromPacketStream(new System.IO.MemoryStream(bytes));
            scriptEnv.Project_Compiler(project, true);
            projectLoaded = true;
        }
        catch (Exception err)
        {

            Debug.LogError("编译脚本项目失败，请检查" + err.ToString());
        }
    }
    public void LoadProjectStreamingAssets()
    {
        if (projectLoaded) return;
        try
        {

            string[] files = System.IO.Directory.GetFiles(Application.streamingAssetsPath, "*.cs", System.IO.SearchOption.AllDirectories);
            Dictionary<string, IList<CSLE.Token>> project = new Dictionary<string, IList<CSLE.Token>>();
            foreach (var v in files)
            {
                var tokens = scriptEnv.tokenParser.Parse(System.IO.File.ReadAllText(v));
                project.Add(v, tokens);
            }
            scriptEnv.Project_Compiler(project, true);
            projectLoaded = true;
        }
        catch (Exception err)
        {

            Debug.LogError("编译脚本项目失败，请检查" + err.ToString());
        }
    }
    public void Execute(string code)
    {
        var content = scriptEnv.CreateContent();


        try
        {
            var tokens = scriptEnv.ParserToken(code);
            var expr = scriptEnv.Expr_CompilerToken(tokens);
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

