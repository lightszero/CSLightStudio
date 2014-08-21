
using CSEvilTestor;
using System.Collections;

public class Script_TestConstructor
{

    private int a;

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

    }
}
