using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Assertions;

public class LinesView : BaseView {

	string _saveFileName;
	Lines _lines = new Lines();

	LineItem _linePrototype;
	LineItem _selectedLine;
	List<LineItem> _lineItems = new List<LineItem>();

	void SetSelectedLine(LineItem lineItem) {
		if (lineItem == _selectedLine) return;

		if (_selectedLine != null) {
			_selectedLine.selected = false;
		}

		if (lineItem != null) {
			lineItem.selected = true;
		}

		_selectedLine = lineItem;
	}

	string GetUniqueName() {
		int index = 0;
		bool searching = true;
		string uniqueName = "";
		while (searching) {
			index++;
			uniqueName = "Line " + index;
			searching = false;
			foreach (LineItem item in _lineItems) {
				if (item.name == uniqueName) {
					searching = true;
					break;
				}
			}
		}
		return uniqueName;
	}

	LineItem CreateLineItem(Line line = null) {
		if (line == null) {
			line = new Line(GetUniqueName());
			_lines.Add(line);
		}

		LineItem lineItem = Instantiate(_linePrototype, _linePrototype.transform.parent);
		lineItem.gameObject.SetActive(true);
		lineItem.line = line;
		_lineItems.Add(lineItem);
		SetSelectedLine(lineItem);
		return lineItem;
	}

	void DeleteLine(LineItem lineItem) {
		int index = _lineItems.IndexOf(lineItem);
		_lineItems.Remove(lineItem);
		_lines.Remove(lineItem.line);
		Destroy(lineItem.gameObject);

		LineItem nextSelected = null;
		if (_lineItems.Count > 0)
			nextSelected = _lineItems[Mathf.Min(_lineItems.Count - 1, index)];
		SetSelectedLine(nextSelected);
	}

	void DuplicateLine(LineItem lineItem) {
		LineItem dupLineItem = CreateLineItem();
		dupLineItem.Copy(lineItem);
		SetSelectedLine(dupLineItem);
	}

	void Awake() {
		_saveFileName = Path.Combine(Application.persistentDataPath, "Lines.json");
		Debug.Log(_saveFileName);

		_linePrototype = GetComponentInChildren<LineItem>();
		_linePrototype.gameObject.SetActive(false);

		_lines = Lines.load(_saveFileName);

		foreach (Line line in _lines) {
			CreateLineItem(line);
		}
	}

	void OnApplicationPause(bool pause) {
		if (pause == false) return;
		_lines.save(_saveFileName);
	}

	void OnApplicationQuit() {
		_lines.save(_saveFileName);
	}

	public void OnAddClicked() {
		CreateLineItem();
	}

	public void OnDeleteClicked() {
		if (_selectedLine == null) return;
		DeleteLine(_selectedLine);
	}

	public void OnDuplicateClicked() {
		if (_selectedLine == null) return;
		DuplicateLine(_selectedLine);
	}

	float _lastLineItemClickTime;
	public void OnLineClicked() {
		GameObject go = EventSystem.current.currentSelectedGameObject;
		LineItem item = go.GetComponent<LineItem>();
		if (item == null) {
			item = go.GetComponentInParent<LineItem>();
			Assert.IsNotNull(item);
			EventSystem.current.SetSelectedGameObject(item.gameObject);
		}

		if (item.selected) {
			const float DOUBLE_CLICK_THRESH = 0.2f;
			float dt = Time.time - _lastLineItemClickTime;
			if (dt < DOUBLE_CLICK_THRESH) {
				ViewManager.inst.ShowTasksView(item.line);
			}
		} else {
			SetSelectedLine(item);
		}

		_lastLineItemClickTime = Time.time;
	}
}
