using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingJoystick : MonoBehaviour {

	public GameObject[] MopJoysticks;
	public GameObject[] BoxyJoysticks;

	public Slider slider;

	// Use this for initialization
	void Start () {
		slider.value = ConfigManager.JoystickType;
	}
	
	public void Toggle () {
		ConfigManager.JoystickType = (int)slider.value;
		SetJoystick (ConfigManager.JoystickType);
	}
	
	public void SetJoystick (int joystickMode) {
		Text mode = slider.GetComponentInChildren <Text> ();
		bool mopEnable = (joystickMode==0);
		foreach (GameObject go in MopJoysticks)
			go.SetActive (mopEnable);
		foreach (GameObject go in BoxyJoysticks)
			go.SetActive (!mopEnable);
	}
}
