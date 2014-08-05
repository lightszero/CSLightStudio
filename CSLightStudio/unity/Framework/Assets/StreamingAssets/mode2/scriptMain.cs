using UnityEngine;
using System.Collections;

public class ScriptMain
{

    static void Run()
    {
        if (g_this == null)
        {
            g_this = new ScriptMain();
        }
        App.onupdate += Update;
        App.onclick += Click;
        App.AddButton(new Rect(0, 200, 200, 50), "fuck", 1);
        Debug.Log("ScriptMain Start.");

    }

    static void Update(int i)
    {
        int c = 0;
        //Debug.Log("ScriptMain Update.");
    }
    static void Click(int i)
    {
        if(i==1)
        {
            ScriptMain.Click1();
            Debug.Log("ScriptMain Click." + i);
            //ScriptMain.curState = new State1();
            //ScriptMain.curState.OnInit();
        }
     
    }
    static void Click1()
    {
        Debug.Log("ScriptMain Click11." );
        g_this.curState = new State1();
        g_this.curState.OnInit();
    }
   static ScriptMain g_this;
    public IState curState;

}
