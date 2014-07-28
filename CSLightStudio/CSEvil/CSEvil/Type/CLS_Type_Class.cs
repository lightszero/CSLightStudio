using System;
using System.Collections.Generic;
using System.Text;

namespace CSEvil
{
    public class SType : Type, ICLS_TypeFunction
    {
        public SType(string keyword, string _namespace = "")
        {
            this._Name = keyword;
            this._Namespace = _namespace;
            this._GUID = Guid.Empty;

        }
        #region impl type
        public override bool Equals(object o)
        {
            return base.Equals(o);
        }
        public new bool Equals(Type o)
        {
            return base.Equals(o);
        }
        public override int GetHashCode()
        {
            return (this.Namespace + "." + this.Name).GetHashCode();
        }
        public override System.Reflection.Assembly Assembly
        {
            get
            {
                return null;
            }
        }

        public override string AssemblyQualifiedName
        {
            get
            {
                return null;
            }
        }

        public override Type BaseType
        {
            get { return typeof(object); }
        }

        public override string FullName
        {
            get { return Namespace + "." + Name; }
        }

        public override Guid GUID
        {
            get
            {
                return _GUID;

            }

        }
        Guid _GUID;
        public override string Namespace
        {
            get
            {
                return _Namespace;
            }
        }

        public override string Name
        {
            get
            {
                return _Name;
            }

        }
        string _Name;
        string _Namespace;
        #endregion
        #region notimpl Type
        protected override System.Reflection.TypeAttributes GetAttributeFlagsImpl()
        {
            throw new NotImplementedException();
        }

        protected override System.Reflection.ConstructorInfo GetConstructorImpl(System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, System.Reflection.CallingConventions callConvention, Type[] types, System.Reflection.ParameterModifier[] modifiers)
        {
            throw new NotImplementedException();
        }

        public override System.Reflection.ConstructorInfo[] GetConstructors(System.Reflection.BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public override Type GetElementType()
        {
            throw new NotImplementedException();
        }

        public override System.Reflection.EventInfo GetEvent(string name, System.Reflection.BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public override System.Reflection.EventInfo[] GetEvents(System.Reflection.BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public override System.Reflection.FieldInfo GetField(string name, System.Reflection.BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public override System.Reflection.FieldInfo[] GetFields(System.Reflection.BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public override Type GetInterface(string name, bool ignoreCase)
        {
            throw new NotImplementedException();
        }

        public override Type[] GetInterfaces()
        {
            throw new NotImplementedException();
        }

        public override System.Reflection.MemberInfo[] GetMembers(System.Reflection.BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        protected override System.Reflection.MethodInfo GetMethodImpl(string name, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, System.Reflection.CallingConventions callConvention, Type[] types, System.Reflection.ParameterModifier[] modifiers)
        {
            throw new NotImplementedException();
        }

        public override System.Reflection.MethodInfo[] GetMethods(System.Reflection.BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public override Type GetNestedType(string name, System.Reflection.BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public override Type[] GetNestedTypes(System.Reflection.BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        public override System.Reflection.PropertyInfo[] GetProperties(System.Reflection.BindingFlags bindingAttr)
        {
            throw new NotImplementedException();
        }

        protected override System.Reflection.PropertyInfo GetPropertyImpl(string name, System.Reflection.BindingFlags bindingAttr, System.Reflection.Binder binder, Type returnType, Type[] types, System.Reflection.ParameterModifier[] modifiers)
        {
            throw new NotImplementedException();
        }

        protected override bool HasElementTypeImpl()
        {
            throw new NotImplementedException();
        }

        public override object InvokeMember(string name, System.Reflection.BindingFlags invokeAttr, System.Reflection.Binder binder, object target, object[] args, System.Reflection.ParameterModifier[] modifiers, System.Globalization.CultureInfo culture, string[] namedParameters)
        {
            throw new NotImplementedException();
        }

        protected override bool IsArrayImpl()
        {
            throw new NotImplementedException();
        }

        protected override bool IsByRefImpl()
        {
            throw new NotImplementedException();
        }

        protected override bool IsCOMObjectImpl()
        {
            throw new NotImplementedException();
        }

        protected override bool IsPointerImpl()
        {
            throw new NotImplementedException();
        }

        protected override bool IsPrimitiveImpl()
        {
            throw new NotImplementedException();
        }

        public override System.Reflection.Module Module
        {
            get { throw new NotImplementedException(); }
        }


        public override Type UnderlyingSystemType
        {
            get
            {
                return this;
            }
        }

        public override object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            throw new NotImplementedException();
        }

        public override object[] GetCustomAttributes(bool inherit)
        {
            throw new NotImplementedException();
        }

        public override bool IsDefined(Type attributeType, bool inherit)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Script IMPL
        CLS_Content mycontent = null;
        public CLS_Content.Value New(ICLS_Environment environment, IList<CLS_Content.Value> _params)
        {
            if (mycontent == null)
                mycontent = new CLS_Content(environment);
            CLS_Value_ScriptValue sv = new CLS_Value_ScriptValue();
            sv.value_type = this;
            sv.value_value = new SInstance();
            sv.value_value.type = this;
            foreach (var i in this.members)
            {
                if (i.Value.bStatic == false)
                {
                    if (i.Value.expr_defvalue == null)
                        sv.value_value.member[i.Key] = null;
                    else
                        sv.value_value.member[i.Key] = i.Value.expr_defvalue.ComputeValue(mycontent);
                }
            }
            if (this.functions.ContainsKey(this.Name))//有同名函数就调用
            {
                MemberCall(environment, sv.value_value, this.Name, _params);
            }
            return CLS_Content.Value.FromICLS_Value(sv);
        }

        public CLS_Content.Value StaticCall(ICLS_Environment environment, string function, IList<CLS_Content.Value> _params)
        {
            throw new NotImplementedException();
        }

        public CLS_Content.Value StaticValueGet(ICLS_Environment environment, string valuename)
        {
            throw new NotImplementedException();
        }

        public void StaticValueSet(ICLS_Environment environment, string valuename, object value)
        {
            throw new NotImplementedException();
        }

        public CLS_Content.Value MemberCall(ICLS_Environment environment, object object_this, string func, IList<CLS_Content.Value> _params)
        {
            if (this.functions.ContainsKey(func))
            {
                if (this.functions[func].bStatic == false)
                {
                    CLS_Content content = new CLS_Content(environment);
                    content.CallType = this;
                    content.CallThis = object_this as SInstance;

                    int i = 0;
                    foreach (var p in this.functions[func]._params)
                    {
                        content.DefineAndSet(p.Key, p.Value.type, _params[i]);
                        i++;
                    }
                    return this.functions[func].expr_runtime.ComputeValue(content);
                }
            }
            throw new NotImplementedException();
        }

        public CLS_Content.Value MemberValueGet(ICLS_Environment environment, object object_this, string valuename)
        {
            throw new NotImplementedException();
        }

        public void MemberValueSet(ICLS_Environment environment, object object_this, string valuename, object value)
        {
            throw new NotImplementedException();
        }

        public CLS_Content.Value IndexGet(ICLS_Environment environment, object object_this, object key)
        {
            throw new NotImplementedException();
        }

        public void IndexSet(ICLS_Environment environment, object object_this, object key, object value)
        {
            throw new NotImplementedException();
        }
        #endregion

        public class Function
        {
            public bool bPublic;
            public bool bStatic;
            public Dictionary<string, ICLS_Type> _params = new Dictionary<string, ICLS_Type>();
            public ICLS_Type _returntype;
            public ICLS_Expression expr_runtime;
        }
        public class Member
        {
            public ICLS_Type type;
            public bool bPublic;
            public bool bStatic;
            public ICLS_Expression expr_defvalue;
        }

        public Dictionary<string, Function> functions = new Dictionary<string, Function>();
        public Dictionary<string, Member> members = new Dictionary<string, Member>();
        public Dictionary<string, CLS_Content.Value> staticMemberInstance = new Dictionary<string, CLS_Content.Value>();

    }
    public class SInstance
    {
        public SType type;
        public Dictionary<string, CLS_Content.Value> member = new Dictionary<string, CLS_Content.Value>();//成员
    }
    public class CLS_Type_Class : ICLS_Type
    {
        public CLS_Type_Class(string keyword)
        {
            this.keyword = keyword;
            type = new SType(keyword);
        }
        public string keyword
        {
            get;
            private set;
        }

        public Type type
        {
            get;
            private set;
        }

        public ICLS_Value MakeValue(object value)
        {
            CLS_Value_ScriptValue svalue = new CLS_Value_ScriptValue();
            svalue.value_value = value as SInstance;
            svalue.value_type = type as SType;
            return svalue;
        }

        public object ConvertTo(ICLS_Environment env, object src, Type targetType)
        {
            throw new NotImplementedException();
        }

        public object Math2Value(ICLS_Environment env, char code, object left, CLS_Content.Value right, out Type returntype)
        {
            throw new NotImplementedException();
        }

        public bool MathLogic(ICLS_Environment env, logictoken code, object left, CLS_Content.Value right)
        {
            throw new NotImplementedException();
        }

        public ICLS_TypeFunction function
        {
            get { return type as ICLS_TypeFunction; }
        }
    }
}
