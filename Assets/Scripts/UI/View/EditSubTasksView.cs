using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditSubTasksView : BaseView<SubTask> {

	Task _task;
	public Task task {
		set {
			_task = value;
			title = _task.name;
			LoadSubTasks();
		}
	}

	protected override SubTask CreateData() {
		SubTask subtask = new SubTask("");
		_task.subtasks.Add(subtask);
		return subtask;
	}

	protected override void OnItemDoubleClicked(BaseItem<SubTask> item) {
		
	}

	protected override void DeleteItem(BaseItem<SubTask> item) {
		_task.subtasks.Remove(item.data);
		base.DeleteItem(item);
	}

	void LoadSubTasks() {
		ClearItems();
		foreach (SubTask subtask in _task.subtasks) {
			CreateItem(subtask);
		}
	}
}
