using System;
using System.Collections.Generic;
using System.Text;
namespace CSLE
{

    public class CLS_Expression_Math2ValueAndOr : ICLS_Expression
    {
        public CLS_Expression_Math2ValueAndOr(int tbegin, int tend, int lbegin, int lend)
        {
            listParam = new List<ICLS_Expression>();
            this.tokenBegin = tbegin;
            this.tokenEnd = tend;
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

            //if (mathop == "&&" || mathop == "||")
            {
                bool bleft = false;
                bool bright = false;
                if (listParam[0] is ICLS_Value)
                {
                    bleft = (bool)((listParam[0] as ICLS_Value).value);
                }
                else
                {
                    bleft = (bool)listParam[0].ComputeValue(content).value;
                }

                if (listParam[1] is ICLS_Value)
                {
                    bright = (bool)((listParam[1] as ICLS_Value).value);
                }
                else
                {
                    bright = (bool)listParam[1].ComputeValue(content).value;
                }
                result.type = typeof(bool);


                if (mathop == '&')
                {

                    result.value = (bool)(bleft && bright);
                }
                else if (mathop == '|')
                {
                    result.value = (bool)(bleft || bright);
                }
            }
            content.OutStack(this);
            return result;

        }


        public char mathop;

        public override string ToString()
        {
            return "Math2ValueAndOr|a" + mathop + "b";
        }
    }
}