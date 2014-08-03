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
        public DeleObject(Delegate instance)
        {
            deleInstance = instance;
        }

        public object source;
        public System.Reflection.EventInfo _event;

        public Delegate deleInstance;
    }
}
