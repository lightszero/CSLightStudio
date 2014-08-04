using CSEvilTestor;
//using CSEvilTestor;
using System;
using System.Collections.Generic;
using System.Text;

class Test04
{
    public static void Run()
    {
        IT4 impl1 = new IT4_Impl();
        Debug.Log(impl1.name);
        IT4 impl2 = new IT4_Impl2();
        Debug.Log(impl2.name);

    }

}
interface IT4
{
    void Call1();
    void Call2(int i, string n);

    string name
    {
        get;
        set;
    }
    string readonlyname
    {
        get;
    }
}
class IT4_Impl : IT4
{
    public IT4_Impl()
    {
        this.name = "IT4_Impl";
    }

    public void Call1()
    {
        Debug.Log("Call1");
    }

    public void Call2(int i, string n)
    {
        Debug.Log("Call2(" + i + "," + n + ")");
    }

    public string name
    {
        get;
        set;
    }

    public string readonlyname
    {
        get;
        private set;
    }
}

class IT4_Impl2 : IT4
{
    public IT4_Impl2()
    {
        this.name = "IT4_Impl2";
    }

    public void Call1()
    {
        Debug.Log("Call1");
    }

    public void Call2(int i, string n)
    {
        Debug.Log("Call2(" + i + "," + n + ")");
    }

    public string name
    {
        get;
        set;
    }

    public string readonlyname
    {
        get;
        private set;
    }
}