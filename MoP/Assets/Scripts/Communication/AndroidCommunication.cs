﻿using UnityEngine;
using System.Collections;

public class AndroidCommunication : CommunicationBase {

	public override bool Connect (string device) {
//		Debug.Log ("[AndroidCommunication::Connect]");
		AndroidManager.Instance.ConnectDevice (device);
		return true;
	}

	public override bool Disconnect () {
		AndroidManager.Instance.DisconnectDevice ();
		return true;
	}

	public override bool IsConnected () {
		return AndroidManager.Instance.IsConnected ();
	}

	public override string[] GetDeviceList () {
//		Debug.Log ("[AndroidCommunication::GetDeviceList]");
		try {
			return AndroidManager.Instance.GetDevice ().ToArray ();
		} catch (System.Exception e) {
			return null;
		}
	}
	
	public override int Read (ref byte[] bytes, int len) {
//		Debug.Log ("[AndroidCommunication::Read]");
		return AndroidManager.Instance.Recv (ref bytes, len);
	}
	
	public override int Write (byte[] bytes, int len) {
		AndroidManager.Instance.Send (bytes);
		return len;
	}
}
