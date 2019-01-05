using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Task : Data {

	public SubTasks subtasks;

	public Task(string name) : base(name) {
		subtasks = new SubTasks();
	}

}
