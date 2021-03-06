﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JoystickManager : MonoBehaviour {

	public GameObject MopMobility;
	public GameObject MopPanTilt;
	public GameObject BoxyMobility;
	public GameObject BoxyFunction;

	public Slider slider;

	void Start () {
		slider.value = ConfigManager.JoystickType;
	}

	public void ToggleJoystick () {
		ConfigManager.JoystickType = (int)slider.value;
		SetJoystick (ConfigManager.JoystickType);
	}

	public void SetJoystick (int joystickMode) {
		Text mode = slider.GetComponentInChildren <Text> ();
		if (joystickMode == 0)
			mode.text = "M";
		else
			mode.text = "B";
		bool mopEnable = (joystickMode==0);
		MopMobility.SetActive (mopEnable);
		MopPanTilt.SetActive (mopEnable);
		BoxyMobility.SetActive (!mopEnable);
		BoxyFunction.SetActive (!mopEnable);
	}
}
