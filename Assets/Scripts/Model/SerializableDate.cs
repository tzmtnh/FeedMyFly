using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDate : ISerializationCallbackReceiver {

	public DateTime dateTime { get; set; }

	public int year;
	public int month;
	public int day;

	public SerializableDate() {
		dateTime = DateTime.Now;
	}

	public void OnBeforeSerialize() {
		year = dateTime.Year;
		month = dateTime.Month;
		day = dateTime.Day;
	}

	public void OnAfterDeserialize() {
		dateTime = new DateTime(year, month, day);
	}

	public void AddDays(int days) {
		dateTime = dateTime.AddDays(days);
	}

	public override string ToString() {
		return dateTime.ToShortDateString();
	}

}
