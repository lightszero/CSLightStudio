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
    static string fuck = null;
    static string fuck2 = null;

    public ScriptClass()
    {
        i = 3;
        j = 31;

    }
    public int GetI(int iii)
    {
        fuck = "abc";
        scriptFun1 f1 = new scriptFun1();
        f1.Update(0.3f);
        //TestDele.instance.onUpdate += Test;
        this.deleA += Test;
        this.Test();
        int j = 0;
        string ist = null;
        //CSEvilTestor.Program.Trace(ist);
        CSEvilTestor.Program.Trace("j=" + this.j+":"+iii);

        return i + j + k;
    }

    Action deleA;
    public void Test()
    {
        j++;
        CSEvilTestor.Program.Trace("j=" + j);
    }

}

