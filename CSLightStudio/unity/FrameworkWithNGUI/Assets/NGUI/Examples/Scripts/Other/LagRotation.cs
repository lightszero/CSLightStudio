using UnityEngine;

/// <summary>
/// Attach to a game object to make its rotation always lag behind its parent as the parent rotates.
/// </summary>

[AddComponentMenu("NGUI/Examples/Lag Rotation")]
public class LagRotation : MonoBehaviour
{
	public int updateOrder = 0;
	public float speed = 10f;
	public bool ignoreTimeScale = false;
	
	Transform mTrans;
	Quaternion mRelative;
	Quaternion mAbsolute;
	
	void Start()
	{
		mTrans = transform;
		mRelative = mTrans.localRotation;
		mAbsolute = mTrans.rotation;
		if (ignoreTimeScale) UpdateManager.AddCoroutine(this, updateOrder, CoroutineUpdate);
		else UpdateManager.AddLateUpdate(this, updateOrder, CoroutineUpdate);
	}

	void CoroutineUpdate (float delta)
	{
		Transform parent = mTrans.parent;
		
		if (parent != null)
		{
			mAbsolute = Quaternion.Slerp(mAbsolute, parent.rotation * mRelative, delta * speed);
			mTrans.rotation = mAbsolute;
		}
	}
}