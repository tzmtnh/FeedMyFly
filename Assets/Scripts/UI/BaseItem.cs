using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class BaseItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerUpHandler {

	Image _bgImage;
	RectTransform _rectTransform;

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

	public float width { get { return _rectTransform.rect.width; } }
	public float height { get { return _rectTransform.rect.height; } }

	protected abstract Color GetBGColor();

	public virtual void Refresh() {
		Color bgColor = GetBGColor();
		if (selected) {
			bgColor = Color.Lerp(bgColor, Color.white, 0.3f);
		}
		_bgImage.color = bgColor;
	}

	void Awake() {
		_bgImage = GetComponent<Image>();
		_rectTransform = GetComponent<RectTransform>();
	}

	bool _beingDragged = false;
	public void OnBeginDrag(PointerEventData eventData) {
		_beingDragged = true;
	}

	public void OnDrag(PointerEventData eventData) {
		Drag(eventData);
	}

	public void OnEndDrag(PointerEventData eventData) {
		_beingDragged = false;
	}

	public void OnPointerUp(PointerEventData eventData) {
		if (_beingDragged) return;
		OnClicked();
	}

	protected virtual void Drag(PointerEventData eventData) { }

	public virtual void OnClicked() { }
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

	protected override void Drag(PointerEventData eventData) {
		view.Drag(eventData);
	}

	public void Copy(BaseItem<T> from) {
		name = from.name + " (Copy)";
	}

	public override void Refresh() {
		base.Refresh();
		name = _data.name;
	}

}
