using System;
using System.Collections.Generic;
using System.Text;
namespace CSLight
{

    public class CLS_Expression_SelfOpWithValue : ICLS_Expression
    {
        public CLS_Expression_SelfOpWithValue(int tbegin,int tend)
        {
            listParam = new List<ICLS_Expression>();
            this.tokenBegin = tbegin;
            tokenEnd = tend;
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

            var v = content.Get(value_name);
            var right = listParam[0].ComputeValue(content);
            ICLS_Type type = content.environment.GetType(v.type);
            //if (mathop == "+=")
            {
                Type returntype;
                object value = type.Math2Value(content.environment, mathop, v.value, right, out returntype);
                value = type.ConvertTo(content.environment, value, v.type);
                content.Set(value_name, value);
            }

            //操作变量之
            //做数学计算
            //从上下文取值
            //_value = null;
            content.OutStack(this);

            return null;
        }


        public string value_name;
        public char mathop;

        public override string ToString()
        {
            return "MathSelfOp|" + value_name + mathop;
        }
    }
}