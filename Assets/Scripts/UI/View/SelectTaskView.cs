using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SelectTaskView : BaseView<Task> {

	Line _line;
	public Line line { set { _line = value; } }

	public static Tasks tasks = new Tasks();

	protected override string namePrefix { get { return "Task"; } }

	protected override Task CreateData() {
		Task task = new Task(GetUniqueName());
		tasks.Add(task);
		return task;
	}

	protected override void OnItemDoubleClicked(BaseItem<Task> item) {
		ViewManager.inst.OnTaskSelected(_line, item.data);
	}

	protected override void Awake() {
		base.Awake();

		Tasks.saveFileName = Path.Combine(Application.persistentDataPath, "Tasks.json");
		tasks = Tasks.load();

		foreach (Task task in tasks) {
			CreateItem(task);
		}
	}
}
