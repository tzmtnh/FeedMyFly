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

	public DateTime dateTime { get; set; }

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

	public void UpdateDate(DateTime newDateTime) {
		dateTime = newDateTime;
		if (OnDateTimeChanged != null) {
			OnDateTimeChanged();
		}
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
