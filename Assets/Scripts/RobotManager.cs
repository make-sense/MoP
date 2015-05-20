using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RobotManager : MonoBehaviour {

	UcrParser _parser = new UcrParser ();
//	List<int> _motorAngle = new List<int> (new int[8]);
	int[] _motorAngle = new int[8];

	public int GetSensorValue(int id) {
		if (0 <= id && id < 8) {
//			Debug.Log ("[RobotManager:GetSensorValue] id: " + id + ", value: " + _motorAngle [id]);
			return _motorAngle [id];
		} else {
			return -1;
		}
	}

	// Use this for initialization
	void Start () {
		StartCoroutine ("UpdateSensor");
	}

	int count;
	IEnumerator UpdateSensor () {
		while (true) {
//			string strSensor = "";
//			foreach (int angle in _motorAngle) {
//				strSensor += angle.ToString () + " ";
//			}
//			Debug.Log ("CurrentSensor4 " + strSensor);
			yield return new WaitForSeconds(1f);
		}
		yield return null;
	}
	
	void Update () {
//		string strSensor = "";
//		foreach (int angle in _motorAngle) {
//			strSensor += angle.ToString () + " ";
//		}
//		Debug.Log ("CurrentSensor2 " + strSensor);

		// update sensor value
		byte[] buff = CommunicationManager.Instance.Read ();
//		Debug.Log ("[RobotManager:Update] Read " + buff.Length.ToString ());
		if (buff != null) {
			foreach (byte data in buff)
				_parser.PushByte (data);
		}
//		Debug.Log ("[RobotManager:Update] Message has " + _parser.Count.ToString ());
		while (_parser.Count > 0) {
			sProtocol protocol = _parser.Dequeue ();
//			Debug.Log ("[RobotManager:Update] Get sensor angle" + protocol.cmd.ToString () + " id " + protocol.id.ToString () + " value " + protocol.value.ToString ());
			switch (protocol.cmd) {
				case UcrParser.MS_SENSOR_ANGLE:
				{
					if (protocol.id == 1)
						_motorAngle[0] = protocol.value;
					else if (protocol.id == 2) 
						_motorAngle[1] = protocol.value;
					else if (protocol.id == 11) 
						_motorAngle[2] = protocol.value;
					else if (protocol.id == 12) 
						_motorAngle[3] = protocol.value;
					else if (protocol.id == 13) 
						_motorAngle[4] = protocol.value;
					else if (protocol.id == 21) 
						_motorAngle[5] = protocol.value;
					else if (protocol.id == 22) 
						_motorAngle[6] = protocol.value;
					else if (protocol.id == 23) 
						_motorAngle[7] = protocol.value;
					Debug.Log ("[RobotManager:Update] Get sensor angle id " + protocol.id + " value " + protocol.value);
					break;
				}
			}
		}
//		string strSensor = "";
//		foreach (int angle in _motorAngle) {
//			strSensor += angle.ToString () + " ";
//		}
//		Debug.Log ("CurrentSensor " + strSensor);
	}

	[RPC]
	public void Move (float linear, float angular) {
		Debug.Log ("[RobotManager:Move] : (" + linear.ToString () + ", " + angular.ToString () + ")"); 
		float velocityLeft = angular + linear;
		float velocityRight = angular - linear;
		CommunicationManager.Instance.Write (UcrParser.GetBuffDcSpeed (51, (int)(velocityLeft*100)));
		CommunicationManager.Instance.Write (UcrParser.GetBuffDcSpeed (52, (int)(velocityRight*100)));
	}

	public void Test () {
		string strSensor = "";
		foreach (int angle in _motorAngle) {
			strSensor += angle.ToString () + " ";
		}
		Debug.Log ("CurrentSensor3 " + strSensor);

//		byte[] buff1 = UcrParser.GetBuffDcSpeed(1, 100);
//		for (int i = 0; i < buff1.Length; i++) {
//			_parser.PushByte(buff1[i]);
//		}
//		byte[] buff2 = UcrParser.GetBuffDcSpeed(2, 100);
//		for (int i = 0; i < buff2.Length; i++) {
//			_parser.PushByte(buff2[i]);
//		}
//		Debug.Log ("[RobotManager:Test] Found message " + _parser.Count.ToString ());
	}

	private static RobotManager _instance = null;
	public static RobotManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(RobotManager)) as RobotManager;
				if (_instance == null)
					Debug.LogError("There needs to be one active BehaviorManager script on a GameObject in your scene.");
				
			}
			return _instance;
		}
	}

}
