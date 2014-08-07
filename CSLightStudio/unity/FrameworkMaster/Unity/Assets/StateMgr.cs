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
    public void ChangeState(IState state)
    {
        if (curState != null)
        {
            curState.OnExit();
        }
        curState = state;
        if (curState != null)
        {
            curState.OnInit();
        }
    }

    ScriptInstanceState CreateScriptState(string classtype)
    {
        ScriptInstanceState type = new ScriptInstanceState(classtype);
        type.mgr = this;
        return type;
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
        type = ScriptEnv.Instance.scriptEnv.GetTypeByKeywordQuiet(scriptTypeName);
        if (type == null)
        {
            Debug.LogError("Type:" + scriptTypeName + "不存在于脚本项目中");
            return;
        }
        content = ScriptEnv.Instance.scriptEnv.CreateContent();
        inst = type.function.New(content, null).value as CSLE.SInstance;
        content.CallType = inst.type;
        content.CallThis = inst;
        Debug.Log("inst=" + inst);
    }
    public StateMgr mgr
    {
        get
        {
            return inst.member["mgr"].value as StateMgr;
        }
        set
        {
            inst.member["mgr"].value = value;
        }
    }

    public void OnInit()
    {
        type.function.MemberCall(content, inst, "OnInit", null);
    }

    public void OnExit()
    {
        type.function.MemberCall(content, inst, "OnExit", null);
    }

    public void OnUpdate()
    {
        type.function.MemberCall(content, inst, "OnUpdate", null);
    }

    public void ClickEvent(int tag)
    {
        List<CSLE.CLS_Content.Value> param = new List<CSLE.CLS_Content.Value>();
        param.Add(new CSLE.CLS_Content.Value());
        param[0].type = typeof(int);
        param[0].value = tag;
        type.function.MemberCall(content, inst, "ClickEvent", param);
    }
}
