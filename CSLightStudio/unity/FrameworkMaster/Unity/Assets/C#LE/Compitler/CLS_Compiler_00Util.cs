using System;
using System.Collections.Generic;
using System.Text;
namespace CSLE
{
    public partial class CLS_Expression_Compiler : ICLS_Expression_Compiler
    {
        //计算最长的类型
        int GetLongType(IList<Token> tokens, int pos)
        {
            int npos = -1;
            for (int i = pos; i < tokens.Count; i += 2)
            {
                if (tokens[i].type == TokenType.TYPE && i + 1 < tokens.Count)
                {
                    if (tokens[i + 1].type == TokenType.PUNCTUATION && tokens[i + 1].text == ".")
                    {
                    }
                    else
                    {
                        npos = i + 1;
                    }
                }
                else
                {
                    break;
                }

            }
            return npos;
        }
        int GetLongName(IList<Token> tokens, int pos)
        {
            int npos = -1;
            int nstate = 0;//type name
            for (int i = pos; i < tokens.Count; i += 2)
            {
                if (nstate == 0 && tokens[i].type == TokenType.TYPE && i + 1 < tokens.Count)
                {
                    if (tokens[i + 1].type == TokenType.PUNCTUATION && tokens[i + 1].text == ".")
                    {
                    }
                    else
                    {
                        npos = i;
                    }
                }
                else if (tokens[i].type == TokenType.IDENTIFIER && i + 1 < tokens.Count)
                {
                    if (tokens[i + 1].type == TokenType.PUNCTUATION && tokens[i + 1].text == ".")
                    {
                    }
                    else
                    {
                        npos = i;
                    }
                }
                else
                {
                    break;
                }

            }

            if (npos < pos || tokens[npos].type != TokenType.IDENTIFIER) return -1;
            return npos;
        }
        //得到完整的表达式
        int FindFullExpression(IList<Token> tokens, int pos, out bool bdepstart)
        {
            int dep = 0;
            bdepstart = false;
            for (int i = pos; i < tokens.Count; i++)
            {

                if (tokens[i].type == TokenType.PUNCTUATION)
                {
                    if (tokens[i].text == "(" && i == pos)
                    {
                        dep++;
                        bdepstart = true;
                    }
                    if (tokens[i].text == ")")
                    {
                        dep--;
                        if (dep == 0 && bdepstart)//括号开始的表达式，括号结束
                            return i;
                        else if (dep < 0)
                            return i - 1;
                    }
                    if (tokens[i].text == ",")//，结束的表达式
                    {
                        if (dep == 0 && !bdepstart)
                            return i - 1;
                    }
                    if (tokens[i].text == ";")
                    {
                        if (dep == 0)
                            return i - 1;
                        else
                            return -1;
                    }
                }
            }
            return -1;
        }
        int GetExpressionOp(IList<Token> tokens, int pos, int posend)
        {
            List<int> oppos = new List<int>();
            int state = 0;//0 表达式 //1 符号
            for (int i = posend; i >= pos; )
            {
                if (state == 0)
                {
                    if (tokens[i].type == TokenType.PUNCTUATION)
                    {
                        if (tokens[i].text == ")" && i > pos)
                        {
                            int dep = 0;
                            for (int j = i - 1; j >= pos; j--)
                            {
                                if (tokens[j].type == TokenType.PUNCTUATION && tokens[j].text == ")")
                                {
                                    dep++;
                                }
                                else if (tokens[j].type == TokenType.PUNCTUATION && tokens[j].text == "(")
                                {
                                    dep--;
                                    if (dep < 0)
                                    {
                                        if (j - 1 > pos && (tokens[j - 1].type == TokenType.IDENTIFIER || tokens[j - 1].type == TokenType.TYPE))
                                        {
                                            i = j - 2;//函数
                                        }
                                        else
                                        {
                                            i = j - 1;
                                        }
                                        break;
                                    }
                                }

                            }
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else if (tokens[i].type == TokenType.VALUE)
                    {
                        i--;
                        //需要考虑负号的问题
                        if (i == pos || tokens[i - 1].type == TokenType.PUNCTUATION)
                        {
                            i--;
                        }
                    }
                    else if (tokens[i].type == TokenType.STRING || tokens[i].type == TokenType.IDENTIFIER)
                    {
                        i--;
                    }
                    else
                    {
                        return -1;
                    }
                    state = 1;
                }
                else
                {
                    if (tokens[i].type != TokenType.PUNCTUATION) return -1;
                    oppos.Add(i);
                    state = 0;
                    i--;
                }
            }
            if (state == 0) return -1;
            //找出优先级最低的操作符
            int nmax = 0;//优先级
            int npos = -1;//字符
            foreach (int i in oppos)
            {
                int max = 0;
                switch (tokens[i].text)
                {
                    case "<":
                        max = 6;
                        break;
                    case ">":
                        max = 6;
                        break;
                    case "<=":
                        max = 6;
                        break;
                    case ">=":
                        max = 6;
                        break;
                    case "&&":
                        max = 5;
                        break;
                    case "||":
                        max = 5;
                        break;
                    case "==":
                        max = 4;
                        break;
                    case "!=":
                        max = 4;
                        break;
                    case "*":
                        max = 3;
                        break;
                    case "/":
                        max = 3;
                        break;
                    case "+":
                        max = 2;
                        break;
                    case "-":
                        max = 2;
                        break;
                }
                if (max > nmax)
                {
                    nmax = max;
                    npos = i;
                }
            }
            return npos;
        }

        int FindCodeBlock(IList<Token> tokens, int pos)
        {
            int dep = 0;
            for (int i = pos; i < tokens.Count; i++)
            {

                if (tokens[i].type == TokenType.PUNCTUATION)
                {
                    if (tokens[i].text == "{")
                    {
                        dep++;
                    }
                    if (tokens[i].text == "}")
                    {
                        dep--;
                        if (dep < 0)
                            return i - 1;
                    }
                }
            }
            if (dep != 0)
                return -1;
            else
                return tokens.Count - 1;
        }

        int FindCodeAny(IList<Token> tokens, ref int pos, out int depstyle)
        {
            int dep = 0;
            Token? start = null;

            depstyle = 0;
            for (int i = pos; i < tokens.Count; i++)
            {

                if (tokens[i].type == TokenType.COMMENT) //注释忽略
                {
                    continue;
                }
                if (start == null)
                {
                    start = tokens[i];

                    pos = i;
                    if (start.Value.type == TokenType.PUNCTUATION)
                    {
                        if (start.Value.text == "{")
                            depstyle = 2;
                        if (start.Value.text == "(")
                            depstyle = 1;
                        if (start.Value.text == "[")
                            depstyle = 1;
                        //bdepstart = true;
                    }
                    if (start.Value.type == TokenType.KEYWORD)
                    {
                        if (start.Value.text == "for")
                        {
                            return FindCodeKeyWord_For(tokens, i);
                        }
                        if (start.Value.text == "foreach")
                        {
                            return FindCodeKeyWord_ForEach(tokens, i);
                        }
                        if (start.Value.text == "while")
                        {
                            return FindCodeKeyWord_While(tokens, i);
                        }
                        if (start.Value.text == "do")
                        {
                            return FindCodeKeyWord_Dowhile(tokens, i);
                        }
                        if (start.Value.text == "if")
                        {
                            return FindCodeKeyWord_If(tokens, i);

                        }
                        if (start.Value.text == "return")
                        {
                            return FindCodeKeyWord_Return(tokens, i);

                        }
                    }
                    //if (start.Value.type == TokenType.TYPE && i < tokens.Count-1)
                    //{
                    //    if(tokens[i+1].type== TokenType.PUNCTUATION&&tokens[i+1].text==".")
                    //    {
                    //        //staticcall = true;
                    //        i++;
                    //        continue;
                    //    }
                    //}
                }

                if (tokens[i].type == TokenType.PUNCTUATION)
                {
                    if (tokens[i].text == "{")
                    {
                        dep++;
                    }
                    if (tokens[i].text == "}")
                    {
                        dep--;
                        if (depstyle == 2 && dep == 0)
                        {
                            return i;
                        }
                        if (dep < 0)
                            return i - 1;
                    }
                    if (tokens[i].text == "(")
                    {
                        dep++;
                    }
                    if (tokens[i].text == ")")
                    {
                        dep--;
                        if (depstyle == 1 && dep == 0)
                        {
                            if (start.Value.text == "(" && dep == 0)
                            {
                                //if (i == (pos + 2) && tokens[i - 1].type == TokenType.TYPE)
                                //{
                                //    depstyle = 0;
                                //}
                                //else
                                {
                                    return i;
                                }
                            }
                        }
                        if (dep < 0)
                            return i - 1;
                    }
                    if (tokens[i].text == "[")
                    {
                        dep++;
                    }
                    if (tokens[i].text == "]")
                    {
                        dep--;
                        if (depstyle == 1 && dep == 0)
                        {
                            if (start.Value.text == "[" && dep == 0)
                            {
                                return i;
                            }
                        }
                        if (dep < 0)
                            return i - 1;
                    }
                    if (depstyle == 0)
                    {
                        //if (tokens[i].text =="."&& start.Value.type == TokenType.TYPE)
                        //{
                        //    if (dep == 0)
                        //        return i - 1;
                        //}
                        if (tokens[i].text == ",")//，结束的表达式
                        {
                            if (dep == 0)
                                return i - 1;
                        }
                        if (tokens[i].text == ";")
                        {
                            if (dep == 0)
                                return i - 1;
                        }
                    }
                }
            }
            if (dep != 0)
                return -1;
            else
                return tokens.Count - 1;
        }

        int FindCodeAnyInFunc(IList<Token> tokens, ref int pos, out int depstyle)
        {
            int dep = 0;
            Token? start = null;

            depstyle = 0;
            for (int i = pos; i < tokens.Count; i++)
            {

                if (tokens[i].type == TokenType.COMMENT) //注释忽略
                {
                    continue;
                }
                if (start == null)
                {
                    start = tokens[i];

                    pos = i;
                    if (start.Value.type == TokenType.PUNCTUATION)
                    {
                        if (start.Value.text == "{")
                            depstyle = 2;
                        if (start.Value.text == "(")
                            depstyle = 1;
                        if (start.Value.text == "[")
                            depstyle = 1;
                        //bdepstart = true;
                    }
                    if (start.Value.type == TokenType.KEYWORD)
                    {
                        if (start.Value.text == "for")
                        {
                            return FindCodeKeyWord_For(tokens, i);
                        }
                        if (start.Value.text == "foreach")
                        {
                            return FindCodeKeyWord_ForEach(tokens, i);
                        }
                        if (start.Value.text == "while")
                        {
                            return FindCodeKeyWord_While(tokens, i);
                        }
                        if (start.Value.text == "do")
                        {
                            return FindCodeKeyWord_Dowhile(tokens, i);
                        }
                        if (start.Value.text == "if")
                        {
                            return FindCodeKeyWord_If(tokens, i);

                        }
                        if (start.Value.text == "return")
                        {
                            return FindCodeKeyWord_Return(tokens, i);

                        }
                    }
                    //if (start.Value.type == TokenType.TYPE && i < tokens.Count-1)
                    //{
                    //    if(tokens[i+1].type== TokenType.PUNCTUATION&&tokens[i+1].text==".")
                    //    {
                    //        //staticcall = true;
                    //        i++;
                    //        continue;
                    //    }
                    //}
                }

                if (tokens[i].type == TokenType.PUNCTUATION)
                {
                    if (tokens[i].text == "{")
                    {
                        dep++;
                    }
                    if (tokens[i].text == "}")
                    {
                        dep--;
                        if (depstyle == 2 && dep == 0)
                        {
                            return i;
                        }
                        if (dep < 0)
                            return i - 1;
                    }
                    if (tokens[i].text == "(")
                    {
                        dep++;
                    }
                    if (tokens[i].text == ")")
                    {
                        dep--;
                        if (depstyle == 1 && dep == 0)
                        {
                            if (start.Value.text == "(" && dep == 0)
                            {
                                if (i == (pos + 2) && tokens[i - 1].type == TokenType.TYPE)
                                {
                                    depstyle = 0;
                                }
                                else
                                {
                                    return i;
                                }
                            }
                        }
                        if (dep < 0)
                            return i - 1;
                    }
                    if (tokens[i].text == "[")
                    {
                        dep++;
                    }
                    if (tokens[i].text == "]")
                    {
                        dep--;
                        if (depstyle == 1 && dep == 0)
                        {
                            if (start.Value.text == "[" && dep == 0)
                            {
                                return i;
                            }
                        }
                        if (dep < 0)
                            return i - 1;
                    }
                    if (depstyle == 0)
                    {
                        //if (tokens[i].text =="."&& start.Value.type == TokenType.TYPE)
                        //{
                        //    if (dep == 0)
                        //        return i - 1;
                        //}
                        if (tokens[i].text == ",")//，结束的表达式
                        {
                            if (dep == 0)
                                return i - 1;
                        }
                        if (tokens[i].text == ";")
                        {
                            if (dep == 0)
                                return i - 1;
                        }
                    }
                }
            }
            if (dep != 0)
                return -1;
            else
                return tokens.Count - 1;
        }

        int FindCodeAnyWithoutKeyword(IList<Token> tokens, ref int pos, out int depstyle)
        {
            int dep = 0;
            Token? start = null;
            depstyle = 0;
            for (int i = pos; i < tokens.Count; i++)
            {
                if (tokens[i].type == TokenType.COMMENT) //注释忽略
                {
                    continue;
                }
                if (start == null)
                {
                    start = tokens[i];
                    pos = i;
                    if (start.Value.type == TokenType.PUNCTUATION)
                    {
                        if (start.Value.text == "{")
                            depstyle = 2;
                        if (start.Value.text == "(")
                            depstyle = 1;
                        //bdepstart = true;
                    }
                }

                if (tokens[i].type == TokenType.PUNCTUATION)
                {
                    if (tokens[i].text == "{")
                    {
                        dep++;
                    }
                    if (tokens[i].text == "}")
                    {
                        dep--;
                        if (depstyle == 2 && dep == 0)
                        {
                            return i;
                        }
                        if (dep < 0)
                            return i - 1;
                    }
                    if (tokens[i].text == "(")
                    {
                        dep++;
                    }
                    if (tokens[i].text == ")")
                    {
                        dep--;
                        if (depstyle == 1 && dep == 0)
                        {
                            if (start.Value.text == "(" && dep == 0)
                            {
                                return i;
                            }
                        }
                        if (dep < 0)
                            return i - 1;
                    }

                    if (depstyle == 0)
                    {
                        if (tokens[i].text == ",")//，结束的表达式
                        {
                            if (dep == 0)
                                return i - 1;
                        }
                        if (tokens[i].text == ";")
                        {
                            if (dep == 0)
                                return i - 1;
                        }
                    }
                }
            }
            if (dep != 0)
                return -1;
            else
                return tokens.Count - 1;
        }

        int FindCodeKeyWord_For(IList<Token> tokens, int pos)
        {
            int b1;
            int fs1 = pos + 1;
            int fe1 = FindCodeAny(tokens, ref fs1, out b1);

            int b2;
            int fs2 = fe1 + 1;
            int fe2 = FindCodeAny(tokens, ref fs2, out b2);
            return fe2;
        }
        int FindCodeKeyWord_ForEach(IList<Token> tokens, int pos)
        {
            int b1;
            int fs1 = pos + 1;
            int fe1 = FindCodeAny(tokens, ref fs1, out b1);

            int b2;
            int fs2 = fe1 + 1;
            int fe2 = FindCodeAny(tokens, ref fs2, out b2);
            return fe2;
        }
        int FindCodeKeyWord_While(IList<Token> tokens, int pos)
        {
            int b1;
            int fs1 = pos + 1;
            int fe1 = FindCodeAny(tokens, ref fs1, out b1);

            int b2;
            int fs2 = fe1 + 1;
            int fe2 = FindCodeAny(tokens, ref fs2, out b2);
            return fe2;
        }
        int FindCodeKeyWord_Dowhile(IList<Token> tokens, int pos)
        {
            int b1;
            int fs1 = pos + 1;
            int fe1 = FindCodeAny(tokens, ref fs1, out b1);

            int b2;
            int fs2 = fe1 + 1;
            int fe2 = FindCodeAny(tokens, ref fs2, out b2);
            return fe2;
        }
        int FindCodeKeyWord_If(IList<Token> tokens, int pos)
        {
            int b1;
            int fs1 = pos + 1;
            int fe1 = FindCodeAny(tokens, ref fs1, out b1);

            int b2;
            int fs2 = fe1 + 1;
            int fe2 = FindCodeAny(tokens, ref fs2, out b2);


            int nelse = fe2 + 1;
            if (b2 == 0) nelse++;
            int feelse = FindCodeAny(tokens, ref nelse, out b2);
            if (tokens.Count > nelse)
            {
                if (tokens[nelse].type == TokenType.KEYWORD && tokens[nelse].text == "else")
                {
                    int b3;
                    int fs3 = nelse + 1;
                    int fe3 = FindCodeAny(tokens, ref fs3, out b3);
                    return fe3;
                }
            }
            return fe2;
        }
        int FindCodeKeyWord_Return(IList<Token> tokens, int pos)
        {

            int fs = pos + 1;
            if (tokens[fs].type == TokenType.PUNCTUATION && tokens[fs].text == ";")
                return pos;
            int b;
            fs = pos;
            int fe = FindCodeAnyWithoutKeyword(tokens, ref fs, out b);
            return fe;
        }
        IList<int> SplitExpressionWithOp(IList<Token> tokens, int pos, int posend)
        {
            List<int> list = new List<int>();
            List<int> listt = new List<int>();
            int dep = 0;
            int skip = 0;
            for (int i = pos; i <= posend; i++)
            {
                if (tokens[i].type == TokenType.PUNCTUATION || (tokens[i].type == TokenType.KEYWORD && tokens[i].text == "as"))
                {
                    if (tokens[i].text == "(")
                    {

                        if (dep == 0 && (i == pos || tokens[i - 1].type == TokenType.PUNCTUATION) && i + 1 <= posend && tokens[i + 1].type == TokenType.TYPE)
                        {
                            list.Add(i);
                        }
                        dep++;
                        skip = i + 1;
                        continue;
                    }
                    else if (tokens[i].text == "{")
                    {
                        dep++;
                        skip = i + 1;
                        continue;
                    }
                    else if (tokens[i].text == "[")
                    {
                        if (dep == 0)
                        {
                            list.Add(i);
                        }
                        dep++;
                        skip = i + 1;
                        continue;
                    }
                    else if (tokens[i].text == ")" || tokens[i].text == "}" || tokens[i].text == "]")
                    {
                        dep--;
                        if (dep < 0) return null;
                        continue;
                    }

                }

                if (dep == 0 && i > pos && i < posend && i != skip)
                {
                    if (tokens[i].type == TokenType.PUNCTUATION || (tokens[i].type == TokenType.KEYWORD && tokens[i].text == "as"))
                    {
                        if (tokens[i].text == "." && tokens[i - 1].type == TokenType.TYPE)
                        {
                            listt.Add(i);
                        }
                        else
                        {
                            list.Add(i);
                        }
                        skip = i + 1;
                    }

                }
            }
            return list.Count > 0 ? list : listt;
        }
        int GetLowestMathOp(IList<Token> tokens, IList<int> list)
        {
            int nmax = int.MaxValue;//优先级
            int npos = -1;//字符
            foreach (int i in list)
            {
                int max = 0;
                switch (tokens[i].text)
                {
                    case "?":
                        max = -1;
                        break;
                    case ":":
                        max = 0;
                        break;
                    case "<":
                        max = 6;
                        break;
                    case ">":
                        max = 6;
                        break;
                    case "<=":
                        max = 6;
                        break;
                    case ">=":
                        max = 6;
                        break;
                    case "&&":
                        max = 5;
                        break;
                    case "||":
                        max = 5;
                        break;
                    case "==":
                        max = 1;
                        break;
                    case "!=":
                        max = 4;
                        break;
                    case "*":
                        max = 3;
                        break;
                    case "/":
                        max = 3;
                        break;
                    case "%":
                        max = 3;
                        break;
                    case "+":
                        max = 2;
                        break;
                    case "-":
                        max = 2;
                        break;
                    case ".":
                        max = 10;
                        break;
                    case "=>":
                        max = 11;
                        break;
                    case "[":
                        max = 10;
                        break;
                    case "(":
                        max = 9;
                        break;
                    case "as":
                        max = 9;
                        break;
                }
                if (max <= nmax)
                {
                    nmax = max;
                    npos = i;
                }
            }

            return npos;
        }
    }
}