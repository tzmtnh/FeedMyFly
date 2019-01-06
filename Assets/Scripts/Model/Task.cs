using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[System.Serializable]
public class Task : Data, ISerializationCallbackReceiver {

	public SubTasks subtasks;

	public Task(string name) : base(name) {
		subtasks = new SubTasks();
		subtasks.parent = this;
	}

	public void OnBeforeSerialize() { }

	public void OnAfterDeserialize() {
		foreach (SubTask subtask in subtasks) {
			subtask.parent = this;
		}
	}

	public void OnSubTaskDateChanged(SubTask subtask) {
		Assert.IsTrue(subtasks.list.Contains(subtask));
		int index = subtasks.list.IndexOf(subtask);

		if (index > 0) {
			DateTime dateTime = subtask.date.dateTime;
			for (int i = index - 1; i >= 0; i--) {
				SubTask s = subtasks.list[i];
				dateTime = dateTime.AddDays(-s.Offset);
				s.date.dateTime = dateTime;
				s.OnChanged();
			}
		}

		if (index < subtasks.Count - 1) {
			DateTime dateTime = subtask.date.dateTime;
			dateTime = dateTime.AddDays(subtask.Offset);
			for (int i = index + 1; i < subtasks.Count; i++) {
				SubTask s = subtasks.list[i];
				s.date.dateTime = dateTime;
				s.OnChanged();
				dateTime = dateTime.AddDays(s.Offset);
			}
		}
	}

}
