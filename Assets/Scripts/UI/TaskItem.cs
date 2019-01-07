﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseTaskItem<T> : BaseItem<T> where T : Data {

	public Text dateText;

	public abstract SerializableDate date { get; }

	public override void Refresh() {
		base.Refresh();
		if (date == null) {
			dateText.text = "??/??/????";
		} else {
			dateText.text = date.ToString();
		}
	}

	public abstract void OnDateClicked();

}

public class TaskItem : BaseTaskItem<Task> {

	public Text tasksText;

	public Task task { get { return data; } }

	public override SerializableDate date {
		get {
			if (task.subtasks.Count == 0)
				return null;
			return task.subtasks.Last.date;
		}
	}

	public override void OnDateClicked() {
		OnClicked();
		if (task.subtasks.Count == 0) return;
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
			task.subtasks.Last.AddDays(-1);
		} else {
			task.subtasks.Last.AddDays(1);
		}
		task.OnChanged();
	}

	public override void Refresh() {
		base.Refresh();
		tasksText.text = "(" + task.subtasks.Count + ")";
	}

	protected override Color GetBGColor() {
		return task.color;
	}

}
