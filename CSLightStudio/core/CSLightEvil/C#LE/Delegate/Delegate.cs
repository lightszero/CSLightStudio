using System;
using System.Collections.Generic;
using System.Text;

namespace CSLE
{

    public class DeleObject //指向系统中的委托
    {
        public DeleObject(object source, System.Reflection.EventInfo _event)
        {
            this.source = source;
            this._event = _event;
        }
        public DeleObject(Delegate instance, CSLE.CLS_Content content)
        {
            deleInstance = instance;
            deleContent = content;
        }
        public object Call(CSLE.CLS_Content parentContent, IList<CSLE.CLS_Content.Value> _params)
        {
            object[] plist = new object[_params.Count];
            for (int i = 0; i < _params.Count; i++)
            {
                plist[i] = _params[i].value;
            }
            if (deleInstance != null)
            {
                if (parentContent != null)
                    parentContent.InStack(deleContent);
                object returnv = deleInstance.Method.Invoke(deleInstance.Target, plist);
                if (parentContent != null)
                    parentContent.OutStack(deleContent);
                return returnv;
            }
            else
            {
                throw new Exception("事件不能調用");
            }
            return null;
        }
        //如果是事件有这两个参数
        public object source;
        public System.Reflection.EventInfo _event;


        //如果是委托实例有这两个参数
        public Delegate deleInstance;
        public CLS_Content deleContent;
    }
}
