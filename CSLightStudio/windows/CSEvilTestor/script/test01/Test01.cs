
using CSEvilTestor;
using System;
using System.Collections;

public class Script_TestConstructor
{

    private int[] a = new int[] { 1, 3, 4 };
    public static int[] b = new int[234];

    public Script_TestConstructor()
    {
        bool a = Test();
        Debug.Log("direct." + a);
        Test2();
        Test3(22, 33, 44);
    }
    public static void Test3(int a, int b, int c)
    {
        ////// 这个是正确的.
        int v = (a * (b + 1)) * (c + 2);
        int value1 = config.Cell(v);
        Debug.Log("value1=>" + value1);

        ////// 发生运行时错误 => 找不到 CeilToInt 方法.
        int value2 = config.Cell((a * (b + 1)) * (c + 2));
        Debug.Log("value2=>" + value2);
    }
    public bool Test()
    {
        return false;
    }

    public static void Test2()
    {
        for (int i = 0; i < 10; i++)
        {
            TestClass.instance.Log();
        }
    }
}
