using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingRobotConnection : MonoBehaviour {

	public Image state;

	void OnEnable () {
		StartCoroutine ("CheckState");
	}
	
	void OnDisable () {
		_run = false;
	}
	
	bool _run;
	IEnumerator CheckState () {
		_run = true;
		while (_run) {
			if (CommunicationManager.Instance.IsConnected ())
				state.color = MS_Color.blue;
			else
				state.color = MS_Color.white;
			yield return new WaitForSeconds(0.5f);
		}
		yield return null;
	}
}
