
using CSEvilTestor;
using System.Collections;

public class Script_TestConstructor
{

    private int[] a = new int[]{1,3,4};
    public static int[] b = new int[234];

    public Script_TestConstructor()
    {
        bool a = Test();
        Debug.Log("direct." + a);
        Test2();
    }

    public bool Test()
    {
        return false;
    }

    public static void Test2()
    {
        for(int i=0;i<10;i++)
        {
            TestClass.instance.Log();
        }
    }
}
