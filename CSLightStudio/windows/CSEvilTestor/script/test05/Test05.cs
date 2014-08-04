using CSEvilTestor;
//using CSEvilTestor;
using System;
using System.Collections.Generic;
using System.Text;

class Test05
{
    public static void Run()
    {
        if (c5 == null)
        {
            c5 = new C5();

        }
        try
        {


            throw new NotImplementedException("E2");
        }
        catch (NotSupportedException err)
        {
            Debug.Log("not here.");
        }
        catch (NotImplementedException err)
        {
            Debug.Log("Got.");
        }
        catch (Exception err)
        {
            Debug.Log("Got 2.");
        }
    }
    static C5 c5 = null;
}

class C5
{

}
