﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[Serializable]
public class Task : Data, ISerializationCallbackReceiver {

	public SubTasks subtasks;

	public override SerializableDate date {
		get {
			if (subtasks.Count == 0)
				return null;
			return subtasks.Last.date;
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
			foreach (SubTask subtask in subtasks) {
				if (subtask.done) continue;
				return subtask.color;
			}
			return ViewManager.GetColor(ViewManager.DeadlineState.Done);
		}
	}

	public Task(string name) : base(name) {
		subtasks = new SubTasks();
	}

	void OnSubTaskChanged(Data data) {
		SubTask subtask = (SubTask)data;
		Assert.IsTrue(subtasks.list.Contains(subtask));
		int index = subtasks.list.IndexOf(subtask);

		if (index > 0) {
			DateTime dateTime = subtask.date.dateTime;
			for (int i = index - 1; i >= 0; i--) {
				SubTask s = subtasks.list[i];
				dateTime = dateTime.AddDays(-s.offset);
				s.date.dateTime = dateTime;
				s.OnChanged();
			}
		}

		if (index < subtasks.Count - 1) {
			DateTime dateTime = subtask.date.dateTime;
			dateTime = dateTime.AddDays(subtask.offset);
			for (int i = index + 1; i < subtasks.Count; i++) {
				SubTask s = subtasks.list[i];
				s.date.dateTime = dateTime;
				s.OnChanged();
				dateTime = dateTime.AddDays(s.offset);
			}
		}
	}

	public Task(Task copyFrom) : base(copyFrom.name) {
		subtasks = new SubTasks();

		foreach (SubTask subtask in copyFrom.subtasks) {
			SubTask copy = new SubTask(subtask);
			copy.OnDataChanged += OnSubTaskChanged;
			subtasks.Add(copy);
		}
	}

	public void OnBeforeSerialize() { }

	public void OnAfterDeserialize() {
		foreach (SubTask subtask in subtasks) {
			subtask.OnDataChanged += OnSubTaskChanged;
		}
	}

}
