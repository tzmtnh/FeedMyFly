using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubTasksView : BaseView<SubTask> {

	Task _task;
	Text _taskNameText;

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
		foreach (SubTask subtask in _task.subtasks) {
			CreateItem(subtask);
		}
	}

	public void SetTask(Task task) {
		_task = task;
		_taskNameText.text = task.name;
		LoadSubTasks();
	}

	protected override void OnItemDoubleClicked(BaseItem<SubTask> item) {
		SubTaskItem subtaskItem = (SubTaskItem)item;
		if (subtaskItem.subtask.deadlineState == ViewManager.DeadlineState.Future) {
			return;
		}

		subtaskItem.subtask.done = !subtaskItem.subtask.done;
		subtaskItem.Refresh();
	}

	protected override void Awake() {
		base.Awake();
		_taskNameText = transform.Find("Text Title").GetComponent<Text>();
	}

}
