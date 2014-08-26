using CSEvilTestor;
//using CSEvilTestor;
using System;
using System.Collections.Generic;
using System.Text;

class Test02
{
    public static void Run()
    
    {

        Action<int> t = (int a) =>
        {
            //Debug.Log("a=" + a);
        };

        TestDele.instance.onUpdateD = Test;
        Action<int> deleTest = Test2;

        deleTest(13333);
        Test02.deleTest2 = deleTest;
        Test02.deleTest2(333);

        int config_citygrade = 0;
        TestDele.instance.ClearDele();
        TestDele.instance.onUpdateD += () =>
        {
            Debug.Log("direct.");
        };
        TestDele.instance.onUpdateD = Test;
        //直接注册回调的用法,+=,-=
        TestDele.instance.onUpdate += Test;

        //用Delegate类型指向函数的语法
        TestDele.instance.onUpdate2 += deleTest;
        TestDele.instance.onUpdate3 += Test3;

        //函数作为参数的用法
        //TestDele.instance.AddDele(Test2);
        TestDele.instance.AddDele(deleTest);

        Test02 ttt = new Test02();
        ttt.deleTest3 = deleTest;
        ttt.deleTest3(3334);

        TestDele.instance.onUpdate2 += t;

        TestDele.instance.onUpdate2 += (b) =>
        {
            Debug.Log("b=" + b);
        };


        TestDele.instance.Run();
    }
    static int i = 0;
    static Action<int> deleTest2 = null;

    Action<int> deleTest3; 
    static void Test()
    {
        Debug.Log("Testi=" + i);
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

