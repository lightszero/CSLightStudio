using System;
using System.Collections.Generic;
using System.Text;
namespace CSLE
{
    public partial class CLS_Expression_Compiler : ICLS_Expression_Compiler
    {

        public ICLS_Expression Compiler_Expression_Loop_For(IList<Token> tlist, ICLS_Environment content, int pos, int posend)
        {
            int b1;
            int fs1 = pos + 1;
            int fe1 = FindCodeAny(tlist, ref fs1, out b1);
            CLS_Expression_LoopFor value = new CLS_Expression_LoopFor(pos, posend, tlist[pos].line, tlist[posend].line);

            int testbegin = fs1 + 1;
            if (b1 != 1)
            {
                return null;
            }
            do
            {

                int fe2 = FindCodeAny(tlist, ref testbegin, out b1);


                ICLS_Expression subvalue;
                bool succ = Compiler_Expression(tlist, content, testbegin, fe2, out subvalue);
                if (!succ) return null;
                if (subvalue != null)
                {
                    value.listParam.Add(subvalue);
                    testbegin = fe2 + 2;
                }
                else
                {
                    value.listParam.Add(null);
                    testbegin = fe2 + 2;
                }
            }
            while (testbegin <= fe1);

            if (value.listParam.Count != 3)
            {
                return null;
            }
            ICLS_Expression subvalueblock;

            int b2;
            int fs2 = fe1 + 1;
            int fecode = FindCodeAny(tlist, ref fs2, out b2);
            bool succ2 = Compiler_Expression_Block(tlist, content, fs2, fecode, out subvalueblock);
            if (succ2)
            {
                value.tokenEnd = fecode;
                value.lineEnd = tlist[fecode].line;
                value.listParam.Add(subvalueblock);
                return value;
            }
            return null;
        }

        public ICLS_Expression Compiler_Expression_Loop_ForEach(IList<Token> tlist, ICLS_Environment content, int pos, int posend)
        {

            int b1;
            int fs1 = pos + 1;
            int fe1 = FindCodeAny(tlist, ref fs1, out b1);
            CLS_Expression_LoopForEach value = new CLS_Expression_LoopForEach(pos, fe1, tlist[pos].line, tlist[fe1].line);
            int testbegin = fs1 + 1;
            if (b1 != 1)
            {
                return null;
            }
            for (int i = fs1 + 1; i <= fe1 - 1; i++)
            {
                if (tlist[i].text == "in" && tlist[i].type == TokenType.KEYWORD)
                {
                    //添加 foreach 定义变量部分
                    {
                        ICLS_Expression subvalue;
                        bool succ = Compiler_Expression(tlist, content, fs1 + 1, i - 1, out subvalue);
                        if (!succ) return null;
                        if (subvalue != null)
                        {
                            value.listParam.Add(subvalue);
                        }
                    }
                    //添加 foreach 列表部分
                    {
                        ICLS_Expression subvalue;
                        bool succ = Compiler_Expression(tlist, content, i + 1, fe1 - 1, out subvalue);
                        if (!succ) return null;
                        if (subvalue != null)
                        {

                            value.listParam.Add(subvalue);
                        }
                    }
                    break;
                }
            }

            ICLS_Expression subvalueblock;

            int b2;
            int fs2 = fe1 + 1;
            int fecode = FindCodeAny(tlist, ref fs2, out b2);
            bool succ2 = Compiler_Expression_Block(tlist, content, fs2, fecode, out subvalueblock);
            if (succ2)
            {
                value.tokenEnd = fecode;
                value.lineEnd = tlist[fecode].line;
                value.listParam.Add(subvalueblock);
                return value;
            }
            return null;
        }
        public ICLS_Expression Compiler_Expression_Loop_While(IList<Token> tlist, ICLS_Environment content, int pos, int posend)
        {
            int b1;
            int fs1 = pos + 1;
            int fe1 = FindCodeAny(tlist, ref fs1, out b1);
            CLS_Expression_LoopWhile value = new CLS_Expression_LoopWhile(pos, fe1, tlist[pos].line, tlist[fe1].line);


            //while(xxx)
            {
                ICLS_Expression subvalue;
                bool succ = Compiler_Expression(tlist, content, fs1, fe1, out subvalue);
                if (succ)
                {
                    value.tokenEnd = fe1;
                    value.lineEnd = tlist[fe1].line;
                    value.listParam.Add(subvalue);
                }
                else
                {
                    return null;
                }
            }

            //while(...){yyy}

            int b2;
            int fs2 = fe1 + 1;
            int fe2 = FindCodeAny(tlist, ref fs2, out b2);
            {
                ICLS_Expression subvalue;
                bool succ = Compiler_Expression_Block(tlist, content, fs2, fe2, out subvalue);
                if (succ)
                {
                    value.tokenEnd = fe2;
                    value.lineEnd = tlist[fe2].line;
                    value.listParam.Add(subvalue);
                }
                else
                {
                    return null;
                }
            }
            return value;
        }
        public ICLS_Expression Compiler_Expression_Loop_Dowhile(IList<Token> tlist, ICLS_Environment content, int pos, int posend)
        {
            int b1;
            int fs1 = pos + 1;
            int fe1 = FindCodeAny(tlist, ref fs1, out b1);
            CLS_Expression_LoopDowhile value = new CLS_Expression_LoopDowhile(pos, fe1, tlist[pos].line, tlist[fe1].line);

            //do(xxx)while(...)
            {
                ICLS_Expression subvalue;
                bool succ = Compiler_Expression_Block(tlist, content, fs1, fe1, out subvalue);
                if (succ)
                {
                    value.tokenEnd = fe1;
                    value.lineEnd = tlist[fe1].line;
                    value.listParam.Add(subvalue);
                }
                else
                {
                    return null;
                }
            }

            //do{...]while(yyy);
            if (tlist[fe1 + 1].text != "while") return null;
            int b2;
            int fs2 = fe1 + 2;
            int fe2 = FindCodeAny(tlist, ref fs2, out b2);
            {
                ICLS_Expression subvalue;
                bool succ = Compiler_Expression(tlist, content, fs2, fe2, out subvalue);
                if (succ)
                {
                    value.tokenEnd = fe2;
                    value.lineEnd = tlist[fe2].line;
                    value.listParam.Add(subvalue);
                }
                else
                {
                    return null;
                }
            }
            return value;
        }

        public ICLS_Expression Compiler_Expression_Loop_If(IList<Token> tlist, ICLS_Environment content, int pos, int posend)
        {

            CLS_Expression_LoopIf value = new CLS_Expression_LoopIf(pos, posend, tlist[pos].line, tlist[posend].line);
            int b1;
            int fs1 = pos + 1;
            int fe1 = FindCodeAny(tlist, ref fs1, out b1);
            if (b1 != 1)
            {
                return null;
            }

            //if(xxx)
            {
                ICLS_Expression subvalue;
                bool succ = Compiler_Expression(tlist, content, fs1, fe1, out subvalue);
                if (succ)
                {
                    value.tokenEnd = fe1;
                    value.lineEnd = tlist[fe1].line;
                    value.listParam.Add(subvalue);
                }
                else
                {
                    return null;
                }
            }

            //if(...){yyy}

            int b2;
            int fs2 = fe1 + 1;
            int fe2 = FindCodeAny(tlist, ref fs2, out b2);
            {
                ICLS_Expression subvalue;
                bool succ = Compiler_Expression_Block(tlist, content, fs2, fe2, out subvalue);
                if (succ)
                {
                    value.tokenEnd = fe2;
                    value.lineEnd = tlist[fe2].line;
                    value.listParam.Add(subvalue);
                }
                else
                {
                    return null;
                }
            }

            int nelse = fe2 + 1;
            if (b2 == 0) nelse++;
            int feelse = FindCodeAny(tlist, ref nelse, out b2);
            if (tlist.Count > nelse)
            {
                if (tlist[nelse].type == TokenType.KEYWORD && tlist[nelse].text == "else")
                { //if(...){...}else{zzz}
                    int b3;
                    int fs3 = nelse + 1;
                    int fe3 = FindCodeAny(tlist, ref fs3, out b3);
                    ICLS_Expression subvalue;
                    bool succ = Compiler_Expression_Block(tlist, content, fs3, fe3, out subvalue);
                    if (succ)
                    {
                        value.tokenEnd = fe3;
                        value.lineEnd = tlist[fe3].line;
                        value.listParam.Add(subvalue);
                    }
                    else
                    {
                        return null;
                    }
                }
            }


            return value;
        }
        public ICLS_Expression Compiler_Expression_Loop_Try(IList<Token> tlist, ICLS_Environment content, int pos, int posend)
        {

            CLS_Expression_LoopTry value = new CLS_Expression_LoopTry(pos, posend, tlist[pos].line, tlist[posend].line);
            int b1;
            int fs1 = pos + 1;
            int fe1 = FindCodeAny(tlist, ref fs1, out b1);
            if (b1 != 2)
            {
                return null;
            }

            //try
            {
                ICLS_Expression subvalue;
                bool succ = Compiler_Expression_Block(tlist, content, fs1, fe1, out subvalue);
                if (succ)
                {
                    value.tokenEnd = fe1;
                    value.lineEnd = tlist[fe1].line;
                    value.listParam.Add(subvalue);
                }
                else
                {
                    return null;
                }
            }

            while (fe1 < posend && tlist[fe1 + 1].text == "catch")
            {
                //catch(...)

                int b2;
                int fs2 = fe1 + 2;
                int fe2 = FindCodeAny(tlist, ref fs2, out b2);
                {
                    if (b2 != 1)
                    {
                        return null;
                    }
                    ICLS_Expression subvalue;
                    bool succ = Compiler_Expression(tlist, content, fs2, fe2, out subvalue);
                    if (succ)
                    {
                        value.tokenEnd = fe2;
                        value.lineEnd = tlist[fe2].line;
                        value.listParam.Add(subvalue);
                    }
                    else
                    {
                        return null;
                    }
                }
                //catch(){...}

                {
                    int b3;
                    int fs3 = fe2 + 1;
                    int fe3 = FindCodeAny(tlist, ref fs3, out b3);
                    if (b3 != 2)
                    {
                        return null;
                    }

                    ICLS_Expression subvalue;
                    bool succ = Compiler_Expression_Block(tlist, content, fs3, fe3, out subvalue);
                    if (succ)
                    {
                        value.tokenEnd = fe3;
                        value.lineEnd = tlist[fe3].line;
                        value.listParam.Add(subvalue);
                    }
                    else
                    {
                        return null;
                    }
                    fe1 = fe3;
                }
            }


            return value;
        }
        public ICLS_Expression Compiler_Expression_Loop_Return(IList<Token> tlist, ICLS_Environment content, int pos, int posend)
        {
            CLS_Expression_LoopReturn value = new CLS_Expression_LoopReturn(pos, posend, tlist[pos].line, tlist[posend].line);

            ICLS_Expression subvalue;
            bool succ = Compiler_Expression(tlist, content, pos + 1, posend, out subvalue);
            if (succ)
            {
                value.listParam.Add(subvalue);
            }

            return value;
        }
        public ICLS_Expression Compiler_Expression_Loop_Break(IList<Token> tlist, int pos)
        {
            CLS_Expression_LoopBreak value = new CLS_Expression_LoopBreak(pos, pos, tlist[pos].line, tlist[pos].line);
            return value;
        }
        public ICLS_Expression Compiler_Expression_Loop_Continue(IList<Token> tlist, int pos)
        {
            CLS_Expression_LoopContinue value = new CLS_Expression_LoopContinue(pos, pos, tlist[pos].line, tlist[pos].line);
            return value;
        }
    }
}