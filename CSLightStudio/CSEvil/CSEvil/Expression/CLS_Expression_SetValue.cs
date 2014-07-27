using System;
using System.Collections.Generic;
using System.Text;
namespace CSLight
{

    public class CLS_Expression_SetValue : ICLS_Expression
    {
        public CLS_Expression_SetValue(int tbegin,int tend)
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
            {
                CLS_Content.Value v = listParam[0].ComputeValue(content);
                Type value_type = content.values[value_name].type;

                object val = v.value;
                if (value_type != typeof(CLS_Type_Var.var) && value_type!=v.type)
                {
                    val = content.environment.GetType(v.type).ConvertTo(content.environment, v.value, value_type);
                }
                content.Set(value_name, val);
            }
            content.OutStack(this);
            return null;
        }


        public string value_name;
   
        public override string ToString()
        {
            return "SetValue|" + value_name +"=";
        }
    }
}