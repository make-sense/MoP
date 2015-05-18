using UnityEngine;
using System.Collections;

public abstract class CommunicationBase : MonoBehaviour {
	public abstract bool Connect (string device);
	public abstract bool Disconnect ();
	public abstract bool IsConnected ();
	public abstract string [] GetDeviceList ();
	public abstract int Read (byte[] bytes, int len);
	public abstract int Write (byte[] bytes, int len);
}
