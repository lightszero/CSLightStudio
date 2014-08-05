using UnityEngine;
using System.Collections;

public class ScriptMain
{

    static void Run()
    {
        App.onupdate += Update;
        App.onclick += Click;
        App.AddButton(new Rect(0, 200, 200, 50), "fuck", 1);
        Debug.Log("ScriptMain Start.");

    }

    static void Update()
    {
        int c = 0;
        if(curState!=null)
        {
            curState.OnUpdate();
        }
        //Debug.Log("ScriptMain Update.");
    }
    static void Click(int i)
    {
        if(i==1)
        {
            ChangeState(new State1());
           
            //ScriptMain.curState = new State1();
            //ScriptMain.curState.OnInit();
        }
     
    }
    static void Click1()
    {
        Debug.Log("ScriptMain Click11." );
        
    }
    static IState curState;
    static void ChangeState(IState state)
    {
        if (curState != null)
            curState.OnExit();
        curState=state;
        if (curState != null)
            curState.OnInit();
    }
}
