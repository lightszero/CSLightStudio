using System;
using System.Collections.Generic;
using System.Text;
namespace CSEvil
{
    public partial class CLS_Expression_Compiler : ICLS_Expression_Compiler
    {

        IList<ICLS_Type> _FileCompiler(IList<Token> tokens, ICLS_Environment env, bool onlyGotType = false)
        {
            List<ICLS_Type> typelist = new List<ICLS_Type>();

            List<List<string>> usingList = new List<List<string>>();
            //识别using

            //扫描token有没有要合并的类型
            //using的实现在token级别处理即可
            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].type == TokenType.PUNCTUATION && tokens[i].text == ";")
                    continue;
                if (tokens[i].type == TokenType.COMMENT)
                    continue;
                if (tokens[i].type == TokenType.KEYWORD && tokens[i].text == "using")
                {
                    int dep;
                    int pos = i;
                    int iend = FindCodeAny(tokens, ref pos, out dep);
                    var list = Compiler_Using(tokens, env, pos, iend);
                    usingList.Add(list);
                    i = iend;
                    continue;
                }
                if (tokens[i].type == TokenType.KEYWORD && tokens[i].text == "class")
                {
                    string name = tokens[i + 1].text;
                    int ibegin = i + 2;
                    while (tokens[ibegin].text != "{")
                        ibegin++;
                    int iend = FindBlock(env, tokens, ibegin);
                    if (onlyGotType)
                    {
                        env.logger.Log("(scriptPreParser)findclass:" + name + "(" + ibegin + "," + iend + ")");

                    }
                    else
                    {
                        env.logger.Log("(scriptParser)findclass:" + name + "(" + ibegin + "," + iend + ")");

                    }
                    ICLS_Type type = Compiler_Class(env, name, tokens, ibegin, iend, onlyGotType);
                    if (type != null)
                    {
                        typelist.Add(type);
                    }
                    i = iend;
                    continue;
                }
            }

            return typelist;
        }
        ICLS_Type Compiler_Class(ICLS_Environment env, string classname, IList<Token> tokens, int ibegin, int iend, bool onlyGotType = false)
        {

            CLS_Type_Class stype = env.GetTypeByKeywordQuiet(classname) as CLS_Type_Class;
            if (stype == null)
                stype = new CLS_Type_Class(classname);
            if (onlyGotType) return stype;

            stype.compiled = false;
            (stype.function as SType).functions.Clear();
            (stype.function as SType).members.Clear();
            //搜寻成员定义和函数
            //定义语法            //Type id[= expr];
            //函数语法            //Type id([Type id,]){block};
            //属性语法            //Type id{get{},set{}};
            bool bPublic = false;
            bool bStatic = false;

            for (int i = ibegin; i <= iend; i++)
            {

                if (tokens[i].type == TokenType.KEYWORD && tokens[i].text == "public")
                {
                    bPublic = true;
                    continue;
                }
                else if (tokens[i].type == TokenType.KEYWORD && tokens[i].text == "private")
                {
                    bPublic = false;
                    continue;
                }
                else if (tokens[i].type == TokenType.KEYWORD && tokens[i].text == "static")
                {
                    bStatic = true;
                    continue;
                }
                else if (tokens[i].type == TokenType.TYPE || (tokens[i].type == TokenType.IDENTIFIER && tokens[i].text == classname))//发现类型
                {

                    ICLS_Type idtype = env.GetTypeByKeyword("null");
                    bool bctor = false;
                    if (tokens[i].type == TokenType.TYPE)//类型
                    {
          
                        if (tokens[i].text == classname && tokens[i+1].text=="(")
                        {//构造函数
                            bctor = true;
                            i--;
                        }
                        else if(tokens[i].text=="void")
                        {

                        }
                        else
                        {
                            idtype = env.GetTypeByKeyword(tokens[i].text);
                        }
                    }

                    if (tokens[i + 1].type == CSEvil.TokenType.IDENTIFIER || bctor) //类型后面是名称
                    {
                        string idname = tokens[i + 1].text;
                        if (tokens[i + 2].type == CSEvil.TokenType.PUNCTUATION && tokens[i + 2].text == "(")//参数开始,这是函数
                        {
                            logger.Log("发现函数:" + idname);
                            SType.Function func = new SType.Function();
                            func.bStatic = bStatic;
                            func.bPublic = bPublic;

                            int funcparambegin = i + 2;
                            int funcparamend = FindBlock(env, tokens, funcparambegin);
                            //Dictionary<string, ICLS_Type> _params = new Dictionary<string, ICLS_Type>();
                            for (int j = funcparambegin; j <= funcparambegin; j++)
                            {
                                if (tokens[j].text == "," || tokens[j].text == ")")
                                {
                                    var ptype = tokens[j - 2].text;
                                    var pid = tokens[j - 1].text;
                                    var type = env.GetTypeByKeyword(ptype);
                                    // _params[pid] = type;
                                    func._params.Add(pid, type);
                                }
                            }

                            int funcbegin = funcparamend + 1;
                            int funcend = FindBlock(env, tokens, funcbegin);

                            ICLS_Expression funcexpr;
                            this.Compiler_Expression_Block(tokens, env, funcbegin, funcend, out func.expr_runtime);

                            (stype.function as SType).functions.Add(idname, func);

                            i = funcend;
                        }
                        else if (tokens[i + 2].type == CSEvil.TokenType.PUNCTUATION && tokens[i + 2].text == "{")//语句块开始，这是 getset属性
                        {
                            throw new Exception("未支持getset");
                        }
                        else if (tokens[i + 2].type == CSEvil.TokenType.PUNCTUATION && (tokens[i + 2].text == "=" || tokens[i + 2].text == ";"))//这是成员定义
                        {
                            logger.Log("发现成员定义:" + idname);

                            var member = new SType.Member();
                            member.bStatic = bStatic;
                            member.bPublic = bPublic;
                            member.type = idtype;

                            ICLS_Expression expr = null;

                            if (tokens[i + 2].text == "=")
                            {
                                int jbegin = i + 3;
                                int jdep;
                                int jend = FindCodeAny(tokens, ref jbegin, out jdep);

                                bool b = Compiler_Expression(tokens, env, jbegin, jend, out  member.expr_defvalue);
                                i = jend;
                            }
                            (stype.function as SType).members.Add(idname, member);
                        }

                        bPublic = false;
                        bStatic = false;

                        continue;
                    }
                    else
                    {
                        throw new Exception("不可识别的表达式");
                    }
                }
            }
            stype.compiled = true;
            return stype;
        }

        List<string> Compiler_Using(IList<Token> tokens, ICLS_Environment env, int pos, int posend)
        {
            List<string> _namespace = new List<string>();

            for (int i = pos + 1; i <= posend; i++)
            {
                if (tokens[i].type == TokenType.PUNCTUATION && tokens[i].text == ".")
                    continue;
                else
                    _namespace.Add(tokens[i].text);
            }
            return _namespace;
        }
        //Dictionary<string, functioninfo> funcs = new Dictionary<string, functioninfo>();



        int FindBlock(ICLS_Environment env, IList<CSEvil.Token> tokens, int start)
        {
            if (tokens[start].type != CSEvil.TokenType.PUNCTUATION)
            {
                env.logger.Log_Error("(script)FindBlock 没有从符号开始");
            }
            string left = tokens[start].text;
            string right = "}";
            if (left == "{") right = "}";
            if (left == "(") right = ")";
            if (left == "[") right = "]";
            int depth = 0;
            for (int i = start; i < tokens.Count; i++)
            {
                if (tokens[i].type == CSEvil.TokenType.PUNCTUATION)
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
    }
}