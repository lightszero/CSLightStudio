//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Update manager allows for simple programmatic ordering of update events.
/// </summary>

[ExecuteInEditMode]
[AddComponentMenu("NGUI/Internal/Update Manager")]
public class UpdateManager : MonoBehaviour
{
	public delegate void OnUpdate (float delta);

	public class UpdateEntry
	{
		public int index = 0;
		public OnUpdate func;
		public MonoBehaviour mb;
		public bool isMonoBehaviour = false;
	}

	public class DestroyEntry
	{
		public Object obj;
		public float time;
	}

	static int Compare (UpdateEntry a, UpdateEntry b)
	{
		if (a.index < b.index) return 1;
		if (a.index > b.index) return -1;
		return 0;
	}

	static UpdateManager mInst;
	List<UpdateEntry> mOnUpdate = new List<UpdateEntry>();
	List<UpdateEntry> mOnLate = new List<UpdateEntry>();
	List<UpdateEntry> mOnCoro = new List<UpdateEntry>();
	BetterList<DestroyEntry> mDest = new BetterList<DestroyEntry>();
	float mTime = 0f;

	/// <summary>
	/// Ensure that there is an instance of this class present.
	/// </summary>

	static void CreateInstance ()
	{
		if (mInst == null)
		{
			mInst = GameObject.FindObjectOfType(typeof(UpdateManager)) as UpdateManager;

			if (mInst == null && Application.isPlaying)
			{
				GameObject go = new GameObject("_UpdateManager");
				DontDestroyOnLoad(go);
				//go.hideFlags = HideFlags.HideAndDontSave;
				mInst = go.AddComponent<UpdateManager>();
			}
		}
	}

	/// <summary>
	/// Update the specified list.
	/// </summary>

	void UpdateList (List<UpdateEntry> list, float delta)
	{
		for (int i = list.Count; i > 0; )
		{
			UpdateEntry ent = list[--i];

			// If it's a monobehaviour we need to do additional checks
			if (ent.isMonoBehaviour)
			{
				// If the monobehaviour is null, remove this entry
				if (ent.mb == null)
				{
					list.RemoveAt(i);
					continue;
				}

				// If the monobehaviour or its game object are disabled, move on to the next entry
				if (!ent.mb.enabled || !NGUITools.GetActive(ent.mb.gameObject)) continue;
			}

			// Call the function
			ent.func(delta);
		}
	}

	/// <summary>
	/// Start the coroutine.
	/// </summary>

	void Start ()
	{
		if (Application.isPlaying)
		{
			mTime = Time.realtimeSinceStartup;
			StartCoroutine(CoroutineFunction());
		}
	}

	/// <summary>
	/// Don't keep this class around after stopping the Play mode.
	/// </summary>

	void OnApplicationQuit () { DestroyImmediate(gameObject); }

	/// <summary>
	/// Call all update functions.
	/// </summary>

	void Update ()
	{
		if (mInst != this) NGUITools.Destroy(gameObject);
		else UpdateList(mOnUpdate, Time.deltaTime);
	}

	/// <summary>
	/// Call all late update functions and destroy this class if no callbacks have been registered.
	/// </summary>

	void LateUpdate ()
	{
		UpdateList(mOnLate, Time.deltaTime);
		if (!Application.isPlaying) CoroutineUpdate();
	}

	/// <summary>
	/// Call all coroutine update functions and destroy all delayed destroy objects.
	/// </summary>

	bool CoroutineUpdate ()
	{
		float time = Time.realtimeSinceStartup;
		float delta = time - mTime;
		if (delta < 0.001f) return true;

		mTime = time;

		UpdateList(mOnCoro, delta);

		bool appIsPlaying = Application.isPlaying;

		for (int i = mDest.size; i > 0; )
		{
			DestroyEntry de = mDest.buffer[--i];

			if (!appIsPlaying || de.time < mTime)
			{
				if (de.obj != null)
				{
					NGUITools.Destroy(de.obj);
					de.obj = null;
				}
				mDest.RemoveAt(i);
			}
		}

		// Nothing left to update? Destroy this game object.
		if (mOnUpdate.Count == 0 && mOnLate.Count == 0 && mOnCoro.Count == 0 && mDest.size == 0)
		{
			NGUITools.Destroy(gameObject);
			return false;
		}
		return true;
	}

	/// <summary>
	/// Coroutine update function.
	/// </summary>

	IEnumerator CoroutineFunction ()
	{
		while (Application.isPlaying)
		{
			if (CoroutineUpdate()) yield return null;
			else break;
		}
	}

	/// <summary>
	/// Generic add function.
	/// Technically 'mb' is not necessary as it can be retrieved by calling 'func.Target as MonoBehaviour'.
	/// Unfortunately Flash export fails to compile with that, claiming the following:
	/// "Error: Access of possibly undefined property Target through a reference with static type Function."
	/// </summary>

	void Add (MonoBehaviour mb, int updateOrder, OnUpdate func, List<UpdateEntry> list)
	{
#if !UNITY_FLASH
		// Flash export fails at life.
		for (int i = 0, imax = list.Count; i < imax; ++i)
		{
			UpdateEntry ent = list[i];
			if (ent.func == func) return;
		}
#endif

		UpdateEntry item = new UpdateEntry();
		item.index = updateOrder;
		item.func = func;
		item.mb = mb;
		item.isMonoBehaviour = (mb != null);

		list.Add(item);
		if (updateOrder != 0) list.Sort(Compare);
	}

	/// <summary>
	/// Add a new update function with the specified update order.
	/// </summary>

	static public void AddUpdate (MonoBehaviour mb, int updateOrder, OnUpdate func) { CreateInstance(); mInst.Add(mb, updateOrder, func, mInst.mOnUpdate); }

	/// <summary>
	/// Add a new late update function with the specified update order.
	/// </summary>

	static public void AddLateUpdate (MonoBehaviour mb, int updateOrder, OnUpdate func) { CreateInstance(); mInst.Add(mb, updateOrder, func, mInst.mOnLate); }

	/// <summary>
	/// Add a new coroutine update function with the specified update order.
	/// </summary>

	static public void AddCoroutine (MonoBehaviour mb, int updateOrder, OnUpdate func) { CreateInstance(); mInst.Add(mb, updateOrder, func, mInst.mOnCoro); }

	/// <summary>
	/// Destroy the object after the specified delay in seconds.
	/// </summary>

	static public void AddDestroy (Object obj, float delay)
	{
		if (obj == null) return;

		if (Application.isPlaying)
		{
			if (delay > 0f)
			{
				CreateInstance();

				DestroyEntry item = new DestroyEntry();
				item.obj = obj;
				item.time = Time.realtimeSinceStartup + delay;
				mInst.mDest.Add(item);
			}
			else Destroy(obj);
		}
		else DestroyImmediate(obj);
	}
}
