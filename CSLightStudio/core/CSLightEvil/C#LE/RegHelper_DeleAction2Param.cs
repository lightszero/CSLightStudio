using System;
using System.Collections.Generic;
using System.Text;

namespace CSLE
{


    public class RegHelper_DeleAction<T1, T2> : RegHelper_Type, ICLS_Type_Dele
    {
        public RegHelper_DeleAction(string setkeyword)
            : base(typeof(Action<T1, T2>), setkeyword)
        {

        }


        public override object Math2Value(CLS_Content env, char code, object left, CLS_Content.Value right, out CLType returntype)
        {
            returntype = null;

            if (left is DeleObject && right.value is DeleObject)
            {
                DeleObject info = left as DeleObject;
                DeleObject calldele = right.value as DeleObject;
                if (code == '+')
                    info._event.AddEventHandler(info.source, calldele.deleInstance);
                else if (code == '-')
                    info._event.AddEventHandler(info.source, calldele.deleInstance);


            }

            return null;
        }
        public override object DefValue
        {
            get
            {
                return new DeleObject(null);

            }
        }


        public string GetParamSign(ICLS_Environment env)
        {
            return "," + env.GetType((CLType)typeof(T1)).keyword + "," + env.GetType((CLType)typeof(T2)).keyword;
            //return "";
        }

        public Delegate CreateDelegate(ICLS_Environment env, SType calltype, SInstance callthis, string function)
        {

            Action<T1, T2> dele = (T1 param0, T2 param1) =>
            {

                CLS_Content content = new CLS_Content(env);
                content.CallThis = callthis;
                content.CallType = calltype;
                content.function = function;
                var func = calltype.functions[function];

                content.DefineAndSet(func._paramnames[0], func._paramtypes[0].type, param0);
                content.DefineAndSet(func._paramnames[1], func._paramtypes[1].type, param1);

                func.expr_runtime.ComputeValue(content);
            };
            return dele;
        }
    }
}
