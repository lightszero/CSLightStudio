using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class TestCool : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        CSLightMgr.InitCSLight();
        CSLightMgr.InitLua();
        //Pre 保持平等对抗
        Test01(1);
        Test02(1);
        Test03(1);

        //开始对抗
        result.Add(Test02(300));
        result.Add( Test01(3000));
        result.Add(Test03(1000));
    }
    List<TestResult> result = new List<TestResult>();
    // Update is called once per frame
    void Update()
    {

    }
    void OnGUI()
    {
        for (int y = 0; y < result.Count; y++)
        {
            GUI.Label(new Rect(0, y * 50, Screen.width, 50), result[y].ToString());
        }
    }
    class TestResult
    {
        public TestResult(string name)
        {
            this.name = name;
        }
        public string name;
        public double baseTime;
        public double T1Time;
        public double T2Time;
        public override string ToString()
        {
            return name + " B:" + baseTime + "|T1:" + T1Time+"(X"+(T1Time/baseTime) + ")|T2:" + T2Time+"(X"+(T2Time/baseTime)+")";
        }
    }
    TestResult Test01(int count)
    {
        TestResult r = new TestResult("New&Destroy 1");

        DateTime t0 = DateTime.Now;

        for (int i = 0; i < count; i++)
        {
            GameObject o = new GameObject();
            o.name = "Unity";
            GameObject.DestroyImmediate(o);
        }
        DateTime t1 = DateTime.Now;

        for (int i = 0; i < count; i++)
        {
            //ulua执行这个代码都不正常
            //CSLightMgr.InitLua();
            CSLightMgr.LuaDoString(
            @"  
            local o = GameObject();
            o.name='lua';
            GameObject.Destroy(o);
            "
                );
        }
        DateTime t2 = DateTime.Now;

        for (int i = 0; i < count; i++)
        {
            CSLightMgr.CSLightDoString(
            @"
            GameObject o =new GameObject();
            o.name=""C#LE"";
            GameObject.DestroyImmediate(o);"
            );
        }
        DateTime t3 = DateTime.Now;
        r.baseTime = (t1 - t0).TotalSeconds;
        r.T1Time = (t2 - t1).TotalSeconds;
        r.T2Time = (t3 - t2).TotalSeconds;
        return r;
    }
    TestResult Test02(int count)
    {
        TestResult r = new TestResult("Log Test");

        DateTime t0 = DateTime.Now;

        for (int i = 0; i < count; i++)
        {
            Debug.Log("Unity Log");
        }
        DateTime t1 = DateTime.Now;

        for (int i = 0; i < count; i++)
        {
            CSLightMgr.LuaDoString("Debug.Log(\"luser log\")");
        }
        DateTime t2 = DateTime.Now;

        for (int i = 0; i < count; i++)
        {
            CSLightMgr.CSLightDoString("Debug.Log(\"C#Light log\");");
        }
        DateTime t3 = DateTime.Now;
        r.baseTime = (t1 - t0).TotalSeconds;
        r.T1Time = (t2 - t1).TotalSeconds;
        r.T2Time = (t3 - t2).TotalSeconds;
        return r;
    }

    TestResult Test03(int count)
    {
        TestResult r = new TestResult("New&Destroy 10");

        DateTime t0 = DateTime.Now;

        for (int i = 0; i < count; i++)
        {
            for(int j=0;j<10;j++)
            {
                GameObject go = new GameObject();
                GameObject camera = GameObject.Find("Camera");
                go.transform.parent = camera.transform;
                go.transform.localScale = Vector3.one;
                GameObject.DestroyImmediate(go);
            }
        }
        DateTime t1 = DateTime.Now;

        for (int i = 0; i < count; i++)
        {
            CSLightMgr.LuaDoString(
            @"for i=1,10 do  
                local go = GameObject()
			    local camera = GameObject.Find('Camera');
			    go.transform.parent = camera.transform;
			    go.transform.localScale = Vector3.one;
                GameObject.DestroyImmediate(go)
            end"
                );
        }
        DateTime t2 = DateTime.Now;

        for (int i = 0; i < count; i++)
        {
            CSLightMgr.CSLightDoString(
            @"
            for(int j=0;j<10;j++)
            {
                GameObject go = new GameObject();
                GameObject camera = GameObject.Find(""Camera"");
                go.transform.parent = camera.transform;
                go.transform.localScale = Vector3.one;
                GameObject.DestroyImmediate(go);
            }"
            );
        }
        DateTime t3 = DateTime.Now;
        r.baseTime = (t1 - t0).TotalSeconds;
        r.T1Time = (t2 - t1).TotalSeconds;
        r.T2Time = (t3 - t2).TotalSeconds;
        return r;
    }

}
