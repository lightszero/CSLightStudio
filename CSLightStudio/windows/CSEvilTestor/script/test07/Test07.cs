using CSEvilTestor;
//using CSEvilTestor;
using System;
using System.Collections.Generic;
using System.Text;

class Test07
{
    public void Run()
    {
        Debug.Log("===-----------===");
        Fun1();
    }
    //static C5 c5 = null;

    public void Fun1()
    {
        Debug.Log("Fun1 in");
        Fun2();
        Debug.Log("Fun1 out");
    }


    int _pix = int.MinValue;
    int _piy = int.MinValue;
    public void Fun2()
    {
        Debug.Log("Fun3 in"); // !!!!!!!!!!!!!!没有了
        int pix = 0;
        int piy = 0;
        if (pix == _pix && piy == _piy)
        {
            return;
        }
        
        Debug.Log("Fun3");
    }
}
