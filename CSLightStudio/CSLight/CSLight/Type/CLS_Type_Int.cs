﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CSLight
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

        public Type type
        {
            get { return typeof(int); }
        }

        public ICLS_Value MakeValue(object value)
        {
            CLS_Value_Value<int> v = new CLS_Value_Value<int>();
            v.value_value = (int)value;
            
            return v;

        }

        public object ConvertTo(CLS_Environment env, object src, Type targetType)
        {
            if (targetType == typeof(int))
            {
                return (int)src;
            }
            else if (targetType == typeof(uint))
            {
                return (uint)(int)src;
            }
            else if (targetType == typeof(double))
            {
                return (double)(int)src;
            }
            else if (targetType == typeof(float))
            {
                return (float)(int)src;
            }
            throw new NotImplementedException();
        }

        public object Math2Value(CLS_Environment env, char code, object left, CLS_Content.Value right, out Type returntype)
        {
            returntype = typeof(int);
            if (right.type == typeof(int))
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
            else if (right.type == typeof(uint))
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
            else if (right.type == typeof(double))
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
            else if (right.type == typeof(float))
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

        public bool MathLogic(CLS_Environment env, logictoken code, object left, CLS_Content.Value right)
        {
            if (right.type == typeof(int))
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
            else if (right.type == typeof(uint))
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
            else if (right.type == typeof(long))
            {
                if (code == logictoken.equal)
                    return (int)left == (long)right.value;
                else if (code == logictoken.less)
                    return (int)left < (long)right.value;
                else if (code == logictoken.less_equal)
                    return (int)left <= (long)right.value;
                else if (code == logictoken.more)
                    return (int)left > (long)right.value;
                else if (code == logictoken.more_equal)
                    return (int)left >= (long)right.value;
                else if (code == logictoken.not_equal)
                    return (int)left != (long)right.value;
            }
            else if (right.type == typeof(double))
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
            else if (right.type == typeof(float))
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
    }
}
