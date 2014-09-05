using System;
using System.Collections.Generic;
using System.Text;
namespace CSLE
{

    public class CLS_Expression_MemberMath : ICLS_Expression
    {
        public CLS_Expression_MemberMath(int tbegin, int tend, int lbegin, int lend)
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
            var parent = listParam[0].ComputeValue(content);
            var type = content.environment.GetType(parent.type);
            //string membername=null;


            var getvalue = type.function.MemberValueGet(content, parent.value, membername);

            CLS_Content.Value vright = CLS_Content.Value.One;
            if (listParam.Count > 1)
            {
                vright = listParam[1].ComputeValue(content);
            }
            CLS_Content.Value vout =new CLS_Content.Value();
            var mtype = content.environment.GetType(getvalue.type);
            vout.value = mtype.Math2Value(content, mathop, getvalue.value, vright, out vout.type);

            type.function.MemberValueSet(content, parent.value, membername, vout.value);
            CLS_Content.Value v = new CLS_Content.Value();

            content.OutStack(this);
            return vout;
            //做数学计算
            //从上下文取值
            //_value = null;
            //return null;

        }

        public string membername;
        public char mathop;
        public override string ToString()
        {
            return "CLS_Expression_MemberMath|a." + membername + " |" + mathop;
        }
    }
}