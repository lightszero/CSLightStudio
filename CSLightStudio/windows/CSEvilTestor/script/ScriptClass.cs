//using CSEvilTestor;
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
        //TestDele.instance.onUpdate += Test;
        this.deleA += Test;
        this.Test();
        int j = 0;
        CSEvilTestor.Program.Trace("j=" + j);
        CSEvilTestor.Program.Trace("j=" + this.j);

        return i + j + k;
    }
    
    Action deleA;
    public void Test()
    {
        j++;
        CSEvilTestor.Program.Trace("j=" + j);
    }

}

