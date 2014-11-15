using System;
using System.Collections.Generic;
using System.Text;

namespace CSLight
{
    class CLS_Type_Long : ICLS_Type
    {
        public CLS_Type_Long()
        {
            function = new RegHelper_TypeFunction(typeof(long));
        }
        public string keyword
        {
            get { return "long"; }
        }

        public Type type
        {
            get { return typeof(long); }
        }

        public ICLS_Value MakeValue(object value)
        {
            CLS_Value_Value<long> v = new CLS_Value_Value<long>();
            v.value_value = (long)value;

            return v;

        }

        public object ConvertTo(CLS_Environment env, object src, Type targetType)
        {
            if (targetType == typeof(long))
            {
                return (long)src;
            }
            else if (targetType == typeof(ulong))
            {
                return (ulong)(long)src;
            }
            else if (targetType == typeof(double))
            {
                return (double)(long)src;
            }
            else if (targetType == typeof(float))
            {
                return (float)(long)src;
            }
            throw new NotImplementedException();
        }

        public object Math2Value(CLS_Environment env, char code, object left, CLS_Content.Value right, out Type returntype)
        {
            returntype = typeof(long);
            if (right.type == typeof(long))
            {
                if (code == '+')
                    return (long)left + (long)right.value;
                else if (code == '-')
                    return (long)left - (long)right.value;
                else if (code == '*')
                    return (long)left * (long)right.value;
                else if (code == '/')
                    return (long)left / (long)right.value;
                else if (code == '%')
                    return (long)left % (long)right.value;
            }
            else if (right.type == typeof(ulong))
            {
                if (code == '+')
                    return (long)left + (long)(ulong)right.value;
                else if (code == '-')
                    return (long)left - (long)(ulong)right.value;
                else if (code == '*')
                    return (long)left * (long)(ulong)right.value;
                else if (code == '/')
                    return (long)left / (long)(ulong)right.value;
                else if (code == '%')
                    return (long)left % (long)(ulong)right.value;
            }
            else if (right.type == typeof(double))
            {
                returntype = typeof(double);

                if (code == '+')
                    return (double)(long)left + (double)right.value;
                else if (code == '-')
                    return (double)(long)left - (double)right.value;
                else if (code == '*')
                    return (double)(long)left * (double)right.value;
                else if (code == '/')
                    return (double)(long)left / (double)right.value;
                else if (code == '%')
                    return (double)(long)left % (double)right.value;
            }
            else if (right.type == typeof(float))
            {
                returntype = typeof(float);
                if (code == '+')
                    return (float)(long)left + (float)right.value;
                else if (code == '-')
                    return (float)(long)left - (float)right.value;
                else if (code == '*')
                    return (float)(long)left * (float)right.value;
                else if (code == '/')
                    return (float)(long)left / (float)right.value;
                else if (code == '%')
                    return (float)(long)left % (float)right.value;
            }
            throw new NotImplementedException("code:" + code + " right:+" + right.type.ToString() + "=" + right.value);
        }

        public bool MathLogic(CLS_Environment env, logictoken code, object left, CLS_Content.Value right)
        {
            if (right.type == typeof(long))
            {
                if (code == logictoken.equal)
                    return (long)left == (long)right.value;
                else if (code == logictoken.less)
                    return (long)left < (long)right.value;
                else if (code == logictoken.less_equal)
                    return (long)left <= (long)right.value;
                else if (code == logictoken.more)
                    return (long)left > (long)right.value;
                else if (code == logictoken.more_equal)
                    return (long)left >= (long)right.value;
                else if (code == logictoken.not_equal)
                    return (long)left != (long)right.value;
            }
            else if (right.type == typeof(ulong))
            {
                if (code == logictoken.equal)
                    return (long)left == (long)right.value;
                else if (code == logictoken.less)
                    return (long)left < (long)right.value;
                else if (code == logictoken.less_equal)
                    return (long)left <= (long)right.value;
                else if (code == logictoken.more)
                    return (long)left > (long)right.value;
                else if (code == logictoken.more_equal)
                    return (long)left >= (long)right.value;
                else if (code == logictoken.not_equal)
                    return (long)left != (long)right.value;
            }
            else if (right.type == typeof(long))
            {
                if (code == logictoken.equal)
                    return (long)left == (long)right.value;
                else if (code == logictoken.less)
                    return (long)left < (long)right.value;
                else if (code == logictoken.less_equal)
                    return (long)left <= (long)right.value;
                else if (code == logictoken.more)
                    return (long)left > (long)right.value;
                else if (code == logictoken.more_equal)
                    return (long)left >= (long)right.value;
                else if (code == logictoken.not_equal)
                    return (long)left != (long)right.value;
            }
            else if (right.type == typeof(double))
            {
                if (code == logictoken.equal)
                    return (long)left == (double)right.value;
                else if (code == logictoken.less)
                    return (long)left < (double)right.value;
                else if (code == logictoken.less_equal)
                    return (long)left <= (double)right.value;
                else if (code == logictoken.more)
                    return (long)left > (double)right.value;
                else if (code == logictoken.more_equal)
                    return (long)left >= (double)right.value;
                else if (code == logictoken.not_equal)
                    return (long)left != (double)right.value;
            }
            else if (right.type == typeof(float))
            {
                if (code == logictoken.equal)
                    return (long)left == (float)right.value;
                else if (code == logictoken.less)
                    return (long)left < (float)right.value;
                else if (code == logictoken.less_equal)
                    return (long)left <= (float)right.value;
                else if (code == logictoken.more)
                    return (long)left > (float)right.value;
                else if (code == logictoken.more_equal)
                    return (long)left >= (float)right.value;
                else if (code == logictoken.not_equal)
                    return (long)left != (float)right.value;
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
