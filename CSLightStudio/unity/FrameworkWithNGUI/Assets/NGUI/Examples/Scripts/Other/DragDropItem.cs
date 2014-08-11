//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2012 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

[AddComponentMenu("NGUI/Examples/Drag and Drop Item")]
public class DragDropItem : MonoBehaviour
{
	/// <summary>
	/// Prefab object that will be instantiated on the DragDropSurface if it receives the OnDrop event.
	/// </summary>

	public GameObject prefab;

	Transform mTrans;
	bool mIsDragging = false;
	bool mSticky = false;
	Transform mParent;

	/// <summary>
	/// Update the table, if there is one.
	/// </summary>

	void UpdateTable ()
	{
		UITable table = NGUITools.FindInParents<UITable>(gameObject);
		if (table != null) table.repositionNow = true;
	}

	/// <summary>
	/// Drop the dragged object.
	/// </summary>

	void Drop ()
	{
		// Is there a droppable container?
		Collider col = UICamera.lastHit.collider;
		DragDropContainer container = (col != null) ? col.gameObject.GetComponent<DragDropContainer>() : null;

		if (container != null)
		{
			// Container found -- parent this object to the container
			mTrans.parent = container.transform;

			Vector3 pos = mTrans.localPosition;
			pos.z = 0f;
			mTrans.localPosition = pos;
		}
		else
		{
			// No valid container under the mouse -- revert the item's parent
			mTrans.parent = mParent;
		}

		// Notify the table of this change
		UpdateTable();

		// Make all widgets update their parents
		NGUITools.MarkParentAsChanged(gameObject);
	}

	/// <summary>
	/// Cache the transform.
	/// </summary>

	void Awake () { mTrans = transform; }

	/// <summary>
	/// Start the drag event and perform the dragging.
	/// </summary>

	void OnDrag (Vector2 delta)
	{
		if (enabled && UICamera.currentTouchID > -2)
		{
			if (!mIsDragging)
			{
				mIsDragging = true;
				mParent = mTrans.parent;
				mTrans.parent = DragDropRoot.root;
				
				Vector3 pos = mTrans.localPosition;
				pos.z = 0f;
				mTrans.localPosition = pos;

				NGUITools.MarkParentAsChanged(gameObject);
			}
			else
			{
				mTrans.localPosition += (Vector3)delta;
			}
		}
	}

	/// <summary>
	/// Start or stop the drag operation.
	/// </summary>

	void OnPress (bool isPressed)
	{
		if (enabled)
		{
			if (isPressed)
			{
				if (!UICamera.current.stickyPress)
				{
					mSticky = true;
					UICamera.current.stickyPress = true;
				}
			}
			else if (mSticky)
			{
				mSticky = false;
				UICamera.current.stickyPress = false;
			}

			mIsDragging = false;
			Collider col = collider;
			if (col != null) col.enabled = !isPressed;
			if (!isPressed) Drop();
		}
	}
}
