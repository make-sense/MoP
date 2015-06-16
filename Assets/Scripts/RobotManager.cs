using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class RobotManager : MonoBehaviour {

	UcrParser _parser = new UcrParser ();
	int[] _motorAngle = new int[8];

	int[] MotorIndexToID = {1, 2, 11, 12, 13, 21, 22, 23};
	int[] MotorIDToIndex = {-1,  0,  1, -1, -1, -1, -1, -1, -1, -1,
	                        -1,  2,  3,  4, -1, -1, -1, -1, -1, -1,
                            -1,  5,  6,  7, -1, -1, -1, -1, -1, -1};

	int[] _angleMin = { 60, 100,  60, 150, 150, 110,  90,  90};
	int[] _angleMax = {240, 180, 190, 220, 220, 240, 150, 150};

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
#if UNITY_ANDROID
#if UNITY_EDITOR
		return;
#endif
#endif
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
//					Debug.Log ("[RobotManager:Update] Get sensor angle id " + protocol.id + " value " + protocol.value);
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

	DateTime lastMove;
	[RPC]
	public void Move (float linear, float angular) {
		linear *= 0.7f;
		angular *= 0.5f;
		Debug.Log ("[RobotManager:Move] : (" + linear.ToString () + ", " + angular.ToString () + ")"); 
		float velocityLeft = angular + linear;
		float velocityRight = angular - linear;
		CommunicationManager.Instance.Write (UcrParser.GetBuffDcSpeed (51, (int)(velocityLeft*100)));
		CommunicationManager.Instance.Write (UcrParser.GetBuffDcSpeed (52, (int)(velocityRight*100)));
		CancelInvoke ("StopMobility");
		Invoke ("StopMobility", 1f);
		lastMove = DateTime.Now;
	}

	void StopMobility () {
		if (DateTime.Now.Subtract(lastMove).TotalMilliseconds > 1000) {
			Debug.Log ("Automatically Stop");
			Move (0, 0);
		}
	}
	
	public void JoystickMove (Vector2 joystick) {
		Move (joystick.y, joystick.x);
	}

	[RPC]
	public void PanTilt (float pan, float tilt) {
		Debug.Log ("[RobotManager:PanTilt] : (" + pan.ToString () + ", " + tilt.ToString () + ")"); 
		CommunicationManager.Instance.Write (UcrParser.GetBuffMotorAngle (1, (int)(pan*90)+150));
		CommunicationManager.Instance.Write (UcrParser.GetBuffMotorAngle (2, (int)(tilt*40)+150));
	}

	public void JoystickPanTilt (Vector2 pantilt) {
		PanTilt (-pantilt.x, -pantilt.y);
	}

	[RPC]
	public void SetAngle (int id, int degree) {
		Debug.Log ("[RobotManager:SetAngle] : (" + id + ", " + degree + ")");
		if (MotorIDToIndex [id] != -1) {
			if (degree < _angleMin[MotorIDToIndex[id]])
				degree = _angleMin[MotorIDToIndex[id]];
			else if (degree > _angleMax[MotorIDToIndex[id]])
				degree = _angleMax[MotorIDToIndex[id]];
			CommunicationManager.Instance.Write (UcrParser.GetBuffMotorAngle (id, degree));
		}
	}

	[RPC]
	public int GetAngle (int id) {
		Debug.Log ("[RobotManager:GetAngle] : (" + id + ")");
		return _motorAngle[MotorIDToIndex[id]];
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
