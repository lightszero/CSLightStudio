using System;
using System.Collections.Generic;

using System.Text;
namespace CSEvil.Ext
{
    class RegHelper_TypeDict : CSEvil.RegHelper_Type
    {
        public RegHelper_TypeDict(Type type, string setkeyword = null)
            : base(type, setkeyword)
        {
            function = new RegHeper_TypeFunction_TypeDict(type);
        }

        class RegHeper_TypeFunction_TypeDict : CSEvil.RegHelper_TypeFunction
        {
            public RegHeper_TypeFunction_TypeDict(Type type)
                : base(type)
            {

            }

            public override CLS_Content.Value MemberValueGet(CLS_Content content, object object_this, string valuename)
            {
                if (valuename == "Keys")
                {

                    System.Collections.IDictionary dict = object_this as System.Collections.IDictionary;
                    CLS_Content.Value v = new CLS_Content.Value();
                    v.value = dict.Keys;
                    v.type = typeof(System.Collections.ICollection);
                    return v;
                }
                if (valuename == "Values")
                {
                    System.Collections.IDictionary dict = object_this as System.Collections.IDictionary;

                    CLS_Content.Value v = new CLS_Content.Value();
                    v.value = dict.Values;
                    v.type = typeof(System.Collections.ICollection);
                    return v;
                }
                if(valuename=="Count")
                {
                    System.Collections.IDictionary dict = object_this as System.Collections.IDictionary;
                    CLS_Content.Value v = new CLS_Content.Value();
                    v.value = dict.Count;
                    v.type = typeof(int);
                    return v;
                }
                return base.MemberValueGet(content, object_this, valuename);
            }
        }
    }
}