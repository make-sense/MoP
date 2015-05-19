using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Program : MonoBehaviour {

	RobotManager robot = new RobotManager ();

	// Use this for initialization
	void Start () {
		Debug.Log ("[Program:Start]");
//		Invoke ("GetBTDevices", 5f);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Forward () {
		robot.Move (1f, 0);
	}
	
	public void Backward () {
		robot.Move (0, 0);
	}
	
	public void RightTurn () {
	}
	
	public void LeftTurn () {
		robot.Test ();
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
