using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	GameObject inventoryPanel;
	GameObject slotPanel;
	ItemDatabase database;
	GameObject inventorySlot;
	GameObject inventoryItem;

	private int slotAmount;
	public List<Item> items = new List<Item>();
	public List<GameObject> slots = new List<GameObject>();

	void Start () 
	{
		database = GetComponent<ItemDatabase> ();

		slotAmount = 3;
		inventoryPanel = GameObject.Find ("Inventory Panel");
		slotPanel = inventoryPanel.transform.FindChild ("Slot Panel").gameObject;

		//items.Add (new Item ());
		slots.Add (GameObject.Find ("Slot1"));
		slots.Add (GameObject.Find ("Slot2"));
		slots.Add (GameObject.Find ("Slot3"));
	}

	public void AddItem(int id)
	{
		Item itemToAdd = database.FetchItemByID (id);
		for (int i = 0; i < items.Count; i++) 
		{
			if(items[i].ID == -1)
			{
				items[i] = itemToAdd;
				//GameObject itemObj = Instantiate(InventoryItem);
				//itemObj.transform.SetParent(slots[i].transform);
				//itemObj.transform.position = Vector2.zero;
				break;
			}
		}
	}

	void Update () {
	
	}
}
