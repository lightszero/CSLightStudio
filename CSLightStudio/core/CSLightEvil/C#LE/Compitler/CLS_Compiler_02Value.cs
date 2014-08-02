using System;
using System.Collections.Generic;
using System.Text;
namespace CSLE
{
    public partial class CLS_Expression_Compiler : ICLS_Expression_Compiler
    {

        public ICLS_Expression Compiler_Expression_Value(Token value,int pos)
        {
            if (value.type == TokenType.VALUE)
            {
                if(value.text[value.text.Length-1]=='f')
                {
                    CLS_Value_Value<float> number = new CLS_Value_Value<float>();
                    number.value_value = float.Parse(value.text.Substring(0,value.text.Length-1));
                    return number;
                }
                else if (value.text.Contains("."))
                {
                    CLS_Value_Value<double> number = new CLS_Value_Value<double>();
                    number.value_value = double.Parse(value.text);
                    return number;
                }
                else
                {
                    CLS_Value_Value<int> number = new CLS_Value_Value<int>();
                    number.value_value = int.Parse(value.text);
                    return number;
                }
            }
            else if (value.type == TokenType.STRING)
            {
                CLS_Value_Value<string> str = new CLS_Value_Value<string>();
                str.value_value = value.text.Substring(1,value.text.Length-2);
                return str;
            }
            else if (value.type == TokenType.IDENTIFIER)
            {
                CLS_Expression_GetValue getvalue = new CLS_Expression_GetValue(pos, pos, value.line, value.line);
                getvalue.value_name = value.text;
                return getvalue;
            }
            else if(value.type == TokenType.TYPE)
            {
                CLS_Expression_GetValue getvalue = new CLS_Expression_GetValue(pos, pos, value.line, value.line);
                int l = value.text.LastIndexOf('.');
                if(l>=0)
                {
                    getvalue.value_name = value.text.Substring(l+1);
                }
                else
                                    getvalue.value_name = value.text;
                return getvalue;
            }
            else
            {
                logger.Log_Error("无法识别的简单表达式" + value);
                return null;
            }
        }

        public ICLS_Expression Compiler_Expression_SubValue(Token value)
        {
            if (value.type == TokenType.VALUE)
            {
                if (value.text[value.text.Length - 1] == 'f')
                {
                    CLS_Value_Value<float> number = new CLS_Value_Value<float>();
                    number.value_value = -float.Parse(value.text.Substring(0, value.text.Length - 1));
                    return number;
                }
                else if (value.text.Contains("."))
                {
                    CLS_Value_Value<double> number = new CLS_Value_Value<double>();
                    number.value_value = -double.Parse(value.text);
                    return number;
                }
                else
                {
                    CLS_Value_Value<int> number = new CLS_Value_Value<int>();
                    number.value_value = -int.Parse(value.text);
                    return number;
                }
            }
            else
            {
                logger.Log_Error("无法识别的简单表达式" + value);
                return null;
            }
        }
        public ICLS_Expression Compiler_Expression_NegativeValue(IList<Token> tlist, ICLS_Environment content, int pos, int posend)
        {
            int expbegin = pos;
            int bdep;
            int expend2 = FindCodeAny(tlist, ref expbegin, out bdep);
            if (expend2 != posend)
            {
                LogError(tlist,"无法识别的负号表达式:" ,expbegin , posend);
                return null;
            }
            else
            {
                ICLS_Expression subvalue;
                bool succ = Compiler_Expression(tlist,content, expbegin, expend2, out subvalue);
                if (succ && subvalue != null)
                {
                    CLS_Expression_NegativeValue v = new CLS_Expression_NegativeValue(pos, expend2, tlist[pos].line, tlist[expend2].line);
                    v.listParam.Add(subvalue);
                    return v;
                }
                else
                {
                    LogError(tlist, "无法识别的负号表达式:", expbegin, posend);
                    return null;
                }
            }
        }
        public ICLS_Expression Compiler_Expression_NegativeLogic(IList<Token> tlist, ICLS_Environment content, int pos, int posend)
        {
            int expbegin = pos;
            int bdep;
            int expend2 = FindCodeAny(tlist, ref expbegin, out bdep);
            if (expend2 != posend)
            {
                LogError(tlist, "无法识别的取反表达式:", expbegin, posend);
                return null;
            }
            else
            {
                ICLS_Expression subvalue;
                bool succ = Compiler_Expression(tlist, content,expbegin, expend2, out subvalue);
                if (succ && subvalue != null)
                {
                    CLS_Expression_NegativeLogic v = new CLS_Expression_NegativeLogic(pos, expend2, tlist[pos].line, tlist[expend2].line);
                    v.listParam.Add(subvalue);
                    return v;
                }
                else
                {
                    LogError(tlist, "无法识别的取反表达式:", expbegin, posend);
                    return null;
                }
            }
        }
    }
}