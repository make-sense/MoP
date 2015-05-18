using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Program : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log ("[Program:Start]");
//		Invoke ("GetBTDevices", 5f);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Forward () {
		byte[] buff1 = new byte[] {0xaa, 0x05, 0x31, 0x33, 0x32, 0x00, 0x6a};
		AndroidManager.Instance.Send(buff1);
		byte[] buff2 = new byte[] {0xaa, 0x05, 0x31, 0x34, 0x32, 0x00, 0x69};
		AndroidManager.Instance.Send(buff2);
	}
	
	public void Backward () {
		byte[] buff1 = new byte[] {0xaa, 0x05, 0x31, 0x33, 0x00, 0x00, 0x9c};
		AndroidManager.Instance.Send(buff1);
		byte[] buff2 = new byte[] {0xaa, 0x05, 0x31, 0x34, 0x00, 0x00, 0x9b};
		AndroidManager.Instance.Send(buff2);
	}
	
	public void RightTurn () {
		
	}
	
	public void LeftTurn () {
		
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
