using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.SimpleAndroidNotifications;

public class FlyNotifications : MonoBehaviour {

	void SetNextReminder() {
		NotificationManager.CancelAll();

		DateTime now = DateTime.Now;
		int nextDays = int.MaxValue;
		int tasksCount = 0;
		TimeSpan nextSpan = new TimeSpan();

		foreach (Line line in LinesView.lines) {
			foreach (Task task in line.tasks) {
				if (task.subtasks.Count == 0) continue;
				if (task.done) continue;

				DateTime dateTime = task.dateTime;
				TimeSpan span = dateTime.Subtract(now);

				int days = (int)span.TotalDays;
				if (days > 0 && days <= nextDays) {
					if (days == nextDays) {
						tasksCount++;
					} else {
						nextDays = days;
						tasksCount = 1;
						nextSpan = span;
					}
				}
			}
		}

		if (tasksCount > 0) {
			NotificationManager.Send(
				nextSpan, 
				"FeedMyFly Daily Reminder",
				"You have " + tasksCount + " task " + (tasksCount == 1 ? "" : "s") + " scheduled for today!",
				Color.yellow,
				NotificationIcon.Bell);
			Debug.Log("Reminder added, Tasks: " + tasksCount);
		}

		/*
		DateTime birthday = new DateTime(2019, 1, 21, 9, 0, 0);
		TimeSpan birthdaySpan = birthday.Subtract(now);
		if (birthdaySpan.TotalDays > 0) {
			NotificationManager.Send(
				birthdaySpan,
				"FeedMyFly Birthday Special!",
				"Happy Birth Day <3",
				Color.red,
				NotificationIcon.Heart);
		}
		*/
	}

	void Awake() {
		NotificationManager.CancelAll();
	}

	void OnApplicationQuit() {
		SetNextReminder();
	}

	void OnApplicationPause(bool pause) {
		if (pause) {
			SetNextReminder();
		} else {
			NotificationManager.CancelAll();
		}
	}
}
