using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubTasksView : BaseView<SubTask> {

	Task _task;
	public Task task {
		set {
			_task = value;
			title = _task.name;
			LoadSubTasks();
		}
	}

	protected override string namePrefix { get { return "SubTask"; } }

	protected override SubTask CreateData() {
		SubTask subtask = new SubTask(GetUniqueName());
		_task.subtasks.Add(subtask);
		return subtask;
	}

	protected override void DeleteItem(BaseItem<SubTask> item) {
		_task.subtasks.Remove(item.data);
		base.DeleteItem(item);
	}

	void LoadSubTasks() {
		ClearItems();
		if (_task.subtasks.Count == 0) return;
		foreach (SubTask subtask in _task.subtasks) {
			CreateItem(subtask);
		}
		_task.subtasks.OnSubTaskChanged(_task.subtasks.list[0]);
	}

	protected override void OnItemDoubleClicked(BaseItem<SubTask> item) {
		SubTaskItem subtaskItem = (SubTaskItem)item;
		if (subtaskItem.subtask.deadlineState == ViewManager.DeadlineState.Future) {
			return;
		}

		subtaskItem.subtask.done = !subtaskItem.subtask.done;
		subtaskItem.Refresh();
	}

}
