using System;
using System.Collections.Generic;
using System.Text;

namespace CSEvil
{
    public enum logictoken
    {
        less,           //<
        less_equal,     //<=
        more,           //>
        more_equal,     //>=
        equal,          //==
        not_equal       //!=

    }
    public interface ICLS_TypeFunction
    {
        CLS_Content.Value New(ICLS_Environment environment, IList<CLS_Content.Value> _params);
        CLS_Content.Value StaticCall(ICLS_Environment environment, string function, IList<CLS_Content.Value> _params);
        CLS_Content.Value StaticValueGet(ICLS_Environment environment, string valuename);
        void StaticValueSet(ICLS_Environment environment, string valuename, object value);
        CLS_Content.Value MemberCall(ICLS_Environment environment, object object_this, string func, IList<CLS_Content.Value> _params);
        CLS_Content.Value MemberValueGet(ICLS_Environment environment, object object_this, string valuename);


        void MemberValueSet(ICLS_Environment environment, object object_this, string valuename, object value);

        CLS_Content.Value IndexGet(ICLS_Environment environment, object object_this, object key);

        void IndexSet(ICLS_Environment environment, object object_this, object key, object value);
    }
    public interface ICLS_Type
    {
        string keyword
        {
            get;
        }
        Type type
        {
            get;
        }
        object DefValue
        {
            get;
        }
        ICLS_Value MakeValue(object value);
        //自动转型能力
        object ConvertTo(ICLS_Environment env, object src, Type targetType);

        //数学计算能力
        object Math2Value(ICLS_Environment env, char code, object left, CLS_Content.Value right, out Type returntype);

        //逻辑计算能力
        bool MathLogic(ICLS_Environment env, logictoken code, object left, CLS_Content.Value right);

        ICLS_TypeFunction function
        {
            get;
        }

    }

}
