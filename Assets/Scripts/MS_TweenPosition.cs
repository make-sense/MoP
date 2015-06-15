using UnityEngine;
using System.Collections;

public class MS_TweenPosition : MonoBehaviour {

	public Vector3 From;
	public Vector3 To;
	public float Duration = 1f;
	COMMAND command = COMMAND.NONE;

	const float DISTANCE_THRESHOLD = 1f;
	float Speed = 10;

	enum COMMAND{
		NONE,
		GO_FROM,
		GO_TO,
	};

	// Use this for initialization
	void Start () {
		transform.localPosition = From;
		if (Duration == 0)
			Duration = 1f;
		Speed = Vector3.Distance(From, To) / Duration;
	}
	
	// Update is called once per frame
	void Update () {
		if (command == COMMAND.GO_FROM) 
		{
			if (!IsNear (transform.localPosition, From))
				transform.localPosition = Vector3.MoveTowards(transform.localPosition, From, Time.deltaTime * Speed);
			else
				command = COMMAND.NONE;
		}
		else if (command == COMMAND.GO_TO) 
		{
			if (!IsNear (transform.localPosition, To))
				transform.localPosition = Vector3.MoveTowards(transform.localPosition, To, Time.deltaTime * Speed);
			else
				command = COMMAND.NONE;
		}
	}

	public void SetFrom () {
		command = COMMAND.GO_FROM;
	}

	public void SetTo () {
		command = COMMAND.GO_TO;
	}

	public void Opposite () {
		if (IsNear (transform.localPosition, From) || command == COMMAND.GO_FROM)
			command = COMMAND.GO_TO;
		else if (IsNear (transform.localPosition, To) || command == COMMAND.GO_TO)
			command = COMMAND.GO_FROM;
	}

	bool IsNear (Vector3 from, Vector3 to) {
		Debug.Log ("IsNear: " + Vector3.Distance (from, to));
		if (Vector3.Distance (from, to) < DISTANCE_THRESHOLD)
			return true;
		return false;
	}
}
