using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseTaskItem<T> : BaseItem<T> where T : Data {

	public Text dateText;

	public override T data {
		get {
			return base.data;
		}

		set {
			base.data = value;
			OnDateChanged();
		}
	}

	public abstract SerializableDate date { get; }

	public void OnDateChanged() {
		if (date == null) return;
		dateText.text = date.ToString();
	}

	public abstract void OnDateClicked();

}

public class TaskItem : BaseTaskItem<Task> {

	public Text tasksText;

	public Task task { get { return data; } }

	public override Task data {
		get {
			return base.data;
		}

		set {
			base.data = value;
			tasksText.text = "(" + task.subtasks.Count + ")";
		}
	}

	public override SerializableDate date {
		get {
			if (task.subtasks.Count == 0)
				return null;
			return task.subtasks.Last.date;
		}
	}

	public override void OnDateClicked() {
		OnClicked();
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
			task.subtasks.Last.AddDays(-1);
		} else {
			task.subtasks.Last.AddDays(1);
		}
		OnDateChanged();
	}

}
