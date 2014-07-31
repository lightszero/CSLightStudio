using System;
using System.Collections.Generic;

using System.Text;
namespace CSEvil.Ext
{
    class RegHelper_TypeComponent : CSEvil.RegHelper_Type
    {
        public RegHelper_TypeComponent(Type type, string setkeyword = null):base(type,setkeyword)
        {
             function = new RegHelper_TypeFunction_TypeComponent(type);
        }

        class RegHelper_TypeFunction_TypeComponent : CSEvil.RegHelper_TypeFunction
        {
            public RegHelper_TypeFunction_TypeComponent(Type type):base(type)
            {

            }

            public override CLS_Content.Value MemberValueGet(CLS_Content environment, object object_this, string valuename)
            {
                if(valuename =="gameObject")
                {
                    CLS_Content.Value v = new CLS_Content.Value();
                    v.value= ((UnityEngine.Component)object_this).gameObject;
                    v.type = typeof(UnityEngine.GameObject);
                    return v;
                }
                if (valuename == "transform")
                {
                    CLS_Content.Value v = new CLS_Content.Value();
                    v.value = ((UnityEngine.Component)object_this).transform;
                    v.type = typeof(UnityEngine.Transform);
                    return v;
                }
                return base.MemberValueGet(environment, object_this, valuename);
            }
        }
    }
}