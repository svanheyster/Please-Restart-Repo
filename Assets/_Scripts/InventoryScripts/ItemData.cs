using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler {
	public Item item;
	public int amount;
	public int slotID;

	private Inventory inv;
	private Vector2 offset;

	void Start () {
		inv = GameObject.Find ("InventoryLoader").GetComponent<Inventory> ();
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (item != null) 
		{
			offset = eventData.position - new Vector2 (this.transform.position.x, this.transform.position.y);
			this.transform.SetParent(this.transform.parent.parent);
			this.transform.position = eventData.position - offset;
			GetComponent<CanvasGroup>().blocksRaycasts = false;
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (item != null)
			this.transform.position = eventData.position - offset;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		this.transform.SetParent (inv.slots[slotID].transform);
		this.transform.position = inv.slots [slotID].transform.position;
		GetComponent<CanvasGroup>().blocksRaycasts = true;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (item != null)
			offset = eventData.position - new Vector2 (this.transform.position.x, this.transform.position.y);
	}

	void Update () {
	
	}
}
