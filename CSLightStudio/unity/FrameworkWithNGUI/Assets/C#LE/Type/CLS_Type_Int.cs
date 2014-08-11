using System;
using System.Collections.Generic;
using System.Text;

namespace CSLE
{
    class CLS_Type_Int : ICLS_Type
    {
        public CLS_Type_Int()
        {

            function = new RegHelper_TypeFunction(typeof(int));
        }
        public string keyword
        {
            get { return "int"; }
        }
        public string _namespace
        {
            get { return ""; }
        }
        public CLType type
        {
            get { return typeof(int); }
        }

        public ICLS_Value MakeValue(object value)
        {
            CLS_Value_Value<int> v = new CLS_Value_Value<int>();
            v.value_value = (int)value;
            
            return v;

        }

        public object ConvertTo(CLS_Content env, object src, CLType targetType)
        {
            if ((Type)targetType == typeof(int))
            {
                return (int)src;
            }
            else if ((Type)targetType == typeof(uint))
            {
                return (uint)(int)src;
            }
            else if ((Type)targetType == typeof(double))
            {
                return (double)(int)src;
            }
            else if ((Type)targetType == typeof(float))
            {
                return (float)(int)src;
            }
            throw new NotImplementedException();
        }

        public object Math2Value(CLS_Content env, char code, object left, CLS_Content.Value right, out CLType returntype)
        {
            returntype = typeof(int);
            if ((Type)right.type == typeof(int))
            {
                if (code == '+')
                    return (int)left + (int)right.value;
                else if (code == '-')
                    return (int)left - (int)right.value;
                else if (code == '*')
                    return (int)left * (int)right.value;
                else if (code == '/')
                    return (int)left / (int)right.value;
                else if (code == '%')
                    return (int)left % (int)right.value;
            }
            else if ((Type)right.type == typeof(uint))
            {
                if (code == '+')
                    return (int)left + (int)(uint)right.value;
                else if (code == '-')
                    return (int)left - (int)(uint)right.value;
                else if (code == '*')
                    return (int)left * (int)(uint)right.value;
                else if (code == '/')
                    return (int)left / (int)(uint)right.value;
                else if (code == '%')
                    return (int)left % (int)(uint)right.value;
            }
            else if ((Type)right.type == typeof(double))
            {
                returntype = typeof(double);

                if (code == '+')
                    return (double)(int)left + (double)right.value;
                else if (code == '-')
                    return (double)(int)left - (double)right.value;
                else if (code == '*')
                    return (double)(int)left * (double)right.value;
                else if (code == '/')
                    return (double)(int)left / (double)right.value;
                else if (code == '%')
                    return (double)(int)left % (double)right.value;
            }
            else if ((Type)right.type == typeof(float))
            {
                returntype = typeof(float);
                if (code == '+')
                    return (float)(int)left + (float)right.value;
                else if (code == '-')
                    return (float)(int)left - (float)right.value;
                else if (code == '*')
                    return (float)(int)left * (float)right.value;
                else if (code == '/')
                    return (float)(int)left / (float)right.value;
                else if (code == '%')
                    return (float)(int)left % (float)right.value;
            }
            throw new NotImplementedException("code:"+code +" right:+"+right.type.ToString()+"="+ right.value);
        }

        public bool MathLogic(CLS_Content env, logictoken code, object left, CLS_Content.Value right)
        {
            if ((Type)right.type == typeof(int))
            {
                if (code ==  logictoken.equal)
                    return (int)left == (int)right.value;
                else if (code == logictoken.less)
                    return (int)left < (int)right.value;
                else if (code == logictoken.less_equal)
                    return (int)left <= (int)right.value;
                else if (code == logictoken.more)
                    return (int)left > (int)right.value;
                else if (code == logictoken.more_equal)
                    return (int)left >= (int)right.value;
                else if (code == logictoken.not_equal)
                    return (int)left != (int)right.value;
            }
            else if ((Type)right.type == typeof(uint))
            {
                if (code == logictoken.equal)
                    return (int)left == (uint)right.value;
                else if (code == logictoken.less)
                    return (int)left < (uint)right.value;
                else if (code == logictoken.less_equal)
                    return (int)left <= (uint)right.value;
                else if (code == logictoken.more)
                    return (int)left > (uint)right.value;
                else if (code == logictoken.more_equal)
                    return (int)left >= (uint)right.value;
                else if (code == logictoken.not_equal)
                    return (int)left != (uint)right.value;
            }
            else if ((Type)right.type == typeof(double))
            {
                if (code == logictoken.equal)
                    return (int)left == (double)right.value;
                else if (code == logictoken.less)
                    return (int)left < (double)right.value;
                else if (code == logictoken.less_equal)
                    return (int)left <= (double)right.value;
                else if (code == logictoken.more)
                    return (int)left > (double)right.value;
                else if (code == logictoken.more_equal)
                    return (int)left >= (double)right.value;
                else if (code == logictoken.not_equal)
                    return (int)left != (double)right.value;
            }
            else if ((Type)right.type == typeof(float))
            {
                if (code == logictoken.equal)
                    return (int)left == (float)right.value;
                else if (code == logictoken.less)
                    return (int)left < (float)right.value;
                else if (code == logictoken.less_equal)
                    return (int)left <= (float)right.value;
                else if (code == logictoken.more)
                    return (int)left > (float)right.value;
                else if (code == logictoken.more_equal)
                    return (int)left >= (float)right.value;
                else if (code == logictoken.not_equal)
                    return (int)left != (float)right.value;
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
            get { return (int)0; }
        }
    }
}
