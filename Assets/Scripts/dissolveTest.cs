using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RenderHeads.Media.AVProVideo;

public class dissolveTest : MonoBehaviour {

	public Canvas canvas1;
	public Canvas canvas2;

	public GameObject video1;
	public GameObject video2;

	// Use this for initialization
	void Start () {
		//StartCoroutine (dissolve());
		//cutOn(video2);
		//cutOn (video1);
		//cutOn(video1);
		//cutOn (video2);
	}

	private IEnumerator dissolve() {
		yield return new WaitForSeconds (2f);
		Color col = video1.GetComponent<DisplayUGUI> ().material.GetColor ("_Color");
		col.a = .5f;
		video1.GetComponent<DisplayUGUI> ().material.SetColor ("_Color", col);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void cutOff(GameObject g) {
		Color col = g.GetComponent<DisplayUGUI> ().material.GetColor ("_Color");
		Debug.Log (g + "color was " + col);
		col.a = 0f;
		g.GetComponent<DisplayUGUI> ().material.SetColor ("_Color", col);
		Debug.Log (g + "color changed to " + g.GetComponent<DisplayUGUI> ().material.GetColor ("_Color"));
	}

	private void cutOn(GameObject g) {
		Color col = g.GetComponent<DisplayUGUI> ().material.GetColor ("_Color");
		col.a = 1f;
		g.GetComponent<DisplayUGUI> ().material.SetColor ("_Color", col);
	}
}
