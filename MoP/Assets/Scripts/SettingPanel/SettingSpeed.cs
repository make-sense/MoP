using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class SettingSpeed : MonoBehaviour {

	public InputField linear;
	public InputField angular;

	// Use this for initialization
	void Start () {
		Refresh ();
	}

	void Refresh () {
		linear.text = ConfigManager.VelocityLinear.ToString ();
		angular.text = ConfigManager.VelocityAngular.ToString ();
	}

	public void SetSpeed () {
		if (linear.text.Length > 0)
			ConfigManager.VelocityLinear = Convert.ToInt32 (linear.text);
		if (angular.text.Length > 0)
			ConfigManager.VelocityAngular = Convert.ToInt32 (angular.text);
	}
}
