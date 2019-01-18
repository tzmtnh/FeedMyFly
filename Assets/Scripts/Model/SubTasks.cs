using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

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

	public void OnSubTaskChanged(SubTask subtask) {
		Assert.IsTrue(list.Contains(subtask));
		int index = list.IndexOf(subtask);

		if (index > 0) {
			DateTime dateTime = subtask.date.dateTime;
			for (int i = index - 1; i >= 0; i--) {
				SubTask s = list[i];
				dateTime = dateTime.AddDays(-s.offset);
				s.date.dateTime = dateTime;
				s.OnChanged();
			}
		}

		if (index < Count - 1) {
			DateTime dateTime = subtask.date.dateTime;
			dateTime = dateTime.AddDays(subtask.offset);
			for (int i = index + 1; i < Count; i++) {
				SubTask s = list[i];
				s.date.dateTime = dateTime;
				s.OnChanged();
				dateTime = dateTime.AddDays(s.offset);
			}
		}
	}

}
