using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BTDeviceButton : MonoBehaviour {

	public void OnClick () {
		Debug.Log ("[BTDeviceButton:OnClick]");
		Text label = GetComponentInChildren<Text> ();
		CommunicationManager.Instance.Connect (label.text);
		ConfigManager.SelectedDevice = label.text;
		GameObject.Find ("ConnectedDeviceName").GetComponentInChildren<Text> ().text = label.text;
		GameObject.Find ("PanelDevice").SetActive (false);
	}
}
