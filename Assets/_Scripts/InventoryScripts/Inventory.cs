using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	GameObject inventoryPanel;
	GameObject slotPanel;
	ItemDatabase database;
	public GameObject inventorySlot;
	public GameObject inventoryItem;

	public int selectedSlot;

	private int slotAmount;
	public List<Item> items = new List<Item>();
	public List<GameObject> slots = new List<GameObject> ();

	void Start () 
	{
		database = GetComponent<ItemDatabase> ();
		slotAmount = 3;
		inventoryPanel = GameObject.Find ("Inventory Panel");
		slotPanel = inventoryPanel.transform.FindChild ("Slot Panel").gameObject;
		for (int i = 0; i < slotAmount; i++) {
			items.Add (new Item ());
			slots.Add (Instantiate (inventorySlot));
			slots [i].GetComponent<InventorySlot> ().id = i;
			slots [i].transform.SetParent (slotPanel.transform);
		}
		slots [0].GetComponent<InventorySlot>().SelectedSlot();
		AddItem (1);
		AddItem (0);
	}

	public void AddItem(int id)
	{
		Item itemToAdd = database.FetchItemByID (id);
		if (itemToAdd.Stackable && CheckIfItemIsInInventory (itemToAdd)) {
			for (int i = 0; i < items.Count; i++) 
				if (items [i].ID == id) {
					ItemData data = slots [i].transform.GetChild (0).GetComponent<ItemData> ();
					data.amount++;
					data.transform.GetChild (0).GetComponent<Text> ().text = data.amount.ToString ();
					break;
				}
		} else {
			if(items[selectedSlot].ID == -1)
			{
				items [selectedSlot] = itemToAdd;
				GameObject itemObj = Instantiate (inventoryItem);
				itemObj.GetComponent<ItemData> ().item = itemToAdd;
				itemObj.GetComponent<ItemData>().amount = 1; 
				itemObj.GetComponent<ItemData> ().slotID = selectedSlot;
				itemObj.transform.SetParent (slots [selectedSlot].transform);
				itemObj.transform.position = slots[selectedSlot].transform.position;
				itemObj.GetComponent<Image> ().sprite = itemToAdd.Sprite;
				itemObj.name = itemToAdd.Title;
			} else {
				for (int i = 0; i < items.Count; i++) {
					if (items [i].ID == -1) {
						items [i] = itemToAdd;
						GameObject itemObj = Instantiate (inventoryItem);
						itemObj.GetComponent<ItemData> ().item = itemToAdd;
						itemObj.GetComponent<ItemData>().amount = 1; 
						itemObj.GetComponent<ItemData> ().slotID = i;
						itemObj.transform.SetParent (slots [i].transform);
						itemObj.transform.position = slots[i].transform.position;
						itemObj.GetComponent<Image> ().sprite = itemToAdd.Sprite;
						itemObj.name = itemToAdd.Title;
						break;
					}
				}
			}
		}
		Debug.Log ("Added To Inventory!");
	}

	bool CheckIfItemIsInInventory(Item item)
	{
		for (int i = 0; i < items.Count; i++) 
			if(items[i].ID == item.ID)
				return true;
		return false;
	}

	void Update()
	{
		if (Input.GetAxisRaw ("Mouse ScrollWheel") > 0) {
			Debug.Log("Goes Up");
			int prevSlot = selectedSlot;
			if(selectedSlot == 2)
				selectedSlot = 0;
			else selectedSlot++;
			slots [selectedSlot].GetComponent<InventorySlot>().SelectedSlot();
			slots [prevSlot].GetComponent<InventorySlot>().DeselectedSlot();
		} else if (Input.GetAxisRaw ("Mouse ScrollWheel") < 0) 
		{
			Debug.Log ("Goes Down");
			int prevSlot = selectedSlot;
			if(selectedSlot == 0)
				selectedSlot = 2;
			else selectedSlot--;
			slots [selectedSlot].GetComponent<InventorySlot>().SelectedSlot();
			slots [prevSlot].GetComponent<InventorySlot>().DeselectedSlot();
		}

		if (Input.GetKeyDown (KeyCode.Q)) 
		{
			if(items[selectedSlot].ID != -1)
			{
				/* Beginning of the dropping mechanic!
				var pos = new Vector3(0,0,0);
				var rot = Quaternion.identity;
				Instantiate(GameObject.Find("_milkcarton"), pos, rot); // The Instantiate command takes a GameObject
				*/

				items[selectedSlot] = new Item();
				Destroy(slots[selectedSlot].transform.GetChild(0).gameObject);
				Debug.Log ("Deleted Item" + selectedSlot);
			}
		}
	}
}
