using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour {

	public static ViewManager inst;

	public enum ViewLabel { Lines, Tasks, SubTasks, SelectTask, EditTasks, EditSubTasks }

	public enum DeadlineState { Future, Today, Late, Done }
	public Color colorFuture = Color.green;
	public Color colorToday = Color.yellow;
	public Color colorLate = Color.red;
	public Color colorDone = Color.gray;

	BaseView _currentView;
	public ViewLabel currentView {
		get { return _currentView.label; }

		set {
			BaseView view = _views[value];
			if (view != _currentView) {
				if (_currentView != null)
					_currentView.Hide();

				_currentView = view;
				_currentView.Show();
			}
		}
	}

	Dictionary<ViewLabel, BaseView> _views = new Dictionary<ViewLabel, BaseView>();

	public void ShowTasksView(Line line) {
		currentView = ViewLabel.Tasks;
		TasksView view = (TasksView)_views[ViewLabel.Tasks];
		view.line = line;
	}

	public void ShowSubTasksView(Task task) {
		currentView = ViewLabel.SubTasks;
		SubTasksView view = (SubTasksView)_views[ViewLabel.SubTasks];
		view.task = task;
	}

	public void ShowEditTasksView() {
		currentView = ViewLabel.EditTasks;
	}

	public void ShowEditSubTasksView(Task task) {
		currentView = ViewLabel.EditSubTasks;
		EditSubTasksView view = (EditSubTasksView)_views[ViewLabel.EditSubTasks];
		view.task = task;
	}

	public void ShowSelectTaskView(Line line) {
		currentView = ViewLabel.SelectTask;
		SelectTaskView view = (SelectTaskView)_views[ViewLabel.SelectTask];
		view.line = line;
	}

	public void OnTaskSelected(Line line, Task task) {
		currentView = ViewLabel.Tasks;
		TasksView view = (TasksView)_views[ViewLabel.Tasks];
		view.line = line;
		view.AddTask(task);
	}

	void Save() {
		SelectTaskView.tasks.Save();
		LinesView.lines.Save();
	}

	public static Color GetColor(DeadlineState state) {
		switch (state) {
			case DeadlineState.Future:	return inst.colorFuture;
			case DeadlineState.Today:	return inst.colorToday;
			case DeadlineState.Late:	return inst.colorLate;
			case DeadlineState.Done:	return inst.colorDone;
			default:					return Color.magenta;
		}
	}

	void Awake() {
		inst = this;

		BaseView[] views = GetComponentsInChildren<BaseView>(true);
		foreach (BaseView view in views) {
			view.gameObject.SetActive(true);
			view.gameObject.SetActive(false);
			_views.Add(view.label, view);
		}

		currentView = ViewLabel.Lines;
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			switch (currentView) {
				case ViewLabel.Tasks:
					currentView = ViewLabel.Lines;
					break;
				case ViewLabel.SubTasks:
					currentView = ViewLabel.Tasks;
					break;
				case ViewLabel.SelectTask:
					currentView = ViewLabel.Tasks;
					break;
				case ViewLabel.EditTasks:
					currentView = ViewLabel.Lines;
					break;
				case ViewLabel.EditSubTasks:
					currentView = ViewLabel.EditTasks;
					break;
			}
		}
	}

	void OnApplicationPause(bool pause) {
		if (pause == false) return;
		Save();
	}

	void OnApplicationQuit() {
		Save();
	}
}
