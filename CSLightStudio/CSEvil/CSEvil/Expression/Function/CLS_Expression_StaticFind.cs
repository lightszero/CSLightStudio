using System;
using System.Collections.Generic;
using System.Text;
namespace CSEvil
{

    public class CLS_Expression_StaticFind : ICLS_Expression
    {
        public CLS_Expression_StaticFind(int tbegin,int tend)
        {
            tokenBegin = tbegin;
            tokenEnd = tend;
         
        }
        //Block的参数 一个就是一行，顺序执行，没有
        public List<ICLS_Expression> listParam
        {
            get
            {
                return null;
            }
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
            var value=type.function.StaticValueGet(content.environment, staticmembername);
            content.OutStack(this);
            return value;


        }

   
        public ICLS_Type type;
        public string staticmembername;
  
        public override string ToString()
        {
            return "StaticFind|" + type.keyword + "." + staticmembername;
        }
    }
}