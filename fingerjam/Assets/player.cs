using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {

	public enum Fingers {thumb, index, middle, ring, pinky};
	public char[] keys;

	// Use this for initialization
	void Start () {
		keys = new char[]{'\0', '\0', '\0', '\0', '\0'};
	}
	
	// Update is called once per frame
	void Update () {
		//offset 32
		//check if input matches 65 (A) or 97 (a) 

	}

}
