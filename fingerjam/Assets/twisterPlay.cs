﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class twisterPlay : MonoBehaviour {
	List<string> alph = new List<string>();
	List<string> digits = new List<string> ();
	public Transform spinnerAlph;
	public Transform spinnerDig;
	public Transform spinner;
	public GameObject sound;
	Quaternion rotation;

	// Use this for initialization
	void Start () {
		rotation = spinner.rotation;
		// Adding values to alph
		alph.Add("A");
		alph.Add("B");
		alph.Add("C");
		alph.Add("D");
		alph.Add("E");
		alph.Add("F");
		alph.Add("G");
		alph.Add("H");
		alph.Add("I");
		alph.Add("J");
		alph.Add("K");
		alph.Add("L");
		alph.Add("M");
		alph.Add("N");
		alph.Add("O");
		alph.Add("P");
		alph.Add("Q");
		alph.Add("R");
		alph.Add("S");
		alph.Add("T");
		alph.Add("U");
		alph.Add("V");
		alph.Add("W");
		alph.Add("X");
		alph.Add("Y");
		alph.Add("Z");
		//Adding digits
		digits.Add ("Pinky finger");
		digits.Add ("Pointer finger");
		digits.Add ("Middle finger");
		digits.Add ("Ring finger");
		digits.Add ("Thumb");
	}
	float angle;
	// Update is called once per frame
	void Update () {
//		if (spinner.rotation != rotation) {
//			//angle = Quaternion.Angle(spinner.rotation, rotation);
//			Debug.Log (rotation);
//		}

	}
	

	void playClip() {
		// play particular audio clip
	}
}
