using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TasksView : BaseView {

	Line _line;
	Text _lineNameText;

	TaskItem _taskPrototype;
	TaskItem _selectedTask;
	List<TaskItem> _taskItems = new List<TaskItem>();

	void SetSelectedLine(TaskItem taskItem) {
		if (taskItem == _selectedTask) return;

		if (_selectedTask != null) {
			_selectedTask.selected = false;
		}

		if (taskItem != null) {
			taskItem.selected = true;
		}

		_selectedTask = taskItem;
	}

	TaskItem CreateTaskItem(Task task = null) {
		if (task == null) {
			task = new Task();
			_line.tasks.Add(task);
		}

		TaskItem taskItem = Instantiate(_taskPrototype, _taskPrototype.transform.parent);
		taskItem.gameObject.SetActive(true);
		taskItem.task = task;
		_taskItems.Add(taskItem);
		SetSelectedLine(taskItem);
		return taskItem;
	}

	void LoadTasks() {
		foreach (TaskItem item in _taskItems) {
			Destroy(item.gameObject);
		}
		_taskItems.Clear();

		foreach (Task task in _line.tasks) {
			CreateTaskItem(task);
		}
	}

	public void SetLine(Line line) {
		_line = line;
		_lineNameText.text = line.name;

		LoadTasks();
	}

	public void OnAddTaskClicked() {

	}

	void Awake() {
		_lineNameText = transform.Find("Text Line Name").GetComponent<Text>();
	}

}
