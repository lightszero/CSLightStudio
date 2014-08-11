using System;
using System.Collections.Generic;
using System.Text;

namespace CSLE
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
        public string _namespace
        {
            get { return ""; }
        }
        public CLType type
        {
            get { return typeof(double); }
        }

        public ICLS_Value MakeValue(object value)
        {
            CLS_Value_Value<double> v = new CLS_Value_Value<double>();
            v.value_value = (double)value;
            
            return v;

        }

        public object ConvertTo(CLS_Content env, object src, CLType targetType)
        {
            if ((Type)targetType == typeof(int))
            {
                return (int)(double)src;
            }
            else if ((Type)targetType == typeof(uint))
            {
                return (uint)(double)src;
            }
            else if ((Type)targetType == typeof(double))
            {
                return (double)src;
            }
            else if ((Type)targetType == typeof(float))
            {
                return (float)(double)src;
            }
            throw new NotImplementedException();
        }

        public object Math2Value(CLS_Content env, char code, object left, CLS_Content.Value right, out CLType returntype)
        {
            returntype = typeof(double);
            if ((Type)right.type == typeof(int))
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
            else if ((Type)right.type == typeof(uint))
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
            else if ((Type)right.type == typeof(double))
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
            else if ((Type)right.type == typeof(float))
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

        public bool MathLogic(CLS_Content env, logictoken code, object left, CLS_Content.Value right)
        {
            if ((Type)right.type == typeof(int))
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
            else if ((Type)right.type == typeof(uint))
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
            else if ((Type)right.type == typeof(double))
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
            else if ((Type)right.type == typeof(float))
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
        public object DefValue
        {
            get { return (double)0; }
        }
    }
}
