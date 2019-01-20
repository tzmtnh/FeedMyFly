using System;
using System.Collections;
using System.Collections.Generic;

public abstract class Data {

	public int version;
	public string name;

	public abstract DateTime dateTime { get; set; }

	public Data(string name) {
		version = ViewManager.version;
		this.name = name;
	}

	public event Action OnDataChanged;

	public void OnChanged() {
		if (OnDataChanged != null) {
			OnDataChanged();
		}
	}

}
