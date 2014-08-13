
using System.Collections.Generic;
using UnityEngine;
using System.Text;
//This is Dynmic Script
class State2: IState
{
    GameObject uiRoot;
    public StateMgr mgr
    {
        get;
        private set;
    }

    public void OnInit(StateMgr _mgr, GameObject rootSprite)
    {
        this.mgr = _mgr;
        this.uiRoot = rootSprite;

        GameObject o = uiRoot.transform.Find("Button").gameObject;
        UIEventListener.Get(o).onClick = (ooo) =>
        {
            Debug.Log("onclick.");
            mgr.ChangeState("state3");
        };
    }
    public void OnExit()
    {
     
    }

    public void OnUpdate()
    {
      
    }

}

