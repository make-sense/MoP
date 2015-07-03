using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Program : MonoBehaviour {

	public Animation anim;
	public AnimationClip animClip;

	public float linearVelocity = 0.7f;
	public float angularVelocity = 0.5f;

	// Use this for initialization
	void Start () {
		Debug.Log ("[Program:Start]");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{
			Application.Quit(); 
		}
	}

	public void Stop () {
		RobotManager.Instance.Mobility (0, 0);
	}
	
	public void Forward () {
		RobotManager.Instance.Mobility (linearVelocity, 0);
	}
	
	public void Backward () {
		RobotManager.Instance.Mobility (-linearVelocity, 0);
	}
	
	public void RightTurn () {
		RobotManager.Instance.Mobility (0, angularVelocity);
	}
	
	public void LeftTurn () {
		RobotManager.Instance.Mobility (0, -angularVelocity);
	}

	public void Shoot () {
		RobotManager.Instance.Mobility (1f, 0);
	}

	public void JoystickMovility (Vector2 joystick) {
		RobotManager.Instance.Mobility (joystick.y*0.7f, joystick.x*0.5f);
	}
	

}
