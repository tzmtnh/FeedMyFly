﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditTasksView : BaseView<Task> {
	protected override string namePrefix { get { return "Task"; } }

	protected override Task CreateData() {
		Task task = new Task(GetUniqueName());
		SelectTaskView.tasks.Add(task);
		return task;
	}

	protected override void OnItemDoubleClicked(BaseItem<Task> item) {
		ViewManager.inst.ShowEditSubTasksView(item.data);
	}

	void Start() {
		foreach (Task task in SelectTaskView.tasks) {
			CreateItem(task);
		}
	}
}
