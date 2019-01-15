using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SubTasks : ISerializationCallbackReceiver {

	public List<SubTask> list = new List<SubTask>();

	public int Count { get { return list.Count; } }

	public int DoneCount {
		get {
			int count = 0;
			foreach (SubTask subtask in list) {
				if (subtask.done) {
					count++;
				}
			}
			return count;
		}
	}

	public void Add(SubTask subtask) {
		subtask.parent = this;
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

	public void OnBeforeSerialize() { }

	public void OnAfterDeserialize() {
		foreach (SubTask subtask in list) {
			subtask.parent = this;
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
