using UnityEngine;
using System.Collections;

public abstract class CommunicationBase : MonoBehaviour {
	public abstract string [] GetDeviceList ();
	public abstract int Read (byte[] bytes, int len);
	public abstract int Write (byte[] bytes, int len);
}
