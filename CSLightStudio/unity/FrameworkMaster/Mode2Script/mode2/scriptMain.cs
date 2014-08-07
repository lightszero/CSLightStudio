using UnityEngine;
using System.Collections;

//This is Dynmic Script
public class ScriptMain
{

    static void Run()
    {
        App.onupdate += Update;
        App.onclick += Click;
        App.AddButton(new Rect(0, 200, 200, 50), "state1", 1);
        App.AddButton(new Rect(200, 200, 200, 50), "state2", 2);
        App.AddButton(new Rect(400, 200, 200, 50), "state3", 3);
        App.AddButton(new Rect(0, 250, 200, 50), "Play", 10);
        App.AddButton(new Rect(200, 250, 200, 50), "Stop", 11);
        Debug.Log("ScriptMain Start.");

    }

    static void Update()
    {
        int c = 0;
        if (curState != null)
        {
            curState.OnUpdate();
        }
        //Debug.Log("ScriptMain Update.");
    }
    static void Click(int i)
    {
        if (i == 1)
        {
            ChangeState(new State1());
        }
        if (i == 2)
        {
            ChangeState(new State2());
        }
        if (i == 3)
        {
            ChangeState(new State3());
        }
        if(i==10)
        {
            if (curState != null)
                curState.ClickEvent(1);
        }
        if (i == 11)
        {
            if (curState != null)
                curState.ClickEvent(2);
        }
    }

    static IState curState;
    static void ChangeState(IState state)
    {
        if (curState != null)
            curState.OnExit();
        curState = state;
        if (curState != null)
            curState.OnInit();
    }
}
