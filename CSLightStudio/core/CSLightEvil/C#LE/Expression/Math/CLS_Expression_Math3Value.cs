using System;
using System.Collections.Generic;
using System.Text;
namespace CSLE
{

    public class CLS_Expression_Math3Value : ICLS_Expression
    {
        public CLS_Expression_Math3Value(int tbegin, int tend, int lbegin, int lend)
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
            CLS_Content.Value result = null;


            {
                result = new CLS_Content.Value();
                var value = listParam[0].ComputeValue(content);
                if ((Type)value.type != typeof(bool))
                {
                    throw new Exception("三元表达式要求条件为bool型");
                }
                bool bv = (bool)value.value;
                if (bv)
                    result = listParam[1].ComputeValue(content);
                else
                    result = listParam[2].ComputeValue(content);
            }
            content.OutStack(this);
            return result;


        }


        public override string ToString()
        {
            return "Math3Value|a?b:c";
        }
    }
}