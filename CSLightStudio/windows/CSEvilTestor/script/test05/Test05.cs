using CSEvilTestor;
//using CSEvilTestor;
using System;
using System.Collections.Generic;
using System.Text;

class Test05
{
    public static void Run()
    {
        if(c5==null)
        {
            c5 = new C5();
           
            throw new NotSupportedException("E2");
        }

        throw new Exception("Make one Error");
    }
    static C5 c5 =null;
}

class C5
{

}
