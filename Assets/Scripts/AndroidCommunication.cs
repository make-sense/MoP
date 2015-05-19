using UnityEngine;
using System.Collections;

public class AndroidCommunication : CommunicationBase {

	public override bool Connect (string device) {
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
		return AndroidManager.Instance.GetDevice ().ToArray ();
	}
	
	public override int Read (byte[] bytes, int len) {
		return AndroidManager.Instance.Recv (bytes, len);
	}
	
	public override int Write (byte[] bytes, int len) {
		AndroidManager.Instance.Send (bytes);
		return len;
	}
}
