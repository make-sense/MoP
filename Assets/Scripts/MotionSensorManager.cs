using UnityEngine;
using System.Collections;

public class MotionSensorManager : MonoBehaviour {

	UcrParser _parser = new UcrParser ();
	int[] _motorAngle = {150, 150, 150, 150, 150, 150, 150, 150};
	
	public static int[] MotorIndexToID = {1, 2, 11, 12, 13, 21, 22, 23};
	public static int[] MotorIDToIndex = {-1,  0,  1, -1, -1, -1, -1, -1, -1, -1,
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
			// update sensor value
			byte[] buff = CommunicationManager.Instance.Read ();
//			Debug.Log ("[RobotManager:Update] Read " + buff.Length.ToString ());
			if (buff != null) {
				foreach (byte data in buff)
					_parser.PushByte (data);
			}
//			Debug.Log ("[RobotManager:Update] Message has " + _parser.Count.ToString ());
			while (_parser.Count > 0) {
				sProtocol protocol = _parser.Dequeue ();
//				Debug.Log ("[RobotManager:Update] Get sensor angle" + protocol.cmd.ToString () + " id " + protocol.id.ToString () + " value " + protocol.value.ToString ());
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
//						Debug.Log ("[RobotManager:Update] Get sensor angle id " + protocol.id + " value " + protocol.value);
						break;
					}
				}
			}
			yield return new WaitForSeconds(0.05f);
		}
		yield return null;
	}

	private static MotionSensorManager _instance = null;
	public static MotionSensorManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(MotionSensorManager)) as MotionSensorManager;
				if (_instance == null)
					Debug.LogError("There needs to be one active BehaviorManager script on a GameObject in your scene.");
				
			}
			return _instance;
		}
	}

}
