using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Program : MonoBehaviour {

	public Animation anim;
	public AnimationClip animClip;

	public float linearVelocity = 0.7f;
	public float angularVelocity = 0.5f;

	NetworkView nv;

	// Use this for initialization
	void Start () {
		Debug.Log ("[Program:Start]");
		nv = GetComponentInChildren<NetworkView> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{
			Application.Quit(); 
		}
	}

	public void Stop () {
		Mobility (0, 0);
	}
	
	public void Forward () {
		Mobility (linearVelocity, 0);
	}
	
	public void Backward () {
		Mobility (-linearVelocity, 0);
	}
	
	public void RightTurn () {
		Mobility (0, angularVelocity);
	}
	
	public void LeftTurn () {
		Mobility (0, -angularVelocity);
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

	[RPC]
	public void Mobility (float linear, float angular) {
		RobotManager.Instance.Mobility (linear, angular);
	}
}