using System;
using System.Collections.Generic;
using System.Text;

namespace CSLE
{
    class CLS_Type_Lambda: ICLS_Type
    {
        public CLS_Type_Lambda()
        {

            function = null;
        }
        public string keyword
        {
            get { return "()=>"; }
        }
        public string _namespace
        {
            get { return ""; }
        }
        public CLType type
        {
            get { return typeof(DeleLambda); }
        }

        public ICLS_Value MakeValue(object value)
        {
            throw new NotSupportedException();

        }

        public object ConvertTo(CLS_Content env, object src, CLType targetType)
        {
            ICLS_Type_Dele dele =  env.environment.GetType(targetType) as ICLS_Type_Dele;
            return dele.CreateDelegate(env.environment, src as DeleLambda);
            //throw new NotImplementedException();
        }

        public object Math2Value(CLS_Content env, char code, object left, CLS_Content.Value right, out CLType returntype)
        {

            throw new NotImplementedException("code:"+code +" right:+"+right.type.ToString()+"="+ right.value);
        }

        public bool MathLogic(CLS_Content env, logictoken code, object left, CLS_Content.Value right)
        {

            throw new NotImplementedException();
        }



        public ICLS_TypeFunction function
        {
            get;
            private set;
        }
        public object DefValue
        {
            get { return null; }
        }
    }
}
