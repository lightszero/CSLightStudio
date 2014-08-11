//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// If you don't have or don't wish to create an atlas, you can simply use this script to draw a texture.
/// Keep in mind though that this will create an extra draw call with each UITexture present, so it's
/// best to use it only for backgrounds or temporary visible widgets.
/// </summary>

[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Texture")]
public class UITexture : UIWidget
{
	[HideInInspector][SerializeField] Rect mRect = new Rect(0f, 0f, 1f, 1f);
	[HideInInspector][SerializeField] Shader mShader;
	[HideInInspector][SerializeField] Texture mTexture;
	[HideInInspector][SerializeField] Material mMat;

	bool mCreatingMat = false;
	Material mDynamicMat = null;
	int mPMA = -1;

	/// <summary>
	/// UV rectangle used by the texture.
	/// </summary>

	public Rect uvRect
	{
		get
		{
			return mRect;
		}
		set
		{
			if (mRect != value)
			{
				mRect = value;
				MarkAsChanged();
			}
		}
	}

	/// <summary>
	/// Shader used by the texture when creating a dynamic material (when the texture was specified, but the material was not).
	/// </summary>

	public Shader shader
	{
		get
		{
			if (mShader == null)
			{
				Material mat = material;
				if (mat != null) mShader = mat.shader;
				if (mShader == null) mShader = Shader.Find("Unlit/Transparent Colored");
			}
			return mShader;
		}
		set
		{
			if (mShader != value)
			{
				mShader = value;
				Material mat = material;
				if (mat != null) mat.shader = value;
				mPMA = -1;
			}
		}
	}

	/// <summary>
	/// Whether the texture has created its material dynamically.
	/// </summary>

	public bool hasDynamicMaterial { get { return mDynamicMat != null; } }

	/// <summary>
	/// Automatically destroy the dynamically-created material.
	/// </summary>

	public override Material material
	{
		get
		{
			if (mMat != null) return mMat;
			if (mDynamicMat != null) return mDynamicMat;

			if (!mCreatingMat && mDynamicMat == null)
			{
				mCreatingMat = true;

				if (mShader == null) mShader = Shader.Find("Unlit/Texture");

				Cleanup();

				mDynamicMat = new Material(mShader);
				mDynamicMat.hideFlags = HideFlags.DontSave;
				mDynamicMat.mainTexture = mTexture;
				mPMA = 0;
				mCreatingMat = false;
			}
			return mDynamicMat;
		}
		set
		{
			if (mMat != value)
			{
				Cleanup();
				mMat = value;
				mPMA = -1;
				MarkAsChanged();
			}
		}
	}

	/// <summary>
	/// Whether the texture is using a premultiplied alpha material.
	/// </summary>

	public bool premultipliedAlpha
	{
		get
		{
			if (mPMA == -1)
			{
				Material mat = material;
				mPMA = (mat != null && mat.shader != null && mat.shader.name.Contains("Premultiplied")) ? 1 : 0;
			}
			return (mPMA == 1);
		}
	}

	/// <summary>
	/// Texture used by the UITexture. You can set it directly, without the need to specify a material.
	/// </summary>

	public override Texture mainTexture
	{
		get
		{
			if (mMat != null) return mMat.mainTexture;
			if (mTexture != null) return mTexture;
			return null;
		}
		set
		{
			RemoveFromPanel();

			Material mat = material;

			if (mat != null)
			{
				mPanel = null;
				mTexture = value;
				mat.mainTexture = value;

				if (enabled) CreatePanel();
			}
		}
	}

	/// <summary>
	/// Clean up.
	/// </summary>

	void OnDestroy () { Cleanup(); }

	void Cleanup ()
	{
		if (mDynamicMat != null)
		{
			NGUITools.Destroy(mDynamicMat);
			mDynamicMat = null;
		}
	}

	/// <summary>
	/// Adjust the scale of the widget to make it pixel-perfect.
	/// </summary>

	public override void MakePixelPerfect ()
	{
		Texture tex = mainTexture;

		if (tex != null)
		{
			Vector3 scale = cachedTransform.localScale;
			scale.x = tex.width * uvRect.width;
			scale.y = tex.height * uvRect.height;
			scale.z = 1f;
			cachedTransform.localScale = scale;
		}
		base.MakePixelPerfect();
	}

	/// <summary>
	/// Virtual function called by the UIScreen that fills the buffers.
	/// </summary>

	public override void OnFill (BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{
		Color colF = color;
		colF.a *= mPanel.alpha;
		Color32 col = premultipliedAlpha ? NGUITools.ApplyPMA(colF) : colF;
	
		verts.Add(new Vector3(1f,  0f, 0f));
		verts.Add(new Vector3(1f, -1f, 0f));
		verts.Add(new Vector3(0f, -1f, 0f));
		verts.Add(new Vector3(0f,  0f, 0f));

		uvs.Add(new Vector2(mRect.xMax, mRect.yMax));
		uvs.Add(new Vector2(mRect.xMax, mRect.yMin));
		uvs.Add(new Vector2(mRect.xMin, mRect.yMin));
		uvs.Add(new Vector2(mRect.xMin, mRect.yMax));

		cols.Add(col);
		cols.Add(col);
		cols.Add(col);
		cols.Add(col);
	}
}
