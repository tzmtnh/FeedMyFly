using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseView : MonoBehaviour {

	public ViewManager.ViewLabel label;

	public virtual void Hide() {
		gameObject.SetActive(false);
	}

	public virtual void Show() {
		gameObject.SetActive(true);
	}
}
