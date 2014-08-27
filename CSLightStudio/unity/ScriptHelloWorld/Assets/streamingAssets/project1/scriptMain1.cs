using UnityEngine;
using System.Collections;

public class ScriptMain1 
{

    static void Start()
    {
        root = GameObject.CreatePrimitive(PrimitiveType.Cylinder);

        for (int i = 0; i < 10; i++)
        {
            GameObject sub = GameObject.CreatePrimitive(PrimitiveType.Cube);
            sub.transform.parent = root.transform;
            sub.transform.localPosition = new Vector3(i, 0, 0);
            sub.GetComponent("Transform");
        }

    }
	static scriptFun1  f1 = new scriptFun1 ();
    static GameObject root =null;
    static void Update()
    {
		f1.Update (Time.deltaTime);
        root.transform.Rotate(Vector3.up,f1.speed);
    }
	static void Exit()
	{
		Object.Destroy (root);
	}
}
