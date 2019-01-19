using System;
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
			if (tasks.Count > 0)
				return tasks.Current.color;
			return ViewManager.GetColor(ViewManager.DeadlineState.Done);
		}
	}

	public override DateTime dateTime {
		get {
			if (tasks.Count > 0)
				return tasks.Current.dateTime;
			return DateTime.Now;
		}

		set {
			if (tasks.Count > 0)
				tasks.Current.dateTime = value;
			OnChanged();
		}
	}

	public Line(string name) : base(name) {
		tasks = new Tasks();
	}

}
