using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class BTDeviceManager : MonoBehaviour {

	public Transform devicePrefab;
	List<Transform> deviceGOs = new List<Transform> ();

	public Image btImage;
	public Sprite BTDisconnected;
	public Sprite BTConnecting;
	public Sprite BTConnected;

	void Start () {
		StartCoroutine ("CheckConnection");
	}

	IEnumerator CheckConnection () {
		while (true) {
			if (CommunicationManager.Instance.IsConnected ())
				btImage.sprite = BTConnected;
			else {
//				if (AndroidManager.Instance.BluetoothConnectingState == 2)
//					btImage.sprite = BTConnecting;
//				else if (AndroidManager.Instance.BluetoothConnectingState == 1)
//					btImage.sprite = BTDisconnected;
			}
			yield return new WaitForSeconds(1f);
		}
		yield return null;
	}

	bool show;
	public void Show () {
		StartCoroutine ("ShowDevice");
	}

	IEnumerator ShowDevice () {
		show = !show;

		if (show)
		{
			if (deviceGOs.Count > 0) {
				foreach (Transform t in deviceGOs)
					GameObject.Destroy (t.gameObject);
				deviceGOs.Clear ();
			}

			int height = 250;
			string[] devices = null;
#if UNITY_ANDROID && UNITY_EDITOR
			devices = new string[] {"This", "is", "device", "test"};
#endif
			DateTime startTime = DateTime.Now;
			while (devices == null) {
				devices = CommunicationManager.Instance.GetDeviceList ();
				yield return new WaitForSeconds(0.1f);
				if (DateTime.Now.Subtract(startTime).TotalSeconds > 3)
					break;
			}
			Debug.Log ("[BTDeviceManager:ShowDevice] Found device: " + devices.Length);
			foreach (string device in devices) {
				Transform t = Instantiate (devicePrefab) as Transform;
				t.SetParent (this.transform);
				t.localPosition = new Vector2(0, height);
				t.localScale = new Vector3(1, 1, 1);
				Text text = t.GetComponentInChildren<Text> ();
				text.text = device;
				height -= 90;
				deviceGOs.Add (t);
				Debug.Log ("[BTDeviceManager:ShowDevice] " + device);
			}
		}
	}
}
