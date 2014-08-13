//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace CSLE
//{


//    public class RegHelper_DeleAction<T1, T2, T3> : RegHelper_Type, ICLS_Type_Dele
//    {
//        public RegHelper_DeleAction(string setkeyword)
//            : base(typeof(Action<T1, T2, T3>), setkeyword)
//        {

//        }


//        public override object Math2Value(CLS_Content env, char code, object left, CLS_Content.Value right, out CLType returntype)
//        {
//            returntype = null;

//            if (left is DeleEvent && right.value is DeleEvent)
//            {
//                DeleEvent info = left as DeleEvent;
//                DeleEvent calldele = right.value as DeleEvent;
//                if (code == '+')
//                {
//                    info._event.AddEventHandler(info.source, calldele.deleInstance);
//                    return null;
//                }
//                else if (code == '-')
//                {
//                    info._event.AddEventHandler(info.source, calldele.deleInstance);
//                    return null;
//                }


//            }
//            if (left is DeleEvent && right.value is DeleLambda)
//            {
//                DeleEvent info = left as DeleEvent;
//                DeleEvent calldele = CreateDelegate(env.environment, right.value as DeleLambda);
//                if (code == '+')
//                {
//                    info._event.AddEventHandler(info.source, calldele.deleInstance);
//                    return null;
//                }
//            }
//            throw new NotSupportedException();
//        }
//        public override object DefValue
//        {
//            get
//            {
//                return new DeleEvent(null, null);

//            }
//        }


//        public string GetParamSign(ICLS_Environment env)
//        {
//            return "," + env.GetType((CLType)typeof(T1)).keyword
//                + "," + env.GetType((CLType)typeof(T2)).keyword
//                + "," + env.GetType((CLType)typeof(T3)).keyword;
//            //return "";
//        }

//        public DeleEvent CreateDelegate(ICLS_Environment env, SType calltype, SInstance callthis, string function)
//        {

//            CLS_Content content = new CLS_Content(env);
//            Action<T1, T2, T3> dele = (T1 param0, T2 param1, T3 param2) =>
//            {
//                content.DepthAdd();
//                content.CallThis = callthis;
//                content.CallType = calltype;
//                content.function = function;
//                var func = calltype.functions[function];

//                content.DefineAndSet(func._paramnames[0], func._paramtypes[0].type, param0);
//                content.DefineAndSet(func._paramnames[1], func._paramtypes[1].type, param1);
//                content.DefineAndSet(func._paramnames[2], func._paramtypes[2].type, param2);

//                func.expr_runtime.ComputeValue(content);
//                content.DepthRemove();
//            };
//            DeleEvent obj = new DeleEvent(dele, content);
//            return obj;
//        }


//        public DeleEvent CreateDelegate(ICLS_Environment env, DeleLambda lambda)
//        {
//            CLS_Content content = lambda.content.Clone();
//            var pnames = lambda.paramNames;
//            var expr = lambda.expr_func;
//            Action<T1, T2, T3> dele = (T1 param0, T2 param1, T3 param2) =>
//            {
//                content.DepthAdd();


//                content.DefineAndSet(pnames[0], typeof(T1), param0);
//                content.DefineAndSet(pnames[1], typeof(T2), param1);
//                content.DefineAndSet(pnames[2], typeof(T3), param2);

//                expr.ComputeValue(content);

//                content.DepthRemove();
//            };

//            DeleEvent obj = new DeleEvent(dele, content);
//            return obj;
//        }
//    }
//}
