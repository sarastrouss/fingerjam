using UnityEngine;
using System.Collections;

public class spin : MonoBehaviour {

	bool spinning;
	public Transform spinner;
	float spinDuration;
	float spinLength;
	float spinDecrement;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		if (spinning && spinDecrement > 0) {
			spinner.Rotate (Vector3.forward * -(spinDecrement));
			spinDecrement -= .05f;
		} else {
			spinning = false;
		}


	}

	void OnMouseDown() {
		if (!spinning) {
			spinning = true;
			spinDecrement = Random.Range (8, 15);
		}

	}
}
