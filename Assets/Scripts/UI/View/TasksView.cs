using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TasksView : BaseView<Task> {

	Line _line;
	public Line line {
		set {
			_line = value;
			title = _line.name;
			LoadTasks();
		}
	}

	protected override string namePrefix { get { return "Task"; } }

	protected override Task CreateData() {
		Task task = new Task(GetUniqueName());
		_line.tasks.Add(task);
		return task;
	}

	protected override void DeleteItem(BaseItem<Task> item) {
		_line.tasks.Remove(item.data);
		base.DeleteItem(item);
	}

	void LoadTasks() {
		ClearItems();
		foreach (Task task in _line.tasks) {
			CreateItem(task);
		}
	}

	protected override void OnItemDoubleClicked(BaseItem<Task> item) {
		ViewManager.inst.ShowSubTasksView(item.data);
	}

	public void AddTask(Task task) {
		Task copy = new Task(task);
		_line.tasks.Add(copy);
		CreateItem(copy);
	}

	public override void OnAddClicked() {
		ViewManager.inst.ShowSelectTaskView(_line);
	}

}
