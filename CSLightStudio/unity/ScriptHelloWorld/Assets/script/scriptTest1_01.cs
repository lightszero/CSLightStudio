using UnityEngine;
using System.Collections;
using System;
/// <summary>
/// 这里演示更进一步的脚本用法，从文件中加载，并且有调用和逻辑分支
/// </summary>
public class scriptTest1_01 : MonoBehaviour {

	// Use this for initialization
    string script1;

    void Start()
    {
        script1 = (Resources.Load("script01") as TextAsset).text;
        Script.Init();
        //将函数Today()注册给脚本使用
        Script.env.RegFunction(new CSLE.RegHelper_Function((deleToday)Today));
        //让脚本能用UnityEngine.Debug
        Script.env.RegType(new CSLE.RegHelper_Type(typeof(UnityEngine.Debug)));
	}
    delegate int deleToday();

    int Today()
    {
        return (int)DateTime.Now.DayOfWeek;
    }
	// Update is called once per frame
	void Update () {
	
	}
    string result = "";
    void OnGUI()
    {


        if (GUI.Button(new Rect(200, 0, 200, 50), "Eval Script1"))
        {
            Script.ClearValue();
            Script.SetValue("Monday", 1);
            Script.SetValue("Sunday", 0);
            Script.SetValue("HP1", 200);
            Script.SetValue("HP2", 300);
            object i = Script.Execute(script1);
            result = "result=" + i;
        }
        GUI.Label(new Rect(0, 50, 200, 50), result);
    }
}
