using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

	public int distanceToItem = 20;
	public GameObject item;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Collect();
	}

	void Collect(){
		if (Input.GetMouseButtonUp (1)) { //Right Click
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if(Physics.Raycast(ray, out hit, distanceToItem)){
				if(hit.collider.gameObject.name == "item"){
					Debug.Log ("item hit");
					Destroy (hit.collider.gameObject);
				}
			}
		}
	}
}
