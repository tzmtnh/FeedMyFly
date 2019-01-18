using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubTaskItem : BaseTaskItem<SubTask> {

	public Text offsetText;

	public SubTask subtask { get { return data; } }

	public override void OnDateClicked() {
		OnClicked();
		ViewManager.inst.ShowSelectDateView(subtask.date);
	}

	public override void Refresh() {
		base.Refresh();
		offsetText.enabled = !subtask.IsLast;
		offsetText.text = "(" + subtask.offset + ")";
	}

	protected override Color GetBGColor() {
		if (dateText == null)
			return ViewManager.GetColor(ViewManager.DeadlineState.Done);
		return subtask.color;
	}
}