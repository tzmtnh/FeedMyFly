using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Task : Data, ISerializationCallbackReceiver {

	public SubTasks subtasks;

	[SerializeField]
	SerializableDate _date;
	public override DateTime dateTime {
		get {
			if (subtasks.Count > 0)
				return subtasks.Current.dateTime;
			return _date.dateTime;
		}

		set {
			if (subtasks.Count > 0)
				subtasks.Current.dateTime = value;
			else
				deadline = value;
			OnChanged();
		}
	}

	public DateTime deadline {
		get { return _date.dateTime; }

		set {
			_date.dateTime = value;
			OnChanged();
		}
	}

	public bool done {
		get {
			if (subtasks.Count == 0)
				return false;
			return subtasks.Last.done;
		}
	}

	public Color color {
		get {
			if (subtasks.Count > 0)
				return subtasks.Current.color;
			return ViewManager.GetColor(ViewManager.DeadlineState.Done);
		}
	}

	public void OnBeforeSerialize() { }

	public void OnAfterDeserialize() {
		subtasks.parent = this;
	}

	public Task(string name) : base(name) {
		subtasks = new SubTasks();
		subtasks.parent = this;
		_date = new SerializableDate();
	}

	public Task(Task copyFrom) : this(copyFrom.name) {
		DateTime dateTime = DateTime.Now;
		foreach (SubTask subtask in copyFrom.subtasks) {
			SubTask copy = new SubTask(subtask);
			subtasks.Add(copy);

			if (subtask != copyFrom.subtasks.Last) {
				dateTime = dateTime.AddDays(copy.offset);
			}
		}

		_date.UpdateDate(dateTime);
	}

}
