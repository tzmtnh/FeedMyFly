using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Lines {
	public static string saveFileName;
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

	public void Save() {
		if (File.Exists(saveFileName)) {
			File.Delete(saveFileName);
		}

		using (StreamWriter streamWriter = File.CreateText(saveFileName)) {
			string jsonString = JsonUtility.ToJson(this, true);
			streamWriter.Write(jsonString);
		}
	}

	public static Lines load() {
		if (File.Exists(saveFileName) == false) return new Lines();

		using (StreamReader streamReader = File.OpenText(saveFileName)) {
			string jsonString = streamReader.ReadToEnd();
			return JsonUtility.FromJson<Lines>(jsonString);
		}
	}
}