using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[Serializable]
public class SubTasks : ISerializationCallbackReceiver {

	[NonSerialized] Task _parent;
	public Task parent {
		get { return _parent; }
		set {
			_parent = value;
			foreach (SubTask subtask in list) {
				subtask.parent = value;
			}
		}
	}

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
		subtask.parent = parent;
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

	public void OnAfterDeserialize() { }

	public SubTask Last {
		get {
			if (Count == 0)
				return null;
			return list[Count - 1];
		}
	}

	public SubTask Current {
		get {
			foreach (SubTask subtask in list) {
				if (subtask.done) continue;
				return subtask;
			}
			return Last;
		}
	}

	public void UpdateAll() {
		foreach (SubTask subtask in list) {
			subtask.OnChanged();
		}
	}

}
