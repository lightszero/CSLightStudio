using System;
using System.Collections.Generic;
using System.Text;

namespace CSLight.Framework
{
    public interface ICodeFile<T> where T : class
    {
        string name
        {
            get;
        }
        void New(T view);//初始化View
        void CallScriptFuncWithoutParam(string scriptname);
        void CallScriptFuncWithParamString(string scriptname, string param);
        void CallScriptWithParamStrings(string scriptname, List<string> param);

    }
    public class ScriptMgr<T> : CSLight.ICLS_Logger where T : class
    {
        //static ScriptMgr<T> g_this = null;
        //public static ScriptMgr<T> Instance
        //{
        //    get
        //    {
        //        if (g_this == null)
        //        {
        //            g_this = new ScriptMgr<T>();
        //            g_this.Init();

        //        }
        //        return g_this;
        //    }
        //}

        protected Dictionary<string, ICodeFile<T>> mapCodeFiles = new Dictionary<string, ICodeFile<T>>();
        public virtual ICodeFile<T> GetCodeFile(string name)
        {
            name = name.ToLower();
            if (mapCodeFiles.ContainsKey(name))
            {
                return mapCodeFiles[name.ToLower()];
            }
            else
            {
                //Log_Error("(script)脚本不存在:" + name);
                return null;
            }
        }
        public void SetCodeFile(string name, ICodeFile<T> code)
        {
            mapCodeFiles[name] = code;
        }

        public CSLight.CLS_Environment scriptEnv
        {
            get;
            private set;
        }
        public virtual void Init()
        {
            scriptEnv = new CSLight.CLS_Environment(this);
            this.scriptEnv.RegType(new CSLight.RegHelper_Type(typeof(ICodeFile<T>)));
            this.scriptEnv.RegType(new CSLight.RegHelper_Type(typeof(List<string>),"List<string>"));
        }

        public void Log(string str)
        {
#if UNITY
            Debug.Log("script:" + str);
#else
            Console.WriteLine("script(Log):" + str);
#endif
        }

        public void Log_Warn(string str)
        {
#if UNITY
            Debug.LogWarning("script:" + str);
#else
            Console.WriteLine("script(LogWarning):" + str);
#endif

        }

        public void Log_Error(string str)
        {
#if UNITY
            Debug.LogError("script:" + str);
#else
            Console.WriteLine("script(LogError):" + str);
#endif
        }
    }
}
