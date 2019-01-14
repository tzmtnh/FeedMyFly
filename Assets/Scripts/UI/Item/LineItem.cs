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
		throw new System.NotImplementedException();
	}

	protected override Color GetBGColor() {
		return line.color;
	}

}
