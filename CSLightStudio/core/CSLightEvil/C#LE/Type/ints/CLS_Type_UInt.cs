using System;
using System.Collections.Generic;
using System.Text;

namespace CSLE
{
    class CLS_Type_UInt : RegHelper_Type
    {
        public CLS_Type_UInt()
            : base(typeof(uint), "uint")
        {
            //function = new RegHelper_TypeFunction(typeof(uint));
        }


        public override object ConvertTo(CLS_Content env, object src, CLType targetType)
        {
            if ((Type)targetType == typeof(uint))
            {
                return (uint)src;
            }
            else if ((Type)targetType == typeof(int))
            {
                return (int)(uint)src;
            }
            else if ((Type)targetType == typeof(double))
            {
                return (double)(uint)src;
            }
            else if ((Type)targetType == typeof(float))
            {
                return (float)(uint)src;
            }
            else if ((Type)targetType == typeof(ushort))
            {
                return (ushort)(uint)src;
            }
            else if ((Type)targetType == typeof(byte))
            {
                return (byte)(uint)src;
            }
            else if ((Type)targetType == typeof(char))
            {
                return (char)(uint)src;
            }
            else if ((Type)targetType == typeof(ushort))
            {
                return (ushort)(uint)src;
            }
            else if ((Type)targetType == typeof(sbyte))
            {
                return (sbyte)(uint)src;
            }
            else if ((Type)targetType == typeof(short))
            {
                return (short)(uint)src;
            }
            return base.ConvertTo(env, src, targetType);
        }

        public override object Math2Value(CLS_Content env, char code, object left, CLS_Content.Value right, out CLType returntype)
        {
            returntype = typeof(uint);
            if ((Type)right.type == typeof(uint))
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
            else if ((Type)right.type == typeof(ushort))
            {
                if (code == '+')
                    return (uint)left + (ushort)right.value;
                else if (code == '-')
                    return (uint)left - (ushort)right.value;
                else if (code == '*')
                    return (uint)left * (ushort)right.value;
                else if (code == '/')
                    return (uint)left / (ushort)right.value;
                else if (code == '%')
                    return (uint)left % (ushort)right.value;
            }
            else if ((Type)right.type == typeof(char))
            {
                if (code == '+')
                    return (uint)left + (char)right.value;
                else if (code == '-')
                    return (uint)left - (char)right.value;
                else if (code == '*')
                    return (uint)left * (char)right.value;
                else if (code == '/')
                    return (uint)left / (char)right.value;
                else if (code == '%')
                    return (uint)left % (char)right.value;
            }
            else if ((Type)right.type == typeof(byte))
            {
                if (code == '+')
                    return (uint)left + (byte)right.value;
                else if (code == '-')
                    return (uint)left - (byte)right.value;
                else if (code == '*')
                    return (uint)left * (byte)right.value;
                else if (code == '/')
                    return (uint)left / (byte)right.value;
                else if (code == '%')
                    return (uint)left % (byte)right.value;
            }
            else if ((Type)right.type == typeof(int))
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
            else if ((Type)right.type == typeof(short))
            {
                returntype = typeof(int);
                if (code == '+')
                    return (int)(uint)left + (short)right.value;
                else if (code == '-')
                    return (int)(uint)left - (short)right.value;
                else if (code == '*')
                    return (int)(uint)left * (short)right.value;
                else if (code == '/')
                    return (int)(uint)left / (short)right.value;
                else if (code == '%')
                    return (int)(uint)left % (short)right.value;
            }
            else if ((Type)right.type == typeof(sbyte))
            {
                returntype = typeof(int);
                if (code == '+')
                    return (int)(uint)left + (sbyte)right.value;
                else if (code == '-')
                    return (int)(uint)left - (sbyte)right.value;
                else if (code == '*')
                    return (int)(uint)left * (sbyte)right.value;
                else if (code == '/')
                    return (int)(uint)left / (sbyte)right.value;
                else if (code == '%')
                    return (int)(uint)left % (sbyte)right.value;
            }
            else if ((Type)right.type == typeof(double))
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
            else if ((Type)right.type == typeof(float))
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
            return base.Math2Value(env, code, left, right, out returntype);
        }

        public override bool MathLogic(CLS_Content env, logictoken code, object left, CLS_Content.Value right)
        {
            if ((Type)right.type == typeof(int))
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
                else if (code == logictoken.not_equal)
                    return (uint)left != (int)right.value;
            }
            else if ((Type)right.type == typeof(short))
            {
                if (code == logictoken.equal)
                    return (uint)left == (short)right.value;
                else if (code == logictoken.less)
                    return (uint)left < (short)right.value;
                else if (code == logictoken.less_equal)
                    return (uint)left <= (short)right.value;
                else if (code == logictoken.more)
                    return (uint)left > (short)right.value;
                else if (code == logictoken.more_equal)
                    return (uint)left >= (short)right.value;
                else if (code == logictoken.not_equal)
                    return (uint)left != (short)right.value;
            }
            else if ((Type)right.type == typeof(sbyte))
            {
                if (code == logictoken.equal)
                    return (uint)left == (sbyte)right.value;
                else if (code == logictoken.less)
                    return (uint)left < (sbyte)right.value;
                else if (code == logictoken.less_equal)
                    return (uint)left <= (sbyte)right.value;
                else if (code == logictoken.more)
                    return (uint)left > (sbyte)right.value;
                else if (code == logictoken.more_equal)
                    return (uint)left >= (sbyte)right.value;
                else if (code == logictoken.not_equal)
                    return (uint)left != (sbyte)right.value;
            }
            else if ((Type)right.type == typeof(uint))
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
                else if (code == logictoken.not_equal)
                    return (uint)left != (uint)right.value;
            }
            else if ((Type)right.type == typeof(ushort))
            {
                if (code == logictoken.equal)
                    return (uint)left == (ushort)right.value;
                else if (code == logictoken.less)
                    return (uint)left < (ushort)right.value;
                else if (code == logictoken.less_equal)
                    return (uint)left <= (ushort)right.value;
                else if (code == logictoken.more)
                    return (uint)left > (ushort)right.value;
                else if (code == logictoken.more_equal)
                    return (uint)left >= (ushort)right.value;
                else if (code == logictoken.not_equal)
                    return (uint)left != (ushort)right.value;
            }
            else if ((Type)right.type == typeof(char))
            {
                if (code == logictoken.equal)
                    return (uint)left == (char)right.value;
                else if (code == logictoken.less)
                    return (uint)left < (char)right.value;
                else if (code == logictoken.less_equal)
                    return (uint)left <= (char)right.value;
                else if (code == logictoken.more)
                    return (uint)left > (char)right.value;
                else if (code == logictoken.more_equal)
                    return (uint)left >= (char)right.value;
                else if (code == logictoken.not_equal)
                    return (uint)left != (char)right.value;
            }
            else if ((Type)right.type == typeof(byte))
            {
                if (code == logictoken.equal)
                    return (uint)left == (byte)right.value;
                else if (code == logictoken.less)
                    return (uint)left < (byte)right.value;
                else if (code == logictoken.less_equal)
                    return (uint)left <= (byte)right.value;
                else if (code == logictoken.more)
                    return (uint)left > (byte)right.value;
                else if (code == logictoken.more_equal)
                    return (uint)left >= (byte)right.value;
                else if (code == logictoken.not_equal)
                    return (uint)left != (byte)right.value;
            }
            else if ((Type)right.type == typeof(double))
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
                else if (code == logictoken.not_equal)
                    return (uint)left != (double)right.value;
            }
            else if ((Type)right.type == typeof(float))
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
                else if (code == logictoken.not_equal)
                    return (uint)left != (float)right.value;
            }
            return base.MathLogic(env, code, left, right);
        }



        public override object DefValue
        {
            get { return (uint)0; }
        }
    }
}
