﻿using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;

public class ItemDatabase: MonoBehaviour {
	private List<Item> database = new List<Item>();
	private JsonData itemData;

	void Start()
	{
		itemData = JsonMapper.ToObject (File.ReadAllText (Application.dataPath + "/_StreamingAssets/Items.json"));
		ConstructItemDatabase ();

		Debug.Log (database [23].Title);
	}

	public Item FetchItemByID(int id)
	{
		for (int i = 0; i < database.Count; i++) 
			if(database[i].ID == id)
				return database[i];
		return null;
	}

	void ConstructItemDatabase()
	{
		for(int i = 0; i < itemData.Count; i++)
		{
			database.Add(new Item(
				(int)itemData[i]["id"], 
				itemData[i]["title"].ToString(), 
				(int)itemData[i]["weight"], 
				itemData[i]["description"].ToString()));
		}
	}
}

public class Item
{
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
}