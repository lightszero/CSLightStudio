using System;
using System.Collections.Generic;

using System.Text;

namespace CSLight.Framework
{
    public class CodeFile_Debug<T> : ICodeFile<T> where T : class
    {
        ScriptMgr<T> scriptmgr;
        public CodeFile_Debug(ScriptMgr<T> scriptmgr,string name, Type target)
        {
            this.scriptmgr = scriptmgr;
            this.name = name;
            this.target = target;
        }
        public string name
        {
            get;
            private set;
        }
        Type target;

        T parent = null;
        public void New(T parent)
        {
            this.parent = parent;
            this.CallScriptFuncWithoutParam("_new");
        }

        public void CallScriptFuncWithoutParam(string scriptname)
        {
            var funcp = target.GetMethod(scriptname, new Type[] { typeof(T), });
            if (funcp == null)
            {
                scriptmgr.scriptEnv.logger.Log("(screenscript) func不存在:" + name + "." + scriptname);
                return;
            }
            funcp.Invoke(null, new object[] { parent });
        }

        public void CallScriptFuncWithParamString(string scriptname, string param)
        {
            var funcp = target.GetMethod(scriptname, new Type[] { typeof(T), typeof(string) });
            if (funcp == null)
            {
                scriptmgr.scriptEnv.logger.Log("(screenscript) func不存在:" + name + "." + scriptname);
                return;
            }
            funcp.Invoke(null, new object[] { parent, param });

        }
        public void CallScriptFuncWithParamFloat(string scriptname, float param)
        {
            var funcp = target.GetMethod(scriptname, new Type[] { typeof(T), typeof(float) });
            if (funcp == null)
            {
                scriptmgr.scriptEnv.logger.Log("(screenscript) func不存在:" + name + "." + scriptname);
                return;
            }
            funcp.Invoke(null, new object[] { parent, param });

        }
        public void CallScriptFuncWithParamStrings(string scriptname, List<string> param)
        {
            var funcp = target.GetMethod(scriptname, new Type[] { typeof(T), typeof(List<string>) });
            if (funcp == null)
            {
                scriptmgr.scriptEnv.logger.Log("(screenscript) func不存在:" + name + "." + scriptname);
                return;
            }
            funcp.Invoke(null, new object[] { parent, param });

        }


    }
}

