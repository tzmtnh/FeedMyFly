using System;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseTask : Data {

	public SerializableDate date;

	public BaseTask(string name) : base(name) {
		date = new SerializableDate();
	}
}

[System.Serializable]
public class Task : BaseTask {

	public SubTasks subtasks;

	public Task(string name) : base(name) {
		subtasks = new SubTasks();
	}

}
