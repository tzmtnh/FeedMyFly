using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SubTask : Data, ISerializationCallbackReceiver {

	public event Action<SubTask> OnUpdated;

	public bool done = false;

	[SerializeField]
	SerializableDate _date;
	public override SerializableDate date { get { return _date; } }

	public int Offset { get { return 2; } }

	public SubTask(string name) : base(name) {
		_date = new SerializableDate();
		_date.OnDateTimeChanged += OnDateTimeChanged;
	}

	public SubTask(SubTask copyFrom) : this(copyFrom.name) { }

	void OnDateTimeChanged() {
		OnChanged();
		if (OnUpdated != null) {
			OnUpdated(this);
		}
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
