using UnityEngine;
using System.Collections;

public class ObjectPickUp : MonoBehaviour {

	float distance = 1f;
	Transform camera;
	RaycastHit hit;

	void LateUpdate()
	{
		if (Input.GetKey (KeyCode.E)) 
		{
			camera = Camera.main.transform;
			if(Physics.Raycast(new Ray(camera.position, camera.forward), out hit, 200)){
				if(hit.collider.gameObject.name.Equals("orangeBox")){

					Vector3 mousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, distance);
					Vector3 objPosition = Camera.main.ScreenToWorldPoint (mousePosition);

					transform.position = objPosition;
				}
			}
		}
	}
}
