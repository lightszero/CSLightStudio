using UnityEngine;
using System.Collections;

public class ScriptMain2 
{
	
	static void Start()
	{
		root = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		
		for (int i = 0; i < 10; i++)
		{
			GameObject sub = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			sub.transform.parent = root.transform;
			sub.transform.localPosition = new Vector3(i, 0, 0);
		}
		
	}
	static scriptFun2  f1 = new scriptFun2 ();
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
