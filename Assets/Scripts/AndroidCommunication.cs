using UnityEngine;
using System.Collections;

public class AndroidCommunication : CommunicationBase {

	public override string[] GetDeviceList () {
		return null;
	}
	
	public override int Read (byte[] bytes, int len) {
		return 0;
	}
	
	public override int Write (byte[] bytes, int len) {
		return 0;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
