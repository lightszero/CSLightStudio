using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

class Script_Com01 : IScriptBehaviour
{
    public GameObject gameObject
    {
        get;
        private set;
    }

    public void Start()
    {
        Debug.Log("Start" + this.gameObject);
        gameObject.name = "Cool name.";
    }

    public void Update()
    {
        Transform trans = this.gameObject.GetComponent("Transform") as Transform;
        Debug.Log("trans=" + trans);
        trans.Rotate(Vector3.up, 5.0f);
    }

}
