using System;
using System.Collections.Generic;
using System.Text;

namespace CSLight
{
    class CLS_Type_NULL : ICLS_Type
    {
        public string keyword
        {
            get { return "null"; }
        }

        public Type type
        {
            get { return null; }
        }

        public ICLS_Value MakeValue(object value)
        {
            CLS_Value_Null v = new CLS_Value_Null();
       
            return v;

        }

        public object ConvertTo(CLS_Environment env, object src, Type targetType)
        {
            return null;
        }

        public object Math2Value(CLS_Environment env, char code, object left, CLS_Content.Value right, out Type returntype)
        {
            throw new NotImplementedException();
        }

        public bool MathLogic(CLS_Environment env, logictoken code, object left, CLS_Content.Value right)
        {
            if (code == logictoken.equal)
            {
                return null == right.value;
            }
            else if(code== logictoken.not_equal)
            {
                return null != right.value;
            }
            throw new NotImplementedException();
        }



        public ICLS_TypeFunction function
        {
            get { throw new NotImplementedException(); }
        }
    }
}
