using System;
using System.Collections.Generic;

using System.Text;
namespace CSEvil.Ext
{
    class RegHelper_TypeGameObject : CSEvil.RegHelper_Type
    {
        public RegHelper_TypeGameObject(Type type, string setkeyword = null)
            : base(type, setkeyword)
        {
            function = new RegHelper_TypeFunction_TypeGameObject(type);
        }

        class RegHelper_TypeFunction_TypeGameObject : CSEvil.RegHelper_TypeFunction
        {
            public RegHelper_TypeFunction_TypeGameObject(Type type)
                : base(type)
            {

            }

            public override CLS_Content.Value MemberValueGet(CLS_Content environment, object object_this, string valuename)
            {

                if (valuename == "transform")
                {
                    CLS_Content.Value v = new CLS_Content.Value();
                    v.value = ((UnityEngine.GameObject)object_this).transform;
                    v.type = typeof(UnityEngine.Transform);
                    return v;
                }
                if (valuename == "gameObject")
                {
                    CLS_Content.Value v = new CLS_Content.Value();
                    v.value = ((UnityEngine.GameObject)object_this).gameObject;
                    v.type = typeof(UnityEngine.GameObject);
                    return v;
                }
                if (valuename == "name")
                {
                    CLS_Content.Value v = new CLS_Content.Value();
                    v.value = ((UnityEngine.GameObject)object_this).name;
                    v.type = typeof(string);
                    return v;
                }
                return base.MemberValueGet(environment, object_this, valuename);
            }
            public override void MemberValueSet(CLS_Content content, object object_this, string valuename, object value)
            {
                if (valuename == "name")
                {
                    if (value != null && value.GetType() != typeof(string))
                    {

                        value = content.environment.GetType(value.GetType()).ConvertTo(content, value, typeof(string));
                    }
                    ((UnityEngine.GameObject)object_this).name = (string)value;
                    return;
                }

                base.MemberValueSet(content, object_this, valuename, value);
            }

            public override CLS_Content.Value StaticCall(CLS_Content environment, string function, IList<CLS_Content.Value> _params)
            {
                if (function == "Instantiate")
                {
                    CLS_Content.Value v = new CLS_Content.Value();
                    v.value = UnityEngine.GameObject.Instantiate(_params[0].value as UnityEngine.GameObject);
                    v.type = _params[0].type;
                    return v;
                }
                if (function == "Destroy")
                {
                    CLS_Content.Value v = new CLS_Content.Value();
                    UnityEngine.GameObject.Destroy(_params[0].value as UnityEngine.GameObject);
                    v.type = null;
                    return v;
                }
                if (function == "DestroyImmediate")
                {
                    CLS_Content.Value v = new CLS_Content.Value();
                    UnityEngine.GameObject.DestroyImmediate(_params[0].value as UnityEngine.GameObject);
                    v.type = null;
                    return v;
                }
                return base.StaticCall(environment, function, _params);
            }
        }
    }
}