using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Task : Data {

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

	public Task(Task copyFrom) : base(copyFrom.name) {
		subtasks = new SubTasks();

		foreach (SubTask subtask in copyFrom.subtasks) {
			SubTask copy = new SubTask(subtask);
			subtasks.Add(copy);
		}
	}

}
