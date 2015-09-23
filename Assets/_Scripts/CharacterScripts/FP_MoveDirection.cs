using UnityEngine;
using System.Collections;

public class FP_MoveDirection : MonoBehaviour {

	public static FP_MoveDirection Instance;

	public enum Direction{
		Stationary, Forward, Backward, Left, Right,
		LeftForward, RightForward, LeftBackward, RightBackward
	}

	public Direction MoveDirection { get; set; }
	
	void Awake(){
		Instance = this;
	}

	void Start () {
	}

	void Update () {
	}

	public void DetermineCurrentMoveDirection(){
		var forward = false;
		var backward = false;
		var left = false;
		var right = false;
		
		if (FP_Motor.Instance.MoveVector.z > 0)
			forward = true;
		if (FP_Motor.Instance.MoveVector.z < 0)
			backward = true;
		if (FP_Motor.Instance.MoveVector.x > 0)
			right = true;
		if (FP_Motor.Instance.MoveVector.x < 0)
			left = true;
		
		if (forward) {
			if (left)
				MoveDirection = Direction.LeftForward;
			else if (right)
				MoveDirection = Direction.RightForward;
			else
				MoveDirection = Direction.Forward;
		}
		else if (backward) {
			if(left)
				MoveDirection = Direction.LeftBackward;
			else if (right)
				MoveDirection = Direction.RightBackward;
			else
				MoveDirection = Direction.Backward;
		} else if (left) {
			MoveDirection = Direction.Left;
		} else if (right) {
			MoveDirection = Direction.Right;
		} else
			MoveDirection = Direction.Stationary;
	}
}
