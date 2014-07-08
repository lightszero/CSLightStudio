using System;
using System.Collections.Generic;
using System.Text;
namespace CSLight
{

    public class CLS_Expression_StaticFunction : ICLS_Expression
    {
        public CLS_Expression_StaticFunction(int tbegin,int tend)
        {
            listParam = new List<ICLS_Expression>();
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
            //var parent = listParam[0].ComputeValue(content);
            //var type = content.environment.GetType(parent.type);
            List<CLS_Content.Value> _params = new List<CLS_Content.Value>();
            for (int i = 0; i < listParam.Count; i++)
            {
                _params.Add(listParam[i].ComputeValue(content));
            }
            var value = type.function.StaticCall(content.environment, functionName, _params);
            content.OutStack(this);
            return value;
            //做数学计算
            //从上下文取值
            //_value = null;
            //return null;
        }

        public ICLS_Type type;
        public string functionName;

        public override string ToString()
        {
            return "StaticCall|" + type.keyword + "." + functionName;
        }
    }
}