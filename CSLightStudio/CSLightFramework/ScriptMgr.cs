using System;
using System.Collections.Generic;
using System.Text;

namespace CSLight.Framework
{
    public interface IScript
    {
        string name
        {
            get;
        }
        void CallScriptFuncWithoutParam(string scriptname);
        void CallScriptFuncWithParamString(string scriptname, string param);
        void CallScriptWithParamStrings(string scriptname, List<string> param);

    }
    public interface ICodeFile<T>:IScript where T : class
    {

        void New(T view);//初始化View

    }
    public class ScriptMgr<T> where T : class
    {
        ICLS_Logger _logger;
        public ScriptMgr(ICLS_Logger logger)
        {
            _logger = logger;
        }
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
            scriptEnv = new CSLight.CLS_Environment(_logger);
            this.scriptEnv.RegType(new CSLight.RegHelper_Type(typeof(IScript)));
            this.scriptEnv.RegType(new CSLight.RegHelper_Type(typeof(List<string>),"List<string>"));
        }

    }
}
