using System;
using System.Collections.Generic;
using System.Text;

namespace CSLE
{
    public enum TokenType
    {
        UNKNOWN,
        KEYWORD,        //关键字
        PUNCTUATION,    //标点
        IDENTIFIER,     //标识符 变量与函数
        TYPE,           //类型
        COMMENT,        //注释
        VALUE,          //数值
        STRING,         //字符串
    }
    public struct Token
    {
        //asTC_UNKNOWN    = 0,//未知
        //asTC_KEYWORD    = 1,//关键字
        //asTC_VALUE      = 2,//value   值
        //asTC_IDENTIFIER = 3,//标识符  变量
        //asTC_COMMENT    = 4,//注释
        //asTC_WHITESPACE = 5,//空格

        public string text;
        public int pos;
        public int line;
        public TokenType type;
        public override string ToString()
        {
            return type.ToString() + "|" + text + "|" + pos.ToString();
        }
    }
    public interface ICLS_TokenParser
    {
        List<string> types
        {
            get;
        }
        List<string> keywords
        {
            get;
        }
        IList<Token> Parse(string line);

        void SaveTokenList(IList<Token> tokens, System.IO.Stream stream);

        IList<Token> ReadTokenList(System.IO.Stream stream);

    }
    public class CLS_TokenParser : ICLS_TokenParser
    {
        public CLS_TokenParser()
        {
            types = new List<string>();
            keywords = new List<string>();
            types.Add("void");
            types.Add("bool");
            types.Add("int");
            types.Add("uint");
            types.Add("float");
            types.Add("double");
            types.Add("string");


            keywords.Add("if");
            keywords.Add("as");
            keywords.Add("else");
            keywords.Add("break");
            keywords.Add("continue");
            keywords.Add("for");
            keywords.Add("do");
            keywords.Add("while");
            keywords.Add("trace");
            keywords.Add("return");
            keywords.Add("true");
            keywords.Add("false");
            keywords.Add("null");
            keywords.Add("new");
            keywords.Add("foreach");
            keywords.Add("in");
            //OO支持 新增关键字
            keywords.Add("class");
            keywords.Add("interface");

            keywords.Add("using");
            keywords.Add("public");
            keywords.Add("private");
            keywords.Add("static");

            keywords.Add("try");
            keywords.Add("catch");
            keywords.Add("throw");

        }
        public List<string> types
        {
            get;
            private set;
        }
        public List<string> keywords
        {
            get;
            private set;
        }

        int FindStart(string lines, int npos)
        {
            int n = npos;
            for (int i = n; i < lines.Length; i++)
            {
                if (lines[i] == '\n')
                    line++;
                if (!char.IsSeparator(lines, i) && lines[i] != '\n' && lines[i] != '\r' && lines[i] != '\t')
                {
                    return i;
                }
            }
            return -1;
        }
        int GetToken(string line, int nstart, out Token t)
        {
            //找到开始字符
            t.pos = nstart;
            t.line = this.line;
            t.text = " ";
            t.type = TokenType.UNKNOWN;
            if (nstart < 0) return -1;
            if (line[nstart] == '\"')
            {
                //字符串查找
                int nend = line.IndexOf('\"', nstart + 1);
                int nsub = line.IndexOf('\\', nstart + 1);
                while (nsub > 0 && nsub < nend)
                {
                    nend = line.IndexOf('\"', nsub + 2);
                    nsub = line.IndexOf('\\', nsub + 2);

                }
                if (nend - nstart + 1 < 1) throw new Exception("查找字符串失败");
                t.type = TokenType.STRING;
                int pos = nend + 1;
                t.text = line.Substring(nstart, nend - nstart + 1);
                t.text = t.text.Replace("\\\"", "\"");
                t.text = t.text.Replace("\\\'", "\'");
                t.text = t.text.Replace("\\\\", "\\");
                t.text = t.text.Replace("\\0", "\0");
                t.text = t.text.Replace("\\a", "\a");
                t.text = t.text.Replace("\\b", "\b");
                t.text = t.text.Replace("\\f", "\f");
                t.text = t.text.Replace("\\n", "\n");
                t.text = t.text.Replace("\\r", "\r");
                t.text = t.text.Replace("\\t", "\t");
                t.text = t.text.Replace("\\v", "\v");
                int sp = t.text.IndexOf('\\');
                if (sp > 0)
                {
                    throw new Exception("不可识别的转义序列:" + t.text.Substring(sp));
                }
                return pos;
            }
            else if (line[nstart] == '/')// / /= 注释
            {

                if (nstart < line.Length - 1 && line[nstart + 1] == '=')
                {
                    t.type = TokenType.PUNCTUATION;
                    t.text = line.Substring(nstart, 2);
                }
                else if (nstart < line.Length - 1 && line[nstart + 1] == '/')
                {
                    t.type = TokenType.COMMENT;
                    int enterpos = line.IndexOf('\n', nstart + 2);
                    if (enterpos < 0) t.text = line.Substring(nstart);
                    else
                        t.text = line.Substring(nstart, line.IndexOf('\n', nstart + 2) - nstart);
                }
                else
                {
                    t.type = TokenType.PUNCTUATION;
                    t.text = line.Substring(nstart, 1);
                }
                return nstart + t.text.Length;
            }
            else if (line[nstart] == '=')//= == =>
            {
                t.type = TokenType.PUNCTUATION;
                if (nstart < line.Length - 1 && line[nstart + 1] == '=')
                    t.text = line.Substring(nstart, 2);
                else if (nstart < line.Length - 1 && line[nstart + 1] == '>')
                    t.text = line.Substring(nstart, 2);
                else
                    t.text = line.Substring(nstart, 1);
                return nstart + t.text.Length;
            }
            else if (line[nstart] == '!')//= ==
            {
                t.type = TokenType.PUNCTUATION;
                if (nstart < line.Length - 1 && line[nstart + 1] == '=')
                    t.text = line.Substring(nstart, 2);
                else
                    t.text = line.Substring(nstart, 1);
                return nstart + t.text.Length;
            }
            else if (line[nstart] == '+')//+ += ++
            {
                t.type = TokenType.PUNCTUATION;
                if (nstart < line.Length - 1 && (line[nstart + 1] == '=' || line[nstart + 1] == '+'))
                    t.text = line.Substring(nstart, 2);
                else
                    t.text = line.Substring(nstart, 1);
                return nstart + t.text.Length;
            }
            //通用的一元二元运算符检查
            else if (line[nstart] == '-')//- -= -- 负数也先作为符号处理
            {
                t.type = TokenType.PUNCTUATION;
                if (nstart < line.Length - 1 && line[nstart + 1] == '=' || line[nstart + 1] == '-')
                    t.text = line.Substring(nstart, 2);
                else
                    t.text = line.Substring(nstart, 1);
                return nstart + t.text.Length;
            }
            else if (line[nstart] == '*')//* *=
            {
                t.type = TokenType.PUNCTUATION;
                if (nstart < line.Length - 1 && line[nstart + 1] == '=')
                    t.text = line.Substring(nstart, 2);
                else
                    t.text = line.Substring(nstart, 1);
                return nstart + t.text.Length;
            }
            else if (line[nstart] == '/')/// /=
            {
                t.type = TokenType.PUNCTUATION;
                if (nstart < line.Length - 1 && line[nstart + 1] == '=')
                    t.text = line.Substring(nstart, 2);
                else
                    t.text = line.Substring(nstart, 1);
                return nstart + t.text.Length;
            }
            else if (line[nstart] == '%')/// /=
            {
                t.type = TokenType.PUNCTUATION;
                if (nstart < line.Length - 1 && line[nstart + 1] == '=')
                    t.text = line.Substring(nstart, 2);
                else
                    t.text = line.Substring(nstart, 1);
                return nstart + t.text.Length;
            }
            else if (line[nstart] == '>')//> >=
            {
                t.type = TokenType.PUNCTUATION;
                if (nstart < line.Length - 1 && line[nstart + 1] == '=')
                    t.text = line.Substring(nstart, 2);
                else
                    t.text = line.Substring(nstart, 1);
                return nstart + t.text.Length;
            }
            else if (line[nstart] == '<')//< <=
            {
                t.type = TokenType.PUNCTUATION;
                if (nstart < line.Length - 1 && line[nstart + 1] == '=')
                    t.text = line.Substring(nstart, 2);
                else
                    t.text = line.Substring(nstart, 1);
                return nstart + t.text.Length;
            }

            else if (line[nstart] == '&')//&&
            {
                t.type = TokenType.PUNCTUATION;
                if (nstart < line.Length - 1 && line[nstart + 1] == '&')
                    t.text = line.Substring(nstart, 2);
                else
                    return -1;
            }
            else if (line[nstart] == '|')//||
            {
                t.type = TokenType.PUNCTUATION;
                if (nstart < line.Length - 1 && line[nstart + 1] == '|')
                    t.text = line.Substring(nstart, 2);
                else
                    return -1;
            }
            else if (char.IsLetter(line, nstart) || line[nstart] == '_')
            {
                //字母逻辑
                //判断完整性
                int i = nstart + 1;
                while (i < line.Length && (char.IsLetterOrDigit(line, i) || line[i] == '_'))
                {
                    i++;
                }
                t.text = line.Substring(nstart, i - nstart);
                //判断字母类型： 关键字 类型 标识符
                if (keywords.Contains(t.text))
                //foreach (string s in keywords)
                {
                    //if (t.text == s)
                    {
                        t.type = TokenType.KEYWORD;
                        return nstart + t.text.Length;
                    }
                }
                if (types.Contains(t.text))
                //foreach (string s in types)
                {
                    //if (t.text == s)
                    {
                      
                        while (line[i] == ' ' && i < line.Length)
                        {
                            i++;
                        }
                        if (line[i] == '<')/*  || line[i] == '['*/
                        {
                            int dep = 0;
                            string text = t.text;
                            while (i < line.Length)
                            {
                                if (line[i] == '<') dep++;
                                if (line[i] == '>') dep--;
                                if (line[i] == ';' || line[i] == '(' || line[i] == '{')
                                {
                                    break;
                                }
                                if (line[i] != ' ') text += line[i];
                                i++;
                                if (dep == 0)
                                {
                                    t.text = text;
                                    break;
                                }
                            }
                            if (types.Contains(t.text))
                            {
                                t.type = TokenType.TYPE;
                                return i;
                            }

                        }
                        else
                        {
                              t.type = TokenType.TYPE;
                            return nstart + t.text.Length;
                        }
                    }
                }
                while (line[i] == ' ' && i < line.Length)
                {
                    i++;
                }
                if (i < line.Length && (line[i] == '<'/* || line[i] == '['*/))//检查特别类型
                {
                    int dep = 0;
                    string text = t.text;
                    while (i < line.Length)
                    {
                        if (line[i] == '<') dep++;
                        if (line[i] == '>') dep--;
                        if(line[i]==';'||line[i]=='('||line[i]=='{')
                        {
                            break;
                        }
                        if (line[i] != ' ') text += line[i];
                        i++;
                        if (dep == 0)
                        {
                            t.text = text;
                            break;
                        }
                    }
                    if (types.Contains(t.text))
                    {
                        t.type = TokenType.TYPE;
                        return i;
                    }

                    //foreach (string s in types)
                    //{
                    //    if (s.Length > t.text.Length && line.IndexOf(s, nstart) == nstart)
                    //    {
                    //        t.type = TokenType.TYPE;
                    //        t.text = s;
                    //        return nstart + s.Length;
                    //    }

                    //}
                }
                t.type = TokenType.IDENTIFIER;
                return nstart + t.text.Length;
            }
            else if (char.IsPunctuation(line, nstart))
            {



                //else
                {
                    t.type = TokenType.PUNCTUATION;
                    t.text = line.Substring(nstart, 1);
                    return nstart + t.text.Length;
                }
                //符号逻辑
                //-号逻辑
                //"号逻辑
                ///逻辑
                //其他符号
            }
            else if (char.IsNumber(line, nstart))
            {
                //数字逻辑
                //判断数字合法性

                if (line[nstart] == '0' && line[nstart + 1] == 'x') //0x....
                {
                    int iend = nstart + 2;
                    for (int i = nstart + 2; i < line.Length; i++)
                    {
                        if (char.IsNumber(line, i))
                        {
                            iend = i;
                        }
                        else
                        {
                            break;
                        }
                    }
                    t.type = TokenType.VALUE;
                    t.text = line.Substring(nstart, iend - nstart + 1);
                }
                else
                {
                    //纯数字

                    int iend = nstart;
                    for (int i = nstart + 1; i < line.Length; i++)
                    {
                        if (char.IsNumber(line, i))
                        {
                            iend = i;
                        }
                        else
                        {
                            break;
                        }
                    }
                    t.type = TokenType.VALUE;
                    int dend = iend + 1;
                    if (dend < line.Length && line[dend] == '.')
                    {
                        int fend = dend;
                        for (int i = dend + 1; i < line.Length; i++)
                        {
                            if (char.IsNumber(line, i))
                            {
                                fend = i;
                            }
                            else
                            {
                                break;
                            }
                        }
                        if (fend + 1 < line.Length && line[fend + 1] == 'f')
                        {
                            t.text = line.Substring(nstart, fend + 2 - nstart);

                        }
                        else
                        {
                            t.text = line.Substring(nstart, fend + 1 - nstart);
                        }
                        //.111
                        //.123f
                    }
                    else
                    {
                        if (dend < line.Length && line[dend] == 'f')
                        {
                            t.text = line.Substring(nstart, dend - nstart + 1);
                        }
                        else
                        {
                            t.text = line.Substring(nstart, dend - nstart);
                        }
                    }

                }
                return nstart + t.text.Length;
            }
            else
            {
                //不可识别逻辑
                int i = nstart + 1;
                while (i < line.Length - 1 && char.IsSeparator(line, i) == false && line[i] != '\n' && line[i] != '\r' && line[i] != '\t')
                {
                    i++;
                }
                t.text = line.Substring(nstart, i - nstart);
                return nstart + t.text.Length;
            }
            //
            //    -逻辑
            //
            //    "逻辑
            //
            //    /逻辑
            //
            //    其他符号逻辑


            return nstart + t.text.Length;
        }
        int line = 0;
        public IList<Token> Parse(string lines)
        {
            line = 1;
            List<Token> ts = new List<Token>();
            int n = 0;
            while (n >= 0)
            {
                Token t;
                t.line = this.line;

                int nstart = FindStart(lines, n);
                t.line = this.line;
                int nend = GetToken(lines, nstart, out t);
                if (nend >= 0)
                {
                    for (int i = nstart; i < nend; i++)
                    {
                        if (lines[i] == '\n')
                            line++;
                    }
                }
                n = nend;
                if (n >= 0)
                {
                    if (ts.Count >= 2 && t.type == TokenType.IDENTIFIER && ts[ts.Count - 1].text == "." && ts[ts.Count - 2].type == TokenType.TYPE)
                    {
                        string ntype = ts[ts.Count - 2].text + ts[ts.Count - 1].text + t.text;
                        if (types.Contains(ntype))
                        {//类中类，合并之
                            t.type = TokenType.TYPE;
                            t.text = ntype;
                            t.pos = ts[ts.Count - 2].pos;
                            t.line = ts[ts.Count - 2].line;
                            ts.RemoveAt(ts.Count - 1);
                            ts.RemoveAt(ts.Count - 1);

                        }
                    }
                    if (ts.Count >= 3 && t.type == TokenType.PUNCTUATION && t.text == ">"
                        && ts[ts.Count - 1].type == TokenType.TYPE
                        && ts[ts.Count - 2].type == TokenType.PUNCTUATION && ts[ts.Count - 2].text == "<"
                        && ts[ts.Count - 3].type == TokenType.IDENTIFIER)
                    {//模板函数调用,合并之
                        string ntype = ts[ts.Count - 3].text + ts[ts.Count - 2].text + ts[ts.Count - 1].text + t.text;
                        t.type = TokenType.IDENTIFIER;
                        t.text = ntype;
                        t.pos = ts[ts.Count - 2].pos;
                        t.line = ts[ts.Count - 2].line;
                        ts.RemoveAt(ts.Count - 1);
                        ts.RemoveAt(ts.Count - 1);
                        ts.RemoveAt(ts.Count - 1);
                    }


                    ts.Add(t);

                }
            }
            return ts;
        }

        public void SaveTokenList(IList<Token> tokens, System.IO.Stream stream)
        {
            Dictionary<string, UInt32> strs = new Dictionary<string, UInt32>();
            List<UInt16> lines = new List<ushort>();
            List<string> strstore = new List<string>();
            List<UInt32> strindex = new List<UInt32>();
            if (tokens.Count > 0xffff)
            {
                throw new Exception("不支持这么复杂的token保存");
            }
            byte[] bs = BitConverter.GetBytes((UInt16)tokens.Count);
            stream.Write(bs, 0, bs.Length);
            int index = 0;
            foreach (var t in tokens)
            {
                UInt32 type = (UInt32)t.type;
                if (strs.ContainsKey(t.text) == false)
                {
                    strstore.Add(t.text);
                    strs[t.text] = (uint)(strs.Count * 0x0100);
                }

                type += strs[t.text];
                strindex.Add(type);
                int line = t.line - 1;
                while (line >= lines.Count)
                    lines.Add((UInt16)index);

                index++;
            }

            byte[] bsstr = BitConverter.GetBytes((UInt16)strstore.Count);
            stream.Write(bsstr, 0, bsstr.Length);
            foreach (var s in strstore)
            {
                byte[] sbs = System.Text.Encoding.UTF8.GetBytes(s);
                byte[] sbslen = BitConverter.GetBytes((UInt16)sbs.Length);

                stream.Write(sbslen, 0, sbslen.Length);
                stream.Write(sbs, 0, sbs.Length);
            }
            foreach (var i in strindex)
            {
                byte[] nbs = BitConverter.GetBytes((UInt32)i);
                stream.Write(nbs, 0, nbs.Length);
            }
            UInt16 linecount = (UInt16)lines.Count;
            stream.Write(BitConverter.GetBytes(linecount), 0, 2);
            foreach (UInt16 lstarttoken in lines)
            {
                byte[] nbs = BitConverter.GetBytes(lstarttoken);
                stream.Write(nbs, 0, 2);
            }
        }
        public IList<Token> ReadTokenList(System.IO.Stream stream)
        {
            byte[] bs = new byte[0xffff];
            stream.Read(bs, 0, 2);
            UInt16 len = BitConverter.ToUInt16(bs, 0);
            stream.Read(bs, 0, 2);
            UInt16 lenstr = BitConverter.ToUInt16(bs, 0);

            List<string> strstore = new List<string>();
            List<Token> tokens = new List<Token>();
            for (int i = 0; i < lenstr; i++)
            {
                stream.Read(bs, 0, 2);
                UInt16 slen = BitConverter.ToUInt16(bs, 0);
                stream.Read(bs, 0, slen);
                strstore.Add(System.Text.Encoding.UTF8.GetString(bs, 0, slen));
            }
            for (int i = 0; i < len; i++)
            {
                Token t = new Token();
                stream.Read(bs, 0, 4);
                UInt32 type = BitConverter.ToUInt32(bs, 0);
                t.type = (TokenType)(type % 0x0100);
                t.text = strstore[(int)(type / 0x100)];
                tokens.Add(t);
            }

            if (stream.Position < stream.Length)
            {
                UInt16 linecount = 0;
                byte[] bufu = new byte[2];
                stream.Read(bufu, 0, 2);
                linecount = BitConverter.ToUInt16(bufu, 0);
                UInt16[] linetoken = new UInt16[linecount];

                for (int i = 0; i < linecount; i++)
                {
                    stream.Read(bufu, 0, 2);
                    linetoken[i] = BitConverter.ToUInt16(bufu, 0);

                }
                int token = 0;
                for (int i = 0; i < linecount; i++)
                {
                    if ((i + 1) < linecount && linetoken[i + 1] == linetoken[i]) continue;

                    if ((i + 1) < linecount)
                    {
                        for (int j = linetoken[i]; j < linetoken[i + 1]; j++)
                        {
                            var t = tokens[j];
                            t.line = i + 1;
                            tokens[j] = t;
                        }
                    }
                    else
                    {
                        for (int j = linetoken[i]; j < tokens.Count; j++)
                        {
                            var t = tokens[j];
                            t.line = i + 1;
                            tokens[j] = t;
                        }
                    }

                    token = linetoken[i];
                }
            }
            return tokens;
        }
    }
}
