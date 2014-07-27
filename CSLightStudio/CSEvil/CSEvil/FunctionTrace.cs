using System;
using System.Collections.Generic;
using System.Text;

namespace CSLight
{
    public class FunctionTrace : ICLS_Function
    {
        public string keyword
        {
            get { return "trace"; }
        }

        public CLS_Content.Value Call(CLS_Environment environment, IList<CLS_Content.Value> param)
        {
            string output = "trace:";
            bool bfirst = true;
            foreach (var p in param)
            {
                if (bfirst) bfirst = false;
                else output += ",";
                if (p.value == null) output += "null";
                else output += p.value.ToString();
            }
            environment.logger.Log(output);
            return CLS_Content.Value.Void;
        }
    }

}
