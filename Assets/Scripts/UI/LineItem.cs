using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineItem : BaseItem<Line> {

	public Line line {
		get { return data; }
		set { data = value; }
	}

	protected override string label {
		get { return string.Format("{0} ({1} Tasks)", name, line.tasks.Count); }
	}
}
