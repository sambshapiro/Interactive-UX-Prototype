using UnityEngine;
 
public class AspectUtility : MonoBehaviour {


	/*Put this script in a folder that compiles first, such as Standard Assets ( "Plugins" folder always compiles first ),
	 * so it can be accessed from Javascript and Boo, and call it AspectUtility. To use it, attach this script to your camera.
	 * It can also be attached to some other object, in which case it will attempt to find a camera tagged "Main Camera".
	 * For the WantedAspectRatio variable, enter the aspect ratio you want the camera to have. 4:3 is 1.333333, 16:10 is 1.6, and 16:9 is 1.777778,
	 * though any ratio can be used. If the aspect ratio is already the same as the desired one, nothing will happen.
	 * If the screen is less wide than the desired ratio, letterboxing with black bars will force a widescreen display.
	 * If the screen is more wide than the desired ratio, pillarboxing with black bars will force a more square display. 
	If letterboxing or pillarboxing is used, however, certain functions will no longer be correct. For one thing, Screen.width
	and Screen.height will still return values for the total screen size, not the new size of the main camera. This can potentially
	cause bad stuff to happen in your code. To compensate for that, you should use AspectUtility.screenWidth and AspectUtility.screenHeight instead.
	These are integers just like Screen.width and Screen.height, but are calculated based on the main camera's corrected ratio. 
		Similarly, Input.mousePosition also returns a value that's not appropriate for letterboxed/pillarboxed screens.
		Use AspectUtility.mousePosition instead, which will return a Vector3 with correct values. (It's true that only the x and y variables are used,
		but Input.mousePosition returns a Vector3 rather than Vector2, so the same is used for consistency.) 
			Note that these variables will not necessarily work if called in a script's Awake function.
			You should wait until Start or later to access them. 
			You can call the SetCamera function as necessary, such as when the screen size changes.
			For example, if you have a web player that uses a 4:3 aspect ratio window, and the user switches to full-screen mode that's 16:9,
			you'd want to call AspectUtility.SetCamera() to set pillarboxing. You can check the screen's width and height to tell if a screen
			resolution change has occurred. 
	*/
	 
	public float _wantedAspectRatio = 5.38569425f;
	static float wantedAspectRatio;
	static Camera cam;
	static Camera backgroundCam;
	 
	void Awake () {
		cam = GetComponent<Camera>();
		if (!cam) {
			cam = Camera.main;
		}
		if (!cam) {
			Debug.LogError ("No camera available");
			return;
		}
		wantedAspectRatio = _wantedAspectRatio;
		SetCamera();
	}
	 
	public static void SetCamera () {
		float currentAspectRatio = (float)Screen.width / Screen.height;
		// If the current aspect ratio is already approximately equal to the desired aspect ratio,
		// use a full-screen Rect (in case it was set to something else previously)
		if ((int)(currentAspectRatio * 100) / 100.0f == (int)(wantedAspectRatio * 100) / 100.0f) {
			cam.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
			if (backgroundCam) {
				Destroy(backgroundCam.gameObject);
			}
			return;
		}
		// Pillarbox
		if (currentAspectRatio > wantedAspectRatio) {
			float inset = 1.0f - wantedAspectRatio/currentAspectRatio;
			cam.rect = new Rect(inset/2, 0.0f, 1.0f-inset, 1.0f);
		}
		// Letterbox
		else {
			float inset = 1.0f - currentAspectRatio/wantedAspectRatio;
			cam.rect = new Rect(0.0f, inset/2, 1.0f, 1.0f-inset);
		}
		if (!backgroundCam) {
			// Make a new camera behind the normal camera which displays black; otherwise the unused space is undefined
			backgroundCam = new GameObject("BackgroundCam", typeof(Camera)).GetComponent<Camera>();
			backgroundCam.depth = int.MinValue;
			backgroundCam.clearFlags = CameraClearFlags.SolidColor;
			backgroundCam.backgroundColor = Color.black;
			backgroundCam.cullingMask = 0;
		}
	}
	 
	public static int screenHeight {
		get {
			return (int)(Screen.height * cam.rect.height);
		}
	}
	 
	public static int screenWidth {
		get {
			return (int)(Screen.width * cam.rect.width);
		}
	}
	 
	public static int xOffset {
		get {
			return (int)(Screen.width * cam.rect.x);
		}
	}
	 
	public static int yOffset {
		get {
			return (int)(Screen.height * cam.rect.y);
		}
	}
	 
	public static Rect screenRect {
		get {
			return new Rect(cam.rect.x * Screen.width, cam.rect.y * Screen.height, cam.rect.width * Screen.width, cam.rect.height * Screen.height);
		}
	}
	 
	public static Vector3 mousePosition {
		get {
			Vector3 mousePos = Input.mousePosition;
			mousePos.y -= (int)(cam.rect.y * Screen.height);
			mousePos.x -= (int)(cam.rect.x * Screen.width);
			return mousePos;
		}
	}
	 
	public static Vector2 guiMousePosition {
		get {
			Vector2 mousePos = Event.current.mousePosition;
			mousePos.y = Mathf.Clamp(mousePos.y, cam.rect.y * Screen.height, cam.rect.y * Screen.height + cam.rect.height * Screen.height);
			mousePos.x = Mathf.Clamp(mousePos.x, cam.rect.x * Screen.width, cam.rect.x * Screen.width + cam.rect.width * Screen.width);
			return mousePos;
		}
	}
}