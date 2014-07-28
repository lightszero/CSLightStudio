using System;
using System.Collections.Generic;
using System.Text;
namespace CSEvil
{

    public class CLS_Expression_MemberFind : ICLS_Expression
    {
        public CLS_Expression_MemberFind(int tbegin,int tend)
        {
           listParam= new List<ICLS_Expression>();
           this.tokenBegin = tbegin;
           this.tokenEnd = tend;
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
            var value=type.function.MemberValueGet(content.environment, parent.value, membername);
            content.OutStack(this);
            return value;
            //做数学计算
            //从上下文取值
            //_value = null;
            //return null;

        }

   
        public string membername;
   
        public override string ToString()
        {
            return "MemberFind|a." + membername;
        }
    }
}