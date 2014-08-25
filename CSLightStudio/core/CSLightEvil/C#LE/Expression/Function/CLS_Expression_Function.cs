using System;
using System.Collections.Generic;
using System.Text;
namespace CSLE
{

    public class CLS_Expression_Function : ICLS_Expression
    {
        public CLS_Expression_Function(int tbegin, int tend, int lbegin, int lend)
        {
            listParam = new List<ICLS_Expression>();
            this.tokenBegin = tbegin;
            this.tokenEnd = tend;
            lineBegin = lbegin;
            lineEnd = lend;
        }
        //Block的参数 一个就是一行，顺序执行，没有
        public List<ICLS_Expression> listParam
        {
            get;
            private set;
        }
        public int tokenBegin
        {
            get;
            private set;
        }
        public int tokenEnd
        {
            get;
            set;
        }
        public int lineBegin
        {
            get;
            private set;
        }
        public int lineEnd
        {
            get;
            set;
        }
        public CLS_Content.Value ComputeValue(CLS_Content content)
        {
            content.InStack(this);
            List<CLS_Content.Value> list = new List<CLS_Content.Value>();
            foreach (ICLS_Expression p in listParam)
            {
                if (p != null)
                {
                    list.Add(p.ComputeValue(content));
                }
            }
            CLS_Content.Value v = null;

            if (content.CallType != null && content.CallType.functions.ContainsKey(funcname))
            {
                if (content.CallType.functions[funcname].bStatic)
                {
                    v = content.CallType.StaticCall(content, funcname, list);

                }
                else
                {
                    v = content.CallType.MemberCall(content, content.CallThis, funcname, list);

                }
            }


            else
            {
                v = content.GetQuiet(funcname);
                if (v != null && v.value is Delegate)
                {
                    //if(v.value is Delegate)
                    {
                        Delegate d = v.value as Delegate;
                        v = new CLS_Content.Value();
                        object[] obja = new object[list.Count];
                        for (int i = 0; i < list.Count; i++)
                        {
                            obja[i] = list[i].value;
                        }
                        v.value = d.DynamicInvoke(obja);
                        if (v.value == null)
                        {
                            v.type = null;
                        }
                        else
                        {
                            v.type = v.value.GetType();
                        }
                    }
                    //else
                    //{
                    //    throw new Exception(funcname + "不是函数");
                    //}
                }
                else
                {
                    v = content.environment.GetFunction(funcname).Call(content, list);
                }
            }
            //操作变量之
            //做数学计算
            //从上下文取值
            //_value = null;
            content.OutStack(this);
            return v;
        }
        public string funcname;

        public override string ToString()
        {

            return "Call|" + funcname + "(params[" + listParam.Count + ")";
        }
    }
}