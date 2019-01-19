using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Tasks {
	public static string saveFileName;
	public List<Task> list = new List<Task>();

	public int Count { get { return list.Count; } }

	public int DoneCount {
		get {
			int count = 0;
			foreach (Task task in list) {
				if (task.done) {
					count++;
				}
			}
			return count;
		}
	}

	public Task Last {
		get {
			if (Count == 0)
				return null;
			return list[Count - 1];
		}
	}

	public Task Current {
		get {
			foreach (Task task in list) {
				if (task.done) continue;
				return task;
			}
			return Last;
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

	public void Save() {
		if (File.Exists(saveFileName)) {
			File.Delete(saveFileName);
		}

		using (StreamWriter streamWriter = File.CreateText(saveFileName)) {
			string jsonString = JsonUtility.ToJson(this, true);
			streamWriter.Write(jsonString);
		}
	}

	public static Tasks Load() {
		if (File.Exists(saveFileName) == false) return new Tasks();

		using (StreamReader streamReader = File.OpenText(saveFileName)) {
			string jsonString = streamReader.ReadToEnd();
			return JsonUtility.FromJson<Tasks>(jsonString);
		}
	}
}