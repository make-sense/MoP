using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	const int PORT = 30000;

	static bool _opened;
	public static bool Opened {
		get {
			return _opened;
		}
	}

	static bool _connected;
	public static bool Connected {
		get {
			return _connected;
		}
	}

	public static void Open () {
		Debug.Log ("NetworkInitializeServer");
		Disconnect ();
		Network.InitializeServer (1, PORT, false);
	}

	public static NetworkConnectionError Connect (string ip) {
		Disconnect ();
		return Network.Connect (ip, PORT);
	}

	public static void Disconnect () {
		Network.Disconnect ();
		MasterServer.UnregisterHost ();
		_opened = false;
		_connected = false;
	}

	void OnConnectedToServer () {
		Debug.Log ("OnConnectedToServer");
		_connected = true;
	}
	
	void OnDisconnectedFromServer () {
		Debug.Log ("OnDisconnectedFromServer");
		_connected = false;
	}

	void OnFailedToConnect () {
		Debug.Log ("OnFailedToConnect");
		_connected = false;
	}

	void OnServerInitialized () {
		Debug.Log ("OnServerInitialized");
		_opened = true;
	}
	
	void OnPlayerConnected (NetworkPlayer player) {
		Debug.Log ("OnPlayerConnected");
	}
}
