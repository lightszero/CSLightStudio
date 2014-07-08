using CSLightFrameworkDemo;
using System;
using System.Collections.Generic;
using System.Text;

//这个文件是代码脚本两用的
//每一个函数会变成一个脚本，脚本之间没有关联性
static class testcode01
{
    public static void _new(MyType parent)
    {
        parent.script.CallScriptFuncWithoutParam("_initnoparam");//呼叫其他的脚本要用这个方法
    }
    public static void _initnoparam(MyType parent)
    {
        parent.AddTextToList("_inited");
        parent.SetColorBlue();
    }
    public static void _update(MyType parent, string param)
    {
        parent.AddTextToList("_ypdate:" + param);
    }
    public static void _click01(MyType parent)
    {
        parent.AddTextToList("_click01");
    }
    public static void _click02(MyType parent, string param)
    {
        parent.AddTextToList("_click02:" + param);
    }
    public static void _click03(MyType parent, List<string> param)
    {
        string strout = "";
        foreach (var p in param)
        {
            strout += p;
            //故意写错的一行，测试异常报告
            //strout += param[10];
        }
        parent.AddTextToList("_click03:" + strout);
    }
}
