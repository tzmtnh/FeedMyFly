using System;
using System.Collections;
using System.Collections.Generic;

public abstract class Data {

	public string name;

	public abstract SerializableDate date { get; }

	public Data(string name) {
		this.name = name;
	}

	public event Action<Data> OnDataChanged;

	public void OnChanged() {
		if (OnDataChanged != null) {
			OnDataChanged(this);
		}
	}

}
