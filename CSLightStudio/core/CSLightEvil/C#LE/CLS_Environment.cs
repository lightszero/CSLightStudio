/// C#Light/Evil
/// 作者 疯光无限 版本见ICLS_Environment.version
/// https://github.com/lightszero/CSLightStudio
/// http://crazylights.cnblogs.com
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

        public string version
        {
            get
            {
                return "0.41Beta";
            }
        }
        public CLS_Environment(ICLS_Logger logger)
        {
            //if(useNamespace==true)
            //{
            //    throw new Exception("使用命名空间还不能完全兼容，建议关闭");
            //}
            this.logger = logger;
            //this.useNamespace = useNamespace;
            tokenParser = new CLS_TokenParser();
            compiler = new CLS_Expression_Compiler(logger);
            RegType(new CLS_Type_Int());
            RegType(new CLS_Type_UInt());
            RegType(new CLS_Type_Float());
            RegType(new CLS_Type_Double());
            RegType(new CLS_Type_String());
            RegType(new CLS_Type_Var());
            RegType(new CLS_Type_Lambda());
            RegType(new CLS_Type_Delegate());

            typess["null"] = new CLS_Type_NULL();
            //contentGloabl = CreateContent();
            //if (!useNamespace)//命名空间模式不能直接用函数
            {
                RegFunction(new FunctionTrace());
            }
        }
        //public bool useNamespace
        //{
        //    get;
        //    private set;
        //}

        Dictionary<CLType, ICLS_Type> types = new Dictionary<CLType, ICLS_Type>();
        Dictionary<string, ICLS_Type> typess = new Dictionary<string, ICLS_Type>();
        Dictionary<string, ICLS_Function> calls = new Dictionary<string, ICLS_Function>();
        //Dictionary<string, ICLS_Type_Dele> deleTypes = new Dictionary<string, ICLS_Type_Dele>();
        public void RegType(ICLS_Type type)
        {
            types[type.type] = type;

            string typename = type.keyword;
            //if (useNamespace)
            //{

            //    if (string.IsNullOrEmpty(type._namespace) == false)
            //    {
            //        typename = type._namespace + "." + type.keyword;
            //    }
            //}
            typess[typename] = type;
            if (tokenParser.types.Contains(typename) == false)
            {
                tokenParser.types.Add(typename);
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
        //public ICLS_Type_Dele GetDeleTypeBySign(string sign)
        //{
        //    if (deleTypes.ContainsKey(sign) == false)
        //    {
        //        return null;
        //        //logger.Log_Error("(CLScript)类型未注册:" + sign);

        //    }
        //    return deleTypes[sign];

        //}
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
            //if (useNamespace)
            //{
            //    throw new Exception("用命名空间时不能直接使用函数，必须直接定义在类里");
            //}
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

        public void Project_Compiler(Dictionary<string, IList<Token>> project, bool embDebugToken)
        {
            foreach (var f in project)
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
                File_CompilerToken(f.Key, f.Value, embDebugToken);
            }
        }
        public void File_PreCompilerToken(string filename, IList<Token> listToken)
        {
            IList<ICLS_Type> types = compiler.FilePreCompiler(this, filename, listToken);
            foreach (var type in types)
            {
                this.RegType(type);
            }
        }
        public void File_CompilerToken(string filename, IList<Token> listToken, bool embDebugToken)
        {
            logger.Log("File_CompilerToken:" + filename);
            IList<ICLS_Type> types = compiler.FileCompiler(this, filename, listToken, embDebugToken);
            foreach (var type in types)
            {
                if (this.GetTypeByKeywordQuiet(type.keyword) == null)
                    this.RegType(type);
            }
        }


        public void Project_PacketToStream(Dictionary<string, IList<Token>> project, System.IO.Stream outstream)
        {
            byte[] FileHead = System.Text.Encoding.UTF8.GetBytes("C#LE-DLL");
            outstream.Write(FileHead, 0, 8);
            UInt16 count = (UInt16)project.Count;
            outstream.Write(BitConverter.GetBytes(count), 0, 2);
            foreach (var p in project)
            {
                byte[] pname = System.Text.Encoding.UTF8.GetBytes(p.Key);
                outstream.WriteByte((byte)pname.Length);
                outstream.Write(pname, 0, pname.Length);
                this.tokenParser.SaveTokenList(p.Value, outstream);
            }
        }
        public Dictionary<string, IList<Token>> Project_FromPacketStream(System.IO.Stream instream)
        {
            Dictionary<string, IList<Token>> project = new Dictionary<string, IList<Token>>();
            byte[] buf = new byte[8];
            instream.Read(buf, 0, 8);
            string filehead = System.Text.Encoding.UTF8.GetString(buf, 0, 8);
            if (filehead != "C#LE-DLL") return null;
            instream.Read(buf, 0, 2);
            UInt16 count = BitConverter.ToUInt16(buf, 0);
            for (int i = 0; i < count; i++)
            {
                int slen = instream.ReadByte();
                byte[] buffilename = new byte[slen];
                instream.Read(buffilename, 0, slen);
                string key = System.Text.Encoding.UTF8.GetString(buffilename, 0, slen);
                var tlist = tokenParser.ReadTokenList(instream);
                project[key] = tlist;
            }
            return project;

        }
    }
}
