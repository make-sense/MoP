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
//		Invoke ("GetBTDevices", 5f);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{
			Application.Quit(); 
		}
	}

	public void Stop () {
		RobotManager.Instance.Move (0, 0);
	}
	
	public void Forward () {
		RobotManager.Instance.Move (linearVelocity, 0);
	}
	
	public void Backward () {
		RobotManager.Instance.Move (-linearVelocity, 0);
	}
	
	public void RightTurn () {
		RobotManager.Instance.Move (0, angularVelocity);
	}
	
	public void LeftTurn () {
		RobotManager.Instance.Move (0, -angularVelocity);
	}

	public void Shoot () {
		RobotManager.Instance.Move (1f, 0);
	}
}
