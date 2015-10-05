using UnityEngine;
using System.Collections;

public class FP_CrossHair : MonoBehaviour {

	public Texture2D crosshairTexture;
	private Rect position;
	static private bool OriginalOn = true;
	private float crossHairLength = Screen.width / 50; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		position = new Rect (
			(Screen.width - crossHairLength) / 2,
			(Screen.height - crossHairLength) / 2 - 32, 
			crossHairLength, crossHairLength);
	}

	void OnGUI(){
		if (OriginalOn == true) 
			GUI.DrawTexture (position, crosshairTexture);
	}
}
