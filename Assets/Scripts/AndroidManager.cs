using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AndroidManager : MonoBehaviour {

	AndroidJavaObject _activity;

	void Awake () {
		AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		_activity = jc.GetStatic<AndroidJavaObject>("currentActivity");
		if (_activity == null)
			Debug.Log ("AndroidActivity is null");
	}

	// Use this for initialization
	void Start () {
//		_activity.Call ("HelloFunction");
	}
	
	// Update is called once per frame
	int count;
	void Update () {
		count++;
		_activity.Call("Echo", count.ToString ());
	}

	void Echo (string message) {
		Debug.Log ("get echo message: " + message);
	}

	private List<string> _bluetoothDeviceNameList = new List<string>();
	private List<byte> _recvBuffer = new List<byte>();

	private bool _isBluetoothConnected;
	private int _receivedData = -1;
	private bool _isReceivedMessage;
	//------------------------------------------
	
	#region RobotCommunicator Interface Methods
	public bool IsBluetoothDeviceNameListDone
	{
		get;
		set;
	}
	
	public List<string> GetDevice()
	{
		if (IsBluetoothDeviceNameListDone == false)
		{
			_SearchDevice();
			
			return null;
		}
		else
		{
			return _bluetoothDeviceNameList;
		}
	}
	
	public bool IsConnected()
	{
		return _isBluetoothConnected;
	}
	
	public void ConnectDevice(string name)
	{
		_ConnectBluetooth(name);
	}
	
	public void DisconnectDevice()
	{
		_DisconnectBluetooth();
	}
	
	private String ByteArrayToHexString(byte[] bytes)
	{
		return BitConverter.ToString(bytes).Replace("-", "");
	}
	
	static byte[] HexStringToByteArray(string hexString)
	{
		if (hexString == null)
			throw new ArgumentNullException("hexString");
		
		if ((hexString.Length & 1) != 0)
			throw new ArgumentOutOfRangeException("hexString", hexString, "hexString must contain an even number of characters.");
		
		byte[] result = new byte[hexString.Length/2];
		for (int i = 0; i < hexString.Length; i += 2)
			result[i/2] = byte.Parse(hexString.Substring(i, 2), System.Globalization.NumberStyles.HexNumber);
		
		return result;
	}
	
	public void Send(byte[] buff)
	{
		_SendBTMessage(ByteArrayToHexString(buff));
	}
	
	public int Recv(byte[] buff, int len)
	{
		int count = _recvBuffer.Count;
		int copyLength = (len < count) ? len : count;
		if (count > 1) {
			Buffer.BlockCopy(_recvBuffer.ToArray(), 0, buff, 0, copyLength);
			_recvBuffer.RemoveRange(0, copyLength);
		}
		return copyLength;
	}
	
	public int GetLength()
	{
		return _recvBuffer.Count;
	}
	#endregion

	#region Event Methods
	void BluetoothDevice(string deviceName)
	{
		Debug.Log ("[AndroidManager::BluetoothDevice] : " + deviceName);
		if (deviceName == "END")
		{
			IsBluetoothDeviceNameListDone = true;
			return;
		}
		
		_bluetoothDeviceNameList.Add(deviceName);
	}
	
	void BluetoothConnectState(string signal)
	{
	}
	
	void BluetoothData(string readData)
	{
		byte[] recvBuffer = HexStringToByteArray(readData);
		for (int i = 0 ; i < readData.Length/2 ; i++)
			_recvBuffer.Add(recvBuffer[i]);
		Debug.Log("[AndroidManager::BluetoothData] " + readData);
	}
	#endregion

	#region Log Methods
	void BTMessage(string logMessage)
	{
		Debug.Log("BT Log : " + logMessage);
		if (logMessage == "STATE_CONNECTED")
			_isBluetoothConnected = true;
		else if (logMessage == "STATE_NOTCONNECTED")
			_isBluetoothConnected = false;
	}
	
	void TWMessage(string logMessage)
	{
		Debug.Log("TW Log : " + logMessage);
	}
	
	void SPMessage(string logMessage)
	{
		Debug.Log("SP Log : " + logMessage);
	}
	#endregion


	#region Bluetooth private Methods
	private void _SendBTMessage(String message)
	{
		_activity.Call("SendMessage", message);
	}
	
	private int _ReceiveBTMessage()
	{
		_isReceivedMessage = false;
		return _receivedData;
	}
	
	private void _SearchDevice()
	{
		_bluetoothDeviceNameList.Clear();
		IsBluetoothDeviceNameListDone = false;
		_activity.Call("SearchBluetoothDevice");
	}
	
	private void _ConnectBluetooth(string deviceName)
	{
		_activity.Call("ConnectDevice", deviceName);
	}
	
	private void _DisconnectBluetooth()
	{
		#if UNITY_EDITOR
		#else
		_activity.Call("DisconnectBluetooth");
		#endif
		_isBluetoothConnected = false;
	}
	#endregion

	private static AndroidManager _instance = null;
	public static AndroidManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(AndroidManager)) as AndroidManager;
				if (_instance == null)
					Debug.LogError("There needs to be one active BehaviorManager script on a GameObject in your scene.");
				
			}
			return _instance;
		}
	}
}
