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
		Debug.Log ("[ProgramController:ArrowUp] 0.7f, 0ff");
		pv.RPC ("Move", PhotonTargets.All, 0.7f, 0f);
	}
	
	public void ArrowDown () {
		Debug.Log ("[ProgramController:ArrowDown] 0f, 0.9f");
		pv.RPC ("Move", PhotonTargets.All, 0f, 0f);
	}
	
	public void ArrowLeft () {
		Debug.Log ("[ProgramController:ArrowLeft] 0f, -0.9f");
		pv.RPC ("Move", PhotonTargets.All, 0f, -0.9f);
	}
	
	public void ArrowRight () {
		Debug.Log ("[ProgramController:ArrowRight] 0f, 0.9f");
		pv.RPC ("Move", PhotonTargets.All, 0f, 0.9f);
	}

	public void SetAngle () {
		UILabel labelID = UISlider.current.transform.FindChild ("LabelID").GetComponentInChildren<UILabel> ();
		UILabel labelValue = UISlider.current.transform.FindChild ("LabelValue").GetComponentInChildren<UILabel> ();
		labelValue.text = ((int)(UISlider.current.value * 300)).ToString () + " deg";
		Debug.Log ("[ProgramController:SetAngle] ID: " + labelID.text + ", value: " + labelValue.text);
		pv.RPC ("SetAngle", PhotonTargets.All, System.Convert.ToInt32(labelID.text), (int)(UISlider.current.value * 300));
	}
}
