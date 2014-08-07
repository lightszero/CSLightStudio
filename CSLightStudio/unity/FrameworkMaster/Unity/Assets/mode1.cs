using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class mode1 : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        ScriptEnv.Instance.Reset(null);
        ScriptEnv.Instance.LoadProjectBytes("script01.CSLEDll.bytes");
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
            +"脚本如何和程序协同工作\n"
            +"按State1 由程序驱动状态机切换，状态实现在脚本中\n"
            +"按Play Stop会发送给脚本执行");
        if(GUI.Button(new Rect(0, 100, 200, 50), "State1"))
        {
            var state = new ScriptInstanceState("State1");
            state.mgr = smgr;
            smgr.ChangeState(state);
        }
        if (GUI.Button(new Rect(200, 100, 200, 50), "State2"))
        {
            var state = new ScriptInstanceState("State2");
            state.mgr = smgr;
            smgr.ChangeState(state);
        }
        if (GUI.Button(new Rect(400, 100, 200, 50), "State3"))
        {
            var state = new ScriptInstanceState("State3");
            state.mgr = smgr;
            smgr.ChangeState(state);
        }
        if(GUI.Button(new Rect(0,150,200,50),"Play"))
        {
            if(smgr.curState!=null)
                smgr.curState.ClickEvent(1);
        }
        if (GUI.Button(new Rect(200, 150, 200, 50), "Stop"))
        {
            if (smgr.curState != null)
                smgr.curState.ClickEvent(2);
        }
    }
    StateMgr smgr =new StateMgr();



}
