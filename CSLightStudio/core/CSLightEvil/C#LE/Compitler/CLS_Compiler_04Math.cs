using System;
using System.Collections.Generic;
using System.Text;
namespace CSLE
{
    public partial class CLS_Expression_Compiler : ICLS_Expression_Compiler
    {

        public ICLS_Expression Compiler_Expression_Math(IList<Token> tlist, ICLS_Environment content, int pos, int posend)
        {
            IList<int> sps = SplitExpressionWithOp(tlist, pos, posend);
            int oppos = GetLowestMathOp(tlist, sps);
            if (oppos < 0)
            {
                ////也有可能是类型转换
                //if (posend >= pos + 3 && tlist[pos].text == "(" && tlist[pos].type == TokenType.PUNCTUATION && tlist[pos + 2].text == ")" && tlist[pos + 2].type == TokenType.PUNCTUATION
                //    && tlist[pos + 1].type == TokenType.TYPE
                //    )
                //{
                //    ICLS_Expression v;
                //    bool succ = Compiler_Expression(tlist, content, pos + 3, posend, out v);
                //    CLS_Expression_TypeConvert convert = new CLS_Expression_TypeConvert();
                //    convert.listParam.Add(v);
                //    convert.targettype = content.environment.GetTypeByKeyword(tlist[pos + 1].text).type;


                //    return convert;
                //}
                //else if (tlist[pos + 1].type == TokenType.PUNCTUATION && tlist[pos + 1].text == "[")//函数表达式
                //{
                //    return Compiler_Expression_IndexFind(tlist, content, pos, posend);
                //}
                if (tlist[pos + 1].type == TokenType.PUNCTUATION && tlist[pos + 1].text == "(")//函数表达式
                {
                    return Compiler_Expression_Function(tlist, content, pos, posend);
                }
                else
                {
                    //if (!bTest && tlist[expbegin + 1].type == TokenType.PUNCTUATION && tlist[expbegin + 1].text == "(")//函数表达式
                    //{
                    //    ICLS_Expression subvalue = Compiler_Expression_Function(tlist,content, expbegin, expend);
                    //    if (null == subvalue) return false;
                    //    else
                    //        values.Add(subvalue);
                    //    bTest = true;
                    //}
                    return null;
                }

            }
            else if (tlist[oppos].text == "=>")
            {//lambda
                return Compiler_Expression_Lambda(tlist, content, pos, posend);
            }
            else if (tlist[oppos].text == "." && pos == oppos - 1 && tlist[pos].type == TokenType.TYPE)
            {


                int right = oppos + 1;
                int rightend = posend;

                ICLS_Expression valueright;
                bool succ2 = Compiler_Expression(tlist, content, right, rightend, out valueright);
                if (succ2)
                {



                    CLS_Expression_GetValue vg = valueright as CLS_Expression_GetValue;
                    CLS_Expression_Function vf = valueright as CLS_Expression_Function;
                    if (vg != null)
                    {
                        CLS_Expression_StaticFind value = new CLS_Expression_StaticFind(pos, rightend, tlist[pos].line, tlist[rightend].line);
                        value.staticmembername = vg.value_name;
                        value.type = content.GetTypeByKeyword(tlist[pos].text);
                        return value;
                    }
                    else if (vf != null)
                    {
                        CLS_Expression_StaticFunction value = new CLS_Expression_StaticFunction(pos, rightend, tlist[pos].line, tlist[rightend].line);
                        value.functionName = vf.funcname;
                        value.type = content.GetTypeByKeyword(tlist[pos].text);
                        //value.listParam.Add(valueleft);
                        value.listParam.AddRange(vf.listParam.ToArray());
                        return value;
                    }
                    else
                    {
                        throw new Exception("不可识别的表达式");
                    }

                }
                else
                {
                    throw new Exception("不可识别的表达式");
                }
            }
            else
            {
                int left = pos;
                int leftend = oppos - 1;
                int right = oppos + 1;
                int rightend = posend;
                if (tlist[oppos].text == "(")
                {
                    ICLS_Expression v;
                    bool succ = Compiler_Expression(tlist, content, oppos + 3, posend, out v);
                    CLS_Expression_TypeConvert convert = new CLS_Expression_TypeConvert(pos, posend, tlist[pos].line, tlist[posend].line);
                    convert.listParam.Add(v);
                    convert.targettype = content.GetTypeByKeyword(tlist[oppos + 1].text).type;


                    return convert;
                }
                ICLS_Expression valueleft;
                bool succ1 = Compiler_Expression(tlist, content, left, leftend, out valueleft);
                ICLS_Expression valueright;
                if (tlist[oppos].text == "[")
                {
                    rightend--;
                    bool succs = Compiler_Expression(tlist, content, right, rightend, out valueright);
                    CLS_Expression_IndexFind value = new CLS_Expression_IndexFind(left, rightend, tlist[left].line, tlist[rightend].line);
                    value.listParam.Add(valueleft);
                    value.listParam.Add(valueright);
                    return value;
                }
                else if (tlist[oppos].text == "as")
                {
                    CLS_Expression_TypeConvert convert = new CLS_Expression_TypeConvert(left, oppos + 1, tlist[left].line, tlist[oppos + 1].line);
                    convert.listParam.Add(valueleft);
                    convert.targettype = content.GetTypeByKeyword(tlist[oppos + 1].text).type;


                    return convert;
                }
                bool succ2 = Compiler_Expression(tlist, content, right, rightend, out valueright);
                if (succ1 && succ2 && valueright != null && valueleft != null)
                {
                    if (tlist[oppos].text == "=")
                    {
                        //member set

                        CLS_Expression_MemberFind mfinde = valueleft as CLS_Expression_MemberFind;
                        CLS_Expression_StaticFind sfinde = valueleft as CLS_Expression_StaticFind;
                        CLS_Expression_IndexFind ifinde = valueleft as CLS_Expression_IndexFind;
                        if (mfinde != null)
                        {
                            CLS_Expression_MemberSetValue value = new CLS_Expression_MemberSetValue(left, rightend, tlist[left].line, tlist[rightend].line);
                            value.membername = mfinde.membername;
                            value.listParam.Add(mfinde.listParam[0]);
                            value.listParam.Add(valueright);
                            return value;
                        }
                        else if (sfinde != null)
                        {
                            CLS_Expression_StaticSetValue value = new CLS_Expression_StaticSetValue(left, rightend, tlist[left].line, tlist[rightend].line);
                            value.staticmembername = sfinde.staticmembername;
                            value.type = sfinde.type;
                            //value.listParam.Add(mfinde.listParam[0]);
                            value.listParam.Add(valueright);
                            return value;
                        }
                        else if (ifinde != null)
                        {
                            CLS_Expression_IndexSetValue value = new CLS_Expression_IndexSetValue(left, rightend, tlist[left].line, tlist[rightend].line);
                            value.listParam.Add(ifinde.listParam[0]);
                            value.listParam.Add(ifinde.listParam[1]);
                            value.listParam.Add(valueright);
                            return value;
                        }
                        else
                        {
                            throw new Exception("非法的Member Set表达式" + valueleft);
                        }




                    }
                    else if (tlist[oppos].text == ".")
                    {
                        //FindMember

                        CLS_Expression_GetValue vg = valueright as CLS_Expression_GetValue;
                        CLS_Expression_Function vf = valueright as CLS_Expression_Function;

                        if (vg != null)
                        {
                            CLS_Expression_MemberFind value = new CLS_Expression_MemberFind(left, rightend, tlist[left].line, tlist[rightend].line);
                            value.listParam.Add(valueleft);
                            value.membername = vg.value_name;
                            return value;
                        }
                        else if (vf != null)
                        {
                            CLS_Expression_MemberFunction value = new CLS_Expression_MemberFunction(left, rightend, tlist[left].line, tlist[rightend].line);
                            value.functionName = vf.funcname;
                            value.listParam.Add(valueleft);
                            value.listParam.AddRange(vf.listParam.ToArray());
                            return value;
                        }

                        else
                        {
                            throw new Exception("不可识别的表达式" + valueleft + "." + valueright);
                        }

                        //value.listParam.Add(valueright);



                    }
                    else if (tlist[oppos].text == "+=" || tlist[oppos].text == "-=" || tlist[oppos].text == "*=" || tlist[oppos].text == "/=" || tlist[oppos].text == "%=")
                    {
                        CLS_Expression_SelfOpWithValue value = new CLS_Expression_SelfOpWithValue(left, rightend, tlist[left].line, tlist[rightend].line);
                        //value.value_name = ((CLS_Expression_GetValue)valueleft).value_name;
                        value.listParam.Add(valueleft);
                        value.listParam.Add(valueright);
                        value.mathop = tlist[oppos].text[0];
                        return value;
                    }
                    else if (tlist[oppos].text == "&&" || tlist[oppos].text == "||")
                    {
                        CLS_Expression_Math2ValueAndOr value = new CLS_Expression_Math2ValueAndOr(left, rightend, tlist[left].line, tlist[rightend].line);
                        value.listParam.Add(valueleft);
                        value.listParam.Add(valueright);
                        value.mathop = tlist[oppos].text[0];
                        return value;
                    }
                    else if (tlist[oppos].text == ">" || tlist[oppos].text == ">=" || tlist[oppos].text == "<" || tlist[oppos].text == "<=" || tlist[oppos].text == "==" || tlist[oppos].text == "!=")
                    {
                        CLS_Expression_Math2ValueLogic value = new CLS_Expression_Math2ValueLogic(left, rightend, tlist[left].line, tlist[rightend].line);
                        value.listParam.Add(valueleft);
                        value.listParam.Add(valueright);
                        logictoken token = logictoken.not_equal;
                        if (tlist[oppos].text == ">")
                        {
                            token = logictoken.more;
                        }
                        else if (tlist[oppos].text == ">=")
                        {
                            token = logictoken.more_equal;
                        }
                        else if (tlist[oppos].text == "<")
                        {
                            token = logictoken.less;
                        }
                        else if (tlist[oppos].text == "<=")
                        {
                            token = logictoken.less_equal;
                        }
                        else if (tlist[oppos].text == "==")
                        {
                            token = logictoken.equal;
                        }
                        else if (tlist[oppos].text == "!=")
                        {
                            token = logictoken.not_equal;
                        }
                        value.mathop = token;
                        return value;
                    }
                    else
                    {
                        char mathop = tlist[oppos].text[0];
                        if (mathop == '?')
                        {
                            CLS_Expression_Math3Value value = new CLS_Expression_Math3Value(left, rightend, tlist[left].line, tlist[rightend].line);
                            value.listParam.Add(valueleft);

                            CLS_Expression_Math2Value vvright = valueright as CLS_Expression_Math2Value;
                            if (vvright.mathop != ':')
                                throw new Exception("三元表达式异常");
                            value.listParam.Add(vvright.listParam[0]);
                            value.listParam.Add(vvright.listParam[1]);
                            return value;
                        }
                        else
                        {
                            CLS_Expression_Math2Value value = new CLS_Expression_Math2Value(left, rightend, tlist[left].line, tlist[rightend].line);
                            value.listParam.Add(valueleft);
                            value.listParam.Add(valueright);
                            value.mathop = mathop;
                            return value;
                        }

                    }


                }
                else
                {
                    LogError(tlist, "编译表达式失败", right, rightend);
                }
            }

            return null;
        }
        public ICLS_Expression Compiler_Expression_MathSelf(IList<Token> tlist, int pos, int posend)
        {
            CLS_Expression_SelfOp value = new CLS_Expression_SelfOp(pos, posend, tlist[pos].line, tlist[posend].line);
            value.value_name = tlist[pos].text;
            value.mathop = tlist[pos + 1].text[0];

            return value;
        }


    }
}