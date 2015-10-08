using UnityEngine;
using System.Collections;

public class FP_MouseLock : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	
	void OnGUI()
	{
		GUILayout.BeginVertical ();
		if (Input.GetKeyDown (KeyCode.Tab)) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		} else if (Input.GetKeyUp (KeyCode.Tab)) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}

	}
}
