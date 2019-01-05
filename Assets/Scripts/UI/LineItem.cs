using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineItem : BaseItem<Line> {

	public Line line {
		get { return data; }
		set { data = value; }
	}
}
