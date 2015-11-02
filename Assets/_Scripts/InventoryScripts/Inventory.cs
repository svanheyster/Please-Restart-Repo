using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	GameObject inventoryPanel;
	GameObject slotPanel;
	GameObject inventorySlot;
	GameObject inventoryItem;

	private int slotAmount;
	public List<Item> items = new List<Item>();
	public List<GameObject> slots = new List<GameObject>();

	void Start () 
	{
		slotAmount = 3;
		inventoryPanel = GameObject.Find ("Inventory Panel");
		slotPanel = inventoryPanel.transform.FindChild ("Slot Panel").gameObject;

		items.Add (new Item ());
		slots.Add (GameObject.Find ("Slot1"));
		slots.Add (GameObject.Find ("Slot2"));
		slots.Add (GameObject.Find ("Slot3"));
	}

	public void AddItem(int id)
	{

	}

	void Update () {
	
	}
}
