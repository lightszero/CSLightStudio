using UnityEngine;

/// <summary>
/// Attach this script to a child of a draggable window to make it tilt as it's dragged.
/// Look at how it's used in Example 6.
/// </summary>

[AddComponentMenu("NGUI/Examples/Window Drag Tilt")]
public class WindowDragTilt : MonoBehaviour
{
	public int updateOrder = 0;
	public float degrees = 30f;

	Vector3 mLastPos;
	Transform mTrans;
	float mAngle = 0f;
	bool mInit = true;

	void Start () { UpdateManager.AddCoroutine(this, updateOrder, CoroutineUpdate); }
	void OnEnable () { mInit = true; }

	void CoroutineUpdate (float delta)
	{
		if (mInit)
		{
			mInit = false;
			mTrans = transform;
			mLastPos = mTrans.position;
		}

		Vector3 deltaPos = mTrans.position - mLastPos;
		mLastPos = mTrans.position;

		mAngle += deltaPos.x * degrees;
		mAngle = NGUIMath.SpringLerp(mAngle, 0f, 20f, delta);

		mTrans.localRotation = Quaternion.Euler(0f, 0f, -mAngle);
	}
}