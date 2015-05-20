using UnityEngine;
using System;
using System.Collections.Generic;

public class SerialCommunication : CommunicationBase {
#if UNITY_STANDALONE
	public System.IO.Ports.SerialPort serialPort;
#endif
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
		#if UNITY_STANDALONE
		return System.IO.Ports.SerialPort.GetPortNames ();
		#else
		return null;
		#endif
	}

	public override int Read (out byte[] bytes, int len) {
		bytes = new byte[len];
		return 0;
	}

	public override int Write (byte[] bytes, int len) {
		return 0;
	}
}
