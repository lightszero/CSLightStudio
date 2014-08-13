using System;
using System.Collections.Generic;
using System.Text;

namespace CSLE
{
    class CLS_Type_Int : RegHelper_Type
    {
        public CLS_Type_Int()
            : base(typeof(int), "int")
        {

        }



        public override object ConvertTo(CLS_Content env, object src, CLType targetType)
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
            else if ((Type)targetType == typeof(byte))
            {
                return (byte)(int)src;
            }
            else if ((Type)targetType == typeof(char))
            {
                return (char)(int)src;
            }
            else if ((Type)targetType == typeof(ushort))
            {
                return (ushort)(int)src;
            }
            else if ((Type)targetType == typeof(sbyte))
            {
                return (sbyte)(int)src;
            }
            else if ((Type)targetType == typeof(short))
            {
                return (short)(int)src;
            }
            return base.ConvertTo(env, src, targetType);
        }

        public override object Math2Value(CLS_Content env, char code, object left, CLS_Content.Value right, out CLType returntype)
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
            else if ((Type)right.type == typeof(short))
            {
                if (code == '+')
                    return (int)left + (short)right.value;
                else if (code == '-')
                    return (int)left - (short)right.value;
                else if (code == '*')
                    return (int)left * (short)right.value;
                else if (code == '/')
                    return (int)left / (short)right.value;
                else if (code == '%')
                    return (int)left % (short)right.value;
            }
            else if ((Type)right.type == typeof(sbyte))
            {
                if (code == '+')
                    return (int)left + (sbyte)right.value;
                else if (code == '-')
                    return (int)left - (sbyte)right.value;
                else if (code == '*')
                    return (int)left * (sbyte)right.value;
                else if (code == '/')
                    return (int)left / (sbyte)right.value;
                else if (code == '%')
                    return (int)left % (sbyte)right.value;
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
            else if ((Type)right.type == typeof(ushort))
            {
                if (code == '+')
                    return (int)left + (int)(ushort)right.value;
                else if (code == '-')
                    return (int)left - (int)(ushort)right.value;
                else if (code == '*')
                    return (int)left * (int)(ushort)right.value;
                else if (code == '/')
                    return (int)left / (int)(ushort)right.value;
                else if (code == '%')
                    return (int)left % (int)(ushort)right.value;
            }
            else if ((Type)right.type == typeof(char))
            {
                if (code == '+')
                    return (int)left + (int)(char)right.value;
                else if (code == '-')
                    return (int)left - (int)(char)right.value;
                else if (code == '*')
                    return (int)left * (int)(char)right.value;
                else if (code == '/')
                    return (int)left / (int)(char)right.value;
                else if (code == '%')
                    return (int)left % (int)(char)right.value;
            }
            else if ((Type)right.type == typeof(byte))
            {
                if (code == '+')
                    return (int)left + (int)(byte)right.value;
                else if (code == '-')
                    return (int)left - (int)(byte)right.value;
                else if (code == '*')
                    return (int)left * (int)(byte)right.value;
                else if (code == '/')
                    return (int)left / (int)(byte)right.value;
                else if (code == '%')
                    return (int)left % (int)(byte)right.value;
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
            return base.Math2Value(env, code, left, right, out returntype);
        }

        public override bool MathLogic(CLS_Content env, logictoken code, object left, CLS_Content.Value right)
        {
            if ((Type)right.type == typeof(int))
            {
                if (code == logictoken.equal)
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
            else if ((Type)right.type == typeof(short))
            {
                if (code == logictoken.equal)
                    return (int)left == (short)right.value;
                else if (code == logictoken.less)
                    return (int)left < (short)right.value;
                else if (code == logictoken.less_equal)
                    return (int)left <= (short)right.value;
                else if (code == logictoken.more)
                    return (int)left > (short)right.value;
                else if (code == logictoken.more_equal)
                    return (int)left >= (short)right.value;
                else if (code == logictoken.not_equal)
                    return (int)left != (short)right.value;
            }
            else if ((Type)right.type == typeof(sbyte))
            {
                if (code == logictoken.equal)
                    return (int)left == (sbyte)right.value;
                else if (code == logictoken.less)
                    return (int)left < (sbyte)right.value;
                else if (code == logictoken.less_equal)
                    return (int)left <= (sbyte)right.value;
                else if (code == logictoken.more)
                    return (int)left > (sbyte)right.value;
                else if (code == logictoken.more_equal)
                    return (int)left >= (sbyte)right.value;
                else if (code == logictoken.not_equal)
                    return (int)left != (sbyte)right.value;
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
            else if ((Type)right.type == typeof(ushort))
            {
                if (code == logictoken.equal)
                    return (int)left == (ushort)right.value;
                else if (code == logictoken.less)
                    return (int)left < (ushort)right.value;
                else if (code == logictoken.less_equal)
                    return (int)left <= (ushort)right.value;
                else if (code == logictoken.more)
                    return (int)left > (ushort)right.value;
                else if (code == logictoken.more_equal)
                    return (int)left >= (ushort)right.value;
                else if (code == logictoken.not_equal)
                    return (int)left != (ushort)right.value;
            }
            else if ((Type)right.type == typeof(char))
            {
                if (code == logictoken.equal)
                    return (int)left == (char)right.value;
                else if (code == logictoken.less)
                    return (int)left < (char)right.value;
                else if (code == logictoken.less_equal)
                    return (int)left <= (char)right.value;
                else if (code == logictoken.more)
                    return (int)left > (char)right.value;
                else if (code == logictoken.more_equal)
                    return (int)left >= (char)right.value;
                else if (code == logictoken.not_equal)
                    return (int)left != (char)right.value;
            }
            else if ((Type)right.type == typeof(byte))
            {
                if (code == logictoken.equal)
                    return (int)left == (byte)right.value;
                else if (code == logictoken.less)
                    return (int)left < (byte)right.value;
                else if (code == logictoken.less_equal)
                    return (int)left <= (byte)right.value;
                else if (code == logictoken.more)
                    return (int)left > (byte)right.value;
                else if (code == logictoken.more_equal)
                    return (int)left >= (byte)right.value;
                else if (code == logictoken.not_equal)
                    return (int)left != (byte)right.value;
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
            return base.MathLogic(env, code, left, right);
        }


        public override object DefValue
        {
            get { return (int)0; }
        }
    }
}
