using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditSubTaskItem : SubTaskItem {

	public Button addDayButton;
	public Button removeDayButton;

	public override void Refresh() {
		base.Refresh();
		offsetText.enabled = !subtask.IsLast;

		offsetText.text = subtask.offset.ToString();
		if (subtask.offset == 1)
			offsetText.text += " (day)";
		else
			offsetText.text += " (days)";

		addDayButton.gameObject.SetActive(offsetText.enabled);
		removeDayButton.gameObject.SetActive(offsetText.enabled);
	}

	public void OnOffsetIncrement() {
		subtask.offset++;
	}

	public void OnOffsetDecrement() {
		subtask.offset--;
	}
}
