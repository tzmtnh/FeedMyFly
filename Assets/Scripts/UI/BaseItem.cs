﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseItem : MonoBehaviour {

	Image _bgImage;
	public InputField nameInput;

	bool _selected = false;
	public bool selected {
		get { return _selected; }

		set {
			if (_selected == value) return;
			if (_bgImage == null) return;
			_selected = value;
			Refresh();
		}
	}

	protected abstract Color GetBGColor();

	public virtual void Refresh() {
		Color bgColor = GetBGColor();
		if (selected == false) {
			bgColor *= 0.8f;
			bgColor.a = 1;
		}
		_bgImage.color = bgColor;
	}

	void Awake() {
		_bgImage = GetComponent<Image>();
	}

}

public abstract class BaseItem<T> : BaseItem where T : Data {

	T _data;
	public T data {
		get { return _data; }
		set {
			if (_data == value) return;
			_data = value;
			_data.OnDataChanged += Refresh;
			Refresh();
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

	public override void Refresh() {
		base.Refresh();
		name = _data.name;
	}

}
