using UnityEngine;
using System.Collections;

public class NetworkClient : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		Network.Connect ("192.168.53.8", 30000);
	}

	void OnConnectedServer () {
		Debug.Log ("OnConnectedServer");
	}
}
