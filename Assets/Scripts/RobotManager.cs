using UnityEngine;
using System.Collections;

public class RobotManager : MonoBehaviour {

	UcrParser _parser = new UcrParser ();
	int[] sensorAngle = new int[8];

	// Use this for initialization
	void Start () {
		StartCoroutine ("UpdateSensor");	
	}
	
	void Update () {
		// update sensor value
		byte[] buff = CommunicationManager.Instance.Recv ();
		if (buff != null) {
			foreach (byte data in buff)
				_parser.PushByte (data);
		}
		if (_parser.Count > 0) {
			sProtocol protocol = _parser.Dequeue ();
			switch (protocol.cmd) {
				case UcrParser.MS_SENSOR_ANGLE:
				{
					if (protocol.id == 11)
						sensorAngle[0] = protocol.value;
					break;
				}
			}
		}
	}

	[RPC]
	public void Move (float linear, float angular) {
		Debug.Log ("[RobotManager:Move] : (" + linear.ToString () + ", " + angular.ToString () + ")"); 
		float velocityLeft = angular - linear;
		float velocityRight = angular + linear;
		CommunicationManager.Instance.Send (UcrParser.GetBuffDcSpeed (51, (int)(velocityLeft*100)));
		CommunicationManager.Instance.Send (UcrParser.GetBuffDcSpeed (52, (int)(velocityRight*100)));
	}

	public void Test () {
		byte[] buff1 = UcrParser.GetBuffDcSpeed(1, 100);
		for (int i = 0; i < buff1.Length; i++) {
			_parser.PushByte(buff1[i]);
		}
		byte[] buff2 = UcrParser.GetBuffDcSpeed(2, 100);
		for (int i = 0; i < buff2.Length; i++) {
			_parser.PushByte(buff2[i]);
		}
		Debug.Log ("[RobotManager:Test] Found message " + _parser.Count.ToString ());
	}
}
