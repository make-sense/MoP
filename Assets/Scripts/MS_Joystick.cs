using UnityEngine;
using System.Collections;

public class MS_Joystick : MonoBehaviour {

	public UISprite Thumb;
	public GameObject Target;
	public string Action;

	int WidthHalf;
	int HeightHalf;

	public float DEAD_ZONE = 0.5f;

	// Use this for initialization
	void Start () {
		Thumb.gameObject.SetActive (false);
		UISprite sprite = GetComponentInChildren<UISprite> ();
		WidthHalf = sprite.width/2;
		HeightHalf = sprite.height/2;
	}
	
	void OnPress (bool pressed) {
//		Debug.Log ("OnPress " + pressed);
		TouchProcess ();
		Thumb.gameObject.SetActive (pressed);
	}

	void OnDrag () {
		TouchProcess ();
	}

	void TouchProcess () {
		Vector2 touchPosition = TouchedLocalPosition ();
		Thumb.transform.localPosition = new Vector3 (touchPosition.x*WidthHalf, touchPosition.y*HeightHalf, 0f);
		Target.SendMessage (Action, touchPosition);
	}

	Vector2 TouchedLocalPosition () {
		Vector3 v3 = transform.InverseTransformPoint (UICamera.lastHit.point);
		Vector2 result = new Vector2 (v3.x / WidthHalf, v3.y / HeightHalf);
		if (Mathf.Abs(result.x) < DEAD_ZONE)
			result.x = 0.0f;
		if (Mathf.Abs(result.y) < DEAD_ZONE)
			result.y = 0.0f;
//		Debug.Log ("TouchedLocalPosition: " + result);
		return result;
	}
}
