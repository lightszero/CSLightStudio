using CSEvilTestor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    public class TestClass
    {

        public string name
        {
            get
            {
                return "hi";
            }
            set
            {
                Debug.Log(value);
            }
        }
        public string ttc = "123";

        private static TestClass g_this = null;
        public static TestClass instance
        {
            get
            {
                if(g_this==null)
                {
                    g_this = new TestClass();
                }
                return g_this;
            }
        }
        int i = 0;
        public void Log()
        {
            i++;
            Debug.Log("i=" + i);
        }
    }
