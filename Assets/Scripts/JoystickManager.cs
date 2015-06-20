using UnityEngine;
using System.Collections;

public class JoystickManager : MonoBehaviour {

	public GameObject MopMobility;
	public GameObject MopPanTilt;
	public GameObject BoxyMobility;
	public GameObject BoxyFunction;

	public UnityEngine.UI.Text mode;
		
	void Start () {
		EnableMop (SettingManager.Instance.JoystickType);
	}

	public void ToggleJoystick () {
		SettingManager.Instance.JoystickType = !SettingManager.Instance.JoystickType;
		EnableMop (SettingManager.Instance.JoystickType);
	}

	public void SelectMop () {
		EnableMop (true);
	}

	public void SelectBoxy () {
		EnableMop (false);
	}

	public void EnableMop (bool mopEnable) {
		if (mopEnable)
			mode.text = "M";
		else
			mode.text = "B";
		MopMobility.SetActive (mopEnable);
		MopPanTilt.SetActive (mopEnable);
		BoxyMobility.SetActive (!mopEnable);
		BoxyFunction.SetActive (!mopEnable);
	}
}
