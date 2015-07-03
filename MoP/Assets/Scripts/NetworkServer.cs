using UnityEngine;
using System.Collections;

public class NetworkServer : MonoBehaviour {

	public static void Open () {
		Debug.Log ("NetworkInitializeServer");
		if (Network.isClient) {
			Network.Disconnect ();
			MasterServer.UnregisterHost ();
		}
		Network.InitializeServer (1, 30000, false);
	}

	void OnServerInitialized () {
		Debug.Log ("OnServerInitialized");
	}

	void OnPlayerConnected (NetworkPlayer player) {
		Debug.Log ("OnPlayerConnected");
	}
}
