using System;
using System.Collections.Generic;
using System.Text;
namespace CSLE
{

    public class CLS_Expression_Lambda: ICLS_Expression
    {
        public CLS_Expression_Lambda(int tbegin, int tend, int lbegin, int lend)
        {
            listParam = new List<ICLS_Expression>();
            this.tokenBegin = tbegin;
            this.tokenEnd = tend;
            lineBegin = lbegin;
            lineEnd = lend;
        }
        //Block的参数 一个就是一行，顺序执行，没有
        public List<ICLS_Expression> listParam
        {
            get;
            private set;
        }
        public List<ICLS_Expression> lambdaParams = new List<ICLS_Expression>();
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
        public CLS_Content.Value ComputeValue(CLS_Content content)
        {
            content.InStack(this);
            List<CLS_Content.Value> list = new List<CLS_Content.Value>();
            CLS_Content.Value value = new CLS_Content.Value();
            value.type = typeof(DeleLambda);
            value.value = new DeleLambda(content,(this.listParam[0] as CLS_Expression_Block).listParam,this.listParam[1]);
            //创建一个匿名方法
            content.OutStack(this);
            return value;

        }

  
        public override string ToString()
        {
            return "()=>{}|";
        }
    }
}