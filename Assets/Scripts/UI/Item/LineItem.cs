using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineItem : BaseTaskItem<Line> {

	public Text tasksText;

	public Line line {
		get { return data; }
		set { data = value; }
	}

	public override void Refresh() {
		base.Refresh();
		tasksText.text = "(" + line.tasks.DoneCount + "/" + line.tasks.Count + ")";
	}

	public override void OnDateClicked() {
		OnClicked();
		ViewManager.inst.ShowSelectDateView(data);
	}

	protected override Color GetBGColor() {
		return line.color;
	}

	public override void Copy(BaseItem<Line> from) {
		base.Copy(from);
		foreach (Task task in ((LineItem)from).line.tasks) {
			Task copy = new Task(task);
			copy.deadline = task.deadline;
			line.tasks.Add(copy);
		}
		Refresh();
	}

}
