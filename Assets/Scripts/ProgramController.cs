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
		pv.RPC ("Move", PhotonTargets.All, 0.7f, 0f);
	}
	
	public void ArrowDown () {
		pv.RPC ("Move", PhotonTargets.All, 0f, 0f);
	}
	
	public void ArrowLeft () {
		pv.RPC ("Move", PhotonTargets.All, 0f, -0.9f);
	}
	
	public void ArrowRight () {
		pv.RPC ("Move", PhotonTargets.All, 0f, 0.9f);
	}

	public void SetAngle () {
		UILabel labelID = UISlider.current.transform.FindChild ("LabelID").GetComponentInChildren<UILabel> ();
		UILabel labelValue = UISlider.current.transform.FindChild ("LabelValue").GetComponentInChildren<UILabel> ();
		labelValue.text = ((int)(UISlider.current.value * 300)).ToString () + " deg";
		Debug.Log ("[ProgramController:SetAngle] ID: " + labelID.text + ", value: " + labelValue.text);
		pv.RPC ("SetAngle", PhotonTargets.All, 1, UISlider.current.value * 300);
	}
}
