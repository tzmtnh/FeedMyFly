﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseView : MonoBehaviour {

	public ViewManager.ViewLabel label;

	public virtual void Hide() {
		gameObject.SetActive(false);
	}

	public virtual void Show() {
		gameObject.SetActive(true);
	}
}

public abstract class BaseView<T> : BaseView where T : Data {

	BaseItem<T> _prototypeItem;
	protected List<BaseItem<T>> _items = new List<BaseItem<T>>();

	BaseItem<T> _selectedItem;
	public BaseItem<T> selectedItem {
		get { return _selectedItem; }

		set {
			if (_selectedItem == value) return;

			if (_selectedItem != null) {
				_selectedItem.selected = false;
			}

			if (value != null) {
				value.selected = true;
			}

			_selectedItem = value;
		}
	}

	protected abstract string namePrefix { get; }
	protected string GetUniqueName() {
		int index = 0;
		bool searching = true;
		string uniqueName = "";
		while (searching) {
			index++;
			uniqueName = namePrefix + " " + index;
			searching = false;
			foreach (BaseItem<T> item in _items) {
				if (item.name == uniqueName) {
					searching = true;
					break;
				}
			}
		}
		return uniqueName;
	}

	RectTransform _itemsRectTransform;
	RectTransform _viewportRectTransform;
	Vector2 _itemsRectPosition;

	void UpdateItemsRect() {
		if (_items.Count == 0) return;
		float height = _items[0].height * _items.Count;
		_itemsRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
	}

	public void Drag(PointerEventData eventData) {
		float max = Mathf.Max(0, _viewportRectTransform.rect.y - _itemsRectTransform.rect.y);
		_itemsRectPosition.y += eventData.delta.y;
		_itemsRectPosition.y = Mathf.Clamp(_itemsRectPosition.y, 0, max);
		_itemsRectTransform.anchoredPosition = _itemsRectPosition;
	}

	protected abstract T CreateData();

	protected BaseItem<T> CreateItem(T data = null) {
		if (data == null) {
			data = CreateData();
		}

		BaseItem<T> item = Instantiate(_prototypeItem, _prototypeItem.transform.parent);
		item.gameObject.SetActive(true);
		item.data = data;
		item.view = this;
		_items.Add(item);
		UpdateItemsRect();
		selectedItem = item;
		return item;
	}

	protected virtual void DeleteItem(BaseItem<T> item) {
		int index = _items.IndexOf(item);
		_items.Remove(item);
		UpdateItemsRect();
		Destroy(item.gameObject);
		
		if (_items.Count > 0)
			selectedItem = _items[Mathf.Min(_items.Count - 1, index)];
		else
			selectedItem = null;
	}

	protected void DuplicateItem(BaseItem<T> item) {
		BaseItem<T> duplicatedItem = CreateItem();
		duplicatedItem.Copy(item);
		selectedItem = duplicatedItem;
	}

	protected void ClearItems() {
		foreach (BaseItem item in _items) {
			Destroy(item.gameObject);
		}
		_items.Clear();
	}

	public void OnAddClicked() {
		CreateItem();
	}

	public void OnDeleteClicked() {
		if (selectedItem == null) return;
		DeleteItem(selectedItem);
	}

	public void OnDuplicateClicked() {
		if (selectedItem == null) return;
		DuplicateItem(selectedItem);
	}

	protected abstract void OnItemDoubleClicked(BaseItem<T> item);

	const float DOUBLE_CLICK_THRESH = 0.3f;
	float _lastItemClickTime;
	public void OnItemClicked(BaseItem<T> item) {
		if (item.selected) {
			float dt = Time.time - _lastItemClickTime;
			if (dt < DOUBLE_CLICK_THRESH) {
				OnItemDoubleClicked(item);
			}
		} else {
			selectedItem = item;
		}

		_lastItemClickTime = Time.time;
	}

	public override void Show() {
		base.Show();
		foreach (BaseItem<T> item in _items) {
			item.Refresh();
		}
	}

	protected virtual void Awake() {
		_prototypeItem = GetComponentInChildren<BaseItem<T>>();
		_prototypeItem.gameObject.SetActive(false);

		_viewportRectTransform = _prototypeItem.transform.parent.parent.GetComponent<RectTransform>();
		_itemsRectTransform = _prototypeItem.transform.parent.GetComponent<RectTransform>();
		_itemsRectPosition = _itemsRectTransform.anchoredPosition;
	}
}
