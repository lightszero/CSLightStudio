using System;
using System.Collections.Generic;
using System.Text;

namespace CSLight
{
    class CLS_Type_UInt : ICLS_Type
    {
        public CLS_Type_UInt()
        {
            function = new RegHelper_TypeFunction(typeof(uint));
        }
        public string keyword
        {
            get { return "uint"; }
        }

        public Type type
        {
            get { return typeof(uint); }
        }

        public ICLS_Value MakeValue(object value)
        {
            CLS_Value_Value<uint> v = new CLS_Value_Value<uint>();
            v.value_value = (uint)value;
            
            return v;

        }

        public object ConvertTo(CLS_Environment env, object src, Type targetType)
        {
            if (targetType == typeof(uint))
            {
                return (uint)src;
            }
            else if (targetType == typeof(int))
            {
                return (int)(uint)src;
            }
            else if (targetType == typeof(double))
            {
                return (double)(uint)src;
            }
            else if (targetType == typeof(float))
            {
                return (float)(uint)src;
            }
            throw new NotImplementedException();
        }

        public object Math2Value(CLS_Environment env, char code, object left, CLS_Content.Value right, out Type returntype)
        {
            returntype = typeof(uint);
            if (right.type == typeof(uint))
            {
                if (code == '+')
                    return (uint)left + (uint)right.value;
                else if (code == '-')
                    return (uint)left - (uint)right.value;
                else if (code == '*')
                    return (uint)left * (uint)right.value;
                else if (code == '/')
                    return (uint)left / (uint)right.value;
                else if (code == '%')
                    return (uint)left % (uint)right.value;
            }
            else if (right.type == typeof(int))
            {
                returntype = typeof(int);
                if (code == '+')
                    return (int)(uint)left + (int)right.value;
                else if (code == '-')
                    return (int)(uint)left - (int)right.value;
                else if (code == '*')
                    return (int)(uint)left * (int)right.value;
                else if (code == '/')
                    return (int)(uint)left / (int)right.value;
                else if (code == '%')
                    return (int)(uint)left % (int)right.value;
            }
            else if (right.type == typeof(double))
            {
                returntype = typeof(double);

                if (code == '+')
                    return (double)(uint)left + (double)right.value;
                else if (code == '-')
                    return (double)(uint)left - (double)right.value;
                else if (code == '*')
                    return (double)(uint)left * (double)right.value;
                else if (code == '/')
                    return (double)(uint)left / (double)right.value;
                else if (code == '%')
                    return (double)(uint)left % (double)right.value;
            }
            else if (right.type == typeof(float))
            {
                returntype = typeof(float);
                if (code == '+')
                    return (float)(uint)left + (float)right.value;
                else if (code == '-')
                    return (float)(uint)left - (float)right.value;
                else if (code == '*')
                    return (float)(uint)left * (float)right.value;
                else if (code == '/')
                    return (float)(uint)left / (float)right.value;
                else if (code == '%')
                    return (float)(uint)left % (float)right.value;
            }
            throw new NotImplementedException();
        }

        public bool MathLogic(CLS_Environment env, logictoken code, object left, CLS_Content.Value right)
        {
            if (right.type == typeof(int))
            {
                if (code == logictoken.equal)
                    return (uint)left == (int)right.value;
                else if (code == logictoken.less)
                    return (uint)left < (int)right.value;
                else if (code == logictoken.less_equal)
                    return (uint)left <= (int)right.value;
                else if (code == logictoken.more)
                    return (uint)left > (int)right.value;
                else if (code == logictoken.more_equal)
                    return (uint)left >= (int)right.value;
                else if (code == logictoken.more_equal)
                    return (uint)left != (int)right.value;
            }
            else if (right.type == typeof(uint))
            {
                if (code == logictoken.equal)
                    return (uint)left == (uint)right.value;
                else if (code == logictoken.less)
                    return (uint)left < (uint)right.value;
                else if (code == logictoken.less_equal)
                    return (uint)left <= (uint)right.value;
                else if (code == logictoken.more)
                    return (uint)left > (uint)right.value;
                else if (code == logictoken.more_equal)
                    return (uint)left >= (uint)right.value;
                else if (code == logictoken.more_equal)
                    return (uint)left != (uint)right.value;
            }
            else if (right.type == typeof(double))
            {
                if (code == logictoken.equal)
                    return (uint)left == (double)right.value;
                else if (code == logictoken.less)
                    return (uint)left < (double)right.value;
                else if (code == logictoken.less_equal)
                    return (uint)left <= (double)right.value;
                else if (code == logictoken.more)
                    return (uint)left > (double)right.value;
                else if (code == logictoken.more_equal)
                    return (uint)left >= (double)right.value;
                else if (code == logictoken.more_equal)
                    return (uint)left != (double)right.value;
            }
            else if (right.type == typeof(float))
            {
                if (code == logictoken.equal)
                    return (uint)left == (float)right.value;
                else if (code == logictoken.less)
                    return (uint)left < (float)right.value;
                else if (code == logictoken.less_equal)
                    return (uint)left <= (float)right.value;
                else if (code == logictoken.more)
                    return (uint)left > (float)right.value;
                else if (code == logictoken.more_equal)
                    return (uint)left >= (float)right.value;
                else if (code == logictoken.more_equal)
                    return (uint)left != (float)right.value;
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
