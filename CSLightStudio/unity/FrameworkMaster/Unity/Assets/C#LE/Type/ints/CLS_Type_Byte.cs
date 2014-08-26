using System;
using System.Collections.Generic;
using System.Text;

namespace CSLE
{
    class CLS_Type_Byte : RegHelper_Type
    {
        public CLS_Type_Byte()
            : base(typeof(byte), "byte")
        {
            //function = new RegHelper_TypeFunction(typeof(uint));
        }


        public override object ConvertTo(CLS_Content env, object src, CLType targetType)
        {
            if ((Type)targetType == typeof(uint))
            {
                return (uint)(byte)src;
            }
            else if ((Type)targetType == typeof(int))
            {
                return (int)(byte)src;
            }
            else if ((Type)targetType == typeof(double))
            {
                return (double)(byte)src;
            }
            else if ((Type)targetType == typeof(float))
            {
                return (float)(byte)src;
            }
            else if ((Type)targetType == typeof(ushort))
            {
                return (ushort)(byte)src;
            }
            else if ((Type)targetType == typeof(byte))
            {
                return (byte)(byte)src;
            }
            else if ((Type)targetType == typeof(char))
            {
                return (char)(byte)src;
            }
            else if ((Type)targetType == typeof(ushort))
            {
                return (ushort)(byte)src;
            }
            else if ((Type)targetType == typeof(sbyte))
            {
                return (sbyte)(byte)src;
            }
            else if ((Type)targetType == typeof(short))
            {
                return (short)(byte)src;
            }
            return base.ConvertTo(env, src, targetType);
        }

        public override object Math2Value(CLS_Content env, char code, object left, CLS_Content.Value right, out CLType returntype)
        {
            returntype = typeof(int);
            if ((Type)right.type == typeof(uint))
            {
                if (code == '+')
                    return (byte)left + (uint)right.value;
                else if (code == '-')
                    return (byte)left - (uint)right.value;
                else if (code == '*')
                    return (byte)left * (uint)right.value;
                else if (code == '/')
                    return (byte)left / (uint)right.value;
                else if (code == '%')
                    return (byte)left % (uint)right.value;
            }
            else if ((Type)right.type == typeof(ushort))
            {
                if (code == '+')
                    return (byte)left + (ushort)right.value;
                else if (code == '-')
                    return (byte)left - (ushort)right.value;
                else if (code == '*')
                    return (byte)left * (ushort)right.value;
                else if (code == '/')
                    return (byte)left / (ushort)right.value;
                else if (code == '%')
                    return (byte)left % (ushort)right.value;
            }
            else if ((Type)right.type == typeof(char))
            {
                if (code == '+')
                    return (byte)left + (char)right.value;
                else if (code == '-')
                    return (byte)left - (char)right.value;
                else if (code == '*')
                    return (byte)left * (char)right.value;
                else if (code == '/')
                    return (byte)left / (char)right.value;
                else if (code == '%')
                    return (byte)left % (char)right.value;
            }
            else if ((Type)right.type == typeof(byte))
            {

                if (code == '+')
                    return (byte)left + (byte)right.value;
                else if (code == '-')
                    return (byte)left - (byte)right.value;
                else if (code == '*')
                    return (byte)left * (byte)right.value;
                else if (code == '/')
                    return (byte)left / (byte)right.value;
                else if (code == '%')
                    return (byte)left % (byte)right.value;
            }
            else if ((Type)right.type == typeof(int))
            {
                returntype = typeof(int);
                if (code == '+')
                    return (int)(byte)left + (int)right.value;
                else if (code == '-')
                    return (int)(byte)left - (int)right.value;
                else if (code == '*')
                    return (int)(byte)left * (int)right.value;
                else if (code == '/')
                    return (int)(byte)left / (int)right.value;
                else if (code == '%')
                    return (int)(byte)left % (int)right.value;
            }
            else if ((Type)right.type == typeof(short))
            {
                returntype = typeof(int);
                if (code == '+')
                    return (int)(byte)left + (short)right.value;
                else if (code == '-')
                    return (int)(byte)left - (short)right.value;
                else if (code == '*')
                    return (int)(byte)left * (short)right.value;
                else if (code == '/')
                    return (int)(byte)left / (short)right.value;
                else if (code == '%')
                    return (int)(byte)left % (short)right.value;
            }
            else if ((Type)right.type == typeof(sbyte))
            {
                returntype = typeof(int);
                if (code == '+')
                    return (int)(byte)left + (sbyte)right.value;
                else if (code == '-')
                    return (int)(byte)left - (sbyte)right.value;
                else if (code == '*')
                    return (int)(byte)left * (sbyte)right.value;
                else if (code == '/')
                    return (int)(byte)left / (sbyte)right.value;
                else if (code == '%')
                    return (int)(byte)left % (sbyte)right.value;
            }
            else if ((Type)right.type == typeof(double))
            {
                returntype = typeof(double);

                if (code == '+')
                    return (double)(byte)left + (double)right.value;
                else if (code == '-')
                    return (double)(byte)left - (double)right.value;
                else if (code == '*')
                    return (double)(byte)left * (double)right.value;
                else if (code == '/')
                    return (double)(byte)left / (double)right.value;
                else if (code == '%')
                    return (double)(byte)left % (double)right.value;
            }
            else if ((Type)right.type == typeof(float))
            {
                returntype = typeof(float);
                if (code == '+')
                    return (float)(byte)left + (float)right.value;
                else if (code == '-')
                    return (float)(byte)left - (float)right.value;
                else if (code == '*')
                    return (float)(byte)left * (float)right.value;
                else if (code == '/')
                    return (float)(byte)left / (float)right.value;
                else if (code == '%')
                    return (float)(byte)left % (float)right.value;
            }
            return base.Math2Value(env, code, left, right, out returntype);
        }

        public override bool MathLogic(CLS_Content env, logictoken code, object left, CLS_Content.Value right)
        {
            if ((Type)right.type == typeof(int))
            {
                if (code == logictoken.equal)
                    return (byte)left == (int)right.value;
                else if (code == logictoken.less)
                    return (byte)left < (int)right.value;
                else if (code == logictoken.less_equal)
                    return (byte)left <= (int)right.value;
                else if (code == logictoken.more)
                    return (byte)left > (int)right.value;
                else if (code == logictoken.more_equal)
                    return (byte)left >= (int)right.value;
                else if (code == logictoken.not_equal)
                    return (byte)left != (int)right.value;
            }
            else if ((Type)right.type == typeof(short))
            {
                if (code == logictoken.equal)
                    return (byte)left == (short)right.value;
                else if (code == logictoken.less)
                    return (byte)left < (short)right.value;
                else if (code == logictoken.less_equal)
                    return (byte)left <= (short)right.value;
                else if (code == logictoken.more)
                    return (byte)left > (short)right.value;
                else if (code == logictoken.more_equal)
                    return (byte)left >= (short)right.value;
                else if (code == logictoken.not_equal)
                    return (byte)left != (short)right.value;
            }
            else if ((Type)right.type == typeof(sbyte))
            {
                if (code == logictoken.equal)
                    return (byte)left == (sbyte)right.value;
                else if (code == logictoken.less)
                    return (byte)left < (sbyte)right.value;
                else if (code == logictoken.less_equal)
                    return (byte)left <= (sbyte)right.value;
                else if (code == logictoken.more)
                    return (byte)left > (sbyte)right.value;
                else if (code == logictoken.more_equal)
                    return (byte)left >= (sbyte)right.value;
                else if (code == logictoken.not_equal)
                    return (byte)left != (sbyte)right.value;
            }
            else if ((Type)right.type == typeof(uint))
            {
                if (code == logictoken.equal)
                    return (byte)left == (uint)right.value;
                else if (code == logictoken.less)
                    return (byte)left < (uint)right.value;
                else if (code == logictoken.less_equal)
                    return (byte)left <= (uint)right.value;
                else if (code == logictoken.more)
                    return (byte)left > (uint)right.value;
                else if (code == logictoken.more_equal)
                    return (byte)left >= (uint)right.value;
                else if (code == logictoken.not_equal)
                    return (byte)left != (uint)right.value;
            }
            else if ((Type)right.type == typeof(ushort))
            {
                if (code == logictoken.equal)
                    return (byte)left == (ushort)right.value;
                else if (code == logictoken.less)
                    return (byte)left < (ushort)right.value;
                else if (code == logictoken.less_equal)
                    return (byte)left <= (ushort)right.value;
                else if (code == logictoken.more)
                    return (byte)left > (ushort)right.value;
                else if (code == logictoken.more_equal)
                    return (byte)left >= (ushort)right.value;
                else if (code == logictoken.not_equal)
                    return (byte)left != (ushort)right.value;
            }
            else if ((Type)right.type == typeof(char))
            {
                if (code == logictoken.equal)
                    return (byte)left == (char)right.value;
                else if (code == logictoken.less)
                    return (byte)left < (char)right.value;
                else if (code == logictoken.less_equal)
                    return (byte)left <= (char)right.value;
                else if (code == logictoken.more)
                    return (byte)left > (char)right.value;
                else if (code == logictoken.more_equal)
                    return (byte)left >= (char)right.value;
                else if (code == logictoken.not_equal)
                    return (byte)left != (char)right.value;
            }
            else if ((Type)right.type == typeof(byte))
            {
                if (code == logictoken.equal)
                    return (byte)left == (byte)right.value;
                else if (code == logictoken.less)
                    return (byte)left < (byte)right.value;
                else if (code == logictoken.less_equal)
                    return (byte)left <= (byte)right.value;
                else if (code == logictoken.more)
                    return (byte)left > (byte)right.value;
                else if (code == logictoken.more_equal)
                    return (byte)left >= (byte)right.value;
                else if (code == logictoken.not_equal)
                    return (byte)left != (byte)right.value;
            }
            else if ((Type)right.type == typeof(double))
            {
                if (code == logictoken.equal)
                    return (byte)left == (double)right.value;
                else if (code == logictoken.less)
                    return (byte)left < (double)right.value;
                else if (code == logictoken.less_equal)
                    return (byte)left <= (double)right.value;
                else if (code == logictoken.more)
                    return (byte)left > (double)right.value;
                else if (code == logictoken.more_equal)
                    return (byte)left >= (double)right.value;
                else if (code == logictoken.not_equal)
                    return (byte)left != (double)right.value;
            }
            else if ((Type)right.type == typeof(float))
            {
                if (code == logictoken.equal)
                    return (byte)left == (float)right.value;
                else if (code == logictoken.less)
                    return (byte)left < (float)right.value;
                else if (code == logictoken.less_equal)
                    return (byte)left <= (float)right.value;
                else if (code == logictoken.more)
                    return (byte)left > (float)right.value;
                else if (code == logictoken.more_equal)
                    return (byte)left >= (float)right.value;
                else if (code == logictoken.not_equal)
                    return (byte)left != (float)right.value;
            }
            return base.MathLogic(env, code, left, right);
        }



        public override object DefValue
        {
            get { return (byte)0; }
        }
    }
}
