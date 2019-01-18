using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour {

	public static ViewManager inst;

	public enum ViewLabel { Lines, Tasks, SubTasks, SelectTask, EditTasks, EditSubTasks, SelectDate, Welcome }

	public enum DeadlineState { Future, Today, Late, Done }
	public Color colorFuture = Color.green;
	public Color colorToday = Color.yellow;
	public Color colorLate = Color.red;
	public Color colorDone = Color.gray;

	bool _storePrevView = true;
	Stack<ViewLabel> _prevViews = new Stack<ViewLabel>(8);
	BaseView _currentView = null;
	public ViewLabel currentView {
		get { return _currentView.label; }

		set {
			BaseView view = _views[value];
			if (view != _currentView) {
				if (_currentView != null) {
					_currentView.Hide();
					if (_storePrevView) {
						_prevViews.Push(_currentView.label);
					}
				}

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

	public void ShowSelectDateView(SerializableDate date) {
		currentView = ViewLabel.SelectDate;
		SelectDateView view = (SelectDateView)_views[ViewLabel.SelectDate];
		view.date = date;
	}

	public void ShowLinesView() {
		currentView = ViewLabel.Lines;
	}

	public void GoBack() {
		if (_prevViews.Count == 0) return;
		_storePrevView = false;
		currentView = _prevViews.Pop();
		_storePrevView = true;
	}

	void Save() {
		SelectTaskView.tasks.Save();
		LinesView.lines.Save();
	}

	public static Color GetColor(DeadlineState state) {
		switch (state) {
			case DeadlineState.Future: return inst.colorFuture;
			case DeadlineState.Today: return inst.colorToday;
			case DeadlineState.Late: return inst.colorLate;
			case DeadlineState.Done: return inst.colorDone;
			default: return Color.magenta;
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

#if UNITY_EDITOR
		currentView = ViewLabel.Lines;
#else
		currentView = ViewLabel.Welcome;
#endif
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			GoBack();
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
