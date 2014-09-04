using System;
using System.Collections.Generic;
using System.Text;
namespace CSLE
{

    public class CLS_Expression_StaticMath : ICLS_Expression
    {
        public CLS_Expression_StaticMath(int tbegin, int tend, int lbegin, int lend)
        {
            listParam = new List<ICLS_Expression>();
            tokenBegin = tbegin;
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


            var getvalue=type.function.StaticValueGet(content, staticmembername);

            CLS_Content.Value vright = CLS_Content.Value.One;
            if (listParam.Count > 0)
            {
                vright = listParam[0].ComputeValue(content);
            }
            CLS_Content.Value vout = new CLS_Content.Value();
            var mtype = content.environment.GetType(getvalue.type);
            vout.value = mtype.Math2Value(content, mathop, getvalue.value, vright, out vout.type);

            type.function.StaticValueSet(content, staticmembername, vout.value);

            content.OutStack(this);
            return vout;


        }

   
        public ICLS_Type type;
        public string staticmembername;
        public char mathop;
        public override string ToString()
        {
            return "StaticMath|" + type.keyword + "." + staticmembername +" |"+mathop;
        }
    }
}