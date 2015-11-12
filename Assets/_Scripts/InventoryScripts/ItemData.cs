using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	public Item item;
	public int amount;

	private Transform originalParent;

	void Start () {
	
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (item != null) 
		{
			originalParent = this.transform.parent;
			this.transform.SetParent(this.transform.parent.parent);
			this.transform.position = eventData.position;
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (item != null)
			this.transform.position = eventData.position;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		this.transform.SetParent (originalParent);
	}

	void Update () {
	
	}
}
