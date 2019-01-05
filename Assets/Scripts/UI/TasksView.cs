using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Assertions;

public class TasksView : BaseView<Task> {

	Line _line;
	Text _lineNameText;

	TaskItem _taskPrototype;
	TaskItem _selectedTask;
	List<TaskItem> _taskItems = new List<TaskItem>();

	void SetSelectedTask(TaskItem taskItem) {
		if (taskItem == _selectedTask) return;

		if (_selectedTask != null) {
			_selectedTask.selected = false;
		}

		if (taskItem != null) {
			taskItem.selected = true;
		}

		_selectedTask = taskItem;
	}

	protected override BaseItem<Task> CreateItem(Task task = null) {
		if (task == null) {
			task = new Task(GetUniqueName());
			_line.tasks.Add(task);
		}

		return base.CreateItem(task);
	}

	protected override void DeleteItem(BaseItem<Task> item) {
		_line.tasks.Remove(item.data);
		DeleteItem(item);
	}

	void LoadTasks() {
		foreach (TaskItem item in _taskItems) {
			Destroy(item.gameObject);
		}
		_taskItems.Clear();

		foreach (Task task in _line.tasks) {
			CreateItem(task);
		}
	}

	public void SetLine(Line line) {
		_line = line;
		_lineNameText.text = line.name;
		LoadTasks();
	}

	protected override void OnItemDoubleClicked(BaseItem<Task> item) {
		Debug.Log("Task double clicked!");
	}

	protected override void Awake() {
		base.Awake();
		_lineNameText = transform.Find("Text Line Name").GetComponent<Text>();
	}

}
