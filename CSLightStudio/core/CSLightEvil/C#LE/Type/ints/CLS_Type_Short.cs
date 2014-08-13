using System;
using System.Collections.Generic;
using System.Text;

namespace CSLE
{
    class CLS_Type_Short : RegHelper_Type
    {
        public CLS_Type_Short()
            : base(typeof(short), "short")
        {

        }



        public override object ConvertTo(CLS_Content env, object src, CLType targetType)
        {
            if ((Type)targetType == typeof(int))
            {
                return (int)(short)src;
            }
            else if ((Type)targetType == typeof(uint))
            {
                return (uint)(short)src;
            }
            else if ((Type)targetType == typeof(double))
            {
                return (double)(short)src;
            }
            else if ((Type)targetType == typeof(float))
            {
                return (float)(short)src;
            }
            else if ((Type)targetType == typeof(byte))
            {
                return (byte)(short)src;
            }
            else if ((Type)targetType == typeof(char))
            {
                return (char)(short)src;
            }
            else if ((Type)targetType == typeof(ushort))
            {
                return (ushort)(short)src;
            }
            else if ((Type)targetType == typeof(sbyte))
            {
                return (sbyte)(short)src;
            }
            else if ((Type)targetType == typeof(short))
            {
                return (short)(short)src;
            }
            return base.ConvertTo(env, src, targetType);
        }

        public override object Math2Value(CLS_Content env, char code, object left, CLS_Content.Value right, out CLType returntype)
        {
            returntype = typeof(int);
            if ((Type)right.type == typeof(int))
            {
                if (code == '+')
                    return (short)left + (int)right.value;
                else if (code == '-')
                    return (short)left - (int)right.value;
                else if (code == '*')
                    return (short)left * (int)right.value;
                else if (code == '/')
                    return (short)left / (int)right.value;
                else if (code == '%')
                    return (short)left % (int)right.value;
            }
            else if ((Type)right.type == typeof(short))
            {
                if (code == '+')
                    return (short)left + (short)right.value;
                else if (code == '-')
                    return (short)left - (short)right.value;
                else if (code == '*')
                    return (short)left * (short)right.value;
                else if (code == '/')
                    return (short)left / (short)right.value;
                else if (code == '%')
                    return (short)left % (short)right.value;
            }
            else if ((Type)right.type == typeof(sbyte))
            {
                if (code == '+')
                    return (short)left + (sbyte)right.value;
                else if (code == '-')
                    return (short)left - (sbyte)right.value;
                else if (code == '*')
                    return (short)left * (sbyte)right.value;
                else if (code == '/')
                    return (short)left / (sbyte)right.value;
                else if (code == '%')
                    return (short)left % (sbyte)right.value;
            }
            else if ((Type)right.type == typeof(uint))
            {
                if (code == '+')
                    return (short)left + (int)(uint)right.value;
                else if (code == '-')
                    return (short)left - (int)(uint)right.value;
                else if (code == '*')
                    return (short)left * (int)(uint)right.value;
                else if (code == '/')
                    return (short)left / (int)(uint)right.value;
                else if (code == '%')
                    return (short)left % (int)(uint)right.value;
            }
            else if ((Type)right.type == typeof(ushort))
            {
                if (code == '+')
                    return (short)left + (int)(ushort)right.value;
                else if (code == '-')
                    return (short)left - (int)(ushort)right.value;
                else if (code == '*')
                    return (short)left * (int)(ushort)right.value;
                else if (code == '/')
                    return (short)left / (int)(ushort)right.value;
                else if (code == '%')
                    return (short)left % (int)(ushort)right.value;
            }
            else if ((Type)right.type == typeof(char))
            {
                if (code == '+')
                    return (short)left + (int)(char)right.value;
                else if (code == '-')
                    return (short)left - (int)(char)right.value;
                else if (code == '*')
                    return (short)left * (int)(char)right.value;
                else if (code == '/')
                    return (short)left / (int)(char)right.value;
                else if (code == '%')
                    return (short)left % (int)(char)right.value;
            }
            else if ((Type)right.type == typeof(byte))
            {
                if (code == '+')
                    return (short)left + (int)(byte)right.value;
                else if (code == '-')
                    return (short)left - (int)(byte)right.value;
                else if (code == '*')
                    return (short)left * (int)(byte)right.value;
                else if (code == '/')
                    return (short)left / (int)(byte)right.value;
                else if (code == '%')
                    return (short)left % (int)(byte)right.value;
            }
            else if ((Type)right.type == typeof(double))
            {
                returntype = typeof(double);

                if (code == '+')
                    return (double)(short)left + (double)right.value;
                else if (code == '-')
                    return (double)(short)left - (double)right.value;
                else if (code == '*')
                    return (double)(short)left * (double)right.value;
                else if (code == '/')
                    return (double)(short)left / (double)right.value;
                else if (code == '%')
                    return (double)(short)left % (double)right.value;
            }
            else if ((Type)right.type == typeof(float))
            {
                returntype = typeof(float);
                if (code == '+')
                    return (float)(short)left + (float)right.value;
                else if (code == '-')
                    return (float)(short)left - (float)right.value;
                else if (code == '*')
                    return (float)(short)left * (float)right.value;
                else if (code == '/')
                    return (float)(short)left / (float)right.value;
                else if (code == '%')
                    return (float)(short)left % (float)right.value;
            }
            return base.Math2Value(env, code, left, right, out returntype);
        }

        public override bool MathLogic(CLS_Content env, logictoken code, object left, CLS_Content.Value right)
        {
            if ((Type)right.type == typeof(int))
            {
                if (code == logictoken.equal)
                    return (short)left == (int)right.value;
                else if (code == logictoken.less)
                    return (short)left < (int)right.value;
                else if (code == logictoken.less_equal)
                    return (short)left <= (int)right.value;
                else if (code == logictoken.more)
                    return (short)left > (int)right.value;
                else if (code == logictoken.more_equal)
                    return (short)left >= (int)right.value;
                else if (code == logictoken.not_equal)
                    return (short)left != (int)right.value;
            }
            else if ((Type)right.type == typeof(short))
            {
                if (code == logictoken.equal)
                    return (short)left == (short)right.value;
                else if (code == logictoken.less)
                    return (short)left < (short)right.value;
                else if (code == logictoken.less_equal)
                    return (short)left <= (short)right.value;
                else if (code == logictoken.more)
                    return (short)left > (short)right.value;
                else if (code == logictoken.more_equal)
                    return (short)left >= (short)right.value;
                else if (code == logictoken.not_equal)
                    return (short)left != (short)right.value;
            }
            else if ((Type)right.type == typeof(sbyte))
            {
                if (code == logictoken.equal)
                    return (short)left == (sbyte)right.value;
                else if (code == logictoken.less)
                    return (short)left < (sbyte)right.value;
                else if (code == logictoken.less_equal)
                    return (short)left <= (sbyte)right.value;
                else if (code == logictoken.more)
                    return (short)left > (sbyte)right.value;
                else if (code == logictoken.more_equal)
                    return (short)left >= (sbyte)right.value;
                else if (code == logictoken.not_equal)
                    return (short)left != (sbyte)right.value;
            }
            else if ((Type)right.type == typeof(uint))
            {
                if (code == logictoken.equal)
                    return (short)left == (uint)right.value;
                else if (code == logictoken.less)
                    return (short)left < (uint)right.value;
                else if (code == logictoken.less_equal)
                    return (short)left <= (uint)right.value;
                else if (code == logictoken.more)
                    return (short)left > (uint)right.value;
                else if (code == logictoken.more_equal)
                    return (short)left >= (uint)right.value;
                else if (code == logictoken.not_equal)
                    return (short)left != (uint)right.value;
            }
            else if ((Type)right.type == typeof(ushort))
            {
                if (code == logictoken.equal)
                    return (short)left == (ushort)right.value;
                else if (code == logictoken.less)
                    return (short)left < (ushort)right.value;
                else if (code == logictoken.less_equal)
                    return (short)left <= (ushort)right.value;
                else if (code == logictoken.more)
                    return (short)left > (ushort)right.value;
                else if (code == logictoken.more_equal)
                    return (short)left >= (ushort)right.value;
                else if (code == logictoken.not_equal)
                    return (short)left != (ushort)right.value;
            }
            else if ((Type)right.type == typeof(char))
            {
                if (code == logictoken.equal)
                    return (short)left == (char)right.value;
                else if (code == logictoken.less)
                    return (short)left < (char)right.value;
                else if (code == logictoken.less_equal)
                    return (short)left <= (char)right.value;
                else if (code == logictoken.more)
                    return (short)left > (char)right.value;
                else if (code == logictoken.more_equal)
                    return (short)left >= (char)right.value;
                else if (code == logictoken.not_equal)
                    return (short)left != (char)right.value;
            }
            else if ((Type)right.type == typeof(byte))
            {
                if (code == logictoken.equal)
                    return (short)left == (byte)right.value;
                else if (code == logictoken.less)
                    return (short)left < (byte)right.value;
                else if (code == logictoken.less_equal)
                    return (short)left <= (byte)right.value;
                else if (code == logictoken.more)
                    return (short)left > (byte)right.value;
                else if (code == logictoken.more_equal)
                    return (short)left >= (byte)right.value;
                else if (code == logictoken.not_equal)
                    return (short)left != (byte)right.value;
            }
            else if ((Type)right.type == typeof(double))
            {
                if (code == logictoken.equal)
                    return (short)left == (double)right.value;
                else if (code == logictoken.less)
                    return (short)left < (double)right.value;
                else if (code == logictoken.less_equal)
                    return (short)left <= (double)right.value;
                else if (code == logictoken.more)
                    return (short)left > (double)right.value;
                else if (code == logictoken.more_equal)
                    return (short)left >= (double)right.value;
                else if (code == logictoken.not_equal)
                    return (short)left != (double)right.value;
            }
            else if ((Type)right.type == typeof(float))
            {
                if (code == logictoken.equal)
                    return (short)left == (float)right.value;
                else if (code == logictoken.less)
                    return (short)left < (float)right.value;
                else if (code == logictoken.less_equal)
                    return (short)left <= (float)right.value;
                else if (code == logictoken.more)
                    return (short)left > (float)right.value;
                else if (code == logictoken.more_equal)
                    return (short)left >= (float)right.value;
                else if (code == logictoken.not_equal)
                    return (short)left != (float)right.value;
            }
            return base.MathLogic(env, code, left, right);
        }


        public override object DefValue
        {
            get { return (short)0; }
        }
    }
}
