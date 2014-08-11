//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2012 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// When Drag & Drop event begins in DragDropObject, it will re-parent itself to the DragDropRoot instead.
/// It's useful when you're dragging something out of a clipped panel -- you will want to reparent it before
/// it can be dragged outside.
/// </summary>

[AddComponentMenu("NGUI/Examples/Drag and Drop Root")]
public class DragDropRoot : MonoBehaviour
{
	static public Transform root;

	void Awake () { root = transform; }
}