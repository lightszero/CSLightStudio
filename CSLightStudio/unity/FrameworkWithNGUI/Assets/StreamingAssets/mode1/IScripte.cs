using UnityEngine;
using System.Collections;
//This is Dynmic Script
public interface IState
{

    void OnInit(StateMgr mgr,GameObject uiobj);

    void OnExit();

    void OnUpdate();

}