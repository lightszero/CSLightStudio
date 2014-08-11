//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2012 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Selectable sprite that follows the mouse.
/// </summary>

[RequireComponent(typeof(UISprite))]
[AddComponentMenu("NGUI/Examples/UI Cursor")]
public class UICursor : MonoBehaviour
{
	static UICursor mInstance;

	// Camera used to draw this cursor
	public Camera uiCamera;

	Transform mTrans;
	UISprite mSprite;

	UIAtlas mAtlas;
	string mSpriteName;

	/// <summary>
	/// Keep an instance reference so this class can be easily found.
	/// </summary>

	void Awake () { mInstance = this; }
	void OnDestroy () { mInstance = null; }

	/// <summary>
	/// Cache the expected components and starting values.
	/// </summary>

	void Start ()
	{
		mTrans = transform;
		mSprite = GetComponentInChildren<UISprite>();
		mAtlas = mSprite.atlas;
		mSpriteName = mSprite.spriteName;
		mSprite.depth = 100;
		if (uiCamera == null) uiCamera = NGUITools.FindCameraForLayer(gameObject.layer);
	}

	/// <summary>
	/// Reposition the sprite.
	/// </summary>

	void Update ()
	{
		if (mSprite.atlas != null)
		{
			Vector3 pos = Input.mousePosition;

			if (uiCamera != null)
			{
				// Since the screen can be of different than expected size, we want to convert
				// mouse coordinates to view space, then convert that to world position.
				pos.x = Mathf.Clamp01(pos.x / Screen.width);
				pos.y = Mathf.Clamp01(pos.y / Screen.height);
				mTrans.position = uiCamera.ViewportToWorldPoint(pos);

				// For pixel-perfect results
				if (uiCamera.isOrthoGraphic)
				{
					mTrans.localPosition = NGUIMath.ApplyHalfPixelOffset(mTrans.localPosition, mTrans.localScale);
				}
			}
			else
			{
				// Simple calculation that assumes that the camera is of fixed size
				pos.x -= Screen.width * 0.5f;
				pos.y -= Screen.height * 0.5f;
				mTrans.localPosition = NGUIMath.ApplyHalfPixelOffset(pos, mTrans.localScale);
			}
		}
	}

	/// <summary>
	/// Clear the cursor back to its original value.
	/// </summary>

	static public void Clear ()
	{
		Set(mInstance.mAtlas, mInstance.mSpriteName);
	}

	/// <summary>
	/// Override the cursor with the specified sprite.
	/// </summary>

	static public void Set (UIAtlas atlas, string sprite)
	{
		if (mInstance != null)
		{
			mInstance.mSprite.atlas = atlas;
			mInstance.mSprite.spriteName = sprite;
			mInstance.mSprite.MakePixelPerfect();
			mInstance.Update();
		}
	}
}