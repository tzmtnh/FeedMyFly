using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TasksView : BaseView<Task> {

	Line _line;
	Text _lineNameText;

	protected override string namePrefix { get { return "Task"; } }

	protected override BaseItem<Task> CreateItem(Task task = null) {
		if (task == null) {
			task = new Task(GetUniqueName());
			_line.tasks.Add(task);
		}

		return base.CreateItem(task);
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
