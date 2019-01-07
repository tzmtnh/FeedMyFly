using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Tasks {
	public List<Task> list = new List<Task>();

	public int Count { get { return list.Count; } }

	public Task Last {
		get {
			if (Count == 0)
				return null;
			return list[Count - 1];
		}
	}

	public void Add(Task task) {
		list.Add(task);
	}

	public void Remove(Task task) {
		list.Remove(task);
	}

	public IEnumerator GetEnumerator() {
		foreach (Task task in list) {
			yield return task;
		}
	}
}