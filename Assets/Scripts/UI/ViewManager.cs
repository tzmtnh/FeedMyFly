using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour {

	public static ViewManager inst;

	public enum ViewLabel { Lines, Tasks, SubTasks }

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
		view.SetLine(line);
	}

	public void ShowSubTasksView(Task task) {
		currentView = ViewLabel.SubTasks;
		SubTasksView view = (SubTasksView)_views[ViewLabel.SubTasks];
		view.SetTask(task);
	}

	void Save() {
		LinesView linesView = (LinesView)_views[ViewLabel.Lines];
		linesView.Save();
	}

	void Awake() {
		inst = this;

		BaseView[] views = GetComponentsInChildren<BaseView>(true);
		foreach (BaseView view in views) {
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
