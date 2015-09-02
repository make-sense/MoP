using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgramController : MonoBehaviour {

	NetworkView nv;

	public Text Mot1;
	public Text Mot2;
	public Text Mot11;
	public Text Mot12;
	public Text Mot13;
	public Text Mot21;
	public Text Mot22;
	public Text Mot23;

	public Image touch;

	Color green = new Color (26f/255f, 242f/255f, 102f/255f);
	Color red   = new Color (209f/255f, 95f/255f, 93f/255f);

	void Awake () {
		nv = GetComponent<NetworkView> ();
	}
	
	// Use this for initialization
	void Start () {
		StartCoroutine ("SyncMotion");
	}
	
	bool enableSync;
	IEnumerator SyncMotion () {
		enableSync = true;
		while (enableSync) {
			if (MotionSensorManager.Instance.Touch[0] > 0)
				SyncArmMotor ();

			DisplayMotorAngle ();
			DisplayTouch ();

			yield return new WaitForSeconds (0.1f);
		}
		yield return null;
	}

	void SyncArmMotor () {
		for (int i = 2; i < 8; i++) 
		{
			int angle = MotionSensorManager.Instance.Angle [i];
			nv.RPC ("SetAngle", RPCMode.Others, MotionSensorManager.MotorIndexToID[i], angle);
		}
	}

	void DisplayMotorAngle () {
		if (Mot1 != null)
			Mot1.text = MotionSensorManager.Instance.Angle [0].ToString ();
		if (Mot2 != null)
			Mot2.text = MotionSensorManager.Instance.Angle [1].ToString ();
		if (Mot11 != null)
			Mot11.text = MotionSensorManager.Instance.Angle [2].ToString ();
		if (Mot12 != null)
			Mot12.text = MotionSensorManager.Instance.Angle [3].ToString ();
		if (Mot13 != null)
			Mot13.text = MotionSensorManager.Instance.Angle [4].ToString ();
		if (Mot21 != null)
			Mot21.text = MotionSensorManager.Instance.Angle [5].ToString ();
		if (Mot22 != null)
			Mot22.text = MotionSensorManager.Instance.Angle [6].ToString ();
		if (Mot23 != null)
			Mot23.text = MotionSensorManager.Instance.Angle [7].ToString ();
	}

	void DisplayTouch () {
		if (touch != null) {
			if (MotionSensorManager.Instance.Touch[0] > 0)
				touch.color = red;
			else
				touch.color = green;
		}
	}

	public void JoystickMove (Vector2 joystick) {
		Debug.Log ("[ProgramController:JoystickMove] " + joystick.ToString ());
//		nv.RPC ("Move", RPCMode.Others, joystick.y, joystick.x);
	}

	public void JoystickPanTilt (Vector2 joystick) {
		Debug.Log ("[ProgramController:JoystickPanTilt] " + joystick.ToString ());
		nv.RPC ("PanTilt", RPCMode.Others, -joystick.x, -joystick.y);
	}

	public void ArrowUp () {
		Debug.Log ("[ProgramController:ArrowUp] 0.7f, 0ff");
		nv.RPC ("Move", RPCMode.Others, 0.7f, 0f);
	}
	
	public void ArrowDown () {
		Debug.Log ("[ProgramController:ArrowDown] 0f, 0.9f");
		nv.RPC ("Move", RPCMode.Others, 0f, 0f);
	}
	
	public void ArrowLeft () {
		Debug.Log ("[ProgramController:ArrowLeft] 0f, -0.9f");
		nv.RPC ("Move", RPCMode.Others, 0f, -0.9f);
	}
	
	public void ArrowRight () {
		Debug.Log ("[ProgramController:ArrowRight] 0f, 0.9f");
		nv.RPC ("Move", RPCMode.Others, 0f, 0.9f);
	}

	public void SetAngle () {
//		UILabel labelID = UISlider.current.transform.FindChild ("LabelID").GetComponentInChildren<UILabel> ();
//		UILabel labelValue = UISlider.current.transform.FindChild ("LabelValue").GetComponentInChildren<UILabel> ();
//		labelValue.text = ((int)(UISlider.current.value * 300)).ToString () + " deg";
//		Debug.Log ("[ProgramController:SetAngle] ID: " + labelID.text + ", value: " + labelValue.text);
//		pv.RPC ("SetAngle", PhotonTargets.All, System.Convert.ToInt32(labelID.text), (int)(UISlider.current.value * 300));
	}

	public void ChangeToPlayMode () {
		Application.LoadLevel ("MoP");
	}
}
