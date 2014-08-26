using System;
using System.Collections.Generic;
using System.Text;

namespace CSLE
{
    class CLS_Type_Sbyte : RegHelper_Type
    {
        public CLS_Type_Sbyte()
            : base(typeof(sbyte), "sbyte")
        {

        }



        public override object ConvertTo(CLS_Content env, object src, CLType targetType)
        {
            if ((Type)targetType == typeof(int))
            {
                return (int)(sbyte)src;
            }
            else if ((Type)targetType == typeof(uint))
            {
                return (uint)(sbyte)src;
            }
            else if ((Type)targetType == typeof(double))
            {
                return (double)(sbyte)src;
            }
            else if ((Type)targetType == typeof(float))
            {
                return (float)(sbyte)src;
            }
            else if ((Type)targetType == typeof(byte))
            {
                return (byte)(sbyte)src;
            }
            else if ((Type)targetType == typeof(char))
            {
                return (char)(sbyte)src;
            }
            else if ((Type)targetType == typeof(ushort))
            {
                return (ushort)(sbyte)src;
            }
            else if ((Type)targetType == typeof(sbyte))
            {
                return (sbyte)(sbyte)src;
            }
            else if ((Type)targetType == typeof(short))
            {
                return (short)(sbyte)src;
            }
            return base.ConvertTo(env, src, targetType);
        }

        public override object Math2Value(CLS_Content env, char code, object left, CLS_Content.Value right, out CLType returntype)
        {
            returntype = typeof(int);
            if ((Type)right.type == typeof(int))
            {
                if (code == '+')
                    return (sbyte)left + (int)right.value;
                else if (code == '-')
                    return (sbyte)left - (int)right.value;
                else if (code == '*')
                    return (sbyte)left * (int)right.value;
                else if (code == '/')
                    return (sbyte)left / (int)right.value;
                else if (code == '%')
                    return (sbyte)left % (int)right.value;
            }
            else if ((Type)right.type == typeof(short))
            {
                if (code == '+')
                    return (sbyte)left + (short)right.value;
                else if (code == '-')
                    return (sbyte)left - (short)right.value;
                else if (code == '*')
                    return (sbyte)left * (short)right.value;
                else if (code == '/')
                    return (sbyte)left / (short)right.value;
                else if (code == '%')
                    return (sbyte)left % (short)right.value;
            }
            else if ((Type)right.type == typeof(sbyte))
            {
                if (code == '+')
                    return (sbyte)left + (sbyte)right.value;
                else if (code == '-')
                    return (sbyte)left - (sbyte)right.value;
                else if (code == '*')
                    return (sbyte)left * (sbyte)right.value;
                else if (code == '/')
                    return (sbyte)left / (sbyte)right.value;
                else if (code == '%')
                    return (sbyte)left % (sbyte)right.value;
            }
            else if ((Type)right.type == typeof(uint))
            {
                if (code == '+')
                    return (sbyte)left + (int)(uint)right.value;
                else if (code == '-')
                    return (sbyte)left - (int)(uint)right.value;
                else if (code == '*')
                    return (sbyte)left * (int)(uint)right.value;
                else if (code == '/')
                    return (sbyte)left / (int)(uint)right.value;
                else if (code == '%')
                    return (sbyte)left % (int)(uint)right.value;
            }
            else if ((Type)right.type == typeof(ushort))
            {
                if (code == '+')
                    return (sbyte)left + (int)(ushort)right.value;
                else if (code == '-')
                    return (sbyte)left - (int)(ushort)right.value;
                else if (code == '*')
                    return (sbyte)left * (int)(ushort)right.value;
                else if (code == '/')
                    return (sbyte)left / (int)(ushort)right.value;
                else if (code == '%')
                    return (sbyte)left % (int)(ushort)right.value;
            }
            else if ((Type)right.type == typeof(char))
            {
                if (code == '+')
                    return (sbyte)left + (int)(char)right.value;
                else if (code == '-')
                    return (sbyte)left - (int)(char)right.value;
                else if (code == '*')
                    return (sbyte)left * (int)(char)right.value;
                else if (code == '/')
                    return (sbyte)left / (int)(char)right.value;
                else if (code == '%')
                    return (sbyte)left % (int)(char)right.value;
            }
            else if ((Type)right.type == typeof(byte))
            {
                if (code == '+')
                    return (sbyte)left + (int)(byte)right.value;
                else if (code == '-')
                    return (sbyte)left - (int)(byte)right.value;
                else if (code == '*')
                    return (sbyte)left * (int)(byte)right.value;
                else if (code == '/')
                    return (sbyte)left / (int)(byte)right.value;
                else if (code == '%')
                    return (sbyte)left % (int)(byte)right.value;
            }
            else if ((Type)right.type == typeof(double))
            {
                returntype = typeof(double);

                if (code == '+')
                    return (double)(sbyte)left + (double)right.value;
                else if (code == '-')
                    return (double)(sbyte)left - (double)right.value;
                else if (code == '*')
                    return (double)(sbyte)left * (double)right.value;
                else if (code == '/')
                    return (double)(sbyte)left / (double)right.value;
                else if (code == '%')
                    return (double)(sbyte)left % (double)right.value;
            }
            else if ((Type)right.type == typeof(float))
            {
                returntype = typeof(float);
                if (code == '+')
                    return (float)(sbyte)left + (float)right.value;
                else if (code == '-')
                    return (float)(sbyte)left - (float)right.value;
                else if (code == '*')
                    return (float)(sbyte)left * (float)right.value;
                else if (code == '/')
                    return (float)(sbyte)left / (float)right.value;
                else if (code == '%')
                    return (float)(sbyte)left % (float)right.value;
            }
            return base.Math2Value(env, code, left, right, out returntype);
        }

        public override bool MathLogic(CLS_Content env, logictoken code, object left, CLS_Content.Value right)
        {
            if ((Type)right.type == typeof(int))
            {
                if (code == logictoken.equal)
                    return (sbyte)left == (int)right.value;
                else if (code == logictoken.less)
                    return (sbyte)left < (int)right.value;
                else if (code == logictoken.less_equal)
                    return (sbyte)left <= (int)right.value;
                else if (code == logictoken.more)
                    return (sbyte)left > (int)right.value;
                else if (code == logictoken.more_equal)
                    return (sbyte)left >= (int)right.value;
                else if (code == logictoken.not_equal)
                    return (sbyte)left != (int)right.value;
            }
            else if ((Type)right.type == typeof(short))
            {
                if (code == logictoken.equal)
                    return (sbyte)left == (short)right.value;
                else if (code == logictoken.less)
                    return (sbyte)left < (short)right.value;
                else if (code == logictoken.less_equal)
                    return (sbyte)left <= (short)right.value;
                else if (code == logictoken.more)
                    return (sbyte)left > (short)right.value;
                else if (code == logictoken.more_equal)
                    return (sbyte)left >= (short)right.value;
                else if (code == logictoken.not_equal)
                    return (sbyte)left != (short)right.value;
            }
            else if ((Type)right.type == typeof(sbyte))
            {
                if (code == logictoken.equal)
                    return (sbyte)left == (sbyte)right.value;
                else if (code == logictoken.less)
                    return (sbyte)left < (sbyte)right.value;
                else if (code == logictoken.less_equal)
                    return (sbyte)left <= (sbyte)right.value;
                else if (code == logictoken.more)
                    return (sbyte)left > (sbyte)right.value;
                else if (code == logictoken.more_equal)
                    return (sbyte)left >= (sbyte)right.value;
                else if (code == logictoken.not_equal)
                    return (sbyte)left != (sbyte)right.value;
            }
            else if ((Type)right.type == typeof(uint))
            {
                if (code == logictoken.equal)
                    return (sbyte)left == (uint)right.value;
                else if (code == logictoken.less)
                    return (sbyte)left < (uint)right.value;
                else if (code == logictoken.less_equal)
                    return (sbyte)left <= (uint)right.value;
                else if (code == logictoken.more)
                    return (sbyte)left > (uint)right.value;
                else if (code == logictoken.more_equal)
                    return (sbyte)left >= (uint)right.value;
                else if (code == logictoken.not_equal)
                    return (sbyte)left != (uint)right.value;
            }
            else if ((Type)right.type == typeof(ushort))
            {
                if (code == logictoken.equal)
                    return (sbyte)left == (ushort)right.value;
                else if (code == logictoken.less)
                    return (sbyte)left < (ushort)right.value;
                else if (code == logictoken.less_equal)
                    return (sbyte)left <= (ushort)right.value;
                else if (code == logictoken.more)
                    return (sbyte)left > (ushort)right.value;
                else if (code == logictoken.more_equal)
                    return (sbyte)left >= (ushort)right.value;
                else if (code == logictoken.not_equal)
                    return (sbyte)left != (ushort)right.value;
            }
            else if ((Type)right.type == typeof(char))
            {
                if (code == logictoken.equal)
                    return (sbyte)left == (char)right.value;
                else if (code == logictoken.less)
                    return (sbyte)left < (char)right.value;
                else if (code == logictoken.less_equal)
                    return (sbyte)left <= (char)right.value;
                else if (code == logictoken.more)
                    return (sbyte)left > (char)right.value;
                else if (code == logictoken.more_equal)
                    return (sbyte)left >= (char)right.value;
                else if (code == logictoken.not_equal)
                    return (sbyte)left != (char)right.value;
            }
            else if ((Type)right.type == typeof(byte))
            {
                if (code == logictoken.equal)
                    return (sbyte)left == (byte)right.value;
                else if (code == logictoken.less)
                    return (sbyte)left < (byte)right.value;
                else if (code == logictoken.less_equal)
                    return (sbyte)left <= (byte)right.value;
                else if (code == logictoken.more)
                    return (sbyte)left > (byte)right.value;
                else if (code == logictoken.more_equal)
                    return (sbyte)left >= (byte)right.value;
                else if (code == logictoken.not_equal)
                    return (sbyte)left != (byte)right.value;
            }
            else if ((Type)right.type == typeof(double))
            {
                if (code == logictoken.equal)
                    return (sbyte)left == (double)right.value;
                else if (code == logictoken.less)
                    return (sbyte)left < (double)right.value;
                else if (code == logictoken.less_equal)
                    return (sbyte)left <= (double)right.value;
                else if (code == logictoken.more)
                    return (sbyte)left > (double)right.value;
                else if (code == logictoken.more_equal)
                    return (sbyte)left >= (double)right.value;
                else if (code == logictoken.not_equal)
                    return (sbyte)left != (double)right.value;
            }
            else if ((Type)right.type == typeof(float))
            {
                if (code == logictoken.equal)
                    return (sbyte)left == (float)right.value;
                else if (code == logictoken.less)
                    return (sbyte)left < (float)right.value;
                else if (code == logictoken.less_equal)
                    return (sbyte)left <= (float)right.value;
                else if (code == logictoken.more)
                    return (sbyte)left > (float)right.value;
                else if (code == logictoken.more_equal)
                    return (sbyte)left >= (float)right.value;
                else if (code == logictoken.not_equal)
                    return (sbyte)left != (float)right.value;
            }
            return base.MathLogic(env, code, left, right);
        }


        public override object DefValue
        {
            get { return (sbyte)0; }
        }
    }
}
