using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Program : MonoBehaviour {

	public UILabel[] labelSensors;
	public Animation anim;
	public AnimationClip animClip;

	// Use this for initialization
	void Start () {
		Debug.Log ("[Program:Start]");
//		Invoke ("GetBTDevices", 5f);
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0 ; i < labelSensors.Length ; i++) {
			try {
				labelSensors[i].text = RobotManager.Instance.GetSensorValue(i).ToString ();
			} catch (Exception e) {
				labelSensors[i].text = "Error";
			}
		}
	}

	public void Forward () {
		RobotManager.Instance.Move (0.7f, 0);
	}
	
	public void Backward () {
		RobotManager.Instance.Move (0, 0);
	}
	
	public void RightTurn () {
		RobotManager.Instance.Move (0, 0.9f);
	}
	
	public void LeftTurn () {
		RobotManager.Instance.Move (0, -0.9f);
	}
	
	//	void GetBTDevices () {
//		Debug.Log ("GetBTDevices ()");
//		AndroidManager.Instance.IsBluetoothDeviceNameListDone = false;
//		StartCoroutine(_SearchDeviceRoutine());
//		List<string> devices = AndroidManager.Instance.GetDevice ();
////		if (devices != null) {
////			foreach (string device in devices)
////				Debug.Log (device);
////		} else {
////			Invoke ("GetBTDevices", 5f);
////		}
//	}
//
//	private IEnumerator _SearchDeviceRoutine()
//	{
//		Debug.Log ("_SearchDeviceRoutine ()");
//		List<string> deviceList = null;
//		int count = 0;
//		
//		while(deviceList == null || deviceList.Count == 0)
//		{
//			deviceList = AndroidManager.Instance.GetDevice();
//			
//			if(count > 50)
//				break;
//			
//			yield return new WaitForSeconds(0.1f);
//			count++;
//		}
//		Debug.Log ("_SearchDeviceRoutine () - 2");
//	}

}
