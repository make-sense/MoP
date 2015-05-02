using UnityEngine;
using System.Collections;

public class CommunicationManager : MonoBehaviour {

	CommunicationBase _comm;

	// Use this for initialization
	void Start () {
#if UNITY_STANDALONE
		_comm = new SerialCommunication ();
#else
		_comm = new CommunicationBase ();
#endif
		string [] devices = _comm.GetDeviceList ();
		Debug.Log ("[CommunicationManager::Start] device count is " + devices.Length);
		foreach (string device in devices) {
			Debug.Log (device);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
