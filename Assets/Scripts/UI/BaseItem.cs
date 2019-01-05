using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseItem : MonoBehaviour {

	Image _bgImage;
	Color _bgColor;
	protected InputField _nameInput;

	bool _selected = false;
	public bool selected {
		get { return _selected; }

		set {
			if (_selected == value) return;
			_selected = value;
			_bgImage.color = value ? Color.yellow : _bgColor;
		}
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

public abstract class BaseItem<T> : BaseItem where T : Data {

	T _data;
	public T data {
		get { return _data; }
		set {
			if (_data == value) return;
			_data = value;
			name = _data.name;
		}
	}

	public new string name {
		get { return _data.name; }

		set {
			_data.name = value;
			gameObject.name = "Item " + value;
			_nameInput.text = label;
		}
	}

	protected abstract string label { get; }

	public void Copy(BaseItem<T> from) {
		name = from.name + " (Copy)";
	}

}
