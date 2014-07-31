/// CSEvil 库 由疯光无线开发
/// 正式名称C#Evil V0.20
/// crazylights.cnblogs.com
/// 请勿删除此声明
using System;
using System.Collections.Generic;
using System.Text;

namespace CSLE
{
    //环境 增加本地代码的管理
    //环境 增加运行中的表达式查询
    public class CLS_Environment : ICLS_Environment, ICLS_Environment_Compiler
    {

        public CLS_Environment(ICLS_Logger logger)
        {
            this.logger = logger;
            tokenParser = new CLS_TokenParser();
            compiler = new CLS_Expression_Compiler(logger);
            RegType(new CLS_Type_Int());
            RegType(new CLS_Type_UInt());
            RegType(new CLS_Type_Float());
            RegType(new CLS_Type_Double());
            RegType(new CLS_Type_String());
            RegType(new CLS_Type_Var());
            typess["null"] = new CLS_Type_NULL();
            //contentGloabl = CreateContent();

            RegFunction(new FunctionTrace());

        }


        Dictionary<CLType, ICLS_Type> types = new Dictionary<CLType, ICLS_Type>();
        Dictionary<string, ICLS_Type> typess = new Dictionary<string, ICLS_Type>();
        Dictionary<string, ICLS_Function> calls = new Dictionary<string, ICLS_Function>();
        public void RegType(ICLS_Type type)
        {
            types[type.type] = type;
            typess[type.keyword] = type;
            if (tokenParser.types.Contains(type.keyword) == false)
            {
                tokenParser.types.Add(type.keyword);
            }
        }
        public ICLS_Type GetType(CLType type)
        {
            if (type == null)
                return typess["null"];
            if (types.ContainsKey(type) == false)
            {
                logger.Log_Error("(CLScript)类型未注册:" + type.ToString());
            }
            return types[type];
        }
        public ICLS_Type GetTypeByKeyword(string keyword)
        {
            if (typess.ContainsKey(keyword) == false)
            {
                logger.Log_Error("(CLScript)类型未注册:" + keyword);
               
            }
            return typess[keyword];
        }
        public ICLS_Type GetTypeByKeywordQuiet(string keyword)
        {
            if (typess.ContainsKey(keyword) == false)
            {
                return null;
            }
            return typess[keyword];
        }
        public void RegFunction(ICLS_Function func)
        {
            calls[func.keyword] = func;
        }
        public ICLS_Function GetFunction(string name)
        {
            return calls[name];
        }
        public ICLS_Logger logger
        {
            get;
            private set;
        }
        //public ICLS_Debugger debugger;
        public ICLS_TokenParser tokenParser
        {
            get;
            private set;
        }
        ICLS_Expression_Compiler compiler = null;
        public IList<Token> ParserToken(string code)
        {
            return tokenParser.Parse(code);
        }
        public ICLS_Expression Expr_CompilerToken(IList<Token> listToken, bool SimpleExpression = false)
        {
            return SimpleExpression ? compiler.Compiler_NoBlock(listToken, this) : compiler.Compiler(listToken, this);
        }
        //CLS_Content contentGloabl = null;
        public ICLS_Expression Expr_Optimize(ICLS_Expression old)
        {
            return compiler.Optimize(old, this);
        }
        public CLS_Content CreateContent()
        {
            return new CLS_Content(this, true);
        }

        public CLS_Content.Value Expr_Execute(ICLS_Expression expr, CLS_Content content = null)
        {
            if (content == null) content = CreateContent();
            return expr.ComputeValue(content);
        }

        public void Project_Compiler(Dictionary<string, IList<Token>> project)
        {
            foreach(var f in project)
            {
                File_PreCompilerToken(f.Key, f.Value);
            }
            foreach (var f in project)
            {
                //预处理符号
                for (int i = 0; i < f.Value.Count; i++)
                {
                    if (f.Value[i].type == TokenType.IDENTIFIER && this.tokenParser.types.Contains(f.Value[i].text))
                    {//有可能预处理导致新的类型
                        Token rp = f.Value[i];
                        rp.type = TokenType.TYPE;
                        f.Value[i] = rp;
                    }
                }
                File_CompilerToken(f.Key, f.Value);
            }
        }
        public void File_PreCompilerToken(string filename, IList<Token> listToken)
        {
            IList<ICLS_Type> types = compiler.FilePreCompiler(listToken, this);
            foreach (var type in types)
            {
                this.RegType(type);
            }
        }
        public void File_CompilerToken(string filename, IList<Token> listToken)
        {
            logger.Log("File_CompilerToken:" + filename);
            IList<ICLS_Type> types = compiler.FileCompiler(listToken, this);
            foreach (var type in types)
            {
                if (this.GetTypeByKeywordQuiet(type.keyword) == null)
                    this.RegType(type);
            }
        }

    }
}
