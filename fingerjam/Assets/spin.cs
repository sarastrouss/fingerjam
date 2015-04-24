using UnityEngine;
using System.Collections;

public class spin : MonoBehaviour {

	bool spinning, spun, countdown;
	public Transform FingerSpinner;
	public Transform LetterSpinner;
	float spinDuration;
	float spinLength;
	float spinDecrementFinger, spinDecrementLetter;
	enum Fingers {Middle, Ring, Pinky, Thumb, Index};
	public GameObject otherPlayerButton;
	public player player;
	public UnityEngine.UI.Text timer;
	public int timeLeft;

	// Use this for initialization
	void Start () {
		spinning = false;
		spun = false;
		timeLeft = 5;
		countdown = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (spinning && (spinDecrementFinger > 0 || spinDecrementLetter > 0)) {
			if (spinDecrementFinger > 0) {
				FingerSpinner.Rotate (Vector3.forward * -(spinDecrementFinger));
				spinDecrementFinger -= .05f;
			}
			if (spinDecrementLetter > 0) {				
				LetterSpinner.Rotate (Vector3.forward * -(spinDecrementLetter));
				spinDecrementLetter -= .05f;
			}
		} else if (!spun) {
			spinning = false;
		} else {		
			// detecting things
			int finger = getFingerSpin(FingerSpinner.rotation.eulerAngles.z); 
			//Debug.Log(finger);
			char alpha = getLetterSpin(360-LetterSpinner.rotation.eulerAngles.z);

			if (finger == (int) Fingers.Middle) {
				player.middle = alpha;
			} else if (finger == (int) Fingers.Ring) {
				player.ring = alpha;
			} else if (finger == (int) Fingers.Pinky) {
				player.pinky = alpha;
			} else if (finger == (int) Fingers.Thumb) {
				player.thumb = alpha;
			} else {
				player.index = alpha;
			}
			if (timeLeft > 0 && countdown) {
				countdown = false;
				StartCoroutine(showTimer(1));
			}

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
		float r = angle / (13.8f);
		/*Debug.Log ((char)(int)(65 + r));
		Debug.Log (r);*/

		return (char) (int) (65 + r);
	}


	void OnMouseDown() {
		if (!spinning) {
			spinning = true;
			spun = true;
			spinDecrementFinger = Random.Range (8, 15);
			spinDecrementLetter = Random.Range (8, 15);

		}

	}

	IEnumerator showTimer(float time) {
		timer.text = ":0" + timeLeft;
		yield return new WaitForSeconds (time);
		timeLeft--;
		countdown = true;
		if (timeLeft == 0) {
			this.gameObject.SetActive (false);
			otherPlayerButton.SetActive (true);
			timer.text = "";
			spun = false;
			timeLeft = 5;
		}
	}

}
