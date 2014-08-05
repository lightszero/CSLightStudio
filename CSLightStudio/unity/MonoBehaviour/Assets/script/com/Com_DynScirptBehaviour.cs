using UnityEngine;
using System.Collections;

public class Com_DynScirptBehaviour : MonoBehaviour
{
    public string ScriptTypeName;
    public float ScriptUpdateFPS = 1.0f;
	// Use this for initialization
	void Start () {
        ScriptMgr.Instance.LoadProject();
        inst = new ScriptInstanceHelper(ScriptTypeName);
        inst.gameObject = this.gameObject;
        inst.Start();
	}
	ScriptInstanceHelper inst;
	// Update is called once per frame
    float timer = 0;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > (1.0f / ScriptUpdateFPS))//限制脚本更新频率，不能太快，会死人
        {
            timer = 0;
            inst.Update();
        }
	}
  
}
class ScriptInstanceHelper : IScriptBehaviour
{
    CSLE.ICLS_Type type;
    CSLE.SInstance inst;//脚本实例
    CSLE.CLS_Content content;//操作上下文
    public ScriptInstanceHelper(string scriptTypeName)
    {
        type = ScriptMgr.Instance.env.GetTypeByKeywordQuiet(scriptTypeName);
        if(type==null)
        {
            Debug.LogError("Type:" + scriptTypeName + "不存在于脚本项目中");
            return;
        }
        content =ScriptMgr.Instance.env.CreateContent();
        inst = type.function.New(content, null).value as CSLE.SInstance;
        content.CallType = inst.type;
        content.CallThis = inst;
        Debug.Log("inst=" + inst);
    }
    public GameObject gameObject
    {
        get
        {
            return inst.member["gameObject"].value as GameObject;
        }
        set
        {
            inst.member["gameObject"].value = value;
        }
    }

    public void Start()
    { 
        type.function.MemberCall(content, inst, "Start", null);
    }

    public void Update()
    {
        type.function.MemberCall(content, inst, "Update", null);
    }
}

