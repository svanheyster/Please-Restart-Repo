﻿using UnityEngine;
using System.Collections;

public class ObjectPickUp : MonoBehaviour {

	public Camera camera;
	private RaycastHit hit;
	bool carry = false;
	private float shit = 0f;

	void FixedUpdate() 
	{
		if (Input.GetKeyDown (KeyCode.E)) {
			if(carry){
				carry = false;
				hit.rigidbody.useGravity = true;
				hit = new RaycastHit();
			} else {
				//if(Input.mousePosition.x < Screen.width / 2){
				Physics.Raycast (camera.ScreenPointToRay(Vector3(0.5,0.5,0)), out hit, 100f);
					Debug.Log(hit.transform.gameObject.name);

					if(Vector3.Distance(transform.position, hit.transform.gameObject.transform.position) < 1){
						hit.rigidbody.useGravity = false;
						carry = true;
					}
				//}
			}
		}

		if (carry)
			Carry ();
	}

	void Carry(){
		Rigidbody hitRidg = hit.transform.gameObject.GetComponent<Rigidbody>();

		hitRidg.MoveRotation (transform.rotation);

		Vector3 newPos = new Vector3 (transform.position.x, transform.position.y + 0.2f, transform.position.z);
		hitRidg.MovePosition (newPos + transform.forward * 0.2f);
	}
	void Start () {
	
	}

	void Update () {
	
	}
}
