using System;
using System.Collections.Generic;
using System.Text;

namespace CSLE
{
    class CLS_Type_Long : RegHelper_Type
    {
        public CLS_Type_Long()
            : base(typeof(long), "long")
        {

        }

        public override object ConvertTo(CLS_Content env, object src, CLType targetType)
        {
            bool convertSuccess = false;
            object convertedObject = NumericTypeUtils.TryConvertTo<long>(src, targetType, out convertSuccess);
            if (convertSuccess) {
                return convertedObject;
            }

            return base.ConvertTo(env, src, targetType);
        }

        public override object Math2Value(CLS_Content env, char code, object left, CLS_Content.Value right, out CLType returntype)
        {
            bool math2ValueSuccess = false;
            object value = NumericTypeUtils.Math2Value<long>(code, left, right, out returntype, out math2ValueSuccess);
            if (math2ValueSuccess) {
                return value;
            }

            return base.Math2Value(env, code, left, right, out returntype);
        }

        public override bool MathLogic(CLS_Content env, logictoken code, object left, CLS_Content.Value right)
        {
            bool mathLogicSuccess = false;
            bool value = NumericTypeUtils.MathLogic<long>(code, left, right, out mathLogicSuccess);
            if (mathLogicSuccess) {
                return value;
            }

            return base.MathLogic(env, code, left, right);
        }

        public override object DefValue
        {
            get { return (long)0; }
        }
    }
}
