using System;
using System.Collections.Generic;
using System.Text;
namespace CSLE
{

    public class CLS_Expression_TypeConvert : ICLS_Expression
    {
        public CLS_Expression_TypeConvert(int tbegin, int tend, int lbegin, int lend)
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

            var right = listParam[0].ComputeValue(content);
            ICLS_Type type = content.environment.GetType(right.type);
            CLS_Content.Value value = new CLS_Content.Value();
            value.type = targettype;
            value.value = type.ConvertTo(content, right.value, targettype);

            //操作变量之
            //做数学计算
            //从上下文取值
            //_value = null;
            content.OutStack(this);

            return value;
        }

        public CLType type
        {
            get { return null; }
        }
        public CLType targettype;

        public override string ToString()
        {
            return "convert<" + targettype.Name + ">";
        }
    }
}