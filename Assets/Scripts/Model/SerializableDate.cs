using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDate : ISerializationCallbackReceiver {

	public event Action OnDateTimeChanged;

	public int year;
	public int month;
	public int day;

	DateTime _dateTime;
	public DateTime dateTime {
		get {
			return _dateTime;
		}

		set {
			_dateTime = value;
			if (OnDateTimeChanged != null) {
				OnDateTimeChanged();
			}
		}
	}

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

	public int Compare(SerializableDate other) {
		return Compare(other.dateTime);
	}

	public int Compare(DateTime other) {
		int yearDelta = dateTime.Year - other.Year;
		if (yearDelta != 0) {
			return (int)Mathf.Sign(yearDelta);
		}

		int monthDelta = dateTime.Month - other.Month;
		if (monthDelta != 0) {
			return (int)Mathf.Sign(monthDelta);
		}

		int dayDelta = dateTime.Day - other.Day;
		return dayDelta == 0 ? 0 : (int)Mathf.Sign(dayDelta);
	}

	public override string ToString() {
		return dateTime.ToShortDateString();
	}

}
