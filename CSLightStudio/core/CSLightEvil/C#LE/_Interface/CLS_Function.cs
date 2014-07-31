using System;
using System.Collections.Generic;
using System.Text;

namespace CSLE
{
    public interface ICLS_Function
    {
        string keyword
        {
            get;
        }

        CLS_Content.Value Call(CLS_Content content, IList<CLS_Content.Value> param);
    }

    public interface ICLS_Function_Member
    {
        string keyword
        {
            get;
        }
        CLS_Content.Value Call(CLS_Content content, object objthis, IList<CLS_Content.Value> param);
    }
}
