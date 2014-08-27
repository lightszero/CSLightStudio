using System;
using System.Collections.Generic;
using System.Text;

namespace CLScriptExt
{
    class Student
    {
        public string name;
        public int age;
        public class S1
        {
            public int v;
        }
        public List<int> vs = new List<int>();
        public int[] vs2 = new int[] { 1, 2, 3, 4 };

        public void ToString2()
        {

        }
        public void ToString2<T>(T obj)
        {
            Console.WriteLine(obj.ToString());
        }



    }

}
