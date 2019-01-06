using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SubTasks {

	[System.NonSerialized]
	public Task parent;

	public List<SubTask> list = new List<SubTask>();

	public int Count { get { return list.Count; } }

	public void Add(SubTask subtask) {
		list.Add(subtask);
		subtask.parent = parent;
	}

	public void Remove(SubTask subtask) {
		list.Remove(subtask);
	}

	public IEnumerator GetEnumerator() {
		foreach (SubTask subtask in list) {
			yield return subtask;
		}
	}

	public SubTask Last {
		get {
			if (Count == 0)
				return null;
			return list[Count - 1];
		}
	}

}
