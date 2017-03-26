using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteIterator : MonoBehaviour {

	private GameObject introOutro;
	private SpriteRenderer ScreenMeshIntroOutro;
	private Sprite[] introTextures;
	private int currentFrame;
	private WaitForSeconds m_FrameRateWait;
	[SerializeField] private float m_FrameRate;

	void Awake() {
		currentFrame = 0;
		m_FrameRateWait = new WaitForSeconds (1f / m_FrameRate);

		introOutro = GameObject.Find ("introOutro");
		ScreenMeshIntroOutro = introOutro.GetComponent<SpriteRenderer> ();
		Debug.Log (ScreenMeshIntroOutro);
		introTextures = new Sprite[101];
		loadTextures (introTextures, "022317/Intro/Nio_Welcome2Manual_sw_16_wWheelref", ScreenMeshIntroOutro);
	}

	// Use this for initialization
	void Start () {
		StartCoroutine (play ());
	}

	private IEnumerator play ()
	{
		//START INTRO
		currentFrame = 0;
		while (currentFrame < 101) {
			Debug.Log ("current frame " + currentFrame);
			ScreenMeshIntroOutro.sprite = introTextures [currentFrame];
			currentFrame++;
			Debug.Log ("current frame " + currentFrame);
			// Wait for the next frame.
			yield return m_FrameRateWait;
		}
		introOutro.SetActive (false);
	}

	// Update is called once per frame
	void Update () {

	}


	private void loadTextures(Sprite[] textures, string filepath, SpriteRenderer mesh) {
		for (int i = 0; i < textures.Length; i++) {
			if (i < 10)
				textures [i] = Resources.Load (filepath + "0000" + i.ToString (), typeof(Sprite)) as Sprite;
			else if (i < 100)
				textures [i] = Resources.Load (filepath + "000" + i.ToString (), typeof(Sprite)) as Sprite;
			else if (i < 1000)
				textures [i] = Resources.Load (filepath + "00" + i.ToString (), typeof(Sprite)) as Sprite;
			else if (i < 10000)
				textures [i] = Resources.Load (filepath + "0" + i.ToString (), typeof(Sprite)) as Sprite;
		}
		mesh.sprite = textures[0];
	}

}
