using System;
using System.Collections.Generic;
using System.Text;
namespace CSLight
{
    public partial class CLS_Expression_Compiler : ICLS_Expression_Compiler
    {


        public ICLS_Expression Compiler_Expression_Function(IList<Token> tlist, CLS_Content content, int pos, int posend)
        {
            CLS_Expression_Function func = new CLS_Expression_Function(pos,posend);

            func.funcname = tlist[pos].text;
            int begin = pos + 2;
            int dep;
            int end = FindCodeAny(tlist, ref begin, out dep);

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
        public ICLS_Expression Compiler_Expression_FunctionTrace(IList<Token> tlist, CLS_Content content, int pos, int posend)
        {
            if (tlist[pos + 1].type == TokenType.PUNCTUATION && tlist[pos + 1].text == "(")
                return Compiler_Expression_Function(tlist,content, pos, posend);
            int begin=pos+1;
            int dep;
            int end= FindCodeAny(tlist,ref begin,out dep);
            if(end!=posend)
            {
                return null;
            }
            CLS_Expression_Function func = new CLS_Expression_Function(pos,end);
            func.funcname = "trace";

            do
            {
                ICLS_Expression param;
                bool succ = Compiler_Expression(tlist, content, begin, end, out param);
                if (succ && param != null)
                {
                    func.listParam.Add(param);
                    func.tokenEnd = end;
                }
                begin = end + 2;
                end = FindCodeAny(tlist, ref begin, out dep);

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

        public ICLS_Expression Compiler_Expression_FunctionNew(IList<Token> tlist, CLS_Content content, int pos, int posend)
        {
            CLS_Expression_FunctionNew func = new CLS_Expression_FunctionNew(pos,posend);
            func.type =content.environment.GetTypeByKeyword( tlist[pos+1].text);
            int begin = pos + 3;
            int dep;
            int end = FindCodeAny(tlist, ref begin, out dep);

            if (tlist[pos + 2].type == TokenType.PUNCTUATION && tlist[pos + 2].text == "(")
            {
                do
                {
                    ICLS_Expression param;
                    bool succ = Compiler_Expression(tlist, content, begin, end, out param);
                    if (succ && param != null)
                    {
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

        public ICLS_Expression Compiler_Expression_FunctionStatic(IList<Token> tlist, CLS_Content content, int pos, int posend)
        {
            CLS_Expression_Function func = new CLS_Expression_Function(pos,posend);
            func.funcname = tlist[pos].text;
            int begin = pos + 2;
            int dep;
            int end = FindCodeAny(tlist, ref begin, out dep);

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


        public ICLS_Expression Compiler_Expression_IndexFind(IList<Token> tlist, CLS_Content content, int pos, int posend)
        {
            CLS_Expression_IndexFind func = new CLS_Expression_IndexFind(pos,posend);
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