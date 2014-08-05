using UnityEngine;
using System.Collections;

public interface IScriptBehaviour
{
    GameObject gameObject
    {
        get;
    }
    void Start();

    void Update();
}