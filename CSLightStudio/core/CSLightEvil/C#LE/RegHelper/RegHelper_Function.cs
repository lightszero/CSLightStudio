using System;
using System.Collections.Generic;
using System.Text;

namespace CSLE
{
    public class RegHelper_Function : ICLS_Function
    {
        Delegate dele;
        public RegHelper_Function(Delegate dele,string setkeyword=null)
        {
            this.dele = dele;
            if (setkeyword != null)
            {
                this.keyword = setkeyword;
            }
            else
            {
                this.keyword = dele.Method.Name;
            }
            returntype = dele.Method.ReturnType;
            foreach (var p in dele.Method.GetParameters())
            {
                defvalues.Add(p.DefaultValue);
                paramtype.Add(p.ParameterType);
            }
        }
        List<object> defvalues = new List<object>();
        List<Type> paramtype = new List<Type>();
        Type returntype;
        public string keyword
        {
            get;
            private set;
        }

        public CLS_Content.Value Call(CLS_Content content, IList<CLS_Content.Value> param)
        {
            CLS_Content.Value v = new CLS_Content.Value();
            List<object> objs = new List<object>();
            //var _params =   dele.Method.GetParameters();
            for (int i = 0; i < this.defvalues.Count; i++)
            {
                if (i >= param.Count)
                {
                    objs.Add(defvalues[i]);
                }
                else
                {
                    if (this.paramtype[i] == (Type)param[i].type)
                    {
                        objs.Add(param[i].value);
                    }
                    else
                    {
                        object conv = content.environment.GetType(param[i].type).ConvertTo(content, param[i].value, paramtype[i]);
                        objs.Add(conv);
                    }
                }
            }
            v.type = this.returntype;
            v.value = dele.DynamicInvoke(objs.ToArray());
            return v;
        }
    }

  
}
