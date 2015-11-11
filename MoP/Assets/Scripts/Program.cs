using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Program : MonoBehaviour {

	public Animation anim;
	public AnimationClip animClip;

	NetworkView nv;

	// Use this for initialization
	void Start () {
		Debug.Log ("[Program:Start]");
		nv = GetComponentInChildren<NetworkView> ();
		StartCoroutine ("SyncMotion");
//		StartCoroutine ("JoystickProcess");
	}

	bool enableSync;
	IEnumerator SyncMotion () {
		enableSync = true;
		while (enableSync) {
			if (RobotManager.Instance.Touch[0] > 0)
				SyncArmMotor ();
			
//			DisplayMotorAngle ();
//			DisplayTouch ();
			
			yield return new WaitForSeconds (0.1f);
		}
		yield return null;
	}

	bool joystick_process = true;
	int xy_mode = 0;	// 0: mobile, 1:neck
	void JoystickProcess () {
		bool mobile_moving = false;
//		while (joystick_process) 
		{
			float joyX = Input.GetAxis ("Horizontal");
			float joyY = Input.GetAxis("Vertical");

//			Debug.Log (string.Format ("JoystickProcess: {0}, {1}", joyX, joyY));

			switch (xy_mode) {
			case 0:
				JoystickMovility (new Vector2 (joyX, joyY));
				Debug.Log (string.Format ("JoystickProcess: {0}, {1}", joyX, joyY));
				break;
			case 1:
				JoystickPanTilt (new Vector2 (joyX, joyY));
				Debug.Log (string.Format ("Joystick PanTilt: {0}, {1}", joyX, joyY));
				break;
			default:
				break;
			}

			if (Input.GetKeyDown (KeyCode.JoystickButton7)) {
				Debug.Log ("Joystick Mode: PanTilt");
				xy_mode = 1;
			}
			if (Input.GetKeyUp (KeyCode.JoystickButton7)) {
				Debug.Log ("Joystick Mode: Mobility");
				xy_mode = 0;
			}

//			yield return new WaitForSeconds (0.1f);
		}
//		yield return null;
	}

	void SyncArmMotor () {
		nv.RPC ("PostureArm", RPCMode.Others, 
		        RobotManager.Instance.Angle[2],
		        RobotManager.Instance.Angle[3],
		        RobotManager.Instance.Angle[4],
		        RobotManager.Instance.Angle[5],
		        RobotManager.Instance.Angle[6],
		        RobotManager.Instance.Angle[7]);
    }

	// Update is called once per frame
	void Update () {
		try {
			JoystickProcess ();

//			Debug.Log ("Horizontal: " + Input.GetAxis ("Horizontal") + ", Vertical: " + Input.GetAxis("Vertical") + ", JoyRX: " + Input.GetAxis("JoyRX") + ", JoyRY: " + Input.GetAxis("JoyRY"));
//			Debug.Log (  ", 5: " + Input.GetKeyDown(KeyCode.JoystickButton5)
//			           + ", 6: " + Input.GetKeyDown(KeyCode.JoystickButton6)
//			           + ", 8: " + Input.GetKeyDown(KeyCode.JoystickButton8)
//			           + ", 9: " + Input.GetKeyDown(KeyCode.JoystickButton9)
//			           + ",11: " + Input.GetKeyDown(KeyCode.JoystickButton11)
//			           + ",12: " + Input.GetKeyDown(KeyCode.JoystickButton12)
//			           + ",13: " + Input.GetKeyDown(KeyCode.JoystickButton13)
//			           + ",14: " + Input.GetKeyDown(KeyCode.JoystickButton14));

//			if (Input.GetKeyDown(KeyCode.JoystickButton0))
//				Debug.Log ("Joystick 0 DN");
//			else if (Input.GetKeyDown (KeyCode.JoystickButton1))
//				Debug.Log ("Joystick 1 DN");
//			else if (Input.GetKeyDown (KeyCode.JoystickButton2))
//				Debug.Log ("Joystick 2 DN");
//			else if (Input.GetKeyDown (KeyCode.JoystickButton3))
//				Debug.Log ("Joystick 3 DN");
//			else if (Input.GetKeyDown (KeyCode.JoystickButton4))
//				Debug.Log ("Joystick 4 DN");
//			else if (Input.GetKeyDown (KeyCode.JoystickButton5))
//				Debug.Log ("Joystick 5 DN");
//			else if (Input.GetKeyDown (KeyCode.JoystickButton6))
//				Debug.Log ("Joystick 6 DN");
//			else if (Input.GetKeyDown (KeyCode.JoystickButton7))
//				Debug.Log ("Joystick 7 DN");
//			else if (Input.GetKeyDown (KeyCode.JoystickButton8))
//				Debug.Log ("Joystick 8 DN");
//	        else if (Input.GetKeyDown (KeyCode.JoystickButton9))
//				Debug.Log ("Joystick 9 DN");
//			else if (Input.GetKeyDown (KeyCode.JoystickButton10))
//				Debug.Log ("Joystick 10 DN");
//			else if (Input.GetKeyDown (KeyCode.JoystickButton11))
//				Debug.Log ("Joystick 11 DN");
//			else if (Input.GetKeyDown (KeyCode.JoystickButton12))
//				Debug.Log ("Joystick 12 DN");
//	        else if (Input.GetKeyDown (KeyCode.JoystickButton13))
//				Debug.Log ("Joystick 13 DN");
//			else if (Input.GetKeyDown (KeyCode.JoystickButton14))
//				Debug.Log ("Joystick 14 DN");
//			else if (Input.GetKeyDown (KeyCode.JoystickButton15))
//				Debug.Log ("Joystick 15 DN");
//			else if (Input.GetKeyDown (KeyCode.JoystickButton16))
//				Debug.Log ("Joystick 16 DN");
//			else if (Input.GetKeyDown (KeyCode.JoystickButton17))
//				Debug.Log ("Joystick 17 DN");
//			else if (Input.GetKeyDown (KeyCode.JoystickButton18))
//				Debug.Log ("Joystick 18 DN");
//			else if (Input.GetKeyDown (KeyCode.JoystickButton19))
//				Debug.Log ("Joystick 19 DN");
//
//			if (Input.GetKeyUp(KeyCode.JoystickButton0))
//				Debug.Log ("Joystick 0 UP");
//			else if (Input.GetKeyUp (KeyCode.JoystickButton1))
//				Debug.Log ("Joystick 1 UP");
//			else if (Input.GetKeyUp (KeyCode.JoystickButton2))
//				Debug.Log ("Joystick 2 UP");
//			else if (Input.GetKeyUp (KeyCode.JoystickButton3))
//				Debug.Log ("Joystick 3 UP");
//			else if (Input.GetKeyUp (KeyCode.JoystickButton4))
//				Debug.Log ("Joystick 4 UP");
//			else if (Input.GetKeyUp (KeyCode.JoystickButton5))
//				Debug.Log ("Joystick 5 UP");
//			else if (Input.GetKeyUp (KeyCode.JoystickButton6))
//				Debug.Log ("Joystick 6 UP");
//			else if (Input.GetKeyUp (KeyCode.JoystickButton7))
//				Debug.Log ("Joystick 7 UP");
//			else if (Input.GetKeyUp (KeyCode.JoystickButton8))
//				Debug.Log ("Joystick 8 UP");
//			else if (Input.GetKeyUp (KeyCode.JoystickButton9))
//				Debug.Log ("Joystick 9 UP");
//			else if (Input.GetKeyUp (KeyCode.JoystickButton10))
//				Debug.Log ("Joystick 10 UP");
//			else if (Input.GetKeyUp (KeyCode.JoystickButton11))
//				Debug.Log ("Joystick 11 UP");
//			else if (Input.GetKeyUp (KeyCode.JoystickButton12))
//				Debug.Log ("Joystick 12 UP");
//			else if (Input.GetKeyUp (KeyCode.JoystickButton13))
//				Debug.Log ("Joystick 13 UP");
//			else if (Input.GetKeyUp (KeyCode.JoystickButton14))
//				Debug.Log ("Joystick 14 UP");
//			else if (Input.GetKeyUp (KeyCode.JoystickButton15))
//				Debug.Log ("Joystick 15 UP");
//			else if (Input.GetKeyUp (KeyCode.JoystickButton16))
//				Debug.Log ("Joystick 16 UP");
//			else if (Input.GetKeyUp (KeyCode.JoystickButton17))
//				Debug.Log ("Joystick 17 UP");
//			else if (Input.GetKeyUp (KeyCode.JoystickButton18))
//				Debug.Log ("Joystick 18 UP");
//			else if (Input.GetKeyUp (KeyCode.JoystickButton19))
//				Debug.Log ("Joystick 19 UP");
//
			if (Input.GetKeyDown(KeyCode.Escape)) {
//				Application.Quit(); 
			}
		}
		catch (Exception e) {
		}
	}

	public void Stop () {
		Mobility (0, 0);
	}
	
	public void Forward () {
		Mobility ((float)ConfigManager.VelocityLinear*0.01f, 0);
	}
	
	public void Backward () {
		Mobility (-(float)ConfigManager.VelocityLinear*0.01f, 0);
	}
	
	public void RightTurn () {
		Mobility (0, (float)ConfigManager.VelocityAngular*0.01f);
	}
	
	public void LeftTurn () {
		Mobility (0, -(float)ConfigManager.VelocityAngular*0.01f);
	}

	public void Shoot () {
		Mobility (1f, 0);
	}

	public void JoystickMovility (Vector2 joystick) {
		if (ConfigManager.NetworkMode == 0)
			RobotManager.Instance.Mobility (joystick.y, joystick.x);
		else
			nv.RPC ("Mobility", RPCMode.Others, joystick.y, joystick.x);
	}

	public void JoystickPanTilt (Vector2 pantilt) {
		if (ConfigManager.NetworkMode == 0)
			RobotManager.Instance.PanTilt (-pantilt.x, -pantilt.y);
		else
			nv.RPC ("PanTilt", RPCMode.Others, -pantilt.x, -pantilt.y);
	}
	
	public void LeftArm (int degree) {
		RobotManager.Instance.WriteAngle (1, degree);
	}
	
	public void RightArm (int degree) {
		RobotManager.Instance.WriteAngle (2, degree);
	}
	
	[RPC]
	public void Mobility (float linear, float angular) {
		RobotManager.Instance.Mobility (linear, angular);
	}

	[RPC]
	public void PanTilt (float pan, float tilt) {
		RobotManager.Instance.PanTilt(pan, tilt);
	}

	[RPC]
	 public void PostureArm (int mot11, int mot12, int mot13, int mot21, int mot22, int mot23) {
		RobotManager.Instance.SetAngle (11, mot11);
		RobotManager.Instance.SetAngle (12, mot12);
		RobotManager.Instance.SetAngle (13, mot13);
		RobotManager.Instance.SetAngle (21, mot21);
		RobotManager.Instance.SetAngle (22, mot22);
		RobotManager.Instance.SetAngle (23, mot23);
	 }

	[RPC]
	public void SetAngle (int id, int degree) {
		RobotManager.Instance.SetAngle (id, degree);
	}

	[RPC]
	public int GetAngle (int id) {
		return RobotManager.Instance.GetAngle (id);
	}
}