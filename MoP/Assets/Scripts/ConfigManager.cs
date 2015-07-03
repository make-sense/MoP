using UnityEngine;
using System.Collections;

public class ConfigManager : MonoBehaviour {

	static string Key_JoystickType = "Key_JoystickType";
	static string Key_SelectedDevice = "Key_SelectedDevice";
	static string Key_NetworkMode = "Key_NetworkMode";
	static string Key_IPAddr = "Key_IPAddr";

	public static int JoystickType {
		get {
			if (!PlayerPrefs.HasKey (Key_JoystickType))
				PlayerPrefs.SetInt (Key_JoystickType, 0);
			return PlayerPrefs.GetInt (Key_JoystickType);
		}
		set {
			PlayerPrefs.SetInt (Key_JoystickType, value);
		}
	}
	
	public static string SelectedDevice {
		get {
			if (!PlayerPrefs.HasKey (Key_SelectedDevice))
				PlayerPrefs.SetString (Key_SelectedDevice, "");
			return PlayerPrefs.GetString (Key_SelectedDevice);
		}
		set {
			PlayerPrefs.SetString (Key_SelectedDevice, value);
		}
	}	
	
	public static int NetworkMode {
		get {
			if (!PlayerPrefs.HasKey (Key_NetworkMode))
				PlayerPrefs.SetInt (Key_NetworkMode, 0);
			return PlayerPrefs.GetInt (Key_NetworkMode);
		}
		set {
			PlayerPrefs.SetInt (Key_NetworkMode, value);
		}
	}
	
	public static string IpAddr {
		get {
			if (!PlayerPrefs.HasKey (Key_IPAddr))
				PlayerPrefs.SetString (Key_IPAddr, "192.168.0.1");
			return PlayerPrefs.GetString (Key_IPAddr);
		}
		set {
			PlayerPrefs.SetString (Key_IPAddr, value);
		}
	}
}