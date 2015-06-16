using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class MS_Joystick : MonoBehaviour , IPointerUpHandler , IPointerDownHandler , IDragHandler {

    public int MovementRange = 100;

    public enum AxisOption
    {                                                    // Options for which axes to use                                                     
        Both,                                                                   // Use both
        OnlyHorizontal,                                                         // Only horizontal
        OnlyVertical                                                            // Only vertical
    }

    AxisOption axesToUse = AxisOption.Both;   // The options for the axes that the still will use
    string horizontalAxisName = "Horizontal";// The name given to the horizontal axis for the cross platform input
    string verticalAxisName = "Vertical";    // The name given to the vertical axis for the cross platform input 

	public GameObject Target;
	public string Action;
	public bool SetZeroWhenRelease;
	bool enableTouchProcess;

    private Vector3 startPos;
	private Vector3 deltaPos;
    private bool useX;                                                          // Toggle for using the x axis
    private bool useY;                                                          // Toggle for using the Y axis

	Vector3 FixPos = new Vector3 ();

    void OnEnable () {
		FixPos = transform.localPosition;
		startPos = transform.position;
        CreateVirtualAxes ();
    }

    private void UpdateVirtualAxes (Vector3 value) {

		deltaPos = startPos - value;
		deltaPos.x = -deltaPos.x;
		deltaPos.y = -deltaPos.y;
		deltaPos /= MovementRange;
    }

    private void CreateVirtualAxes()
    {
        // set axes to use
        useX = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyHorizontal);
        useY = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyVertical);
    }


    public  void OnDrag(PointerEventData data) {

        Vector3 newPos = Vector3.zero;

        if (useX) {
            int delta = (int) (data.position.x - startPos.x);
            delta = Mathf.Clamp(delta,  - MovementRange,  MovementRange);
            newPos.x = delta;
        }

        if (useY)
        {
            int delta = (int)(data.position.y - startPos.y);
            delta = Mathf.Clamp(delta, -MovementRange,  MovementRange);
            newPos.y = delta;
        }
        transform.position = new Vector3(startPos.x + newPos.x , startPos.y + newPos.y , startPos.z + newPos.z);
        UpdateVirtualAxes (transform.position);
    }


    public  void OnPointerUp(PointerEventData data)
    {
//		transform.position = FixPos;
		transform.localPosition = FixPos;
		UpdateVirtualAxes (FixPos);
		enableTouchProcess = false;
    }


    public  void OnPointerDown (PointerEventData data) {
		startPos = data.position;
		deltaPos = Vector3.zero;
		enableTouchProcess = true;
		StartCoroutine ("TargetAction");
    }

	IEnumerator TargetAction () {
		while (enableTouchProcess) {
			Target.SendMessage (Action, new Vector2(deltaPos.x, deltaPos.y));
			yield return new WaitForSeconds(0.1f);
		}
		if (SetZeroWhenRelease)
			Target.SendMessage (Action, new Vector2 (0f, 0f));
		yield return null;
	}

    void OnDisable () {
    }
}
