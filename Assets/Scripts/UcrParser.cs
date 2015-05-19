using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sProtocol {
	public byte cmd;
	public byte id;
	public int value;
}

public class UcrParser {

	enum PROTOCOL_STATE{
		START,
		LENGTH,
		DATA,
	};

	#region MESSAGE_TYPE
	public const int MS_RESET = 0x01;
	public const int MS_COMPANY_ID = 0x02;
	public const int MS_PRODUCT_ID = 0x03;
	public const int MS_VERSION_MAJOR = 0x04;
	public const int MS_VERSION_MINOR = 0x05;
	
	public const int MS_DEVICE_DC = 0x31;
	public const int MS_DEVICE_SERVO = 0x41;
	public const int MS_DIGITAL_OUT = 0xa1;
	
	public const int MS_SENSOR_ANGLE = 0x45;
	public const int MS_SENSOR_DISTANCE = 0x93;
	public const int MS_SENSOR_IR = 0x96;
	public const int MS_SENSOR_BATTERY = 0xb1;
	public const int MS_DIGITAL_IN = 0xd1;
	public const int MS_ANALOG_IN = 0xd2;
	
	public const int MS_DEVICE_INFO = 0x0a;

	#endregion

	public static byte[] GetBuffDcSpeed(int id, int speed) {
		if (speed > 100)
			speed = 100;
		return GetMessage (MS_DEVICE_DC, id, speed);
	}

	public static byte[] GetBuffMotorAngle(int id, int angle) {
		if (angle > 360)
			angle = 360;
		return GetMessage (MS_DEVICE_SERVO, id, angle);
	}

	public void PushByte (byte data) {
		_update (data);
	}

	public int Count {
		get {
			return _protocol.Count;
		}
	}

	public sProtocol Dequeue () {
		if (_protocol.Count > 0) {
			sProtocol protocol = _protocol[0];
			_protocol.Remove(protocol);
			return protocol;
		}
		return null;
	}

	static byte[] GetMessage (int type, int id, int value) {
		byte[] _send_buff = new byte[7];
		_send_buff[0] = 0xaa;
		_send_buff[1] = 0x05;
		_send_buff[2] = (byte)type;
		_send_buff[3] = (byte)id;
		_send_buff[4] = (byte)(value&0x00ff);
		_send_buff[5] = (byte)(value>>8 & 0x00ff);
		_send_buff[6] = GetChecksum(_send_buff);
		return _send_buff;
	}

	static byte GetChecksum (byte[] buff) {
		byte checksum = 0;
		for (int i = 2 ; i < buff[1]+1 ; i++)
			checksum += buff[i];
		return (byte) (0-checksum);
	}

	PROTOCOL_STATE _state;
	byte[] _buff = new byte[7];
	byte _buff_cnt;
	byte _buff_len;

	List<sProtocol> _protocol = new List<sProtocol> (64);

	public void _update (byte inChar) {
		switch (_state) 
		{
		case PROTOCOL_STATE.START :  // Ready to Start
		{
			if (inChar == 0xAA) 
				_state = PROTOCOL_STATE.LENGTH;
			break;
		}
			
		case PROTOCOL_STATE.LENGTH :
		{
			_buff_len = inChar;
			_buff_cnt = 0;
			_state = PROTOCOL_STATE.DATA;
			break;
		}
			
		case PROTOCOL_STATE.DATA :
		{
			_buff[_buff_cnt] = inChar;
			_buff_cnt++;
			if (_buff_cnt >= _buff_len) 
			{
				// Checksum
				int sum = 0;
				for (int i = 0 ; i < _buff_len ; i++)
					sum += _buff[i];
				
				if ((sum&0xff) == 0) 
				{
					sProtocol protocol = new sProtocol ();
					protocol.cmd = _buff[0];
					protocol.id = _buff[1];
					if (_buff_len==4) 
					{
						protocol.value = _buff[2];
					}
					else if (_buff_len==5) 
					{
						protocol.value = _buff[3];
						protocol.value = protocol.value<<8;
						protocol.value += _buff[2];
					}
					_protocol.Add(protocol);
				}
				_state = PROTOCOL_STATE.START;
			}
			break;
		}
			
		default :
		{
			_state = PROTOCOL_STATE.START;
			break;
		}
		}	
	}
}
