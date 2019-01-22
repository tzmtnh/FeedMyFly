using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCropper : MonoBehaviour {

	public float aspect = 1;

	Camera _camera;
	Vector2 _lastScreenSize;

	void Awake() {
		_camera = GetComponent<Camera>();
	}

	void Update () {
		float w = Screen.width;
		float h = Screen.height;
		if (_lastScreenSize.x == w && _lastScreenSize.y == h)
			return;
		_lastScreenSize = new Vector2(w, h);

		float a = h / w;
		float wantedWidth = w * a / aspect;
		float width = Mathf.Min(w, wantedWidth);
		float x = (w - width) / 2f;

		// crop view so we see black bars on the sides
		if (Application.isMobilePlatform == false) {
			_camera.rect = new Rect(x / w, 0, width / w, 1);
		}
	}

	void OnValidate() {
		_lastScreenSize = new Vector2();
	}
}
