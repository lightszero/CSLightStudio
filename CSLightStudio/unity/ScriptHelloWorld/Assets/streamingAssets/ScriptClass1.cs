using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class ScriptClass1
{
    public int Calc1()
    {
        return defHP1 + (int)(defHP2*0.5f);
    }
    public int Calc2()
    {
        return defHP1 + (defHP2 * 2);
    }
    public int GetHP()
    {
        if(DateTime.Now.DayOfWeek == DayOfWeek.Monday)
        {
            return Calc1();
        }
        else if(DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
        {
            return Calc2();
        }
        else
        {
            return defHP1 + defHP2;
        }
    }
    public int defHP1;
    public int defHP2;
}

