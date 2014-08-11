using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class mode1 : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        ScriptMgr.Instance.LoadProject();
        smgr.ChangeState("state1");
	}
	
	// Update is called once per frame
    float timer = 0;
    public float ScriptUpdateFPS = 5.0f;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > (1.0f / ScriptUpdateFPS))//限制脚本更新频率，不能太快，会死人
        {
            timer = 0;
            if (smgr.curState != null)
                smgr.curState.OnUpdate();
        }
    }
    void OnGUI()
    {
        GUI.TextArea(new Rect(0, 0, 600, 100), "这个模式演示传统的状态机模型\n"
            + "脚本如何和程序协同工作\n");
    }
    StateMgr smgr =new StateMgr();



}
