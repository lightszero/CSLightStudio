using System;
using System.Collections.Generic;
using System.Text;

namespace CSLE
{
    //改为DeleEvent
    public class DeleEvent //指向系统中的事件委托
    {
        public DeleEvent(object source, System.Reflection.EventInfo _event)
        {
            this.source = source;
            this._event = _event;
        }
        //public DeleEvent(Delegate instance, CSLE.CLS_Content content)
        //{
        //    deleInstance = instance;
        //    deleContent = content;
        //}
        //public object Call(CSLE.CLS_Content parentContent, IList<CSLE.CLS_Content.Value> _params)
        //{
        //    object[] plist = new object[_params.Count];
        //    for (int i = 0; i < _params.Count; i++)
        //    {
        //        plist[i] = _params[i].value;
        //    }
        //    if (deleInstance != null)
        //    {
        //        if (parentContent != null)
        //            parentContent.InStack(deleContent);
        //        object returnv = deleInstance.Method.Invoke(deleInstance.Target, plist);
        //        if (parentContent != null)
        //            parentContent.OutStack(deleContent);
        //        return returnv;
        //    }
        //    else
        //    {
        //        throw new Exception("事件不能調用");
        //    }
        //    return null;
        //}
        //如果是事件有这两个参数
        public object source;
        public System.Reflection.EventInfo _event;


        //如果是委托实例有这两个参数
        //public Delegate deleInstance;
        //public CLS_Content deleContent;
    }


    public class DeleFunction //指向脚本中的函数
    {
        public DeleFunction(SType stype, SInstance _this, string function)
        {
            this.calltype = stype;
            this.callthis = callthis;
            this.function = function;
        }
        public SType calltype;
        public SInstance callthis;
        public string function;
    }
    public class DeleLambda  //指向Lambda表达式
    {
        public DeleLambda(CLS_Content content,IList<ICLS_Expression> param,ICLS_Expression func)
        {
            this.content = content.Clone();
            this.expr_func = func;
            foreach(var p in param)
            {
                CLS_Expression_GetValue v1 = p as CLS_Expression_GetValue;
                CLS_Expression_Define v2 = p as CLS_Expression_Define;
                if (v1 != null)
                {
                    paramTypes.Add(null);
                    paramNames.Add(v1.value_name);
                }
                else if (v2 != null)
                {
                    paramTypes.Add(v2.value_type);
                    paramNames.Add(v2.value_name);
                }
                else
                {
                    throw new Exception("DeleLambda 参数不正确");
                }
            }
        }
        public List<Type> paramTypes = new List<Type>();
        public List<string> paramNames = new List<string>(); 
        public CLS_Content content;
        public ICLS_Expression expr_func;
    }
}
