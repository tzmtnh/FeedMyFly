using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseTaskItem<T> : BaseItem<T> where T : Data {

	public Text dateText;

	public override void Refresh() {
		base.Refresh();
		if (dateText != null) {
			dateText.text = data.dateTime.ToShortDateString();
		}
	}

	public abstract void OnDateClicked();

}

public class TaskItem : BaseTaskItem<Task> {

	public Text tasksText;

	public Task task { get { return data; } }

	public override void OnDateClicked() {
		OnClicked();
		ViewManager.inst.ShowSelectDateView(task);
	}

	public override void Refresh() {
		base.Refresh();
		tasksText.text = "(" + task.subtasks.DoneCount + "/" + task.subtasks.Count + ")";
	}

	protected override Color GetBGColor() {
		if (dateText == null)
			return ViewManager.GetColor(ViewManager.DeadlineState.Done);
		return task.color;
	}

}
