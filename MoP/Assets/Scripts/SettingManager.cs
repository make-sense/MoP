using UnityEngine;
using System.Collections;

public class SettingManager : MonoBehaviour {

	public GameObject SettingPanel;

	void Start () {
		SettingPanel.SetActive (false);
	}

	public void PanelToggle () {
		SettingPanel.SetActive (!SettingPanel.activeSelf);
	}
	public void PanelHide () {
		SettingPanel.SetActive (false);
	}
}
