using System;
using System.Collections.Generic;
using System.Text;
namespace CSLE
{

    public class CLS_Expression_Math2ValueLogic : ICLS_Expression
    {
        public CLS_Expression_Math2ValueLogic(int tbegin, int tend, int lbegin, int lend)
        {
            listParam = new List<ICLS_Expression>();
            this.tokenBegin = tbegin;
            tokenEnd = tend;
            lineBegin = lbegin;
            lineEnd = lend;
        }
        public int lineBegin
        {
            get;
            private set;
        }
        public int lineEnd
        {
            get;
            private set;
        }
        //Block的参数 一个就是一行，顺序执行，没有
        public List<ICLS_Expression> listParam
        {
            get;
            private set;
        }
        public int tokenBegin
        {
            get;
            private set;
        }
        public int tokenEnd
        {
            get;
            private set;
        }
        public CLS_Content.Value ComputeValue(CLS_Content content)
        {
            content.InStack(this);
            CLS_Content.Value result = new CLS_Content.Value();


            //if(mathop=="<"||mathop=="<="||mathop==">"||mathop==">="||mathop=="=="||mathop=="!=")
            {
                result.type = typeof(bool);
                var left = listParam[0].ComputeValue(content);
                var right = listParam[1].ComputeValue(content);
                if ((Type)left.type == typeof(bool) && (Type)right.type == typeof(bool))
                {
                    if (mathop == logictoken.equal)
                    {
                        result.value = (bool)left.value == (bool)right.value;
                        //return result;
                    }
                    else if (mathop == logictoken.not_equal)
                    {
                        result.value = (bool)left.value != (bool)right.value;
                        //return result;
                    }
                    else
                    {
                        throw new Exception("bool 不支持此运算符");
                    }
                }
                else
                {

                    result.value = content.environment.GetType(left.type).MathLogic(content, mathop, left.value, right);

                }
            }
            content.OutStack(this);

            return result;
        }

        public logictoken mathop;

        public override string ToString()
        {
            return "Math2ValueLogic|a" + mathop + "b";
        }
    }
}