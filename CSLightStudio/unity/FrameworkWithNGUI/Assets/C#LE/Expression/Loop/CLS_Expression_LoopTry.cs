using System;
using System.Collections.Generic;
using System.Text;
namespace CSLE
{

    public class CLS_Expression_LoopTry : ICLS_Expression
    {
        public CLS_Expression_LoopTry(int tbegin, int tend, int lbegin, int lend)
        {
            listParam = new List<ICLS_Expression>();
            tokenBegin = tbegin;
            tokenEnd = tend;
            lineBegin = lbegin;
            lineEnd = lend;
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
        public CLS_Content.Value ComputeValue(CLS_Content content)
        {
            content.InStack(this);
            List<string> depth__;
            content.Record(out depth__);
            try
            {
                ICLS_Expression expr = listParam[0];
                if (expr is CLS_Expression_Block)
                {
                    expr.ComputeValue(content);
                }
                else
                {
                    content.DepthAdd();
                    expr.ComputeValue(content);
                    content.DepthRemove();
                }

            }
            catch (Exception err)
            {
                bool bParse = false;
                int i = 1;
                while (i < listParam.Count)
                {
                    CLS_Expression_Define def = listParam[i] as CLS_Expression_Define;
                    if (err.GetType()==(Type)def.value_type || err.GetType().IsSubclassOf((Type)def.value_type))
                    {
                        content.DepthAdd();
                        content.DefineAndSet(def.value_name, def.value_type, err);

                        listParam[i + 1].ComputeValue(content);
                        content.DepthRemove();
                        bParse = true;
                        break;
                    }
                    i += 2;
                }
                if (!bParse)
                {
                    throw err;
                }
            }
            content.Restore(depth__, this);
            //while((bool)expr_continue.value);

            //for 逻辑
            //做数学计算
            //从上下文取值
            //_value = null;
            content.OutStack(this);
            return null;
        }


        public override string ToString()
        {
            return "Try|";
        }
    }
}