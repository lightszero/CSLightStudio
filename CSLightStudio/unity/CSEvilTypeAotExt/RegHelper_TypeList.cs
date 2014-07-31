using System;
using System.Collections.Generic;

using System.Text;
namespace CSEvil.Ext
{
    class RegHelper_TypeList : CSEvil.RegHelper_Type
    {
        public RegHelper_TypeList(Type type, string setkeyword = null)
            : base(type, setkeyword)
        {
            function = new RegHeper_TypeFunction_TypeList(type);
        }

        class RegHeper_TypeFunction_TypeList : CSEvil.RegHelper_TypeFunction
        {
            public RegHeper_TypeFunction_TypeList(Type type)
                : base(type)
            {

            }

            public override CLS_Content.Value MemberValueGet(CLS_Content environment, object object_this, string valuename)
            {
                if(valuename=="Count")
                {
                    System.Collections.ICollection list = object_this as System.Collections.ICollection;
                    CLS_Content.Value v = new CLS_Content.Value();
                    v.value = list.Count;
                    v.type = typeof(int);
                    return v;
                }
                return base.MemberValueGet(environment, object_this, valuename);
            }
        }
    }
}