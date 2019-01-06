using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubTaskItem : BaseTaskItem<SubTask> {

	public Text offsetText;

	public override SubTask data {
		get {
			return base.data;
		}

		set {
			base.data = value;
			offsetText.text = "(" + subtask.Offset + ")";
		}
	}

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
		view.RefreshItems();
	}
}