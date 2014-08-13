using System;
using System.Collections.Generic;
using System.Text;
namespace CSLE
{
    public partial class CLS_Expression_Compiler : ICLS_Expression_Compiler
    {
        public ICLS_Expression Compiler_Expression_Define(IList<Token> tlist, ICLS_Environment content, int pos, int posend)
        {
            CLS_Expression_Define define = new CLS_Expression_Define(pos, posend, tlist[pos].line, tlist[posend].line);
            if (tlist[pos].text == "bool")
            {
                define.value_type = typeof(bool);
            }
            else
            {
                ICLS_Type type =    content.GetTypeByKeyword(tlist[pos].text);
                define.value_type = type.type;
            }
            define.value_name = tlist[pos+1].text;
            return define;
        }

        public ICLS_Expression Compiler_Expression_DefineArray(IList<Token> tlist, ICLS_Environment content, int pos, int posend)
        {
            CLS_Expression_Define define = new CLS_Expression_Define(pos, posend, tlist[pos].line, tlist[posend].line);
            {
                ICLS_Type type = content.GetTypeByKeyword(tlist[pos].text+"[]");
                define.value_type = type.type;
            }
            define.value_name = tlist[pos + 3].text;
            return define;
        }
        public ICLS_Expression Compiler_Expression_Lambda(IList<Token> tlist, ICLS_Environment content, int pos, int posend)
        {
            int b1;
            int fs1 = pos;
            int fe1 = FindCodeAny(tlist, ref fs1, out b1);
            CLS_Expression_Lambda value = new CLS_Expression_Lambda(pos, posend, tlist[pos].line, tlist[posend].line);

            int testbegin = fs1 + 1;
            if (b1 != 1)
            {
                return null;
            }
            //(xxx)=>{...}
            CLS_Expression_Block block = new CLS_Expression_Block(fs1, fe1, tlist[fs1].line, tlist[fe1].line);
            do
            {

                int fe2 = FindCodeAny(tlist, ref testbegin, out b1);


                ICLS_Expression subvalue;
                bool succ = Compiler_Expression(tlist, content, testbegin, fe2, out subvalue);
                if (!succ) break;
                if (subvalue != null)
                {
                    block.listParam.Add(subvalue);
                    testbegin = fe2 + 2;
                }
                else
                {
                    block.listParam.Add(null);
                    testbegin = fe2 + 2;
                }
            }
            while (testbegin <= fe1);

            value.listParam.Add(block);
            //(...)=>{}
            ICLS_Expression subvalueblock;

            int b2;
            int fs2 = fe1 + 2;
            int fecode = FindCodeAny(tlist, ref fs2, out b2);
            bool succ2 = Compiler_Expression_Block(tlist, content, fs2, fecode, out subvalueblock);
            if (succ2)
            {
                //value.tokenEnd = fecode;
                //value.lineEnd = tlist[fecode].line;
                value.listParam.Add(subvalueblock);
                return value;
            }
            return null;
        }
        public ICLS_Expression Compiler_Expression_DefineAndSet(IList<Token> tlist, ICLS_Environment content, int pos, int posend)
        {
            int expbegin =pos+3;
            int bdep;
            int expend = FindCodeAny(tlist, ref expbegin, out bdep);
            if(expend!=posend)
            {
                expend = posend;
            }
            ICLS_Expression v;
            bool succ = Compiler_Expression(tlist,content, expbegin, expend, out v);
            if(succ&&v!=null)
            {
                CLS_Expression_Define define = new CLS_Expression_Define(pos, posend, tlist[pos].line, tlist[posend].line);
                if (tlist[pos].text == "bool")
                {
                    define.value_type = typeof(bool);
                }
                else
                {
                    ICLS_Type type = content.GetTypeByKeyword(tlist[pos].text);
                    define.value_type = type.type;
                }
                define.value_name = tlist[pos + 1].text;
                define.listParam.Add(v);
                return define;
            }
            LogError(tlist,"不正确的定义表达式:" , pos,posend);
            return null;
        }
        public ICLS_Expression Compiler_Expression_DefineAndSetArray(IList<Token> tlist, ICLS_Environment content, int pos, int posend)
        {
            int expbegin = pos + 5;
            int bdep;
            int expend = FindCodeAny(tlist, ref expbegin, out bdep);
            if (expend != posend)
            {
                expend = posend;
            }
            ICLS_Expression v;
            bool succ = Compiler_Expression(tlist, content, expbegin, expend, out v);
            if (succ && v != null)
            {
                CLS_Expression_Define define = new CLS_Expression_Define(pos, posend, tlist[pos].line, tlist[posend].line);
                {
                    ICLS_Type type = content.GetTypeByKeyword(tlist[pos].text+"[]");
                    define.value_type = type.type;
                }
                define.value_name = tlist[pos + 3].text;
                define.listParam.Add(v);
                return define;
            }
            LogError(tlist, "不正确的定义表达式:", pos, posend);
            return null;
        }
        public ICLS_Expression Compiler_Expression_Set(IList<Token> tlist, ICLS_Environment content, int pos, int posend)
        {
            int expbegin = pos + 2;
            int bdep;
            int expend = FindCodeAny(tlist, ref expbegin, out bdep);
            if (expend != posend)
            {
              
                expend = posend;
            }
            ICLS_Expression v;
            bool succ = Compiler_Expression(tlist,content, expbegin, expend, out v);
            if (succ && v != null)
            {
                CLS_Expression_SetValue define = new CLS_Expression_SetValue(pos, expend, tlist[pos].line, tlist[expend].line);
                define.value_name = tlist[pos].text;
                define.listParam.Add(v);
                return define;
            }
            LogError(tlist,"不正确的定义表达式:" ,pos,posend);
            return null;
        }
 
    }
}