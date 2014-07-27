using System;
using System.Collections.Generic;
using System.Text;

namespace CSLight
{
    class CLS_Type_Double : ICLS_Type
    {
        public CLS_Type_Double()
        {
            function = new RegHelper_TypeFunction(typeof(double));
        }
        public string keyword
        {
            get { return "double"; }
        }

        public Type type
        {
            get { return typeof(double); }
        }

        public ICLS_Value MakeValue(object value)
        {
            CLS_Value_Value<double> v = new CLS_Value_Value<double>();
            v.value_value = (double)value;
            
            return v;

        }

        public object ConvertTo(CLS_Environment env, object src, Type targetType)
        {
            if (targetType == typeof(int))
            {
                return (int)(double)src;
            }
            else if (targetType == typeof(uint))
            {
                return (uint)(double)src;
            }
            else if (targetType == typeof(double))
            {
                return (double)src;
            }
            else if (targetType == typeof(float))
            {
                return (float)(double)src;
            }
            throw new NotImplementedException();
        }

        public object Math2Value(CLS_Environment env, char code, object left, CLS_Content.Value right, out Type returntype)
        {
            returntype = typeof(double);
            if (right.type == typeof(int))
            {
                if (code == '+')
                    return (double)left + (double)(int)right.value;
                else if (code == '-')
                    return (double)left - (double)(int)right.value;
                else if (code == '*')
                    return (double)left * (double)(int)right.value;
                else if (code == '/')
                    return (double)left / (double)(int)right.value;
                else if (code == '%')
                    return (double)left % (double)(int)right.value;
            }
            else if (right.type == typeof(uint))
            {
                if (code == '+')
                    return (double)left + (double)(uint)right.value;
                else if (code == '-')
                    return (double)left - (double)(uint)right.value;
                else if (code == '*')
                    return (double)left * (double)(uint)right.value;
                else if (code == '/')
                    return (double)left / (double)(uint)right.value;
                else if (code == '%')
                    return (double)left % (double)(uint)right.value;
            }
            else if (right.type == typeof(double))
            {
                returntype = typeof(double);

                if (code == '+')
                    return (double)left + (double)right.value;
                else if (code == '-')
                    return (double)left - (double)right.value;
                else if (code == '*')
                    return (double)left * (double)right.value;
                else if (code == '/')
                    return (double)left / (double)right.value;
                else if (code == '%')
                    return (double)left % (double)right.value;
            }
            else if (right.type == typeof(float))
            {
                returntype = typeof(double);
                if (code == '+')
                    return (double)left + (double)(float)right.value;
                else if (code == '-')
                    return (double)left - (double)(float)right.value;
                else if (code == '*')
                    return (double)left * (double)(float)right.value;
                else if (code == '/')
                    return (double)left / (double)(float)right.value;
                else if (code == '%')
                    return (double)left % (double)(float)right.value;
            }
            throw new NotImplementedException();
        }

        public bool MathLogic(CLS_Environment env, logictoken code, object left, CLS_Content.Value right)
        {
            if (right.type == typeof(int))
            {
                if (code == logictoken.equal)
                    return (double)left == (int)right.value;
                else if (code == logictoken.less)
                    return (double)left < (int)right.value;
                else if (code == logictoken.less_equal)
                    return (double)left <= (int)right.value;
                else if (code == logictoken.more)
                    return (double)left > (int)right.value;
                else if (code == logictoken.more_equal)
                    return (double)left >= (int)right.value;
                else if (code == logictoken.not_equal)
                    return (double)left != (int)right.value;
            }
            else if (right.type == typeof(uint))
            {
                if (code == logictoken.equal)
                    return (double)left == (uint)right.value;
                else if (code == logictoken.less)
                    return (double)left < (uint)right.value;
                else if (code == logictoken.less_equal)
                    return (double)left <= (uint)right.value;
                else if (code == logictoken.more)
                    return (double)left > (uint)right.value;
                else if (code == logictoken.more_equal)
                    return (double)left >= (uint)right.value;
                else if (code == logictoken.not_equal)
                    return (double)left != (uint)right.value;
            }
            else if (right.type == typeof(double))
            {
                if (code == logictoken.equal)
                    return (double)left == (double)right.value;
                else if (code == logictoken.less)
                    return (double)left < (double)right.value;
                else if (code == logictoken.less_equal)
                    return (double)left <= (double)right.value;
                else if (code == logictoken.more)
                    return (double)left > (double)right.value;
                else if (code == logictoken.more_equal)
                    return (double)left >= (double)right.value;
                else if (code == logictoken.not_equal)
                    return (double)left != (double)right.value;
            }
            else if (right.type == typeof(float))
            {
                if (code == logictoken.equal)
                    return (double)left == (float)right.value;
                else if (code == logictoken.less)
                    return (double)left < (float)right.value;
                else if (code == logictoken.less_equal)
                    return (double)left <= (float)right.value;
                else if (code == logictoken.more)
                    return (double)left > (float)right.value;
                else if (code == logictoken.more_equal)
                    return (double)left >= (float)right.value;
                else if (code == logictoken.not_equal)
                    return (double)left != (float)right.value;
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
