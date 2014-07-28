using System;
using System.Collections.Generic;
using System.Text;

namespace CSLight
{
    public interface ICLS_Function
    {
        string keyword
        {
            get;
        }

        CLS_Content.Value Call(ICLS_Environment environment, IList<CLS_Content.Value> param);
    }

    public interface ICLS_Function_Member
    {
        string keyword
        {
            get;
        }
        CLS_Content.Value Call(ICLS_Environment environment, object objthis, IList<CLS_Content.Value> param);
    }
}
