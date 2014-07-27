using System;
using System.Collections.Generic;
using System.Text;
namespace CSLight
{

    public class CLS_Expression_StaticSetValue : ICLS_Expression
    {
        public CLS_Expression_StaticSetValue(int tbegin, int tend)
        {
            listParam = new List<ICLS_Expression>();
            tokenBegin = tbegin;
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
            //var parent = listParam[0].ComputeValue(content);
            var value = listParam[0].ComputeValue(content);

            type.function.StaticValueSet(content.environment, staticmembername, value.value);
            //做数学计算
            //从上下文取值
            //_value = null;
            content.OutStack(this);
            return null;
        }
        public ICLS_Type type;
        public string staticmembername;

        public override string ToString()
        {
            return "StaticSetvalue|" + type.keyword + "." + staticmembername + "=";
        }
    }
}