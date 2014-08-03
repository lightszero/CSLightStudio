using CSEvilTestor;
//using CSEvilTestor;
using System;
using System.Collections.Generic;
using System.Text;

class Test02
{
    public static void Run()
    {
        TestDele.instance.ClearDele();

        //直接注册回调的用法,+=,-=
        TestDele.instance.onUpdate += Test;

        //用Delegate类型指向函数的语法
        Action<int> deleTest = Test2;
        TestDele.instance.onUpdate2 += deleTest;
        TestDele.instance.onUpdate3 += Test3;

        //函数作为参数的用法
        TestDele.instance.AddDele(Test2);
        TestDele.instance.AddDele(deleTest);


        TestDele.instance.Run();
    }
    static int i = 0;

    static void Test()
    {
        Debug.Log("i=" + i);
        i++;
    }
    static void Test2(int v)
    {
        Debug.Log("Test2 i=" + v);
        i--;
    }

    static void Test3(int v, string str)
    {
        Debug.Log("test3:" + v + ":" + str);
    }
}

