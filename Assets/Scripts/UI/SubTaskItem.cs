using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubTaskItem : BaseItem<SubTask> {

	public SubTask subtask {
		get { return data; }
		set { data = value; }
	}

	protected override string label {
		get { return name; }
	}
}