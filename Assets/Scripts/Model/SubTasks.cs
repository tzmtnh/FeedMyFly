using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SubTasks {

	public List<SubTask> list = new List<SubTask>();

	public int Count { get { return list.Count; } }

	public void Add(SubTask subtask) {
		list.Add(subtask);
	}

	public void Remove(SubTask subtask) {
		list.Remove(subtask);
	}

	public IEnumerator GetEnumerator() {
		foreach (SubTask subtask in list) {
			yield return subtask;
		}
	}
}
