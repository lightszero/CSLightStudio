using UnityEngine;
using System.Collections;
using System;

public class ScriptTestEval : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Script.Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    string textcode = "return \"helloworld\"+(1+3*2);";

    string textreturn = "result.";

    void OnGUI()
    {
        textcode = GUI.TextArea(new Rect(0, 0, Screen.width, Screen.height / 2 - 25), textcode);

        if (GUI.Button(new Rect(0, Screen.height / 2 - 25, 200, 50), "Call"))
        {
            Script.content = Script.env.CreateContent();
            try
            {
                textreturn = Script.Execute(textcode).ToString();
            }
            catch (Exception err)
            {

                textreturn = "error\n" + Script.content.DumpStack(null); ;
            }
        }
        GUI.Label(new Rect(250, Screen.height / 2 - 25, 200, 50), "C#LightEvil EngineCore:" + Script.env.version);
        GUI.TextArea(new Rect(0, Screen.height / 2 + 25, Screen.width, Screen.height / 2 - 25), textreturn);
    }
}
