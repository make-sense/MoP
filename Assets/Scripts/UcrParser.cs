using UnityEngine;
using System.Collections;


//typedef enum {
//	START,
//	LENGTH,
//	DATA,  
//} PROTOCOL_STATE;
//
//typedef struct {
//	unsigned char cmd;
//	unsigned char id;
//	unsigned short value;
//} sProtocol;

public class UcrParser {

	#region MESSAGE_TYPE
	const int MS_RESET = 0x01;
	const int MS_COMPANY_ID = 0x02;
	const int MS_PRODUCT_ID = 0x03;
	const int MS_VERSION_MAJOR = 0x04;
	const int MS_VERSION_MINOR = 0x05;
	
	const int MS_DEVICE_DC = 0x31;
	const int MS_DEVICE_SERVO = 0x41;
	const int MS_DIGITAL_OUT = 0xa1;
	
	const int MS_SENSOR_ANGLE = 0x45;
	const int MS_SENSOR_DISTANCE = 0x93;
	const int MS_SENSOR_IR = 0x96;
	const int MS_SENSOR_BATTERY = 0xb1;
	const int MS_DIGITAL_IN = 0xd1;
	const int MS_ANALOG_IN = 0xd2;
	
	const int MS_DEVICE_INFO = 0x0a;
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
}
