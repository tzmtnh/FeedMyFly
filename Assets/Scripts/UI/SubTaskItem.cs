using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubTaskItem : BaseTaskItem<SubTask> {

	public Text offsetText;

	public SubTask subtask { get { return data; } }

	public override SerializableDate date {
		get {
			return subtask.date;
		}
	}

	public override void OnDateClicked() {
		OnClicked();
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
			subtask.AddDays(-1);
		} else {
			subtask.AddDays(1);
		}
	}

	public override void Refresh() {
		base.Refresh();
		offsetText.text = "(" + subtask.Offset + ")";
	}

	protected override Color GetBGColor() {
		int c = date.Compare(System.DateTime.Now);
		ViewManager.DeadlineState state;
		if (c > 0) {
			state = ViewManager.DeadlineState.Future;
		} else if (c == 0) {
			state = ViewManager.DeadlineState.Today;
		} else {
			state = ViewManager.DeadlineState.Late;
		}
		return ViewManager.GetColor(state);
	}
}