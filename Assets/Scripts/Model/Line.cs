using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Line : Data {

	public Tasks tasks;

	public bool done {
		get {
			if (tasks.Count == 0)
				return false;
			return tasks.Last.done;
		}
	}

	public Color color {
		get {
			foreach (Task task in tasks) {
				if (task.done) continue;
				return task.color;
			}
			return ViewManager.GetColor(ViewManager.DeadlineState.Done);
		}
	}

	public override SerializableDate date {
		get {
			if (tasks.Count == 0)
				return null;
			return tasks.Last.date;
		}
	}

	public Line(string name) : base(name) {
		tasks = new Tasks();
	}

}
