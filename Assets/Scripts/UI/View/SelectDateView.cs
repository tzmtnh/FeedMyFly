using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectDateView : BaseView {

	public Text dayText;
	public Text monthText;
	public Text yearText;

	DateTime _dateTime;
	public DateTime dateTime {
		get { return _dateTime; }
		set {
			_dateTime = value;
			Refresh();
		}
	}

	static readonly string[] MONTHS = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

	void Refresh() {
		dayText.text = _dateTime.Day.ToString();
		monthText.text = MONTHS[_dateTime.Month - 1];
		yearText.text = _dateTime.Year.ToString();
	}

	public void OnPrevDayClicked() {
		_dateTime = _dateTime.AddDays(-1);
		Refresh();
	}

	public void OnNextDayClicked() {
		_dateTime = _dateTime.AddDays(1);
		Refresh();
	}

	public void OnPrevMonthClicked() {
		_dateTime = _dateTime.AddMonths(-1);
		Refresh();
	}

	public void OnNextMonthClicked() {
		_dateTime = _dateTime.AddMonths(1);
		Refresh();
	}

	public void OnPrevYearClicked() {
		_dateTime = _dateTime.AddYears(-1);
		Refresh();
	}

	public void OnNextYearClicked() {
		_dateTime = _dateTime.AddYears(1);
		Refresh();
	}

	public void OnOKClicked() {
		ViewManager.inst.OnDateSelected(_dateTime);
	}
}
