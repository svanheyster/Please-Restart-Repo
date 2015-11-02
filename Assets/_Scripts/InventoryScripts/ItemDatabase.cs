using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;

public class ItemDatabase : MonoBehaviour {
	private List<Item> database = new List<Item>();
	private JsonData itemData;


	void Start () 
	{
		itemData = JsonMapper.ToObject (File.ReadAllText(Application.dataPath + "/_StreamingAssests/Items.json"));
		ConstructItemDatabase ();

		Debug.Log (database [1].Title);
	}

	void ConstructItemDatabase()
	{
		for (int i = 0; i < itemData.Count; i++) 
		{
			database.Add(new Item((int)itemData[i]["id"], 
			                      itemData[i]["title"].ToString(), 
			                      (int)itemData[i]["weight"], 
			                      itemData[i]["description"].ToString()));
		}
	}
}

public class Item{
	public int ID { get; set; }
	public string Title { get; set; }
	public int Weight { get; set; }
	public string Description { get; set; }

	public Item(int id, string title, int weight, string description)
	{
		this.ID = id;
		this.Title = title;
		this.Weight = weight;
		this.Description = description;
	}

	public Item(){
		this.ID = -1;
	}
}
