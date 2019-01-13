using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SubTask : Data {

	[System.NonSerialized]
	public Task parent;

	public bool done = false;

	public SerializableDate date;

	public int Offset { get { return 2; } }

	public SubTask(string name) : base(name) {
		date = new SerializableDate();
	}

	public SubTask(SubTask copyFrom) : base(copyFrom.name) {
		date = new SerializableDate();
	}

	public void AddDays(int days) {
		date.AddDays(days);
		OnChanged();
		parent.OnSubTaskDateChanged(this);
	}

	public ViewManager.DeadlineState deadlineState {
		get {
			int c = date.Compare(System.DateTime.Now);
			if (done) {
				return ViewManager.DeadlineState.Done;
			} else if (c > 0) {
				return ViewManager.DeadlineState.Future;
			} else if (c == 0) {
				return ViewManager.DeadlineState.Today;
			} else {
				return ViewManager.DeadlineState.Late;
			}
		}
	}

	public Color color { get { return ViewManager.GetColor(deadlineState); } }

}
