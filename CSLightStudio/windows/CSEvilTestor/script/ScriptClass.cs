using CSEvilTestor;
using System;
using System.Collections.Generic;
using System.Text;

class ScriptClass
{
    public int i = 56;
    public int j;
    public int k;
    public float jj = -1.20f;
    public ScriptClass()
    {
        i = 3;
        j = 31;
    }
    public int GetI()
    {
        TestDele.instance.onUpdate += Test;
        deleA += Test;
        Test();
        return i + j + k;
    }
    
    Action deleA;
    public void Test()
    {
        j++;
        Program.Trace("j=" + j);
    }

}

