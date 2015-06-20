using UnityEngine;
using System.Collections;

public class SettingManager : MonoBehaviour {

	const string Key_JoystickType = "Key_JoystickType";
	const string Key_SelectedDevice = "Key_SelectedDevice";

	public bool JoystickType {
		get {
			if (!PlayerPrefs.HasKey (Key_JoystickType))
				PlayerPrefs.SetInt (Key_JoystickType, 1);
			if (PlayerPrefs.GetInt (Key_JoystickType) == 0)
				return false;
			else
				return true;
		}
		set {
			if (value)
				PlayerPrefs.SetInt (Key_JoystickType, 1);
			else
				PlayerPrefs.SetInt (Key_JoystickType, 0);
		}
	}

	public string SelectedDevice {
		get {
			if (!PlayerPrefs.HasKey (Key_SelectedDevice))
				PlayerPrefs.SetString (Key_SelectedDevice, "");
			return PlayerPrefs.GetString (Key_SelectedDevice);
		}
		set {
			PlayerPrefs.SetString (Key_SelectedDevice, value);
		}
	}

	private static SettingManager _instance = null;
	public static SettingManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(SettingManager)) as SettingManager;
				if (_instance == null)
					Debug.LogError("There needs to be one active BehaviorManager script on a GameObject in your scene.");
				
			}
			return _instance;
		}
	}

}
