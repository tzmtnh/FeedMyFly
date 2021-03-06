﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BaseView : MonoBehaviour {

	public ViewManager.ViewLabel label;

	public virtual void Hide() {
		gameObject.SetActive(false);
	}

	public virtual void Show() {
		gameObject.SetActive(true);
	}
}

public abstract class BaseView<T> : BaseView where T : Data {

	ScrollPanel _scrollPanel;

	Text _titleText;
	public string title { set { _titleText.text = value; } }

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
		_scrollPanel.UpdateItemsRect(_items);
		selectedItem = item;
		return item;
	}

	protected virtual void DeleteItem(BaseItem<T> item) {
		int index = _items.IndexOf(item);
		_items.Remove(item);
		_scrollPanel.UpdateItemsRect(_items);
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

	public virtual void OnAddClicked() {
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
		_scrollPanel = GetComponentInChildren<ScrollPanel>();
		_titleText = transform.Find("Text Title").GetComponent<Text>();
	}
}
