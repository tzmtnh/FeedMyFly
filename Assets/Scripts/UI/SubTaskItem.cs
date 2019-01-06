using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubTaskItem : BaseTask<SubTask> {

	public Text offsetText;

	public SubTask subtask {
		get { return data; }
		set { data = value; }
	}

}