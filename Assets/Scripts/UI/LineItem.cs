using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineItem : BaseItem<Line> {

	public Line line {
		get { return data; }
		set { data = value; }
	}

	protected override Color GetBGColor() {
		return Color.white;
	}

}
