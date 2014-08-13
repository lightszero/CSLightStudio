using System;
using System.Collections.Generic;
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
        public delegate void myup();
        public myup onUpdateD;

        public event myup onUpdate;
        public event Action<int> onUpdate2;
        public event Action<int,string> onUpdate3;

        public void Run()
        {
            if (onUpdateD != null)
                onUpdateD();
            if (onUpdate != null)
                onUpdate();
            if(onUpdate2!=null)
            {
                onUpdate2(12346);
            }
            if (onUpdate3 != null)
            {
                onUpdate3(123,"hello world.");
            }
        }
        public void AddDele(Action<int> dele)
        {
            onUpdate2 += dele;
        }

        public void ClearDele()
        {
            onUpdate = null;
            onUpdate2 = null;
            onUpdate3 = null;

        }
    }

