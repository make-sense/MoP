using UnityEngine;
using System.Collections;

public class NetworkServer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Network.InitializeServer (1, 30000, false);
		Debug.Log ("NetworkInitializeServer");
	}
	
	void OnPlayerConnected (NetworkPlayer player) {
		Debug.Log ("OnPlayerConnected");
	}
}
