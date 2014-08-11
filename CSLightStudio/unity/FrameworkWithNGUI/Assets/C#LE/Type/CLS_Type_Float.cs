using System;
using System.Collections.Generic;
using System.Text;

namespace CSLE
{
    class CLS_Type_Float : ICLS_Type
    {
        public CLS_Type_Float()
        {
            function = new RegHelper_TypeFunction(typeof(float));
        }
        public string keyword
        {
            get { return "float"; }
        }
        public string _namespace
        {
            get { return ""; }
        }
        public CLType type
        {
            get { return typeof(float); }
        }

        public ICLS_Value MakeValue(object value)
        {
            CLS_Value_Value<float> v = new CLS_Value_Value<float>();
            v.value_value = (float)value;
            
            return v;

        }

        public object ConvertTo(CLS_Content env, object src, CLType targetType)
        {
            if ((Type)targetType == typeof(int))
            {
                return (int)(float)src;
            }
            else if ((Type)targetType == typeof(uint))
            {
                return (uint)(float)src;
            }
            else if ((Type)targetType == typeof(double))
            {
                return (double)(float)src;
            }
            else if ((Type)targetType == typeof(float))
            {
                return (float)src;
            }
            throw new NotImplementedException();
        }

        public object Math2Value(CLS_Content env, char code, object left, CLS_Content.Value right, out CLType returntype)
        {
            returntype = typeof(float);
            if ((Type)right.type == typeof(int))
            {
                if (code == '+')
                    return (float)left + (float)(int)right.value;
                else if (code == '-')
                    return (float)left - (float)(int)right.value;
                else if (code == '*')
                    return (float)left * (float)(int)right.value;
                else if (code == '/')
                    return (float)left / (float)(int)right.value;
                else if (code == '%')
                    return (float)left % (float)(int)right.value;
            }
            else if ((Type)right.type == typeof(uint))
            {
                if (code == '+')
                    return (float)left + (float)(uint)right.value;
                else if (code == '-')
                    return (float)left - (float)(uint)right.value;
                else if (code == '*')
                    return (float)left * (float)(uint)right.value;
                else if (code == '/')
                    return (float)left / (float)(uint)right.value;
                else if (code == '%')
                    return (float)left % (float)(uint)right.value;
            }
            else if ((Type)right.type == typeof(double))
            {
                returntype = typeof(double);

                if (code == '+')
                    return (double)(float)left + (double)right.value;
                else if (code == '-')
                    return (double)(float)left - (double)right.value;
                else if (code == '*')
                    return (double)(float)left * (double)right.value;
                else if (code == '/')
                    return (double)(float)left / (double)right.value;
                else if (code == '%')
                    return (double)(float)left % (double)right.value;
            }
            else if ((Type)right.type == typeof(float))
            {
                returntype = typeof(float);
                if (code == '+')
                    return (float)left + (float)right.value;
                else if (code == '-')
                    return (float)left - (float)right.value;
                else if (code == '*')
                    return (float)left * (float)right.value;
                else if (code == '/')
                    return (float)left / (float)right.value;
                else if (code == '%')
                    return (float)left % (float)right.value;
            }
            throw new NotImplementedException();
        }

        public bool MathLogic(CLS_Content env, logictoken code, object left, CLS_Content.Value right)
        {
            if ((Type)right.type == typeof(int))
            {
                if (code == logictoken.equal)
                    return (float)left == (int)right.value;
                else if (code == logictoken.less)
                    return (float)left < (int)right.value;
                else if (code == logictoken.less_equal)
                    return (float)left <= (int)right.value;
                else if (code == logictoken.more)
                    return (float)left > (int)right.value;
                else if (code == logictoken.more_equal)
                    return (float)left >= (int)right.value;
                else if (code == logictoken.not_equal)
                    return (float)left != (int)right.value;
            }
            else if ((Type)right.type == typeof(uint))
            {
                if (code == logictoken.equal)
                    return (float)left == (uint)right.value;
                else if (code == logictoken.less)
                    return (float)left < (uint)right.value;
                else if (code == logictoken.less_equal)
                    return (float)left <= (uint)right.value;
                else if (code == logictoken.more)
                    return (float)left > (uint)right.value;
                else if (code == logictoken.more_equal)
                    return (float)left >= (uint)right.value;
                else if (code == logictoken.not_equal)
                    return (float)left != (uint)right.value;
            }
            else if ((Type)right.type == typeof(double))
            {
                if (code == logictoken.equal)
                    return (float)left == (double)right.value;
                else if (code == logictoken.less)
                    return (float)left < (double)right.value;
                else if (code == logictoken.less_equal)
                    return (float)left <= (double)right.value;
                else if (code == logictoken.more)
                    return (float)left > (double)right.value;
                else if (code == logictoken.more_equal)
                    return (float)left >= (double)right.value;
                else if (code == logictoken.not_equal)
                    return (float)left != (double)right.value;
            }
            else if ((Type)right.type == typeof(float))
            {
                if (code == logictoken.equal)
                    return (float)left == (float)right.value;
                else if (code == logictoken.less)
                    return (float)left < (float)right.value;
                else if (code == logictoken.less_equal)
                    return (float)left <= (float)right.value;
                else if (code == logictoken.more)
                    return (float)left > (float)right.value;
                else if (code == logictoken.more_equal)
                    return (float)left >= (float)right.value;
                else if (code == logictoken.not_equal)
                    return (float)left != (float)right.value;
            }
            throw new NotImplementedException();
        }

        public ICLS_TypeFunction function
        {
            get;
            private set;
        }
        public object DefValue
        {
            get { return (float)0; }
        }
    }
}
