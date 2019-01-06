using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SubTask : Data {

	[System.NonSerialized]
	public Task parent;

	public SerializableDate date;

	public int Offset { get { return 2; } }

	public SubTask(string name) : base(name) {
		date = new SerializableDate();
	}

	public void AddDays(int days) {
		date.AddDays(days);
		parent.OnSubTaskDateChanged(this);
	}

}
