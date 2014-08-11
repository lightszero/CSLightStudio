using System.Collections.Generic;
using UnityEngine;
//最简状态机

public class StateMgr
{
    public IState curState
    {
        get;
        private set;
    }
    private GameObject curUiObj;

    public void ChangeState(string state)
    {
        
        if(state=="state1")
        {
            GameObject sp = GameObject.Instantiate(Resources.Load("state1")) as GameObject;
            IState s = null;
            s = new ScriptInstanceState("State1");//从脚本创建
            //s = new State1();//从真实代码创建
            ChangeState(s,sp);
        }
        if (state == "state2")
        {
            GameObject sp = GameObject.Instantiate(Resources.Load("state2")) as GameObject;
            IState s = null;
            s = new ScriptInstanceState("State2");//从脚本创建
            //s = new State2();//从真实代码创建
            ChangeState(s, sp);
        }
        if (state == "state3")
        {
            GameObject sp = GameObject.Instantiate(Resources.Load("state3")) as GameObject;
            IState s = null;
            s = new ScriptInstanceState("State3");//从脚本创建
            //s = new State3();//从真实代码创建
            ChangeState(s, sp);
        }
    }
    void ChangeState(IState state,GameObject ui)
    {
        GameObject uiRoot = GameObject.Find("UI Root (2D)/Camera/Anchor");
        if (curState != null)
        {
            if(curUiObj!=null)
            {
                GameObject.Destroy(curUiObj);
            }
            curState.OnExit();
        }
        curState = state;
        curUiObj = ui;
        if (curState != null)
        {
            if(curUiObj!=null)
            {
                curUiObj.transform.parent = uiRoot.transform;
                curUiObj.transform.localScale = Vector3.one;
                curUiObj.transform.localPosition = Vector3.zero;
            }
            curState.OnInit(this,curUiObj);
        }
    }


}


//调用脚本状态
class ScriptInstanceState : IState
{
    CSLE.ICLS_Type type;
    CSLE.SInstance inst;//脚本实例
    CSLE.CLS_Content content;//操作上下文
    public ScriptInstanceState(string scriptTypeName)
    {
        type = ScriptMgr.Instance.env.GetTypeByKeywordQuiet(scriptTypeName);
        if (type == null)
        {
            Debug.LogError("Type:" + scriptTypeName + "不存在于脚本项目中");
            return;
        }
        content = ScriptMgr.Instance.env.CreateContent();
        inst = type.function.New(content, null).value as CSLE.SInstance;
        content.CallType = inst.type;
        content.CallThis = inst;
        Debug.Log("inst=" + inst);
    }


    public void OnInit(StateMgr mgr,GameObject ui)
    {
        List<CSLE.CLS_Content.Value> _params = new List<CSLE.CLS_Content.Value>();
        _params.Add(new CSLE.CLS_Content.Value());
        _params.Add(new CSLE.CLS_Content.Value());
        _params[0].type = typeof(StateMgr);
        _params[0].value = mgr;
        _params[1].type = typeof(GameObject);
        _params[1].value = ui;
        type.function.MemberCall(content, inst, "OnInit", _params);
    }

    public void OnExit()
    {
        type.function.MemberCall(content, inst, "OnExit", null);
    }

    public void OnUpdate()
    {
        type.function.MemberCall(content, inst, "OnUpdate", null);
    }

}
