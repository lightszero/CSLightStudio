using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class mode2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ScriptMgr.Instance.LoadProject();
        ScriptMgr.Instance.Execute("ScriptMain.Run()");
	}
    float timer = 0;
    public float ScriptUpdateFPS = 5.0f;

	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > (1.0f / ScriptUpdateFPS))//限制脚本更新频率，不能太快，会死人
        {
            timer = 0;
            App.CallUpdate();
        }

	}

    void OnGUI()
    {
        GUI.TextArea(new Rect(0, 0, 600, 100), "这个模式演示脚本驱动模型\n"
    + "脚本如何和程序协同工作\n"
    + "把初始化权利交给脚本，只调用ScriptMain.Run\n"
    + "然后程序就给脚本提供功能等脚本调用");
        foreach(var b in App.btns)
        {
            if(GUI.Button(b.pos,b.text))
            {
                App.CallClick(b.tag);
            }
        }
    }
}
public class App
{
    public class ButtonRect
    {
        public string text;
        public Rect pos;
        public int tag;
    }
    public static List<ButtonRect> btns = new List<ButtonRect>();

    public static void AddButton(Rect rect, string text, int tag)
    {
        ButtonRect brect = new ButtonRect();
        brect.pos = rect;
        brect.text = text;
        brect.tag = tag;
        btns.Add(brect);
    }
    public static void CallClick(int i)
    {
        if (onclick != null)
            onclick(i);
    }
    public static void CallUpdate()
    {
        if(onupdate!=null)
        {
            onupdate();
        }
    }
    public static event Action<int> onclick;
    public static event Action onupdate;
}