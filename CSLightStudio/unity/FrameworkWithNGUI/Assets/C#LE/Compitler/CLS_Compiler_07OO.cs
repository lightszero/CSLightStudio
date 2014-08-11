using System;
using System.Collections.Generic;
using System.Text;
namespace CSLE
{
    public class NotScipt : Attribute
    {

    }
    public partial class CLS_Expression_Compiler : ICLS_Expression_Compiler
    {

        IList<ICLS_Type> _FileCompiler(string filename, IList<Token> tokens, bool embDeubgToken, ICLS_Environment env, bool onlyGotType = false)
        {
            List<ICLS_Type> typelist = new List<ICLS_Type>();

            List<string> usingList = new List<string>();
            //识别using

            //扫描token有没有要合并的类型
            //using的实现在token级别处理即可
            bool bJumpClass = false;
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
                    string useText = "";
                    for (int j = 0; j < list.Count; j++)
                    {
                        useText += list[j];
                        if (j != list.Count - 1)
                        {
                            useText += ".";
                        }
                    }
                    usingList.Add(useText);
                    i = iend;
                    continue;
                }

                if (tokens[i].type == TokenType.PUNCTUATION && tokens[i].text == "[")
                {
                    if (tokens[i + 1].text == "NotScipt" || (tokens[i + 1].text == "CSLE" && tokens[i + 3].text == "NotScipt"))
                    {
                        bJumpClass = true;
                        i = i + 2;
                        continue;
                    }
                }
                if (tokens[i].type == TokenType.KEYWORD && (tokens[i].text == "class" || tokens[i].text == "interface"))
                {
                    string name = tokens[i + 1].text;
                    //在这里检查继承
                    List<string> typebase = null;
                    int ibegin = i + 2;
                    if (onlyGotType)
                    {
                        while (tokens[ibegin].text != "{")
                        {
                            ibegin++;
                        }
                    }
                    else
                    {
                        if (tokens[ibegin].text == ":")
                        {
                            typebase = new List<string>();
                            ibegin++;
                        }
                        while (tokens[ibegin].text != "{")
                        {
                            if (tokens[ibegin].type == TokenType.TYPE)
                            {
                                typebase.Add(tokens[ibegin].text);
                            }
                            ibegin++;
                        }
                    }
                    int iend = FindBlock(env, tokens, ibegin);
                    if (bJumpClass)
                    {
                        env.logger.Log("(NotScript)findclass:" + name + "(" + ibegin + "," + iend + ")");
                    }
                    else if (onlyGotType)
                    {
                        env.logger.Log("(scriptPreParser)findclass:" + name + "(" + ibegin + "," + iend + ")");

                    }
                    else
                    {
                        env.logger.Log("(scriptParser)findclass:" + name + "(" + ibegin + "," + iend + ")");

                    }
                    if (bJumpClass)
                    {//忽略这个Class
                        //ICLS_Type type = Compiler_Class(env, name, (tokens[i].text == "interface"), filename, tokens, ibegin, iend, embDeubgToken, true);
                        //bJumpClass = false;
                    }
                    else
                    {
                        ICLS_Type type = Compiler_Class(env, name, (tokens[i].text == "interface"), typebase, filename, tokens, ibegin, iend, embDeubgToken, onlyGotType, usingList);
                        if (type != null)
                        {
                            typelist.Add(type);
                        }
                    }
                    i = iend;
                    continue;
                }
            }

            return typelist;
        }
        ICLS_Type Compiler_Class(ICLS_Environment env, string classname, bool bInterface, IList<string> basetype, string filename, IList<Token> tokens, int ibegin, int iend, bool EmbDebugToken, bool onlyGotType = false, IList<string> usinglist = null)
        {

            CLS_Type_Class stype = env.GetTypeByKeywordQuiet(classname) as CLS_Type_Class;

            if (stype == null)
                stype = new CLS_Type_Class(classname, bInterface, filename);

            if (basetype != null && basetype.Count != 0 && onlyGotType == false)
            {
                List<ICLS_Type> basetypess = new List<ICLS_Type>();
                foreach (var t in basetype)
                {
                    ICLS_Type type = env.GetTypeByKeyword(t);
                    basetypess.Add(type);
                }
                stype.SetBaseType(basetypess);
            }

            if (onlyGotType) return stype;

            //if (env.useNamespace && usinglist != null)
            //{//使用命名空间,替换token

            //    List<Token> newTokens = new List<Token>();
            //    for (int i = ibegin; i <= iend; i++)
            //    {
            //        if (tokens[i].type == TokenType.IDENTIFIER)
            //        {
            //            string ntype = null;
            //            string shortname = tokens[i].text;
            //            int startpos = i;
            //            while (ntype == null)
            //            {

            //                foreach (var u in usinglist)
            //                {
            //                    string ttype = u + "." + shortname;
            //                    if (env.GetTypeByKeywordQuiet(ttype) != null)
            //                    {
            //                        ntype = ttype;

            //                        break;
            //                    }

            //                }
            //                if (ntype != null) break;
            //                if ((startpos + 2) <= iend && tokens[startpos + 1].text == "." && tokens[startpos + 2].type == TokenType.IDENTIFIER)
            //                {
            //                    shortname += "." + tokens[startpos + 2].text;

            //                    startpos += 2;
            //                    if (env.GetTypeByKeywordQuiet(shortname) != null)
            //                    {
            //                        ntype = shortname;

            //                        break;
            //                    }
            //                    continue;
            //                }
            //                else
            //                {
            //                    break;
            //                }
            //            }
            //            if (ntype != null)
            //            {
            //                var t = tokens[i];
            //                t.text = ntype;
            //                t.type = TokenType.TYPE;
            //                newTokens.Add(t);
            //                i = startpos;
            //                continue;
            //            }
            //        }
            //        newTokens.Add(tokens[i]);
            //    }
            //    tokens = newTokens;
            //    ibegin = 0;
            //    iend = tokens.Count - 1;
            //}

            stype.compiled = false;
            (stype.function as SType).functions.Clear();
            (stype.function as SType).members.Clear();
            //搜寻成员定义和函数
            //定义语法            //Type id[= expr];
            //函数语法            //Type id([Type id,]){block};
            //属性语法            //Type id{get{},set{}};
            bool bPublic = false;
            bool bStatic = false;
            if (EmbDebugToken)//SType 嵌入Token
            {
                stype.EmbDebugToken(tokens);
            }
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

                        if (tokens[i].text == classname && tokens[i + 1].text == "(")
                        {//构造函数
                            bctor = true;
                            i--;
                        }
                        else if (tokens[i].text == "void")
                        {

                        }
                        else
                        {
                            idtype = env.GetTypeByKeyword(tokens[i].text);
                        }
                    }

                    if (tokens[i + 1].type == CSLE.TokenType.IDENTIFIER || bctor) //类型后面是名称
                    {
                        string idname = tokens[i + 1].text;
                        if (tokens[i + 2].type == CSLE.TokenType.PUNCTUATION && tokens[i + 2].text == "(")//参数开始,这是函数
                        {
                            logger.Log("发现函数:" + idname);
                            SType.Function func = new SType.Function();
                            func.bStatic = bStatic;
                            func.bPublic = bPublic;

                            int funcparambegin = i + 2;
                            int funcparamend = FindBlock(env, tokens, funcparambegin);
                            if (funcparamend - funcparambegin > 1)
                            {


                                //Dictionary<string, ICLS_Type> _params = new Dictionary<string, ICLS_Type>();
                                for (int j = funcparambegin; j <= funcparamend; j++)
                                {
                                    if (tokens[j].text == "," || tokens[j].text == ")")
                                    {
                                        var ptype = tokens[j - 2].text;
                                        var pid = tokens[j - 1].text;
                                        var type = env.GetTypeByKeyword(ptype);
                                        // _params[pid] = type;
                                        //func._params.Add(pid, type);
                                        func._paramnames.Add(pid);
                                        func._paramtypes.Add(type);
                                    }
                                }
                            }

                            int funcbegin = funcparamend + 1;
                            if (tokens[funcbegin].text == "{")
                            {
                                int funcend = FindBlock(env, tokens, funcbegin);
                                this.Compiler_Expression_Block(tokens, env, funcbegin, funcend, out func.expr_runtime);

                                (stype.function as SType).functions.Add(idname, func);

                                i = funcend;
                            }
                            else if (tokens[funcbegin].text == ";")
                            {

                                func.expr_runtime = null;
                                (stype.function as SType).functions.Add(idname, func);
                                i = funcbegin;
                            }
                            else
                            {
                                throw new Exception("不可识别的函数表达式");
                            }
                        }
                        else if (tokens[i + 2].type == CSLE.TokenType.PUNCTUATION && tokens[i + 2].text == "{")//语句块开始，这是 getset属性
                        {
                            //get set 成员定义

                            bool setpublic = true;
                            bool haveset = false;
                            for (int j = i + 3; j <= iend; j++)
                            {
                                if (tokens[j].text == "get")
                                {
                                    setpublic = true;
                                }
                                if (tokens[j].text == "private")
                                {
                                    setpublic = false;
                                }
                                if (tokens[j].text == "set")
                                {
                                    haveset = true;
                                }
                                if (tokens[j].text == "}")
                                {
                                    break;
                                }
                            }


                            var member = new SType.Member();
                            member.bStatic = bStatic;
                            member.bPublic = bPublic;
                            member.bReadOnly = !(haveset && setpublic);
                            member.type = idtype;
                            logger.Log("发现Get/Set:" + idname);
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
                        else if (tokens[i + 2].type == CSLE.TokenType.PUNCTUATION && (tokens[i + 2].text == "=" || tokens[i + 2].text == ";"))//这是成员定义
                        {
                            logger.Log("发现成员定义:" + idname);

                            var member = new SType.Member();
                            member.bStatic = bStatic;
                            member.bPublic = bPublic;
                            member.bReadOnly = false;
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



        int FindBlock(ICLS_Environment env, IList<CSLE.Token> tokens, int start)
        {
            if (tokens[start].type != CSLE.TokenType.PUNCTUATION)
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
                if (tokens[i].type == CSLE.TokenType.PUNCTUATION)
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