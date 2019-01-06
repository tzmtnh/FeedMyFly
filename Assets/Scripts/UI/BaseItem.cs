using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseItem : MonoBehaviour {

	Image _bgImage;
	Color _bgColor;
	public InputField nameInput;

	bool _selected = false;
	public bool selected {
		get { return _selected; }

		set {
			if (_selected == value) return;
			_selected = value;
			_bgImage.color = value ? Color.yellow : _bgColor;
		}
	}

	void Awake() {
		_bgImage = GetComponent<Image>();
		_bgColor = _bgImage.color;
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
			nameInput.text = value;
		}
	}

	public BaseView<T> view { protected get; set; }

	public void OnClicked() {
		view.OnItemClicked(this);
	}

	public void OnInputNameChanged() {
		if (nameInput.text.Length == 0) {
			nameInput.text = name;
		} else {
			name = nameInput.text;
		}
	}

	public void Copy(BaseItem<T> from) {
		name = from.name + " (Copy)";
	}

}
