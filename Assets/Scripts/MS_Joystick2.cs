using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MS_Joystick2 : MonoBehaviour {

	public Image Thumb;
	public GameObject Target;
	public string Action;
	public bool SetZeroWhenRelease;
	bool enableTouchProcess;

	int WidthHalf;
	int HeightHalf;

	public float DEAD_ZONE = 0.2f;

	// Use this for initialization
	void Start () {
		Thumb.gameObject.SetActive (false);
		Image image = GetComponentInChildren<Image> ();
		WidthHalf = (int)image.rectTransform.rect.width/2;
		HeightHalf = (int)image.rectTransform.rect.height/2;
	}
	
	public void OnPress () {
		Debug.Log ("OnPress");
		Thumb.gameObject.SetActive (true);
		enableTouchProcess = true;
		StartCoroutine ("TargetAction");
	}

	public void OnRelease () {
		Debug.Log ("OnRelease");
		Thumb.gameObject.SetActive (false);
		enableTouchProcess = false;
	}

	IEnumerator TargetAction () {
		while (enableTouchProcess) {
			TouchProcess ();
			yield return new WaitForSeconds(0.1f);
		}
		if (SetZeroWhenRelease)
			Target.SendMessage (Action, new Vector2 (0f, 0f));
		yield return null;
	}

	void TouchProcess () {
		Debug.Log ("TouchProcess");
		Debug.Log (Input.mousePosition);
//		Vector2 touchPosition = TouchedLocalPosition ();
//		Thumb.transform.localPosition = new Vector3 (touchPosition.x*WidthHalf, touchPosition.y*HeightHalf, 0f);
//		Target.SendMessage (Action, touchPosition);
	}

	Vector2 TouchedLocalPosition () {
//		Vector3 v3 = transform.InverseTransformPoint (UICamera.lastHit.point);
//		Vector2 result = new Vector2 (v3.x / WidthHalf, v3.y / HeightHalf);
//		if (Mathf.Abs(result.x) < DEAD_ZONE)
//			result.x = 0.0f;
//		if (Mathf.Abs(result.y) < DEAD_ZONE)
//			result.y = 0.0f;
//		Debug.Log ("TouchedLocalPosition: " + result);
//		return result;
		return new Vector3();
	}
}
