using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SubTask : Data, ISerializationCallbackReceiver {

	public bool done = false;

	public override DateTime dateTime {
		get {
			DateTime dt = parent.deadline;
			if (this == parent.subtasks.Last)
				return dt;

			for (int i = parent.subtasks.Count - 2; i >= 0; i--) {
				SubTask subtask = parent.subtasks.list[i];
				dt = dt.AddDays(-subtask.offset);
				if (subtask == this)
					return dt;
			}

			return DateTime.Now;
		}

		set {
			DateTime dt = value;
			int index = parent.subtasks.list.IndexOf(this);
			for (int i = index; i < parent.subtasks.Count - 1; i++) {
				SubTask subtask = parent.subtasks.list[i];
				dt = dt.AddDays(subtask.offset);
			}
			parent.deadline = dt;
			parent.subtasks.UpdateAll();
		}
	}

	[SerializeField]
	int _offset = 1;
	public int offset {
		get { return _offset; }

		set {
			int val = Mathf.Max(0, value);
			if (_offset == val) return;
			_offset = val;
			parent.subtasks.UpdateAll();
		}
	}

	[NonSerialized]
	public Task parent;

	public bool IsLast { get { return this == parent.subtasks.Last; } }

	public SubTask(string name) : base(name) { }

	public SubTask(SubTask copyFrom) : this(copyFrom.name) {
		_offset = copyFrom._offset;
	}

	public void OnBeforeSerialize() { }

	public void OnAfterDeserialize() { }

	public ViewManager.DeadlineState deadlineState {
		get {
			int c = SerializableDate.Compare(dateTime, DateTime.Now);
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
