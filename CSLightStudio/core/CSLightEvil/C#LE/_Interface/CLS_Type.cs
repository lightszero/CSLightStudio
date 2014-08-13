using System;
using System.Collections.Generic;
using System.Text;

namespace CSLE
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
        CLS_Content.Value New(CLS_Content environment, IList<CLS_Content.Value> _params);
        CLS_Content.Value StaticCall(CLS_Content environment, string function, IList<CLS_Content.Value> _params);
        CLS_Content.Value StaticValueGet(CLS_Content environment, string valuename);
        void StaticValueSet(CLS_Content environment, string valuename, object value);
        CLS_Content.Value MemberCall(CLS_Content environment, object object_this, string func, IList<CLS_Content.Value> _params);
        CLS_Content.Value MemberValueGet(CLS_Content environment, object object_this, string valuename);


        void MemberValueSet(CLS_Content environment, object object_this, string valuename, object value);

        CLS_Content.Value IndexGet(CLS_Content environment, object object_this, object key);

        void IndexSet(CLS_Content environment, object object_this, object key, object value);
    }
    public class CLType
    {
        private CLType(Type type)
        {
            this.type = type;
        }
        private CLType(SType type)
        {
            this.stype = type;
        }
        public static implicit operator Type(CLType m)
        {
            return m.type;
        }
        public static implicit operator SType(CLType m)
        {
            if (m == null) return null;

            return m.stype;
        }
        static Dictionary<Type, CLType> types = new Dictionary<Type, CLType>();
        static Dictionary<SType, CLType> stypes = new Dictionary<SType, CLType>();

        public static implicit operator CLType(Type type)
        {
            if (types.ContainsKey(type)) return types[type];
            else
            {
                var ct = new CLType(type);
                types[type] = ct;
                return ct;
            }
        }
        public static implicit operator CLType(SType type)
        {
            if (stypes.ContainsKey(type)) return stypes[type];
            else
            {
                var ct = new CLType(type);
                stypes[type] = ct;
                return ct;
            }
        }
        //public static bool operator ==(CLType left, Type right)
        //{
        //    return left.type == right;
        //}
        //public static bool operator !=(CLType left, Type right)
        //{
        //    return left != right.type;
        //}

        public override string ToString()
        {
            if (type != null) return type.ToString();
            return stype.ToString();
        }
        Type type;
        SType stype = null;
        public string Name
        {
            get
            {
                if (type != null) return type.Name;
                else return stype.Name;
            }
        }
        public string NameSpace
        {
            get
            {
                if (type != null) return type.Namespace;
                else return stype.Namespace;
            }
        }
    }
    public interface ICLS_Type
    {
        string keyword
        {
            get;
        }
        string _namespace
        {
            get;
        }
        CLType type
        {
            get;
        }
        object DefValue
        {
            get;
        }
            
        ICLS_Value MakeValue(object value);
        //自动转型能力
        object ConvertTo(CLS_Content env, object src, CLType targetType);

        //数学计算能力
        object Math2Value(CLS_Content env, char code, object left, CLS_Content.Value right, out CLType returntype);

        //逻辑计算能力
        bool MathLogic(CLS_Content env, logictoken code, object left, CLS_Content.Value right);

        ICLS_TypeFunction function
        {
            get;
        }

    }

    public interface ICLS_Type_WithBase:ICLS_Type
    {
        void SetBaseType(IList<ICLS_Type> types);

    }
    public interface ICLS_Type_Dele:ICLS_Type
    {
        //string GetParamSign(ICLS_Environment env);
        //Delegate CreateDelegate(ICLS_Environment env, SType calltype, SInstance callthis, string function);

        Delegate CreateDelegate(ICLS_Environment env, DeleFunction lambda);

        Delegate CreateDelegate(ICLS_Environment env, DeleLambda lambda);
    }
}
