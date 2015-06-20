using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Program : MonoBehaviour {

	public Animation anim;
	public AnimationClip animClip;

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
		RobotManager.Instance.Move (1f, 0);
	}
	
	public void Backward () {
		RobotManager.Instance.Move (-1f, 0);
	}
	
	public void RightTurn () {
		RobotManager.Instance.Move (0, 1f);
	}
	
	public void LeftTurn () {
		RobotManager.Instance.Move (0, -1f);
	}
}
