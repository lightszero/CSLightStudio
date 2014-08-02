using System;
using System.Collections.Generic;
using System.Text;
namespace CSLE
{

    public class CLS_Expression_SelfOpWithValue : ICLS_Expression
    {
        public CLS_Expression_SelfOpWithValue(int tbegin, int tend, int lbegin, int lend)
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


            var left = listParam[0].ComputeValue(content);
            var right = listParam[1].ComputeValue(content);
            ICLS_Type type = content.environment.GetType(left.type);
            //if (mathop == "+=")

            {
                CLType returntype;
                object value = type.Math2Value(content, mathop, left.value, right, out returntype);
                value = type.ConvertTo(content, value, left.type);
                left.value = value;

                //content.Set(value_name, value);
            }

            //操作变量之
            //做数学计算
            //从上下文取值
            //_value = null;
            content.OutStack(this);

            return null;
        }


        //public string value_name;
        public char mathop;

        public override string ToString()
        {
            return "MathSelfOp|" +  mathop;
        }
    }
}