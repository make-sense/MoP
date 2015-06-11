using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BTDeviceButton : MonoBehaviour {

	public void OnClick () {
		Debug.Log ("[BTDeviceButton:OnClick]");
		Text label = GetComponentInChildren<Text> ();
		CommunicationManager.Instance.Connect (label.text);
	}
}
