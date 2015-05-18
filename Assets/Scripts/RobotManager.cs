using UnityEngine;
using System.Collections;

public class RobotManager : MonoBehaviour {

	CommunicationBase _communicate;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// update sensors value
	}

	[RPC]
	public void Move (float linear, float angular) {
		Debug.Log ("[RobotManager:Move] : (" + linear.ToString () + ", " + angular.ToString () + ")"); 
		float velocityLeft = angular - linear;
		float velocityRight = angular + linear;
		AndroidManager.Instance.Send (UcrParser.GetBuffDcSpeed (51, (int)(velocityLeft*100)));
		AndroidManager.Instance.Send (UcrParser.GetBuffDcSpeed (52, (int)(velocityRight*100)));
	}
}
