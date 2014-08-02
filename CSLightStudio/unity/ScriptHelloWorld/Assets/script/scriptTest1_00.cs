using UnityEngine;
using System.Collections;

//这里演示最基本的脚本用法，用作公式计算
public class scriptTest1_00 : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    string result = "";
    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 200, 50), "Eval 1+2"))
        {
            int i = (int)Script.Eval("1+2");
            result = "result=" + i;
        }

        if (GUI.Button(new Rect(200, 0, 200, 50), "Eval HP1+HP2*0.5"))
        {
            Script.ClearValue();
            Script.SetValue("HP1", 200);
            Script.SetValue("HP2", 300);
            double i = (double)Script.Eval("HP1+HP2*0.5");
            result = "result=" + i;
        }
        GUI.Label(new Rect(0, 50, 200, 50), result);
    }
}
