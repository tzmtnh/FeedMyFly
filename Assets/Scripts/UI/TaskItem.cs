using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskItem : BaseItem<Task> {

	public Task task {
		get { return data; }
		set { data = value; }
	}

}
