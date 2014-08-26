using System;
using System.Collections.Generic;
using System.Text;

namespace CSLE
{
    class CLS_Type_Float : RegHelper_Type
    {
        public CLS_Type_Float()
            : base(typeof(float), "float")
        {
            //function = new RegHelper_TypeFunction(typeof(float));
        }
    

        public override object ConvertTo(CLS_Content env, object src, CLType targetType)
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
            return base.ConvertTo(env, src, targetType);
        }

        public override object Math2Value(CLS_Content env, char code, object left, CLS_Content.Value right, out CLType returntype)
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
            return base.Math2Value(env, code, left, right, out returntype);
        }

        public override bool MathLogic(CLS_Content env, logictoken code, object left, CLS_Content.Value right)
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
            return base.MathLogic(env, code, left, right);
        }

        public override object DefValue
        {
            get { return (float)0; }
        }
    }
}
