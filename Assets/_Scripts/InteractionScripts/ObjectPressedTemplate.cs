using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjectPressedTemplate : MonoBehaviour {

	private Text UIText;
	private Inventory inv;


	// Use this for initialization
	void Start () {
		UIText = GameObject.Find ("InteractiveText").GetComponent<Text>();
		inv = GameObject.Find ("InventoryLoader").GetComponent<Inventory> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void OnMouseEnter(){
		UIText.text = "Press E to interact.";
	}

	public void OnMouseExit(){
		UIText.text = "";
	}

	public void OnMouseOver(){
		if (Input.GetKeyDown (KeyCode.E)) 
		{
			Debug.Log ("GameObject: " + this.gameObject.name + " was clicked.");
			inv.AddItem (4);
		}
	}
}
