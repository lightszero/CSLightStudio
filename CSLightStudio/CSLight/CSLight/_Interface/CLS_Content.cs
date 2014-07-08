using System;
using System.Collections.Generic;
using System.Text;

namespace CSLight
{
    public class CLS_Content
    {
        public CLS_Environment environment
        {
            get;
            private set;
        }
        public CLS_Content(CLS_Environment environment,bool useDebug=true)
        {
            this.environment = environment;
            this.useDebug = useDebug;
            if(useDebug)
            {
                stacklist = new Stack<ICLS_Expression>();
            }
        }
        public bool useDebug
        {
            get;
            private set;
        }
        public Stack<ICLS_Expression> stacklist
        {
            get;
            private set;
        }
        public void InStack(ICLS_Expression expr)
        {
            if (!useDebug) return;
            if (stacklist.Count>0&&stacklist.Peek() == expr)
            {
                throw new Exception("InStack error");
            }
            stacklist.Push(expr);
        }
        public void OutStack(ICLS_Expression expr)
        {
            if (!useDebug) return;
            if (stacklist.Peek() != expr)
            {
                throw new Exception("OutStack error:" + expr.ToString() + " err:"+stacklist.Peek().ToString());
            }
            stacklist.Pop();
        }
        public string Dump(IList<Token> tokenlist)
        {
            string svalues = "";
            foreach(var v in this.values)
            {
                svalues += "V:" + v.Key + "=" + v.Value.ToString()+"\n";
            }
            if (useDebug)
            {
                foreach(var s in stacklist)
                {
                    if ((s.tokenBegin == 0 && s.tokenEnd == 0)||tokenlist==null)
                    {
                        svalues += "在脚本:" + s.ToString() + "\n";
                    }
                    else
                    {
                        svalues += "在脚本 :";
                        if (s.tokenEnd - s.tokenBegin >= 20)
                        {
                            for(int i=s.tokenBegin;i<s.tokenBegin+8;i++)
                            {
                                svalues += tokenlist[i].text + " ";
                            }
                            svalues += "...";
                            for (int i = s.tokenEnd-7; i <= s.tokenEnd; i++)
                            {
                                svalues += tokenlist[i].text + " ";
                            }
                        }
                        else
                        {
                            for (int i = s.tokenBegin; i <= s.tokenEnd; i++)
                            {
                                svalues += tokenlist[i].text + " ";
                            }
                        }
                        svalues += "\n";

                    }
                   
                }
            }
            return svalues;

        }
        public class Value
        {
            public Type type;
            public object value;
            public int breakBlock = 0;//是否是块结束
            public static Value FromICLS_Value(ICLS_Value value)
            {
                Value v = new Value();
                v.type = value.type;
                v.value = value.value;
                return v;
            }
            public static Value One
            {
                get
                {
                    if(g_one==null)
                    {
                        g_one = new Value();
                        g_one.type = typeof(int);
                        g_one.value = (int)1;
                    }
                    return g_one;
                }
            }
            public static Value OneMinus
            {
                get
                {
                    if (g_oneM == null)
                    {
                        g_oneM = new Value();
                        g_oneM.type = typeof(int);
                        g_oneM.value = (int)-1;
                    }
                    return g_oneM;
                }
            }
            public static Value Void
            {
                get
                {
                    if (g_void == null)
                    {
                        g_void = new Value();
                        g_void.type = typeof(void);
                        g_void.value = null;
                    }
                    return g_void;
                }
            }
            static Value g_one = null;
            static Value g_oneM = null;
            static Value g_void = null;

            public override string ToString()
            {
                if(type==null)
                {
                    return "<null>" + value;
                }
                return "<" + type.ToString() + ">" + value;
            }
        }

        public Dictionary<string, Value> values = new Dictionary<string, Value>();
        public void Define(string name,Type type)
        {
            if (values.ContainsKey(name)) throw new Exception("已经定义过");
            Value v = new Value();
            v.type = type;
            values[name] = v;
            if (tvalues.Count > 0)
            {
                tvalues.Peek().Add(name);//暂存临时变量
            }
        }
        public void Set(string name,object value)
        {
            if (!values.ContainsKey(name)) throw new Exception("值没有定义过");
            if (values[name].type == typeof(CLS_Type_Var.var)&&value!=null)
                values[name].type = value.GetType();
            values[name].value = value;
        }

        public void DefineAndSet(string name,Type type,object value)
        {
            if (values.ContainsKey(name)) throw new Exception(type.ToString()+":"+name+"已经定义过");
            Value v = new Value();
            v.type = type;
            v.value = value;
            values[name] = v;
            if(tvalues.Count>0)
            {
                tvalues.Peek().Add(name);//暂存临时变量
            }
        }
        public Value Get(string name)
        {
            if (!values.ContainsKey(name)) throw new Exception("值"+name+"没有定义过");
            return values[name];
        }
        public Stack<List<string>> tvalues = new Stack<List<string>>();
        public void DepthAdd()//控制变量作用域，深一层
        {
            tvalues.Push(new List<string>());
        }
        public void DepthRemove()//控制变量作用域，退出一层，上一层的变量都清除
        {
            List<string> list = tvalues.Pop();
            foreach(var v in list)
            {
                values.Remove(v);
            }
        }
    }
}
