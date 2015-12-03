using UnityEngine;
using System.Collections;

public class DragAndThrow : MonoBehaviour {

	int normalCollisionCount = 1;
	public float spring = 50.0f;
	public float damper = 5.0f;
	public float drag = 10.0f;
	public float angularDrag = 5.0f;
	public float distance = 0.2f;
	public int throwForce = 500;
	public int throwRange = 1000;
	public bool attachToCenterOfMass = false;

	private SpringJoint springjoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//Checks if user pressed mousebutton.
		if (!Input.GetMouseButton (0))
			return;

		Camera mainCamera = FindCamera ();

		//Checks if an object is hit.
		RaycastHit hit;
		if (!Physics.Raycast (mainCamera.ScreenPointToRay (Input.mousePosition), out hit, 100))
			return;
		//Checks if object is rigidbody, not kinematic.
		if (!hit.rigidbody || hit.rigidbody.isKinematic)
			return;

		if (!springjoint) 
		{
			GameObject go = new GameObject("Rigidbody dragger");
			Rigidbody body = go.AddComponent<Rigidbody>() as Rigidbody;
			springjoint = go.AddComponent<SpringJoint>();
			body.isKinematic = true;
		}
	
		springjoint.transform.position = hit.point;
		if (attachToCenterOfMass) {
			Vector3 anchor = transform.TransformDirection (hit.rigidbody.centerOfMass) + hit.rigidbody.transform.position;
			anchor = springjoint.transform.InverseTransformPoint (anchor);
			springjoint.anchor = anchor;
		} else {
			springjoint.anchor = Vector3.zero;
		}

		springjoint.spring = spring;
		springjoint.damper = spring;
		springjoint.maxDistance = distance;
		springjoint.connectedBody = hit.rigidbody;

		StartCoroutine (DragObject, hit.distance);
	}

	void DragObject(float distance)
	{
		float oldDrag = springjoint.connectedBody.drag;
		float oldAngularDrag = springjoint.connectedBody.angularDrag;
		springjoint.connectedBody.drag = drag;
		springjoint.connectedBody.angularDrag = angularDrag;
		Camera mainCamera = FindCamera ();
		while (Input.GetMouseButton(0)) 
		{
			Debug.Log("Pressing somethin!");
			Ray ray = mainCamera.ScreenPointToRay (Input.mousePosition);
			springjoint.transform.position = ray.GetPoint(distance); 

			if(Input.GetMouseButton(1))
			{
				springjoint.connectedBody.AddExplosionForce(throwForce, mainCamera.transform.position, throwRange);
				springjoint.connectedBody.drag = oldDrag;
				springjoint.connectedBody.angularDrag = oldAngularDrag;
				springjoint.connectedBody = null;
			}
		}
		if(springjoint.connectedBody)
		{
			springjoint.connectedBody.drag = oldDrag;
			springjoint.connectedBody.angularDrag = oldAngularDrag;
			springjoint.connectedBody = null;
		}

	} 

	Camera FindCamera(){
		if (GetComponent<Camera>())
			return GetComponent<Camera>();
		else
			return Camera.main;
	}
}
