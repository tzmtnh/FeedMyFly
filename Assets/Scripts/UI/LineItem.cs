using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineItem : MonoBehaviour {

	bool _selected = false;
	public bool selected {
		get { return _selected; }

		set {
			if (_selected == value) return;
			_selected = value;
			_bgImage.color = value ? Color.yellow : _bgColor;
		}
	}

	public new string name {
		get { return _line.name; }

		set {
			_line.name = value;
			gameObject.name = "Item " + value;
			_nameInput.text = value;
		}
	}

	Line _line;
	public Line line {
		get { return _line; }
		set {
			if (_line == value) return;
			_line = value;
			name = _line.name;
		}
	}

	Image _bgImage;
	InputField _nameInput;
	Color _bgColor;

	public void Copy(LineItem from) {
		name = from.name + " (Copy)";
	}

	public void OnInputNameChanged() {
		if (_nameInput.text.Length == 0) {
			_nameInput.text = name;
		} else {
			name = _nameInput.text;
		}
	}

	void Awake() {
		_bgImage = GetComponent<Image>();
		_bgColor = _bgImage.color;
		_nameInput = GetComponentInChildren<InputField>();
	}
}
