using System;
using System.Collections.Generic;
using System.Text;

namespace CSEvilTestor
{
    class TestReg
    {
        public static void Reg(CSLE.ICLS_Environment env)
        {
            env.RegType(new CSLE.RegHelper_Type(typeof(Debug)));

            env.RegType(new CSLE.RegHelper_Type(typeof(TestDele)));
            env.RegType(new CSLE.RegHelper_Type(typeof(Program)));

            env.RegType(new CSLE.RegHelper_DeleAction<int,string>(typeof(Action<int,string>),"Action<int,string>"));
            env.RegType(new CSLE.RegHelper_DeleAction<int>(typeof(Action<int>), "Action<int>"));
            env.RegType(new CSLE.RegHelper_DeleAction(typeof(Action),"Action"));
            env.RegType(new CSLE.RegHelper_DeleAction(typeof(TestDele.myup), "TestDele.myup"));

            env.RegType(new CSLE.RegHelper_Type(typeof(object), "object"));
            //env.RegType(new CSLE.RegHelper_Type(typeof(List<object>), "List<object>"));
            env.RegType(new CSLE.RegHelper_Type(typeof(Dictionary<int,int>), "Dictionary<int,int>"));
            env.RegType(new CSLE.RegHelper_Type(typeof(Dictionary<string, object>), "Dictionary<string,object>"));

            Type t = Type.GetType("System.Collections.Generic.List`1");
            env.RegType(new CSLE.RegHelper_Type(t, "List"));
            //Type t2 = Type.GetType("System.Collections.Generic.Dictionary`2");
            //env.RegType(new CSLE.RegHelper_Type(t2, "Dictionary"));

            env.RegType(new CSLE.RegHelper_Type(typeof(HashSet<object>), "HashSet<object>"));

            env.RegType(new CSLE.RegHelper_Type(typeof(List<string>), "List<string>"));
            env.RegType(new CSLE.RegHelper_Type(typeof(int[]), "int[]"));

            env.RegType(new CSLE.RegHelper_Type(typeof(List<int>), "List < int>"));
            env.RegType(new CSLE.RegHelper_Type(typeof(List<List<int>>), "List<List<int>>"));
            env.RegType(new CSLE.RegHelper_Type(typeof(List<List<List<double>>>), "List<List<List<double>>>"));
            env.RegType(new CSLE.RegHelper_Type(typeof(List<List<List<int>>>), "List<List<List<int>>>"));

            env.RegType(new CSLE.RegHelper_Type(typeof(config)));
            env.RegType(new CSLE.RegHelper_Type(typeof(Math)));

            env.RegType(new CSLE.RegHelper_Type(typeof(Exception)));
            env.RegType(new CSLE.RegHelper_Type(typeof(NotSupportedException)));
            env.RegType(new CSLE.RegHelper_Type(typeof(NotImplementedException)));
            env.RegType(new CSLE.RegHelper_Type(typeof(TestClass)));

        }
        
    }
    class config
    {
        public static int Cell(double i)
        {
            return (int)i;
        }
    }
}
