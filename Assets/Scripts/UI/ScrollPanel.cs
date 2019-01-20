using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class ScrollPanel : MonoBehaviour {

	public RectTransform contentRectTransform;
	RectTransform _viewportRectTransform;
	Vector2 _itemsRectPosition;

	public void UpdateItemsRect<T>(List<T> items) where T : ScrollItem {
		if (items.Count == 0) return;
		float height = 0;
		foreach (ScrollItem item in items) {
			height += item.height;
		}
		contentRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
		_itemsRectPosition.y = Mathf.Min(_itemsRectPosition.y, height - items[0].height);
		contentRectTransform.anchoredPosition = _itemsRectPosition;
	}

	public void Drag(PointerEventData eventData) {
		float max = Mathf.Max(0, _viewportRectTransform.rect.y - contentRectTransform.rect.y);
		_itemsRectPosition.y += eventData.delta.y;
		_itemsRectPosition.y = Mathf.Clamp(_itemsRectPosition.y, 0, max);
		contentRectTransform.anchoredPosition = _itemsRectPosition;
	}

	void Awake() {
		_viewportRectTransform = GetComponent<RectTransform>();
		_itemsRectPosition = contentRectTransform.anchoredPosition;
	}
}
