using System;
using System.Collections.Generic;

using System.Text;


namespace CSLight.Framework
{
    public class CodeFile_CLScript<T> : ICodeFile<T> where T : class
    {
        ScriptMgr<T> scriptmgr;
        public CodeFile_CLScript(ScriptMgr<T> scriptmgr, string name, string src)
        {
            this.scriptmgr = scriptmgr;
            this.name = name;
            scriptmgr.scriptEnv.logger.Log("(scriptParser)Gen:" + name);
            ParseCode(src);
        }
        public string name
        {
            get;
            private set;
        }
        public class functioninfo
        {
            public string name;
            public List<string> paramname = new List<string>();
            //public string code;
            public CSLight.ICLS_Expression exp;
            public IList<Token> tokens;
        }

        Dictionary<string, functioninfo> funcs = new Dictionary<string, functioninfo>();
        void ParseFunc(IList<CSLight.Token> tokens, string funcname, int parambegin, int paramend, int funcbegin, int funcend)
        {
            functioninfo func = new functioninfo();
            string paramname = "";
            //检查参数
            for (int i = parambegin; i <= paramend; i++)
            {
                if (tokens[i].text == "," || tokens[i].text == ")")
                {
                    func.paramname.Add(tokens[i - 1].text);
                    paramname += tokens[i - 1].text + "|";
                }
            }

            //编译表达式
            List<CSLight.Token> codetokens = new List<CSLight.Token>();
            for (int i = funcbegin; i <= funcend; i++)
            {
                codetokens.Add(tokens[i]);
                //Debug.Log("(script)ft:" + tokens[i].text);
            }
            var exp = scriptmgr.scriptEnv.CompilerToken(codetokens);
            scriptmgr.scriptEnv.logger.Log("(scriptParser)findfunction:" + funcname + "(" + paramname + "){" + exp + "}");
            func.name = funcname;
            func.exp = exp;
            func.tokens = codetokens;
            funcs[funcname] = func;
        }
        void ParseCode(string src)
        {
            //scriptmgr.scriptEnv.logger.Log("(scriptParser)ParseCode=" + src);
            var parser = scriptmgr.scriptEnv.tokenParser;
            IList<CSLight.Token> tokens = parser.Parse(src);

            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].text == "class")
                {
                    string name = tokens[i + 1].text;
                    int ibegin = i + 2;
                    while (tokens[ibegin].text != "{") 
                        ibegin++;
                    int iend = FindBlock(tokens, ibegin);
                    scriptmgr.scriptEnv.logger.Log("(scriptParser)findclass:" + name + "(" + ibegin + "," + iend + ")");
                    for (int j = ibegin + 1; j < iend; j++)
                    {
                        if (tokens[j].type == CSLight.TokenType.TYPE)//发现类型
                        {
                            if (tokens[j + 1].type == CSLight.TokenType.IDENTIFIER) //类型后面是名称
                            {
                                string funcname = tokens[j + 1].text;
                                if (tokens[j + 2].type == CSLight.TokenType.PUNCTUATION && tokens[j + 2].text == "(")//参数开始,这是函数
                                {
                                    int funcparambegin = j + 2;
                                    int funcparamend = FindBlock(tokens, funcparambegin);

                                    int funcbegin = funcparamend + 1;
                                    int funcend = FindBlock(tokens, funcbegin);

                                    ParseFunc(tokens, funcname, funcparambegin, funcparamend, funcbegin, funcend);


                                    j = funcend;
                                }
                            }
                        }
                    }
                    return;
                }
            }
            scriptmgr.scriptEnv.logger.Log_Error("(script)not findclass");

        }
        int FindBlock(IList<CSLight.Token> tokens, int start)
        {
            if (tokens[start].type != CSLight.TokenType.PUNCTUATION)
            {
                scriptmgr.scriptEnv.logger.Log_Error("(script)FindBlock 没有从符号开始");
            }
            string left = tokens[start].text;
            string right = "}";
            if (left == "{") right = "}";
            if (left == "(") right = ")";
            if (left == "[") right = "]";
            int depth = 0;
            for (int i = start; i < tokens.Count; i++)
            {
                if (tokens[i].type == CSLight.TokenType.PUNCTUATION)
                {
                    if (tokens[i].text == left)
                    {
                        depth++;
                    }
                    else if (tokens[i].text == right)
                    {
                        depth--;
                        if (depth == 0)
                        {
                            return i;
                        }
                    }
                }
            }
            return -1;
        }
        T parent = null;
        public void New(T parent)
        {
            this.parent = parent;
            this.CallScriptFuncWithoutParam("_new");
        }

        public void CallScriptFuncWithoutParam(string scriptname)
        {
            if (funcs.ContainsKey(scriptname) == false)
            {
                scriptmgr.scriptEnv.logger.Log("(script)" + this.name + "." + scriptname + " not found.");
                return;
            }
            var func = funcs[scriptname];
            if (func.exp == null) return;
            CSLight.CLS_Content content = new CSLight.CLS_Content(scriptmgr.scriptEnv);
            try
            {
                content.DefineAndSet(func.paramname[0], typeof(T), parent);
                func.exp.ComputeValue(content);
            }
            catch (Exception err)
            {
                string msg = this.name + ":" + scriptname + "\n" + err.Message + "\n" + content.Dump(func.tokens) + "\n";
                throw new Exception(msg, err);
            }
        }

        public void CallScriptFuncWithParamString(string scriptname, string param)
        {
            if (funcs.ContainsKey(scriptname) == false)
            {
                scriptmgr.scriptEnv.logger.Log("(script)" + this.name + "." + scriptname + " not found.");
                return;
            }
            var func = funcs[scriptname];
            if (func.exp == null) return;
            CSLight.CLS_Content content = new CSLight.CLS_Content(scriptmgr.scriptEnv);
            try
            {
                content.DefineAndSet(func.paramname[0], typeof(T), parent);
                content.DefineAndSet(func.paramname[1], typeof(string), param);
                func.exp.ComputeValue(content);
            }
            catch (Exception err)
            {
                string msg = this.name + ":" + scriptname + "\n" + err.Message + "\n" + content.Dump(func.tokens) + "\n";
                throw new Exception(msg, err);
            }
        }
        public void CallScriptFuncWithParamFloat(string scriptname, float param)
        {
            if (funcs.ContainsKey(scriptname) == false)
            {
                scriptmgr.scriptEnv.logger.Log("(script)" + this.name + "." + scriptname + " not found.");
                return;
            }
            var func = funcs[scriptname];
            if (func.exp == null) return;
            CSLight.CLS_Content content = new CSLight.CLS_Content(scriptmgr.scriptEnv);
            try
            {
                content.DefineAndSet(func.paramname[0], typeof(T), parent);
                content.DefineAndSet(func.paramname[1], typeof(float), param);
                func.exp.ComputeValue(content);
            }
            catch (Exception err)
            {
                string msg = this.name + ":" + scriptname + "\n" + err.Message + "\n" + content.Dump(func.tokens) + "\n";
                throw new Exception(msg, err);
            }
        }
        public void CallScriptFuncWithParamStrings(string scriptname, List<string> param)
        {
            if (funcs.ContainsKey(scriptname) == false)
            {
                scriptmgr.scriptEnv.logger.Log("(script)" + this.name + "." + scriptname + " not found.");
                return;
            }
            var func = funcs[scriptname];
            if (func.exp == null) return;
            CSLight.CLS_Content content = new CSLight.CLS_Content(scriptmgr.scriptEnv);
            try
            {
                content.DefineAndSet(func.paramname[0], typeof(T), parent);
                content.DefineAndSet(func.paramname[1], typeof(List<string>), param);
                func.exp.ComputeValue(content);
            }
            catch (Exception err)
            {
                string msg = this.name + ":" + scriptname + "\n" + err.Message + "\n" + content.Dump(func.tokens) + "\n";
                throw new Exception(msg, err);
            }
        }

    }

}
