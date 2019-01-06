using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDate : ISerializationCallbackReceiver {

	DateTime _dateTime;

	public int year;
	public int month;
	public int day;

	public SerializableDate() {
		_dateTime = DateTime.Now;
	}

	public void OnBeforeSerialize() {
		year = _dateTime.Year;
		month = _dateTime.Month;
		day = _dateTime.Day;
	}

	public void OnAfterDeserialize() {
		_dateTime = new DateTime(year, month, day);
	}

	public void AddDays(int days) {
		_dateTime = _dateTime.AddDays(days);
	}

	public override string ToString() {
		return _dateTime.ToShortDateString();
	}

}
