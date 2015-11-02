using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	GameObject inventoryPanel;
	GameObject slotPanel;
	public GameObject inventorySlot;
	public GameObject inventoryItem;

	private int slotAmount;
	public List<Item> items = new List<Item>();
	public List<GameObject> slots = new List<GameObject> ();

	void Start () 
	{
		slotAmount = 3;
		inventoryPanel = GameObject.Find ("Inventory Panel");
		slotPanel = inventoryPanel.transform.FindChild ("Slot Panel").gameObject;
		for (int i = 0; i < slotAmount; i++) 
		{
			slots.Add (Instantiate(inventorySlot));
			slots[i].transform.SetParent(slotPanel.transform);
		}
	}
}
