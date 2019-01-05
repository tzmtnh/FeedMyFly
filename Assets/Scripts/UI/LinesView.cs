using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LinesView : BaseView<Line> {

	string _saveFileName;
	Lines _lines = new Lines();

	protected override string namePrefix { get { return "Line"; } }

	protected override BaseItem<Line> CreateItem(Line line = null) {
		if (line == null) {
			line = new Line(GetUniqueName());
			_lines.Add(line);
		}

		return base.CreateItem(line);
	}

	protected override void DeleteItem(BaseItem<Line> item) {
		_lines.Remove(item.data);
		base.DeleteItem(item);
	}

	protected override void OnItemDoubleClicked(BaseItem<Line> item) {
		ViewManager.inst.ShowTasksView(item.data);
	}

	public void Save() {
		_lines.Save(_saveFileName);
	}

	protected override void Awake() {
		base.Awake();

		_saveFileName = Path.Combine(Application.persistentDataPath, "Lines.json");
		_lines = Lines.load(_saveFileName);

		foreach (Line line in _lines) {
			CreateItem(line);
		}
	}
}
