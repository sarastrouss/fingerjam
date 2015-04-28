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
	public GameObject respinButton;
	public player player;
	public player otherPlayer;
	public UnityEngine.UI.Text timer;
	public UnityEngine.UI.Text errorText;
	public UnityEngine.UI.Text description;
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
			Debug.Log (alpha);

			// add to "current" array
			alph.Add (alpha);


			if(gameObject.tag == "player1" && gameObject.activeSelf) {
				if(otherPlayer.keys.Contains(alpha)) {
					errorText.text ="Your friend is on that letter, try again!";
					this.gameObject.SetActive (false);
					respinButton.SetActive (true);
					spun = false;
				} else {
					for(int i=0; i < Input.inputString.Length; i++) {
						/*Debug.Log (alpha);
						Debug.Log (Input.inputString);*/
						if((char) Input.inputString.ToCharArray(i, 1)[i] == alpha) {
							//Debug.Log ("added p1" + (char) Input.inputString.ToCharArray(i, 1)[0]);
							
							p1keys.Add((char) Input.inputString.ToCharArray(i, 1)[0]);
						}
						
					}
				}
				
				
			}
			/**
			 * If the gameObject tag is equal to player 2 and that tag is currently active and the other
			 * hashset does not already contain this key, add the key
			 * 
			 */ 
			//Only adds a key if it equals the one that is spun
			if(gameObject.tag == "player2" && gameObject.activeSelf && !p1keys.Contains(alpha)) {
				//if the other hashset already contains alpha, spin again
				if(otherPlayer.keys.Contains(alpha)) {
					errorText.text ="Your friend is on that letter, try again!";
					this.gameObject.SetActive (false);
					respinButton.SetActive (true);
					spun = false;
				} else {
					for(int i=0; i < Input.inputString.Length; i++) {
						/*Debug.Log (alpha);
						Debug.Log (Input.inputString);*/
						if((char) Input.inputString.ToCharArray(i, 1)[i] == alpha) {
							Debug.Log ("added p2" + (char) Input.inputString.ToCharArray(i, 1)[0]);
							p2keys.Add((char) Input.inputString.ToCharArray(i, 1)[0]);
						}
						
					}
				}
			}

			if (gameObject.activeSelf) {
				if (finger == (int) Fingers.Middle) {
					player.keys[(int) player.Fingers.middle] = alpha;
				} else if (finger == (int) Fingers.Ring) {
					player.keys[(int) player.Fingers.ring] = alpha;
				} else if (finger == (int) Fingers.Pinky) {
					player.keys[(int) player.Fingers.pinky] = alpha;
				} else if (finger == (int) Fingers.Thumb) {
					player.keys[(int) player.Fingers.thumb] = alpha;
				} else {
					player.keys[(int) player.Fingers.index] = alpha;
				}
			}


			/**
			 * If the gameObject tag is equal to player 1 and that tag is currently active and the other
			 * hashset does not already contain this key, add the key
			 * 
			 */ 
			//only adds a key if that player is currently spinning
			
			if (timeLeft > 0 && countdown && gameObject.activeSelf) {
				countdown = false;
				setText(finger, alpha);
				StartCoroutine(showTimer(1));

			}
		}

		/**
		 * This block works by removing the letter from a particular player's hashset 
		 * Currently works in each spin state, but fails in between spins.
		 */
		// remove key if finger isn't pressing key, only works in spin state for some reason
		for (int i = 97; i <= 122; i++) {
			if (Input.GetKeyUp ((char)i + "")) {
				//works here in between spins
				Debug.Log("Lifted " + (char) i);
				p1keys.Remove((char)i);
				p2keys.Remove ((char)i);
				if (player.keys.Contains((char) i) || otherPlayer.keys.Contains ((char) i)) {
					StartCoroutine(playerLose(1));
				}
				//doesn't enter this block in between spins
				/*if(p1keys.RemoveWhere (x => x == (char)i) == 1) {
					//switch to player 1 lose screen here
					Debug.Log ("LOSER 1 UR A LOSER. Removed " + (char) i );
				} 
				if(p2keys.RemoveWhere (x => x == (char)i) == 1) {
					// switch to player 2 lose screen here
					Debug.Log ("LOSER 2 UR A LOSER. Removed " + (char) i );

				}*/
			}
		}
	

	}
	
	// finds what finger the arrow is pointing to
	int getFingerSpin(float angle) {
		if (angle >= 327 || angle <= 33) {
			return (int) Fingers.Middle;
		} else if (angle > 33 && angle <= 180) {
			return (int) Fingers.Pinky;
		}  else {
			return (int) Fingers.Thumb;
		}
	}

	// finds what letter the arrow is pointing to -- only catches lower case letters as that is what is returned by input
	char getLetterSpin(float angle) {
		// A = 65 = 0
		// B = 66 = 1
		// C = 67 = 2
		float r = angle / (13.8f);
		return (char) (int) (97 + r);
	}


	void OnMouseDown() {
		if (!spinning) {
			spinning = true;
			spun = true;
			spinDecrementFinger = Random.Range (8, 15);
			spinDecrementLetter = Random.Range (8, 15);
			errorText.text = "";
			GetComponent<AudioSource>().Play();

		}
	}

	IEnumerator showTimer(float time) {
		timer.text = ":0" + timeLeft;
		yield return new WaitForSeconds (time);
		timeLeft--;
		countdown = true;
		if (timeLeft == 0) {
			checkForLoserCurrentPlayer();
			this.gameObject.SetActive (false);
			otherPlayerButton.SetActive (true);
			timer.text = "";
			description.text = "";
			spun = false;
			timeLeft = 5;
		}
	}

	IEnumerator playerLose(float time) {
		yield return new WaitForSeconds (time);
		checkForLoserOtherPlayer ();
		checkForLoserCurrentPlayer ();
	}

	void checkForLoserCurrentPlayer() {
		Debug.Log ("here");
		if (gameObject.tag == "player1") {
			Debug.Log ("player1");
			for (int i = 0; i < 5; i++) {
				if (player.keys[i] != '\0' && !p1keys.Contains(player.keys[i])) {
					Debug.Log ("Loser");
					Application.LoadLevel ("p2win");
				}
			}
		}
		if (gameObject.tag == "player2") {
			Debug.Log ("player2");
			for (int i = 0; i < 5; i++) {
				if (player.keys[i] != '\0' && !p2keys.Contains(player.keys[i])) {
					Application.LoadLevel ("p1win");
				}
	
			}
		}
	}

	void checkForLoserOtherPlayer() {
		Debug.Log ("here");
		if (gameObject.tag == "player1") {
			Debug.Log ("player1");
			for (int i = 0; i < 5; i++) {
				if (otherPlayer.keys[i] != '\0' && !p2keys.Contains(otherPlayer.keys[i])) {
					Debug.Log ("Loser");
					Application.LoadLevel ("p1win");
				}
			}
		}
		if (gameObject.tag == "player2") {
			Debug.Log ("player2");
			for (int i = 0; i < 5; i++) {
				if (otherPlayer.keys[i] != '\0' && !p1keys.Contains(otherPlayer.keys[i])) {
					Application.LoadLevel ("p2win");
				}
				
			}
		}
	}

	void setText(int finger, char letter) {
		string digit = "";
		string character;

		if (finger == (int) Fingers.Index) {
			digit = "Pointer";
		}
		if (finger == (int) Fingers.Middle) {
			digit = "Middle";
		}
		if (finger == (int) Fingers.Pinky) {
			digit = "Pinky";
		}
		if (finger == (int) Fingers.Ring) {
			digit = "Ring";
		}
		if (finger == (int) Fingers.Thumb) {
			digit = "Thumb";	
		}

		character = letter.ToString();

		description.text = digit + " on " + character.ToUpper();

	}

}
