using CSEvilTestor;
//using CSEvilTestor;
using System;
using System.Collections.Generic;
using System.Text;

class Test07
{
    public static void Run()
    {
		StateManager.Method1("eventName","eventName2","eventName3");
		
		StateManager.Instance.DoMethod1("eventName","eventName2","eventName3");
		
        try
        {


            throw new NotImplementedException("E2");
        }
        catch (NotSupportedException err)
        {
            Debug.Log("not here.");
        }
        catch (NotImplementedException err)
        {
            Debug.Log("Got.");
        }
        catch (Exception err)
        {
            Debug.Log("Got 2.");
        }
    }
    //static C5 c5 = null;
}

/// <summary>
/// 状态中心
/// </summary>
public class StateManager
{
    public static StateManager Instance
    {
        get
        {
            if (g_this == null)
                g_this = new StateManager();
            return g_this;

        }
    }

    public static StateManager g_this;

    public StateMgr mgr
    {
        get;
        private set;
    }

    public void OnInit(StateMgr _mgr)
    {
        Debug.Log("StateManager Init");

        this.mgr = _mgr;

        StateManager inst = StateManager.Instance;
    }

    public static void Method1(string eventName,string scriptTypeName, string method)
    {
        Debug.Log("Method1");
    }

    public void DoMethod1(string eventName, string scriptTypeName, string method)
    {
        Debug.Log("DoMethod1");
    }
}