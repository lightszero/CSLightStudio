using System;
using System.Collections.Generic;
using System.Text;

namespace CSLE
{
    class CLS_Type_NULL : ICLS_Type
    {
        public string keyword
        {
            get { return "null"; }
        }
        public string _namespace
        {
            get { return ""; }
        }
        public CLType type
        {
            get { return null; }
        }

        public ICLS_Value MakeValue(object value)
        {
            CLS_Value_Null v = new CLS_Value_Null();
       
            return v;

        }

        public object ConvertTo(CLS_Content env, object src, CLType targetType)
        {
            return null;
        }

        public object Math2Value(CLS_Content env, char code, object left, CLS_Content.Value right, out CLType returntype)
        {
            throw new NotImplementedException();
        }

        public bool MathLogic(CLS_Content env, logictoken code, object left, CLS_Content.Value right)
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
        public object DefValue
        {
            get { return null; }
        }
    }
}
