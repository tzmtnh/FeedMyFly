using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SubTask : Data, ISerializationCallbackReceiver {

	public bool done = false;

	[SerializeField]
	SerializableDate _date;
	public override SerializableDate date { get { return _date; } }

	[SerializeField]
	int _offset = 1;
	public int offset {
		get { return _offset; }

		set {
			int val = Mathf.Max(0, value);
			if (_offset == val) return;
			_offset = val;
			OnChanged();
		}
	}

	[NonSerialized]
	public SubTasks parent;

	public bool IsLast { get { return this == parent.Last; } }

	public SubTask(string name) : base(name) {
		_date = new SerializableDate();
		_date.OnDateTimeChanged += OnDateTimeChanged;
	}

	public SubTask(SubTask copyFrom) : this(copyFrom.name) { }

	void OnDateTimeChanged() {
		OnChanged();
	}

	public void OnBeforeSerialize() { }

	public void OnAfterDeserialize() {
		if (_date == null) return;
		_date.OnDateTimeChanged -= OnDateTimeChanged;
		_date.OnDateTimeChanged += OnDateTimeChanged;
	}

	public ViewManager.DeadlineState deadlineState {
		get {
			int c = date.Compare(System.DateTime.Now);
			if (done) {
				return ViewManager.DeadlineState.Done;
			} else if (c > 0) {
				return ViewManager.DeadlineState.Future;
			} else if (c == 0) {
				return ViewManager.DeadlineState.Today;
			} else {
				return ViewManager.DeadlineState.Late;
			}
		}
	}

	public Color color { get { return ViewManager.GetColor(deadlineState); } }

}
