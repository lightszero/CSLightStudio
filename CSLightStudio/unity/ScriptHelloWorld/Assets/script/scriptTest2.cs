using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 这里演示一个脚本项目的使用
/// </summary>
public class scriptTest2 : MonoBehaviour {

	// Use this for initialization

    void Start()
    {
        ResetScriptEnv();
    }
    void ResetScriptEnv()
    {
        Script.Reset();
        //将函数Today()注册给脚本使用
        //Script.env.RegFunction(new CSLE.RegHelper_Function((deleToday)Today));
        //让脚本能用UnityEngine.Debug
        Script.env.RegType(new CSLE.RegHelper_Type(typeof(UnityEngine.Debug)));
        Script.env.RegType(new CSLE.RegHelper_Type(typeof(System.DateTime)));
        Script.env.RegType(new CSLE.RegHelper_Type(typeof(System.DayOfWeek)));

        Script.env.RegType(new CSLE.RegHelper_Type(typeof(UnityEngine.Vector3)));
        Script.env.RegType(new CSLE.RegHelper_Type(typeof(UnityEngine.GameObject)));
		Script.env.RegType(new CSLE.RegHelper_Type(typeof(UnityEngine.Object)));
        Script.env.RegType(new CSLE.RegHelper_Type(typeof(UnityEngine.PrimitiveType)));
        Script.env.RegType(new CSLE.RegHelper_Type(typeof(UnityEngine.Transform)));
		Script.env.RegType(new CSLE.RegHelper_Type(typeof(UnityEngine.Time)));
    }
	// Update is called once per frame
	void Update () {
        if (updateCode!=null)
            Script.Eval(updateCode);
	}
    string updateCode = null;
	void Clear()
	{
		if(updateCode=="ScriptMain1.Update();")
			Script.Eval("ScriptMain1.Exit();");
		if(updateCode=="ScriptMain2.Update();")
			Script.Eval("ScriptMain2.Exit();");
	}
    void OnGUI()
    {


        if (GUI.Button(new Rect(0, 0, 300, 50), "RunScriptProject1"))
        {
			Clear();
            ResetScriptEnv();
            Script.BuildProject(Application.streamingAssetsPath + "/project1");
            DateTime t1 = DateTime.Now;
            Script.Eval("ScriptMain1.Start();");
            DateTime t2 = DateTime.Now;
            Debug.Log("t1=" + (t2 - t1).TotalSeconds);
            updateCode = "ScriptMain1.Update();";

        }

        if (GUI.Button(new Rect(300, 0, 300, 50), "RunScriptProject2"))
        {
			Clear();
            ResetScriptEnv();
            Script.BuildProject(Application.streamingAssetsPath + "/project2");
            Script.Eval("ScriptMain2.Start();");
            updateCode = "ScriptMain2.Update();";
        }


    }
}
