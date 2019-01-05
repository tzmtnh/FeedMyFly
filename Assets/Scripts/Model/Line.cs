using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Line : Data {

	public Tasks tasks;

	public Line(string name) : base(name) {
		tasks = new Tasks();
	}

}
