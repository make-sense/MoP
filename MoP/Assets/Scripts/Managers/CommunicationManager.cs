﻿using UnityEngine;
using System.Collections;

public class CommunicationManager : MonoBehaviour {

	CommunicationBase _comm;

	// Use this for initialization
	void Awake () {
#if UNITY_ANDROID
		_comm = new AndroidCommunication ();
#elif UNITY_STANDALONE
		_comm = new SerialCommunication ();
#else
		throw new System.NotImplementedException("Not implemented except android and windows");
#endif

//#if !UNITY_EDITOR
//		string [] devices = _comm.GetDeviceList ();
//		Debug.Log ("[CommunicationManager::Start] device count is " + devices.Length);
//		foreach (string device in devices) {
//			Debug.Log (device);
//		}
//#endif
	}

	void Start () {
		Invoke ("AutoConnect", 1f);
	}

	void OnDestroy () {
		Disconnect ();
	}

	void AutoConnect () {
		Debug.Log ("SelectedDevice: " + ConfigManager.SelectedDevice);
		if (ConfigManager.SelectedDevice.Length > 0) 
		{
			Connect (ConfigManager.SelectedDevice);
		}
	}

	public bool Connect (string device)
	{
		Debug.Log ("[CommunicationManager:Connect] Connect to device " + device);
		if (IsConnected ())
			Disconnect ();
		return _comm.Connect (device);
	}

	public bool Disconnect () 
	{
		return _comm.Disconnect ();
	}

	public bool IsConnected ()
	{
		if (_comm != null)
			return _comm.IsConnected ();
		return false;
	}

	public string[] GetDeviceList () {
		return _comm.GetDeviceList ();
	}

	public void Write(byte[] buff) {
		if (IsConnected())
			_comm.Write (buff, buff.Length);
	}

	public byte[] Read () {
		byte[] buff = new byte[2048];
		if (IsConnected ())
			_comm.Read (ref buff, 2048);
//		Debug.Log ("[CommunicationManager:Read] nRead " + nRead.ToString ());
		return buff;
	}

	private static CommunicationManager _instance = null;
	public static CommunicationManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(CommunicationManager)) as CommunicationManager;
				if (_instance == null)
					Debug.LogError("There needs to be one active BehaviorManager script on a GameObject in your scene.");
				
			}
			return _instance;
		}
	}
}
