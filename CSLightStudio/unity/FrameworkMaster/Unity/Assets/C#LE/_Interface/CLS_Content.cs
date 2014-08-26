using System;
using System.Collections.Generic;
using System.Text;

namespace CSLE
{
    public class CLS_Content
    {
        public CLS_Content Clone()
        {
            CLS_Content con = new CLS_Content(environment,useDebug);
            foreach(var c in this.values)
            {
                con.values.Add(c.Key, c.Value);
            }
            con.CallThis = this.CallThis;
            con.CallType = this.CallType;

            return con;
        }
        public ICLS_Environment environment
        {
            get;
            private set;
        }

        public CLS_Content(ICLS_Environment environment,bool useDebug=true)
        {
            this.environment = environment;
            this.useDebug = useDebug;
            if(useDebug)
            {
                stackExpr = new Stack<ICLS_Expression>();
                stackContent = new Stack<CLS_Content>();
            }
        }
        public string function
        {
            get;
            set;
        }
        public string CallName
        {
            get
            {
                string strout = "";
                if (this.CallType != null)
                {
                    if (string.IsNullOrEmpty(this.CallType.filename) == false)
                        strout += "(" + this.CallType.filename + ")";
                    strout += this.CallType.Name + ":";
                }
                strout += this.function;
                return strout;
            }

        }
        public bool useDebug
        {
            get;
            private set;
        }
        public Stack<ICLS_Expression> stackExpr
        {
            get;
            private set;
        }
        public Stack<CLS_Content> stackContent
        {
            get;
            private set;
        }
        public void InStack(CLS_Content expr)
        {
            if (!useDebug) return;
            if (stackContent.Count > 0 && stackContent.Peek() == expr)
            {
                throw new Exception("InStackContent error");
            }
            stackContent.Push(expr);
        }
        public void OutStack(CLS_Content expr)
        {
            if (!useDebug) return;
            if (stackContent.Peek() != expr)
            {
                throw new Exception("OutStackContent error:" + expr.ToString() + " err:" + stackContent.Peek().ToString());
            }
            stackContent.Pop();
        }
        public void InStack(ICLS_Expression expr)
        {
            if (!useDebug) return;
            if (stackExpr.Count > 0 && stackExpr.Peek() == expr)
            {
                throw new Exception("InStack error");
            }
            stackExpr.Push(expr);
        }
        public void OutStack(ICLS_Expression expr)
        {
            if (!useDebug) return;
            if (stackExpr.Peek() != expr)
            {
                throw new Exception("OutStack error:" + expr.ToString() + " err:" + stackExpr.Peek().ToString());
            }
            stackExpr.Pop();
        }
        public void Record(out List<string> depth)
        {
            depth = tvalues.Peek();
        }
        public void Restore(List<string> depth, ICLS_Expression expr)
        {
            while(tvalues.Peek()!=depth)
            {
                tvalues.Pop();
            }
            while(stackExpr.Peek()!=expr)
            {
                stackExpr.Pop();
            }
        }
		public string DumpValue()
		{
			string svalues = "";
            foreach (var subc in this.stackContent)
            {
                svalues += subc.DumpValue();
            }
            svalues += "DumpValue:" + this.CallName + "\n";
            foreach(var v in this.values)
            {
                svalues += "V:" + v.Key + "=" + v.Value.ToString()+"\n";
            }
			return svalues;
		}
		public string DumpStack(IList<Token> tokenlist)
        {
			string svalues = "";
            if (useDebug)
            {
                if(this.CallType!=null&&this.CallType.tokenlist!=null)
                {
                    tokenlist = this.CallType.tokenlist;
                }
                foreach(var subc in this.stackContent)
                {
                    svalues += subc.DumpStack(tokenlist);
                }
                svalues += "DumpStack:" + this.CallName + "\n";
                foreach(var s in stackExpr)
                {
                    if ((s.tokenBegin == 0 && s.tokenEnd == 0)||tokenlist==null)
                    {
                        svalues += "<C#LE>:line(" + s.lineBegin + "-" + s.lineEnd + ")\n";
                    }
                    else
                    {
                        svalues += "<C#LE>:line(" + s.lineBegin + "-" + s.lineEnd + ")";
                        
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

		public string Dump(IList<Token> tokenlist=null)
		{
			string str = DumpValue();
			str += DumpStack(tokenlist);
			return str;
		}
        public class Value
        {
            public CLType type;
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
        public void Define(string name,CLType type)
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
            if (!values.ContainsKey(name))
            {
                if (CallType != null)
                {
                    if (CallType.members.ContainsKey(name))
                    {
                        if (CallType.members[name].bStatic)
                        {
                            CallType.staticMemberInstance[name].value=value;
                        }
                        else
                        {
                            CallThis.member[name].value=value;
                        }
                        return;
                    }

                }
                string err = CallType.Name + "\n";
                foreach(var m in CallType.members)
                {
                    err += m.Key + ",";
                }
                throw new Exception("值没有定义过" + name + "," + err);

            }
            if ((Type)values[name].type == typeof(CLS_Type_Var.var) && value != null)
                values[name].type = value.GetType();
            values[name].value = value;
        }

        public void DefineAndSet(string name,CLType type,object value)
        {
            if (values.ContainsKey(name)) 
                throw new Exception(type.ToString()+":"+name+"已经定义过");
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
            Value v = GetQuiet(name);
            if(v==null)
                throw new Exception("值"+name+"没有定义过");
            return v;
        }
        public Value GetQuiet(string name)
        {
            if (name == "this")
            {
                Value v = new Value();
                v.type = CallType;
                v.value = CallThis;
                return v;
            }

            if (values.ContainsKey(name))//优先上下文变量
                return values[name];

            if (CallType != null)
            {
                if (CallType.members.ContainsKey(name))
                {
                    if (CallType.members[name].bStatic)
                    {
                        return CallType.staticMemberInstance[name];
                    }
                    else
                    {
                        return CallThis.member[name];
                    }
                }
                if (CallType.functions.ContainsKey(name))
                {
                    Value v = new Value();
                    //如果直接得到代理实例，
                    DeleFunction dele = new DeleFunction(CallType,this.CallThis,name);


                    //DeleScript dele =new DeleScript();
                    //dele.function = name;
                    //dele.calltype = CallType;
                    //dele.callthis = CallThis;
                    v.value = dele;
                    v.type = typeof(DeleFunction);
                    return v;

                }
            }
            return null;
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

        public SType CallType;
        public SInstance CallThis;
           
    }
}
