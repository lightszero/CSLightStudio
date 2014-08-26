using System;
using System.Collections.Generic;
using System.Text;
namespace CSLE
{

    public class CLS_Expression_IndexFind : ICLS_Expression
    {
        public CLS_Expression_IndexFind(int tbegin, int tend, int lbegin, int lend)
        {
            listParam = new List<ICLS_Expression>();
            tokenBegin = tbegin;
            tokenEnd = tend;
            lineBegin = lbegin;
            lineEnd = lend;
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
            set;
        }
        public int lineBegin
        {
            get;
            private set;
        }
        public int lineEnd
        {
            get;
            set;
        }
        public CLS_Content.Value ComputeValue(CLS_Content content)
        {
            content.InStack(this);
            var parent = listParam[0].ComputeValue(content);
            var key = listParam[1].ComputeValue(content);
            var type = content.environment.GetType(parent.type);

            var value = type.function.IndexGet(content, parent.value, key.value);
            content.OutStack(this);
            return value;
            //return type.function.MemberValueGet(content.environment, parent.value, membername);
            //做数学计算
            //从上下文取值
            //_value = null;
            //return null;
        }

        public override string ToString()
        {
            return "IndexFind[]|";
        }
    }
}