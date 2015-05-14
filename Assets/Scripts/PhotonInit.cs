using UnityEngine;
using System.Collections;

public class PhotonInit : MonoBehaviour {

	public string version = "v1.0";

	void Awake () {
		Debug.Log ("[PhotonInit:Awake]");
		PhotonNetwork.ConnectUsingSettings (version);
	}

	void OnJoinedLobby () {
		Debug.Log ("Entered Lobby!");
		PhotonNetwork.JoinRandomRoom ();
	}

	void OnPhotonRandomJoinFailed () {
		Debug.Log ("No Rooms!");
		PhotonNetwork.CreateRoom ("MopRoom", true, true, 5);
	}

	void OnJoinedRoom () {
		Debug.Log ("Enter Room!");
	}

	void OnGUI () {
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());
	}
}
