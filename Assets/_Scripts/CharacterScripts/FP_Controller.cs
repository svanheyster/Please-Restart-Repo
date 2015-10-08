using UnityEngine;
using System.Collections;

public class FP_Controller : MonoBehaviour {

	public static CharacterController CharacterController;
	public static FP_Controller Instance;



	void Awake()
	{
		CharacterController = GetComponent ("CharacterController") as CharacterController;
		Instance = this;
		FP_Camera.UseExistingOrCreateNewMainCamera();
	}

	void Update () 
	{
		if (Camera.main == null)
			return;

		GetLocomotionInput ();
		HandleActionInput ();

		FP_Motor.Instance.UpdateMotor ();
	}

	void GetLocomotionInput()
	{
		var deadZone = 0.1f;

		FP_Motor.Instance.VerticalVelocity = FP_Motor.Instance.MoveVector.y;
		FP_Motor.Instance.MoveVector = Vector3.zero;

		if (Input.GetAxis ("Vertical") > deadZone || Input.GetAxis ("Vertical") < -deadZone)
			FP_Motor.Instance.MoveVector += new Vector3 (0, 0, Input.GetAxis ("Vertical"));

		if (Input.GetAxis ("Horizontal") > deadZone || Input.GetAxis ("Horizontal") < -deadZone)
			FP_Motor.Instance.MoveVector += new Vector3 (Input.GetAxis ("Horizontal"), 0, 0);

		FP_MoveDirection.Instance.DetermineCurrentMoveDirection ();
	}

	void HandleActionInput()
	{
		if (Input.GetButton ("Jump"))
			Jump ();
	}

	void Jump()
	{
		FP_Motor.Instance.Jump ();
	}
}
