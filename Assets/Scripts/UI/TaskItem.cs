using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskItem : BaseItem<Task> {

	public Task task {
		get { return data; }
		set { data = value; }
	}
}
