using UnityEngine;
using System.Collections;

public class spin : MonoBehaviour {

	bool spinning;
	float spinDuration;
	float spinLength;
	float spinDecrement;

	// Use this for initialization
	void Start () {
		spinning = true;
		spinDecrement = Random.Range (8, 15);
	}
	
	// Update is called once per frame
	void Update () {
		if (spinning && spinDecrement > 0) {
			transform.Rotate (Vector3.forward * -(spinDecrement));
			spinDecrement -= .05f;
		}
	
	}
}
