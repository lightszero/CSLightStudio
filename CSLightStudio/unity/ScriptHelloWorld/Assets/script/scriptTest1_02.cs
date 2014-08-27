using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 这里演示一个脚本类的使用
/// </summary>
public class scriptTest1_02 : MonoBehaviour {

	// Use this for initialization

    void Start()
    {
#if UNITY_STANDALONE
        string script1 =System.IO.File.ReadAllText(Application.streamingAssetsPath+"/scriptclass1.cs");
        Script.Init();
        //将函数Today()注册给脚本使用
        //Script.env.RegFunction(new CSLE.RegHelper_Function((deleToday)Today));
        //让脚本能用UnityEngine.Debug
        Script.env.RegType(new CSLE.RegHelper_Type(typeof(UnityEngine.Debug)));
        Script.env.RegType(new CSLE.RegHelper_Type(typeof(System.DateTime)));
        Script.env.RegType(new CSLE.RegHelper_Type(typeof(System.DayOfWeek)));

        Script.BuildFile("scriptclass1.cs", script1);
#endif
	}

	// Update is called once per frame
	void Update () {
	
	}
    string result = "";
    void OnGUI()
    {


        if (GUI.Button(new Rect(0, 0, 200, 50), "Eval use String"))
        {
            Script.ClearValue();
            string callExpr ="ScriptClass1 sc =new ScriptClass1();\n"+
                            "sc.defHP1=200;\n"+
                            "sc.defHP2=300;\n"+
                            "return sc.GetHP();";
            object i = Script.Execute(callExpr);
            result = "result=" + i;
        }

        if (GUI.Button(new Rect(200, 0, 200, 50), "Eval use Code"))
        {
            Script.ClearValue();
            CSLE.CLS_Content content = new CSLE.CLS_Content(Script.env);
            //得到脚本类型
            var typeOfScript = Script.env.GetTypeByKeyword("ScriptClass1");
            //调用脚本类构造创造一个实例
            var thisOfScript = typeOfScript.function.New(content, null).value;
            //调用脚本类成员变量赋值
            //Debug.LogWarning(thisOfScript+","+ typeOfScript+","+ typeOfScript.function);
            typeOfScript.function.MemberValueSet(content, thisOfScript, "defHP1", 200);
            typeOfScript.function.MemberValueSet(content, thisOfScript, "defHP2", 300);
            //调用脚本类成员函数
            var returnvalue = typeOfScript.function.MemberCall(content, thisOfScript, "GetHP", null);
            object i = returnvalue.value;
            result = "result=" + i;
        }

        GUI.Label(new Rect(0, 50, 200, 50), result);
    }
}
