using System;
using System.Collections.Generic;
using System.Text;

namespace CSLE
{
    class CLS_Type_Char : RegHelper_Type
    {
        public CLS_Type_Char()
            : base(typeof(char), "char")
        {
            //function = new RegHelper_TypeFunction(typeof(uint));
        }


        public override object ConvertTo(CLS_Content env, object src, CLType targetType)
        {
            if ((Type)targetType == typeof(uint))
            {
                return (uint)(char)src;
            }
            else if ((Type)targetType == typeof(int))
            {
                return (int)(char)src;
            }
            else if ((Type)targetType == typeof(double))
            {
                return (double)(char)src;
            }
            else if ((Type)targetType == typeof(float))
            {
                return (float)(char)src;
            }
            else if ((Type)targetType == typeof(ushort))
            {
                return (ushort)(char)src;
            }
            else if ((Type)targetType == typeof(byte))
            {
                return (byte)(char)src;
            }
            else if ((Type)targetType == typeof(char))
            {
                return (char)(char)src;
            }
            else if ((Type)targetType == typeof(ushort))
            {
                return (ushort)(char)src;
            }
            else if ((Type)targetType == typeof(sbyte))
            {
                return (sbyte)(char)src;
            }
            else if ((Type)targetType == typeof(short))
            {
                return (short)(char)src;
            }
            return base.ConvertTo(env, src, targetType);
        }

        public override object Math2Value(CLS_Content env, char code, object left, CLS_Content.Value right, out CLType returntype)
        {
            returntype = typeof(uint);
            if ((Type)right.type == typeof(uint))
            {
                if (code == '+')
                    return (char)left + (uint)right.value;
                else if (code == '-')
                    return (char)left - (uint)right.value;
                else if (code == '*')
                    return (char)left * (uint)right.value;
                else if (code == '/')
                    return (char)left / (uint)right.value;
                else if (code == '%')
                    return (char)left % (uint)right.value;
            }
            else if ((Type)right.type == typeof(ushort))
            {
                if (code == '+')
                    return (char)left + (ushort)right.value;
                else if (code == '-')
                    return (char)left - (ushort)right.value;
                else if (code == '*')
                    return (char)left * (ushort)right.value;
                else if (code == '/')
                    return (char)left / (ushort)right.value;
                else if (code == '%')
                    return (char)left % (ushort)right.value;
            }
            else if ((Type)right.type == typeof(char))
            {
                if (code == '+')
                    return (char)left + (char)right.value;
                else if (code == '-')
                    return (char)left - (char)right.value;
                else if (code == '*')
                    return (char)left * (char)right.value;
                else if (code == '/')
                    return (char)left / (char)right.value;
                else if (code == '%')
                    return (char)left % (char)right.value;
            }
            else if ((Type)right.type == typeof(byte))
            {
                if (code == '+')
                    return (char)left + (byte)right.value;
                else if (code == '-')
                    return (char)left - (byte)right.value;
                else if (code == '*')
                    return (char)left * (byte)right.value;
                else if (code == '/')
                    return (char)left / (byte)right.value;
                else if (code == '%')
                    return (char)left % (byte)right.value;
            }
            else if ((Type)right.type == typeof(int))
            {
                returntype = typeof(int);
                if (code == '+')
                    return (int)(char)left + (int)right.value;
                else if (code == '-')
                    return (int)(char)left - (int)right.value;
                else if (code == '*')
                    return (int)(char)left * (int)right.value;
                else if (code == '/')
                    return (int)(char)left / (int)right.value;
                else if (code == '%')
                    return (int)(char)left % (int)right.value;
            }
            else if ((Type)right.type == typeof(short))
            {
                returntype = typeof(int);
                if (code == '+')
                    return (int)(char)left + (short)right.value;
                else if (code == '-')
                    return (int)(char)left - (short)right.value;
                else if (code == '*')
                    return (int)(char)left * (short)right.value;
                else if (code == '/')
                    return (int)(char)left / (short)right.value;
                else if (code == '%')
                    return (int)(char)left % (short)right.value;
            }
            else if ((Type)right.type == typeof(sbyte))
            {
                returntype = typeof(int);
                if (code == '+')
                    return (int)(char)left + (sbyte)right.value;
                else if (code == '-')
                    return (int)(char)left - (sbyte)right.value;
                else if (code == '*')
                    return (int)(char)left * (sbyte)right.value;
                else if (code == '/')
                    return (int)(char)left / (sbyte)right.value;
                else if (code == '%')
                    return (int)(char)left % (sbyte)right.value;
            }
            else if ((Type)right.type == typeof(double))
            {
                returntype = typeof(double);

                if (code == '+')
                    return (double)(char)left + (double)right.value;
                else if (code == '-')
                    return (double)(char)left - (double)right.value;
                else if (code == '*')
                    return (double)(char)left * (double)right.value;
                else if (code == '/')
                    return (double)(char)left / (double)right.value;
                else if (code == '%')
                    return (double)(char)left % (double)right.value;
            }
            else if ((Type)right.type == typeof(float))
            {
                returntype = typeof(float);
                if (code == '+')
                    return (float)(char)left + (float)right.value;
                else if (code == '-')
                    return (float)(char)left - (float)right.value;
                else if (code == '*')
                    return (float)(char)left * (float)right.value;
                else if (code == '/')
                    return (float)(char)left / (float)right.value;
                else if (code == '%')
                    return (float)(char)left % (float)right.value;
            }
            return base.Math2Value(env, code, left, right, out returntype);
        }

        public override bool MathLogic(CLS_Content env, logictoken code, object left, CLS_Content.Value right)
        {
            if ((Type)right.type == typeof(int))
            {
                if (code == logictoken.equal)
                    return (char)left == (int)right.value;
                else if (code == logictoken.less)
                    return (char)left < (int)right.value;
                else if (code == logictoken.less_equal)
                    return (char)left <= (int)right.value;
                else if (code == logictoken.more)
                    return (char)left > (int)right.value;
                else if (code == logictoken.more_equal)
                    return (char)left >= (int)right.value;
                else if (code == logictoken.not_equal)
                    return (char)left != (int)right.value;
            }
            else if ((Type)right.type == typeof(short))
            {
                if (code == logictoken.equal)
                    return (char)left == (short)right.value;
                else if (code == logictoken.less)
                    return (char)left < (short)right.value;
                else if (code == logictoken.less_equal)
                    return (char)left <= (short)right.value;
                else if (code == logictoken.more)
                    return (char)left > (short)right.value;
                else if (code == logictoken.more_equal)
                    return (char)left >= (short)right.value;
                else if (code == logictoken.not_equal)
                    return (char)left != (short)right.value;
            }
            else if ((Type)right.type == typeof(sbyte))
            {
                if (code == logictoken.equal)
                    return (char)left == (sbyte)right.value;
                else if (code == logictoken.less)
                    return (char)left < (sbyte)right.value;
                else if (code == logictoken.less_equal)
                    return (char)left <= (sbyte)right.value;
                else if (code == logictoken.more)
                    return (char)left > (sbyte)right.value;
                else if (code == logictoken.more_equal)
                    return (char)left >= (sbyte)right.value;
                else if (code == logictoken.not_equal)
                    return (char)left != (sbyte)right.value;
            }
            else if ((Type)right.type == typeof(uint))
            {
                if (code == logictoken.equal)
                    return (char)left == (uint)right.value;
                else if (code == logictoken.less)
                    return (char)left < (uint)right.value;
                else if (code == logictoken.less_equal)
                    return (char)left <= (uint)right.value;
                else if (code == logictoken.more)
                    return (char)left > (uint)right.value;
                else if (code == logictoken.more_equal)
                    return (char)left >= (uint)right.value;
                else if (code == logictoken.not_equal)
                    return (char)left != (uint)right.value;
            }
            else if ((Type)right.type == typeof(ushort))
            {
                if (code == logictoken.equal)
                    return (char)left == (ushort)right.value;
                else if (code == logictoken.less)
                    return (char)left < (ushort)right.value;
                else if (code == logictoken.less_equal)
                    return (char)left <= (ushort)right.value;
                else if (code == logictoken.more)
                    return (char)left > (ushort)right.value;
                else if (code == logictoken.more_equal)
                    return (char)left >= (ushort)right.value;
                else if (code == logictoken.not_equal)
                    return (char)left != (ushort)right.value;
            }
            else if ((Type)right.type == typeof(char))
            {
                if (code == logictoken.equal)
                    return (char)left == (char)right.value;
                else if (code == logictoken.less)
                    return (char)left < (char)right.value;
                else if (code == logictoken.less_equal)
                    return (char)left <= (char)right.value;
                else if (code == logictoken.more)
                    return (char)left > (char)right.value;
                else if (code == logictoken.more_equal)
                    return (char)left >= (char)right.value;
                else if (code == logictoken.not_equal)
                    return (char)left != (char)right.value;
            }
            else if ((Type)right.type == typeof(byte))
            {
                if (code == logictoken.equal)
                    return (char)left == (byte)right.value;
                else if (code == logictoken.less)
                    return (char)left < (byte)right.value;
                else if (code == logictoken.less_equal)
                    return (char)left <= (byte)right.value;
                else if (code == logictoken.more)
                    return (char)left > (byte)right.value;
                else if (code == logictoken.more_equal)
                    return (char)left >= (byte)right.value;
                else if (code == logictoken.not_equal)
                    return (char)left != (byte)right.value;
            }
            else if ((Type)right.type == typeof(double))
            {
                if (code == logictoken.equal)
                    return (char)left == (double)right.value;
                else if (code == logictoken.less)
                    return (char)left < (double)right.value;
                else if (code == logictoken.less_equal)
                    return (char)left <= (double)right.value;
                else if (code == logictoken.more)
                    return (char)left > (double)right.value;
                else if (code == logictoken.more_equal)
                    return (char)left >= (double)right.value;
                else if (code == logictoken.not_equal)
                    return (char)left != (double)right.value;
            }
            else if ((Type)right.type == typeof(float))
            {
                if (code == logictoken.equal)
                    return (char)left == (float)right.value;
                else if (code == logictoken.less)
                    return (char)left < (float)right.value;
                else if (code == logictoken.less_equal)
                    return (char)left <= (float)right.value;
                else if (code == logictoken.more)
                    return (char)left > (float)right.value;
                else if (code == logictoken.more_equal)
                    return (char)left >= (float)right.value;
                else if (code == logictoken.not_equal)
                    return (char)left != (float)right.value;
            }
            return base.MathLogic(env, code, left, right);
        }



        public override object DefValue
        {
            get { return (char)0; }
        }
    }
}
