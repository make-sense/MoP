using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BTDeviceManager : MonoBehaviour {

	public Transform devicePrefab;
		
	void ConnectDevice () {
		UILabel label = UIButton.current.GetComponentInChildren<UILabel> ();
		Debug.Log ("[BTDeviceManager:ConnectDevice] Connect device: " + label.text);
#if !UNITY_EDITOR
		AndroidManager.Instance.ConnectDevice (label.text);
#endif
	}

	public void Show () {
		StartCoroutine ("ShowDevice");
	}

	IEnumerator ShowDevice () {
		int height = 0;
		List<string> devices = null;
#if UNITY_EDITOR
		devices = new List<string> (new string[] {"This", "is", "device", "test"});
#endif
		while (devices == null) {
			devices = AndroidManager.Instance.GetDevice ();
			yield return new WaitForSeconds(0.1f);
		}
		Debug.Log ("[BTDeviceManager:ShowDevice] Found device: " + devices.Count.ToString ());
		foreach (string device in devices) {
			Transform t = Instantiate (devicePrefab) as Transform;
			t.parent = this.transform;
			t.localPosition = new Vector2(0, height);
			t.localScale = new Vector3(1, 1, 1);
			UILabel label = t.GetComponentInChildren<UILabel> ();
			label.text = device;
			height -= 80;
			UIButton button = t.GetComponentInChildren<UIButton> ();	
			EventDelegate.Set (button.onClick, ConnectDevice);
//			button.onClick += ConnectDevice;
			Debug.Log ("[BTDeviceManager:ShowDevice] " + device);
		}
	}
}
