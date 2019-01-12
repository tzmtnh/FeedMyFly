using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTaskView : BaseView<Task> {

	Line _line;
	public Line line { set { _line = value; } }

	Tasks _tasks = new Tasks();

	protected override string namePrefix { get { return "Task"; } }

	protected override Task CreateData() {
		Task task = new Task(GetUniqueName());
		_tasks.Add(task);
		return task;
	}

	protected override void OnItemDoubleClicked(BaseItem<Task> item) {
		ViewManager.inst.OnTaskSelected(_line, item.data);
	}

	protected override void Awake() {
		base.Awake();
		CreateItem();
		CreateItem();
		CreateItem();
	}
}
