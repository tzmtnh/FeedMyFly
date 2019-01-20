using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class BaseItem : ScrollItem {

	Image _bgImage;

	public bool canEditName = true;
	public InputField nameInput;

	protected bool _selected = false;
	public bool selected {
		get { return _selected; }

		set {
			if (_selected == value) return;
			if (_bgImage == null) return;
			_selected = value;
			nameInput.interactable = canEditName && _selected;
			Refresh();
		}
	}

	protected abstract Color GetBGColor();

	public virtual void Refresh() {
		if (_bgImage != null) {
			Color bgColor = GetBGColor();
			if (selected) {
				bgColor = Color.Lerp(bgColor, Color.white, 0f);
			} else {
				bgColor = Color.Lerp(bgColor, Color.black, 0.2f);
			}
			_bgImage.color = bgColor;
		}
	}

	protected override void Awake() {
		base.Awake();
		_bgImage = GetComponent<Image>();
	}
}

public abstract class BaseItem<T> : BaseItem where T : Data {

	T _data;
	public T data {
		get { return _data; }
		set {
			if (_data == value) return;
			if (_data != null)
				_data.OnDataChanged -= Refresh;
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

	public override void OnClicked() {
		view.OnItemClicked(this);
	}

	public void OnInputNameChanged() {
		if (nameInput.text.Length == 0) {
			nameInput.text = name;
		} else {
			name = nameInput.text;
		}
	}

	public virtual void Copy(BaseItem<T> from) {
		name = from.name + " (Copy)";
	}

	public override void Refresh() {
		base.Refresh();
		name = _data.name;
		nameInput.textComponent.fontStyle = _selected ? FontStyle.Bold : FontStyle.Normal;
	}

}
