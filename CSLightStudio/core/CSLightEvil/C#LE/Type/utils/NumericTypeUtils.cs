using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSLE {
    /// <summary>
    /// 数值类型系列类的公用工具函数.
    /// 因为这些函数逻辑都是固定的，不存在多态行为，不适合放在现有的继承结构中去实现，故而独立出来。
    /// </summary>
    public class NumericTypeUtils {

        /// <summary>
        /// 类型转换.
        /// </summary>
        /// <typeparam name="OriginalType"></typeparam>
        /// <param name="src"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public static object TryConvertTo<OriginalType>(object src, CLType targetType, out bool convertSuccess) where OriginalType : struct {
            
            convertSuccess = true;

            try {
                decimal srcValue = GetDecimalValue(typeof(OriginalType), src);
                return Decimal2TargetType(targetType, srcValue);
            } catch (Exception) {
                convertSuccess = false;
                return null;
            }
        }

        /// <summary>
        /// Math2Value.
        /// </summary>
        /// <typeparam name="LeftType"></typeparam>
        /// <param name="opCode"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="returntype"></param>
        /// <param name="math2ValueSuccess"></param>
        /// <returns></returns>
        public static object Math2Value<LeftType>(char opCode, object left, CLS_Content.Value right, out CLType returntype, out bool math2ValueSuccess) where LeftType : struct {

            math2ValueSuccess = true;

            try {
                decimal leftValue = GetDecimalValue(typeof(LeftType), left);
                decimal rightValue = GetDecimalValue(right.type, right.value);
                decimal finalValue = 0;

                switch (opCode) {
                    case '+':
                        finalValue = leftValue + rightValue;
                        break;
                    case '-':
                        finalValue = leftValue - rightValue;
                        break;
                    case '*':
                        finalValue = leftValue * rightValue;
                        break;
                    case '/':
                        finalValue = leftValue / rightValue;
                        break;
                    case '%':
                        finalValue = leftValue % rightValue;
                        break;
                    default:
                        throw new Exception("Invalid math operation::opCode = " + opCode);
                }

                returntype = GetReturnType_Math2Value(typeof(LeftType), right.type);
                return Decimal2TargetType(returntype, finalValue);

            } catch (Exception) {
                math2ValueSuccess = false;
                returntype = null;
                return null;
            }
        }

        public static bool MathLogic<LeftType>(logictoken logicCode, object left, CLS_Content.Value right, out bool mathLogicSuccess) {

            mathLogicSuccess = true;

            try {
                decimal leftValue = GetDecimalValue(typeof(LeftType), left);
                decimal rightValue = GetDecimalValue(right.type, right.value);

                switch (logicCode) {
                    case logictoken.equal:
                        return leftValue == rightValue;
                    case logictoken.less:
                        return leftValue < rightValue;
                    case logictoken.less_equal:
                        return leftValue <= rightValue;
                    case logictoken.more:
                        return leftValue > rightValue;
                    case logictoken.more_equal:
                        return leftValue >= rightValue;
                    case logictoken.not_equal:
                        return leftValue != rightValue;
                    default:
                        throw new Exception("Invalid logic operation::logicCode = " + logicCode.ToString());
                }
            } catch (Exception) {
                mathLogicSuccess = false;
                return false;
            }
        }

        private static decimal GetDecimalValue(Type type, object value) {

            if (type == typeof(double))
                return (decimal)Convert.ToDouble(value);
            if (type == typeof(float))
                return (decimal)Convert.ToSingle(value);
            if (type == typeof(long))
                return Convert.ToInt64(value);
            if (type == typeof(int))
                return Convert.ToInt32(value);
            if (type == typeof(uint))
                return Convert.ToUInt32(value);
            if (type == typeof(short))
                return Convert.ToInt16(value);
            if (type == typeof(ushort))
                return Convert.ToUInt16(value);
            if (type == typeof(sbyte))
                return Convert.ToSByte(value);
            if (type == typeof(byte))
                return Convert.ToByte(value);
            if (type == typeof(char))
                return Convert.ToChar(value);

            throw new Exception("unknown decimal type...");
        }

        private static object Decimal2TargetType(Type type, decimal value) {
            if (type == typeof(double))
                return (double)value;
            if (type == typeof(float))
                return (float)value;
            if (type == typeof(long))
                return (long)value;
            if (type == typeof(int))
                return (int)value;
            if (type == typeof(uint))
                return (uint)value;
            if (type == typeof(short))
                return (short)value;
            if (type == typeof(ushort))
                return (ushort)value;
            if (type == typeof(sbyte))
                return (sbyte)value;
            if (type == typeof(byte))
                return (byte)value;
            if (type == typeof(char))
                return (char)value;

            throw new Exception("unknown target type...");
        }

        /// <summary>
        /// 获取Math2Value的返回类型.
        /// 这里并没有严格仿照C#的类型系统进行数学计算时的返回类型。
        /// </summary>
        private static Type GetReturnType_Math2Value(Type leftType, Type rightType) {

            int ltIndex = _TypeList.IndexOf(leftType);
            int rtIndex = _TypeList.IndexOf(rightType);

            //0. double 和 float 类型优先级最高.
            if (ltIndex == T_Double || rtIndex == T_Double ) {
                return typeof(double);
            }
            if (ltIndex == T_Float || rtIndex == T_Float) {
                return typeof(float);
            }

            //1. 整数运算时，long 类型优先级最高.
            if (ltIndex == T_Long || rtIndex == T_Long) {
                return typeof(long);
            }

            //2. int 和 uint 结合会返回 long.
            if ((ltIndex == T_Int && rtIndex == T_UInt) || (ltIndex == T_UInt && rtIndex == T_Int)) { 
                return typeof(long);
            }

            //3. uint 和 非int结合会返回 uint.
            if ((ltIndex == T_UInt && rtIndex != T_Int) || (rtIndex == T_UInt && ltIndex != T_Int)) {
                return typeof(uint);
            }

            //其他统一返回 int.
            //在C#类型系统中，即使是两个 ushort 结合返回的也是int类型。
            return typeof(int);
        }

        private static List<Type> _TypeList = new List<Type> { 
            typeof(double),
            typeof(float),
            typeof(long),
            typeof(int),
            typeof(uint),
            typeof(short),
            typeof(ushort),
            typeof(sbyte),
            typeof(byte),
            typeof(char)
        };

        private const int T_Double = 0;
        private const int T_Float = 1;
        private const int T_Long = 2;
        private const int T_Int = 3;
        private const int T_UInt = 4;
        private const int T_Short = 5;
        private const int T_UShort = 6;
        private const int T_SByte = 7;
        private const int T_Byte = 8;
        private const int T_Char = 9;
    }
}