using System;
using System.Collections.Generic;
using System.Text;

namespace CSLight
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
        CLS_Content.Value New(CLS_Environment environment,IList<CLS_Content.Value> _params);
        CLS_Content.Value StaticCall(CLS_Environment environment, string function, IList<CLS_Content.Value> _params);
        CLS_Content.Value StaticValueGet(CLS_Environment environment, string valuename);
        void StaticValueSet(CLS_Environment environment, string valuename, object value);
        CLS_Content.Value MemberCall(CLS_Environment environment, object object_this, string func, IList<CLS_Content.Value> _params);
        CLS_Content.Value MemberValueGet(CLS_Environment environment, object object_this, string valuename);


        void MemberValueSet(CLS_Environment environment, object object_this, string valuename, object value);

        CLS_Content.Value IndexGet(CLS_Environment environment, object object_this, object key);

        void IndexSet(CLS_Environment environment, object object_this, object key, object value);
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

        ICLS_Value MakeValue(object value);
        //自动转型能力
        object ConvertTo(CLS_Environment env, object src, Type targetType);

        //数学计算能力
        object Math2Value(CLS_Environment env, char code, object left, CLS_Content.Value right, out Type returntype);

        //逻辑计算能力
        bool MathLogic(CLS_Environment env, logictoken code, object left, CLS_Content.Value right);

        ICLS_TypeFunction function
        {
            get;
        }

    }

}
