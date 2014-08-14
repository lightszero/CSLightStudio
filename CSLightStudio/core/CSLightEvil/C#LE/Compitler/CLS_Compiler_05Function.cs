using System;
using System.Collections.Generic;
using System.Text;
namespace CSLE
{
    public partial class CLS_Expression_Compiler : ICLS_Expression_Compiler
    {


        public ICLS_Expression Compiler_Expression_Function(IList<Token> tlist, ICLS_Environment content, int pos, int posend)
        {
            CLS_Expression_Function func = new CLS_Expression_Function(pos, posend, tlist[pos].line, tlist[posend].line);

            func.funcname = tlist[pos].text;
            int begin = pos + 2;
            int dep;
            int end = FindCodeAnyInFunc(tlist, ref begin, out dep);

            if (tlist[pos + 1].type == TokenType.PUNCTUATION && tlist[pos + 1].text == "(")
            {
                do
                {
                    ICLS_Expression param;
                    bool succ = Compiler_Expression(tlist, content, begin, end, out param);
                    if (succ && param != null)
                    {
                        func.listParam.Add(param);
                        func.tokenEnd = end;
                        func.lineEnd = tlist[end].line;
                    }
                    begin = end + 2;
                    end = FindCodeAnyInFunc(tlist, ref begin, out dep);

                }
                while (end < posend && begin <= end);


                return func;
            }
            //一般函数
            return null;
        }
        public ICLS_Expression Compiler_Expression_FunctionTrace(IList<Token> tlist, ICLS_Environment content, int pos, int posend)
        {
            if (tlist[pos + 1].type == TokenType.PUNCTUATION && tlist[pos + 1].text == "(")
                return Compiler_Expression_Function(tlist, content, pos, posend);
            int begin = pos + 1;
            int dep;
            int end = FindCodeAnyInFunc(tlist, ref begin, out dep);
            if (end != posend)
            {
                return null;
            }
            CLS_Expression_Function func = new CLS_Expression_Function(pos, end, tlist[pos].line, tlist[end].line);
            func.funcname = "trace";

            do
            {
                ICLS_Expression param;
                bool succ = Compiler_Expression(tlist, content, begin, end, out param);
                if (succ && param != null)
                {
                    func.listParam.Add(param);
                    func.tokenEnd = end;
                    func.lineEnd = tlist[end].line;
                }
                begin = end + 2;
                end = FindCodeAnyInFunc(tlist, ref begin, out dep);

            }
            while (end < posend && begin <= end);

            //ICLS_Expression param0;
            //bool succ = Compiler_Expression(tlist,content, begin, end, out param0);
            //if(succ&&param0!=null)
            //{
            //    func.listParam.Add(param0);
            //    return func;

            //}
            return func;
            //trace ,单值直接dump,否则按逗号分隔的表达式处理

            //return null;
        }
        public ICLS_Expression Compiler_Expression_FunctionThrow(IList<Token> tlist, ICLS_Environment content, int pos, int posend)
        {
            CLS_Expression_Throw func = new CLS_Expression_Throw(pos, posend, tlist[pos].line, tlist[posend].line);

            ICLS_Expression subvalue;
            bool succ = Compiler_Expression(tlist, content, pos + 1, posend, out subvalue);
            if (succ)
            {
                func.listParam.Add(subvalue);
            }


            return func;
            //trace ,单值直接dump,否则按逗号分隔的表达式处理

            //return null;
        }

        public ICLS_Expression Compiler_Expression_FunctionNew(IList<Token> tlist, ICLS_Environment content, int pos, int posend)
        {
            int begin = pos + 3;
            int dep;
            int end = FindCodeAnyInFunc(tlist, ref begin, out dep);

            if (tlist[pos + 2].type == TokenType.PUNCTUATION && tlist[pos + 2].text == "(")
            {
                //一般函数
                CLS_Expression_FunctionNew func = new CLS_Expression_FunctionNew(pos, posend, tlist[pos].line, tlist[posend].line);
                func.type = content.GetTypeByKeyword(tlist[pos + 1].text);

                do
                {
                    ICLS_Expression param;
                    bool succ = Compiler_Expression(tlist, content, begin, end, out param);
                    if (succ && param != null)
                    {
                        func.listParam.Add(param);
                    }
                    begin = end + 2;
                    end = FindCodeAnyInFunc(tlist, ref begin, out dep);

                }
                while (end < posend && begin <= end);


                return func;
            }
            else if (tlist[pos + 2].type == TokenType.PUNCTUATION && tlist[pos + 2].text == "[")//数组实例化表达式
            {
                CLS_Expression_FunctionNewArray func = new CLS_Expression_FunctionNewArray(pos, posend, tlist[pos].line, tlist[posend].line);
                func.type = content.GetTypeByKeyword(tlist[pos + 1].text + "[]");

                int valuebegin = 0;
                ICLS_Expression count = null;
                if (tlist[pos + 3].text == "]")
                {
                    valuebegin = pos + 4;
                }
                else
                {
                    int nbegin = pos + 3;
                    int dep2;
                    int end2 = FindCodeAny(tlist, ref nbegin, out dep2);

                    bool succ = Compiler_Expression(tlist, content, nbegin, end2, out count);
                    if (!succ)
                    {
                        throw new Exception("数组数量无法识别");
                    }
                    valuebegin = end2 + 2;
                }
                func.listParam.Add(count);
                if (tlist[valuebegin].text == "{")//InitValue
                {
                    int nbegin = valuebegin + 1;
                    do
                    {
                        int dep2;
                        int nend = FindCodeAny(tlist, ref nbegin, out dep2);
                        ICLS_Expression valueI;
                        bool succ = Compiler_Expression(tlist, content, nbegin, nend, out valueI);
                        if (!succ)
                        {
                            throw new Exception("数组初始值无法识别");
                        }
                        func.listParam.Add(valueI);
                      
                        if (tlist[nend + 1].text != ",")
                            break;
                        nbegin = nend + 2;
                    }
                    while (nbegin >= pos && nbegin < posend);
                }
                return func;
            }
            return null;
        }

        public ICLS_Expression Compiler_Expression_FunctionStatic(IList<Token> tlist, ICLS_Environment content, int pos, int posend)
        {
            CLS_Expression_Function func = new CLS_Expression_Function(pos, posend, tlist[pos].line, tlist[posend].line);
            func.funcname = tlist[pos].text;
            int begin = pos + 2;
            int dep;
            int end = FindCodeAnyInFunc(tlist, ref begin, out dep);

            if (tlist[pos + 1].type == TokenType.PUNCTUATION && tlist[pos + 1].text == "(")
            {
                do
                {
                    ICLS_Expression param;
                    bool succ = Compiler_Expression(tlist, content, begin, end, out param);
                    if (succ && param != null)
                    {
                        func.listParam.Add(param);
                        func.tokenEnd = end;
                        func.lineEnd = tlist[end].line;
                    }
                    begin = end + 2;
                    end = FindCodeAnyInFunc(tlist, ref begin, out dep);

                }
                while (end < posend && begin <= end);


                return func;
            }
            //一般函数
            return null;
        }


        public ICLS_Expression Compiler_Expression_IndexFind(IList<Token> tlist, ICLS_Environment content, int pos, int posend)
        {
            CLS_Expression_IndexFind func = new CLS_Expression_IndexFind(pos, posend, tlist[pos].line, tlist[posend].line);
            ICLS_Expression lefv;
            bool b = Compiler_Expression(tlist, content, pos, pos, out lefv);
            func.listParam.Add(lefv);
            //func.funcname = tlist[pos].text;
            int begin = pos + 2;
            int dep;
            int end = FindCodeAny(tlist, ref begin, out dep);

            if (tlist[pos + 1].type == TokenType.PUNCTUATION && tlist[pos + 1].text == "[")
            {
                do
                {
                    ICLS_Expression param;
                    bool succ = Compiler_Expression(tlist, content, begin, end, out param);
                    if (succ && param != null)
                    {
                        func.tokenEnd = end;
                        func.lineEnd = tlist[end].line;
                        func.listParam.Add(param);
                    }
                    begin = end + 2;
                    end = FindCodeAny(tlist, ref begin, out dep);

                }
                while (end < posend && begin <= end);


                return func;
            }
            //一般函数
            return null;
        }
    }
}