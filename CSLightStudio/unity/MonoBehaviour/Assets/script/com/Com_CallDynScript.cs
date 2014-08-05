using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Com_CallDynScript : MonoBehaviour {

    public string OnStartFunc;
    public string OnUpdateFunc;
    public float ScriptUpdateFPS = 1.0f;
	// Use this for initialization
	void Start () 
    {
        //gameObject.name = "cool";
        BuildScript("start", OnStartFunc);
        BuildScript("update", OnUpdateFunc);
        CallScript("start");
	}
	
	// Update is called once per frame
    float timer = 0;
	void Update () {
        //transform.Rotate(new Vector3(0,1,0),0.1f);
        timer += Time.deltaTime;
        if(timer>(1.0f/ScriptUpdateFPS))//限制脚本更新频率，不能太快，会死人
        {
            timer = 0;
            CallScript("update");

        }
	}

    Dictionary<string, CSLE.ICLS_Expression> dictExpr = new Dictionary<string, CSLE.ICLS_Expression>();

    void BuildScript(string key,string code)
    {
        try
        {
            var token = ScriptMgr.Instance.env.ParserToken(code);
            var expr = ScriptMgr.Instance.env.Expr_CompilerToken(token);
            dictExpr[key] = expr;
        }
        catch(Exception err)
        {
            Debug.LogError("BuildScript:" + key + " err\n" + err.ToString());
        }
    }
    void CallScript(string key)
    {
        if (dictExpr.ContainsKey(key) == false)
            return;
        var expr = dictExpr[key];
        if (expr == null) return;
        var content = ScriptMgr.Instance.env.CreateContent();//创建上下文，并设置变量给脚本访问
        content.DefineAndSet("gameObject", typeof(GameObject), this.gameObject);
        content.DefineAndSet("transform", typeof(Transform), this.transform);
        try
        {
            expr.ComputeValue(content);
        }
        catch(Exception err)
        {
            string dumpValue = content.DumpValue();//出错可以dump脚本上现存的变量
            string dumpStack = content.DumpStack(null);//dump脚本堆栈，如果保存着Token就可以dump出具体的代码
            string dumpException = err.ToString();
            Debug.LogError("callscript:"+key +"error\n"+dumpValue + "\n" + dumpStack + "\n" + dumpException);
        }

    }
}
