using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseTask<T> : BaseItem<T> where T : BaseTask {

	public Text dateText;

	public override T data {
		get {
			return base.data;
		}

		set {
			base.data = value;
			OnDateChanged();
		}
	}

	protected void OnDateChanged() {
		dateText.text = data.date.ToString();
	}

	public void OnDateClicked() {
		OnClicked();
		data.date.AddDays(1);
		OnDateChanged();
	}

}

public class TaskItem : BaseTask<Task> {

	public Text tasksText;

	public Task task {
		get { return data; }
		set { data = value; }
	}

}
