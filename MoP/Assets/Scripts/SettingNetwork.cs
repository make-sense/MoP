using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Net;
using System.Net.Sockets;

public class SettingNetwork : MonoBehaviour {

	public Text mode;
	public Text textOpenConnect;
	public Text ipBase;
	public Text ipTarget;

	// Use this for initialization
	void Start () {
		ModeDisplay ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OpenConnect () {
		if (ConfigManager.NetworkMode == 0) {
			// Server Mode
			NetworkServer.Open ();
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
