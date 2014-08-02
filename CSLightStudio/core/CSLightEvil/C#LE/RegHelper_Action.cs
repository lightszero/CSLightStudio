using System;
using System.Collections.Generic;
using System.Text;

namespace CSLE
{


    public class RegHelper_DeleAction : RegHelper_Type
    {
        public RegHelper_DeleAction(string setkeyword = null)
            : base(typeof(Action), setkeyword)
        {

        }


        public override object Math2Value(CLS_Content env, char code, object left, CLS_Content.Value right, out CLType returntype)
        {
            returntype = null;

            System.Reflection.MethodInfo call = null;

            Action dele = () =>
                {
                    DeleScript sdele = right.value as DeleScript;
                    CLS_Content content =new CLS_Content(env.environment);
                    content.CallThis = sdele.callthis;
                    content.CallType = sdele.calltype;
                    content.function = sdele.function;
                    sdele.calltype.functions[sdele.function].expr_runtime.ComputeValue(content);
                };
            if (left is DeleSystem)
            {
                DeleSystem info = left as DeleSystem;
                if (code == '+')
                    info._event.AddEventHandler(info.source, dele);
                else if (code == '-')
                    info._event.AddEventHandler(info.source, dele);


            }

            return null;
        }
        public override object DefValue
        {
            get
            {
                Action a = new Action(() => { });
                return a;
            }
        }
    }
}
