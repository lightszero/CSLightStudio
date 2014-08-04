using CSEvilTestor;
//using CSEvilTestor;
using System;
using System.Collections.Generic;
using System.Text;

class Test01
{
    public static void Run()
    {
        Debug.Log("Hello world.");

        TestClass c1 = new TestClass();
        Test(c1);
    }
    static string hi = "sdfasdf";
    static void Test(TestClass c1)
    {
        Debug.Log("hi=" + hi);
        c1.name = hi;
    }
}

