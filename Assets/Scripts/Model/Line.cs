using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Line {

	public string name;

	public Tasks tasks;

	public Line(string name) {
		this.name = name;
		tasks = new Tasks();
	}

}
