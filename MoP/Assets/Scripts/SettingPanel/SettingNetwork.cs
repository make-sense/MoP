using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Net;
using System.Net.Sockets;

public class SettingNetwork : MonoBehaviour {

	public Image state;
	public Text mode;
	public Text textOpenConnect;
	public Text ipBase;
	public Text ipTarget;

	// Use this for initialization
	void Start () {
		ModeDisplay ();
	}

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
			if (ConfigManager.NetworkMode == 0) {
				if (NetworkManager.Opened)
					state.color = MS_Color.blue;
				else
					state.color = MS_Color.white;
			}
			else {
				if (NetworkManager.Connected)
					state.color = MS_Color.blue;
				else
					state.color = MS_Color.white;
			}
			yield return new WaitForSeconds(0.5f);
		}
		yield return null;
	}

	public void OpenConnect () {
		if (ConfigManager.NetworkMode == 0) {
			// Server Mode
			NetworkManager.Open ();
		} else {
			// Client Mode
			if (ipTarget.text.Length > 0)
				ipBase.text = ipTarget.text;
			ConfigManager.IpAddr = ipBase.text;
		}
	}

	public void ModeToggle () {
		ConfigManager.NetworkMode = (ConfigManager.NetworkMode==0)? 1: 0;
		ModeDisplay ();
	}

	public void ModeDisplay () {
		if (ConfigManager.NetworkMode == 0) {
			mode.text = "Server";
			textOpenConnect.text = "Open";
			ipBase.text = myIp ();
		} else {
			mode.text = "Client";
			textOpenConnect.text = "Connect";
			ipBase.text = ConfigManager.IpAddr;
		}
	}

	string myIp () {
		IPHostEntry host;
		string localIP = "?";
		host = Dns.GetHostEntry(Dns.GetHostName());
		foreach (IPAddress ip in host.AddressList)
		{
			if (ip.AddressFamily == AddressFamily.InterNetwork)
			{
				localIP = ip.ToString();
			}
		}
		return localIP;
	}
}
