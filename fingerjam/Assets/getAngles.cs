using UnityEngine;
using System.Collections;

public class getAngles : MonoBehaviour {
	public Transform spinner;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (spinner.rotation.eulerAngles.z);
		// detecting things
		if (spinner.rotation.eulerAngles.z < 0 && spinner.rotation.eulerAngles.z < 72) {
			Debug.Log ("middle finger");
		} else if (spinner.rotation.eulerAngles.z > 72) {
			Debug.Log ("not it");
		}
	}
}
