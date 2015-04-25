using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


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
	List<char> alph;
	HashSet<char> p1keys = new HashSet<char> ();
	HashSet<char> p2keys = new HashSet<char>();
	bool p1;
	bool p2;
	
	// Use this for initialization
	void Start () {
		spinning = false;
		spun = false;
		timeLeft = 5;
		countdown = true;
		alph = new List<char>();
		p1 = true;
		p2 = false;
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
			// add to "current" array
			alph.Add (alpha);
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
			//want to check time
			if (timeLeft < 0 ) {
				Debug.Log ("Lose");

			}
			if(gameObject.tag == "player1" && gameObject.activeSelf) {
				for(int i=0; i < Input.inputString.Length; i++) {
					Debug.Log (alpha);
					Debug.Log (Input.inputString);
					if((char) Input.inputString.ToCharArray(i, 1)[i] == alpha) {
						Debug.Log ("added p1");
						p1keys.Add((char) Input.inputString.ToCharArray(i, 1)[0]);
					}
					
				}


			}
			//Only adds a key if it equals the one that is spun

			if(gameObject.tag == "player2" && gameObject.activeSelf) {
				for(int i=0; i < Input.inputString.Length; i++) {
					Debug.Log (alpha);
					Debug.Log (Input.inputString);
					if((char) Input.inputString.ToCharArray(i, 1)[i] == alpha) {
						Debug.Log ("added p2");
						p2keys.Add((char) Input.inputString.ToCharArray(i, 1)[0]);
					}
					
				}
				p1 = true;
			}
		}

		if (gameObject.tag == "player1" && gameObject.activeSelf) {
			// remove key if finger isn't pressing key
			for (int i = 97; i <= 122; i++) {
				if (Input.GetKeyUp ((char)i + "")) {
					p1keys.RemoveWhere (x => x == (char)i);
					Debug.Log ("LOSER 1 UR A LOSER");
				}
			}
		}

		// remove key if finger isn't pressing key
		for (int i = 97; i <= 122; i++) {
			if (Input.GetKeyUp ((char)i + ""))  {
				p2keys.RemoveWhere (x => x == (char)i);
				Debug.Log ("LOSER 2 UR A LOSER");
			}
		}

	}
	



	//Every second: add input string to a set
	//I

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

		return (char) (int) (97 + r);
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
