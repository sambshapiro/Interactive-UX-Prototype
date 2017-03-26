using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (restart ());
	}

	private IEnumerator restart() {
		SceneManager.UnloadSceneAsync ("NIO");
		yield return new WaitForSeconds (2f);
		SceneManager.LoadScene ("NIO");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
