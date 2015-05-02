using UnityEngine;
using System;
using System.Collections;
using System.IO.Ports;

public class SerialCommunication : CommunicationBase {

	public SerialPort serialPort;

	public override string[] GetDeviceList () {
		return SerialPort.GetPortNames ();
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
