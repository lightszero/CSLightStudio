using System;
using System.Collections.Generic;
using System.Text;
namespace CSLE
{

    public class CLS_Expression_SetValue : ICLS_Expression
    {
        public CLS_Expression_SetValue(int tbegin, int tend, int lbegin, int lend)
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

            {
                
                CLS_Content.Value v = listParam[0].ComputeValue(content);

                        {
                            object val = v.value;
                            if (content.values.ContainsKey(value_name))
                            {
                                CLType value_type = content.values[value_name].type;

                                val = v.value;
                                if ((Type)value_type != typeof(CLS_Type_Var.var) && value_type != v.type)
                                {
                                    val = content.environment.GetType(v.type).ConvertTo(content, v.value, value_type);
                                }
                            }
                    content.Set(value_name, val);
                    }
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