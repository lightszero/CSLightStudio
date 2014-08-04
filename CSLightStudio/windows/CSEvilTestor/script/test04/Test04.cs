using CSEvilTestor;
//using CSEvilTestor;
using System;
using System.Collections.Generic;
using System.Text;

class Test04
{
    public static void Run()
    {
        List<int> list1 = new List<int>();//c#Light 不支持模板，所以这里要注意一下
        //List<int> 可以 List < int > 有空格不可以
        list1.Add(1);
        list1.Add(2);
        list1.Add(3);
        List<List<int>> list2 = new List<List<int>>();

        list2.Add(list1);
        List<List<List<int>>> list3 = new List<List<List<int>>>();
        list3.Add(list2);
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