using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Lines {
	public List<Line> list = new List<Line>();

	public int Count { get { return list.Count; } }

	public void Add(Line line) {
		list.Add(line);
	}

	public void Remove(Line line) {
		list.Remove(line);
	}

	public IEnumerator GetEnumerator() {
		foreach (Line line in list) {
			yield return line;
		}
	}

	public void Save(string filename) {
		if (File.Exists(filename)) {
			File.Delete(filename);
		}

		using (StreamWriter streamWriter = File.CreateText(filename)) {
			string jsonString = JsonUtility.ToJson(this);
			streamWriter.Write(jsonString);
		}
	}

	public static Lines load(string filename) {
		if (File.Exists(filename) == false) return null;

		using (StreamReader streamReader = File.OpenText(filename)) {
			string jsonString = streamReader.ReadToEnd();
			return JsonUtility.FromJson<Lines>(jsonString);
		}
	}
}