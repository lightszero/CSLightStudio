using System;
using System.Collections.Generic;
using System.Text;

namespace CSLight
{
    class CLS_Type_String : ICLS_Type
    {
        public CLS_Type_String()
        {
            function = new RegHelper_TypeFunction(typeof(string));
        }
        public string keyword
        {
            get { return "string"; }
        }

        public Type type
        {
            get { return typeof(string); }
        }

        public ICLS_Value MakeValue(object value)
        {
            CLS_Value_Value<string> v = new CLS_Value_Value<string>();
            v.value_value = (string)value;
            
            return v;

        }

        public object ConvertTo(ICLS_Environment env, object src, Type targetType)
        {
            if (targetType == type) return src;
            if (targetType == typeof(void))
            {
                return null;
            }
            throw new NotImplementedException();
        }

        public object Math2Value(ICLS_Environment env, char code, object left, CLS_Content.Value right, out Type returntype)
        {
            returntype = typeof(string);
            if (code == '+')
                return (string)left + right.value.ToString();
         
            throw new NotImplementedException();
        }

        public bool MathLogic(ICLS_Environment env, logictoken code, object left, CLS_Content.Value right)
        {
            if (code == logictoken.equal)
            {
                return (string)left == (string)right.value;
            }
            else if(code== logictoken.not_equal)
            {
                return (string)left != (string)right.value;
            }
            throw new NotImplementedException();
        }


        public ICLS_TypeFunction function
        {
            get;
            private set;
        }
    }
}
