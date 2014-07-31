using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    class TestDele
    {
        public static TestDele instance
        {
            get
            {
                if (g_this == null)
                    g_this = new TestDele();
                return g_this;
            }
        }
        static TestDele g_this = null;

        public event Action onUpdate;
        public void Run()
        {
            if (onUpdate != null)
                onUpdate();
        }
    }

