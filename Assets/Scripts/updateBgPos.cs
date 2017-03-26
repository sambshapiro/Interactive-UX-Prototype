using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updateBgPos : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void updateFromValue(float val) {
		GetComponent<RectTransform> ().localPosition = new Vector3 (
			GetComponent<RectTransform> ().localPosition.x,
			val,
			GetComponent<RectTransform> ().localPosition.z);
	}
}
