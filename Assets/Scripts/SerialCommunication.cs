using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO.Ports;

public class SerialCommunication : CommunicationBase {

	public SerialPort serialPort;

	public override bool Connect (string device) {
		return false;
	}
	
	public override bool Disconnect () {
		return false;
	}
	
	public override bool IsConnected () {
		return false;
	}

	public override string[] GetDeviceList () {
		return SerialPort.GetPortNames ();
	}

	public override int Read (out byte[] bytes, int len) {
		bytes = new byte[len];
		return 0;
	}

	public override int Write (byte[] bytes, int len) {
		return 0;
	}
}
