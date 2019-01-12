using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class ScrollItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerUpHandler {

	ScrollPanel _scrollPanel;

	RectTransform _rectTransform;
	bool _beingDragged = false;

	public float width { get { return _rectTransform.rect.width; } }
	public float height { get { return _rectTransform.rect.height; } }

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

	protected virtual void Drag(PointerEventData eventData) {
		_scrollPanel.Drag(eventData);
	}

	public virtual void OnClicked() { }

	protected virtual void Awake() {
		_rectTransform = GetComponent<RectTransform>();
		_scrollPanel = GetComponentInParent<ScrollPanel>();
	}
}
