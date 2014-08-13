using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using Object = UnityEngine.Object;//如果发生名字冲突可以这样写
//This is Dynmic Script
class State1 : IState
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

        GameObject o  =uiRoot.transform.Find("Button").gameObject;
        UIEventListener.Get(o).onClick = (ooo) =>
            {
                Debug.Log("onclick.");
                mgr.ChangeState("state2");
            };
    }

    public void OnExit()
    {
 
    }

    public void OnUpdate()
    {

    }

}

