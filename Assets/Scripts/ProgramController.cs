using UnityEngine;
using System.Collections;

public class ProgramController : MonoBehaviour {

	PhotonView pv;

	void Awake () {
		pv = GetComponent<PhotonView> ();
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ArrowUp () {
		pv.RPC ("Move", PhotonTargets.All, 1f, 0f);
	}

	public void ArrowDown () {
		pv.RPC ("Move", PhotonTargets.All, 0f, 0f);
	}
}
