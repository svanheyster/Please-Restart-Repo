using UnityEngine;
using System.Collections;

public class FP_Motor : MonoBehaviour {

	public static FP_Motor Instance;

	public float ForwardSpeed = 5.5f;
	public float BackwardSpeed = 2f;
	public float StrafingSpeed = 4.5f;
	public float SlideSpeed = 5.5f;
	public float JumpSpeed = 9f;
	public float Gravity = 21f;
	public float TerminalVelocity = 20f;
	public float SlideThreshold = 0.6f;
	public float MaxControllableSlideMagnitude = 0.4f;

	private Vector3 slideDirection;

	public Vector3 MoveVector { get; set; }
	public float VerticalVelocity { get; set; }

	void Awake()
	{
		Instance = this;
	}
	
	public void UpdateMotor()
	{
		SnapAlignCharacterWithCamera ();
		ProcessMotion ();
	}

	void ProcessMotion()
	{
		//Transform MoveVector to World Space
		MoveVector = transform.TransformDirection (MoveVector);

		//Normalise MoveVector if Magnitude > 1
		if (MoveVector.magnitude > 1)
			MoveVector = Vector3.Normalize (MoveVector);

		//Apply sliding if applicable
		ApplySlide ();

		//Multyply MoveVector by MoveSpeed
		MoveVector *= MoveSpeed ();

		//Reapply VerticalVelocity MoveVector.y
		MoveVector = new Vector3 (MoveVector.x, VerticalVelocity, MoveVector.z);

		//Apply Gravity
		ApplyGravity ();

		//Move Character in World Space, deltaTime converts from Units/frame to Units/second
		FP_Controller.CharacterController.Move (MoveVector * Time.deltaTime);
	}

	void ApplyGravity()
	{
		if (MoveVector.y > -TerminalVelocity)
			MoveVector = new Vector3 (MoveVector.x, MoveVector.y - Gravity * Time.deltaTime, MoveVector.z);

		if (FP_Controller.CharacterController.isGrounded && MoveVector.y < -1)
			MoveVector = new Vector3 (MoveVector.x, -1, MoveVector.z);
	}

	void ApplySlide()
	{
		if (!FP_Controller.CharacterController.isGrounded)
			return;
		slideDirection = Vector3.zero;
		RaycastHit hitInfo;

		if (Physics.Raycast (transform.position + Vector3.up, Vector3.down, out hitInfo)) 
		{
			if(hitInfo.normal.y < SlideThreshold)
				slideDirection = new Vector3(hitInfo.normal.x, -hitInfo.normal.y, hitInfo.normal.z);
		}

		if (slideDirection.magnitude < MaxControllableSlideMagnitude)
			MoveVector += slideDirection;
		else
			MoveVector = slideDirection;
	}

	public void Jump()
	{
		if (FP_Controller.CharacterController.isGrounded)
			VerticalVelocity = JumpSpeed;
	}

	void SnapAlignCharacterWithCamera()
	{
		if (MoveVector.x != 0 || MoveVector.z != 0) 
		{
			transform.rotation = Quaternion.Euler(
				transform.eulerAngles.x,
				Camera.main.transform.eulerAngles.y,
				transform.eulerAngles.z);
		}
	}

	float MoveSpeed()
	{
		var moveSpeed = 0f;

		switch (FP_MoveDirection.Instance.MoveDirection) 
		{
			case FP_MoveDirection.Direction.Stationary:
				moveSpeed = 0;
				break;
			case FP_MoveDirection.Direction.Forward:
				moveSpeed = ForwardSpeed;
				break;
			case FP_MoveDirection.Direction.Backward:
				moveSpeed = BackwardSpeed;
				break;
			case FP_MoveDirection.Direction.Left:
				moveSpeed = StrafingSpeed;
				break;
			case FP_MoveDirection.Direction.Right:
				moveSpeed = StrafingSpeed;
				break;
			case FP_MoveDirection.Direction.LeftForward:
				moveSpeed = ForwardSpeed;
				break;
			case FP_MoveDirection.Direction.RightForward:
				moveSpeed = ForwardSpeed;
				break;
			case FP_MoveDirection.Direction.RightBackward:
				moveSpeed = BackwardSpeed;
				break;
			case FP_MoveDirection.Direction.LeftBackward:
				moveSpeed = BackwardSpeed;
				break;
		}

		if (slideDirection.magnitude > 0)
			moveSpeed = SlideSpeed;

		return moveSpeed;
	}
}
