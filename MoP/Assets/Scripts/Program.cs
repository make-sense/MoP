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
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit(); 
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
			RobotManager.Instance.Mobility (joystick.y*0.7f, joystick.x*0.5f);
		else
			nv.RPC ("Mobility", RPCMode.Others, joystick.y*0.7f, joystick.x*0f);
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