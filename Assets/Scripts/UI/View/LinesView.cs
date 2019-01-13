using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LinesView : BaseView<Line> {

	public static Lines lines = new Lines();

	protected override string namePrefix { get { return "Line"; } }

	protected override Line CreateData() {
		Line line = new Line(GetUniqueName());
		lines.Add(line);
		return line;
	}

	protected override void DeleteItem(BaseItem<Line> item) {
		lines.Remove(item.data);
		base.DeleteItem(item);
	}

	protected override void OnItemDoubleClicked(BaseItem<Line> item) {
		ViewManager.inst.ShowTasksView(item.data);
	}

	protected override void Awake() {
		base.Awake();

		Lines.saveFileName = Path.Combine(Application.persistentDataPath, "Lines.json");
		lines = Lines.load();

		foreach (Line line in lines) {
			CreateItem(line);
		}
	}
}
