using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class ConnectionDevices : MonoBehaviour {

	public Transform devicePrefab;
	public Transform deviceRoot;
	public Text connectButtonText;
	List<Transform> deviceGOs = new List<Transform> ();

	public Image state;

	const int HEIGHT_STEP = 70;

	void Start () {
		StartCoroutine ("CheckConnection");
		connectButtonText.text = ConfigManager.SelectedDevice;
	}

	bool isConnectedPrev = false;
	IEnumerator CheckConnection () {
		while (true) {
			if (CommunicationManager.Instance.IsConnected ()) {
				state.color = MS_Color.blue;
				if (!isConnectedPrev) {
					RobotManager.Instance.PanTilt (0f, 0f);
				}
			}
			else
			{
//				if (AndroidManager.Instance.BluetoothConnectingState == 2)
//					btImage.sprite = BTConnecting;
//				else if (AndroidManager.Instance.BluetoothConnectingState == 1)
				state.color = MS_Color.white;
			}
			isConnectedPrev = CommunicationManager.Instance.IsConnected ();
			yield return new WaitForSeconds(1f);
		}
		yield return null;
	}
	
	bool show;
	public void Show () {
		gameObject.SetActive (true);
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
			
			int height = 40;
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
			if (devices.Length > 0) {
//				GetComponentInChildren <ScrollRect> ().content.sizeDelta = new Vector2 (380, devices.Length*HEIGHT_STEP+20);
				GetComponentInChildren <RectTransform> ().sizeDelta = new Vector2 (380, devices.Length*HEIGHT_STEP+20);
			}

			foreach (string device in devices) {
				Transform t = Instantiate (devicePrefab) as Transform;
				t.SetParent (deviceRoot.transform);
				t.localPosition = new Vector2(0, height);
				t.localScale = new Vector3(1, 1, 1);
				Text text = t.GetComponentInChildren<Text> ();
				text.text = device;
				height -= HEIGHT_STEP;
				deviceGOs.Add (t);
				Debug.Log ("[BTDeviceManager:ShowDevice] " + device);
			}
		}
	}}
