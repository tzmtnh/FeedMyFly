using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TasksView : BaseView<Task> {

	Line _line;
	public Line line {
		set {
			_line = value;
			_lineNameText.text = _line.name;
			LoadTasks();
		}
	}

	Text _lineNameText;

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
		CreateItem(task);
	}

	public override void OnAddClicked() {
		ViewManager.inst.ShowSelectTaskView(_line);
	}

	protected override void Awake() {
		base.Awake();
		_lineNameText = transform.Find("Text Title").GetComponent<Text>();
	}

}
