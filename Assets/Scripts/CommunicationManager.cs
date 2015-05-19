using UnityEngine;
using System.Collections;

public class CommunicationManager : MonoBehaviour {

	CommunicationBase _comm;

	// Use this for initialization
	void Start () {
#if UNITY_Android
		_comm = new AndroidCommunication ();
#else
		_comm = new SerialCommunication ();
#endif

#if !UNITY_EDITOR
		string [] devices = _comm.GetDeviceList ();
		Debug.Log ("[CommunicationManager::Start] device count is " + devices.Length);
		foreach (string device in devices) {
			Debug.Log (device);
		}
#endif
	}

	public void Send(byte[] buff) {
		_comm.Write (buff, buff.Length);
	}

	public byte[] Recv () {
		byte[] buff = new byte[2048];
		_comm.Read (buff, 2048);
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
