using UnityEngine;
using System.Collections;

public class spin : MonoBehaviour {

	bool spinning;
	public Transform spinner;
	public Transform alphaSpinner;
	float spinDuration;
	float spinLength;
	float spinDecrement;
	enum Fingers {Middle, Ring, Pinky, Thumb, Index};

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
			// detecting things
			int finger = getFingerSpin(spinner.rotation.eulerAngles.z); 
			//Debug.Log(finger);
			char alpha = getLetterSpin(360-alphaSpinner.rotation.eulerAngles.z);
		}
	}


	int getFingerSpin(float angle) {
		if (angle < 0 && angle < 34) {
			return (int) Fingers.Middle;
		} else if (angle > 324 && angle < 360) {
			return (int) Fingers.Middle;
		} else if (angle > 36 && angle < 108) {
			return (int) Fingers.Ring;
		} else if (angle > 108 && angle < 180) {
			return (int) Fingers.Pinky;
		} else if (angle > 180 && angle < 252) {
			return (int) Fingers.Thumb;
		} else {
			return (int) Fingers.Index;
		}
	}

	char getLetterSpin(float angle) {
		// A = 65 = 0
		// B = 66 = 1
		// C = 67 = 2
		float r = angle / (360/26);
		Debug.Log ((char)(int)(65 + r));
		Debug.Log (r);

		return (char) (int) (65 + r);
	}


	void OnMouseDown() {
		if (!spinning) {
			spinning = true;
			spinDecrement = Random.Range (8, 15);
		}

	}
}
