using System;
using System.Collections.Generic;
using System.Text;
namespace CSLE
{

    public class CLS_Expression_Define : ICLS_Expression
    {
        public CLS_Expression_Define(int tbegin, int tend, int lbegin, int lend)
        {
            //listParam = new List<ICLS_Value>();
            this.tokenBegin = tbegin;
            this.tokenEnd = tend;
            lineBegin = lbegin;
            lineEnd = lend;
        }
        //Block的参数 一个就是一行，顺序执行，没有
        List<ICLS_Expression> _listParam = null;
        public List<ICLS_Expression> listParam
        {
            get
            {
                if (_listParam == null)
                {
                    _listParam = new List<ICLS_Expression>();
                }
                return _listParam;
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

            if (_listParam != null && _listParam.Count > 0)
            {

                CLS_Content.Value v = _listParam[0].ComputeValue(content);
                object val = v.value;
                if ((Type)value_type == typeof(CLS_Type_Var.var))
                {
                    if(v.type!=null)
                        value_type = v.type;
                    
                }
                else if (v.type != value_type)
                {
                    val = content.environment.GetType(v.type).ConvertTo(content, v.value, value_type);
                   
                }

                content.DefineAndSet(value_name, value_type, val);
            }
            else
            {
                content.Define(value_name, value_type);
            }
            //设置环境变量为
            content.OutStack(this);

            return null;
        }
        public string value_name;
        public CLType value_type;
        public override string ToString()
        {
            string outs = "Define|" + value_type.Name + " " + value_name;
            if (_listParam != null)
            {
                if (_listParam.Count > 0)
                {
                    outs += "=";
                }
            }
            return outs;
        }
    }
}