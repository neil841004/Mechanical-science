using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResetLevel : MonoBehaviour {
	public UnityEvent reset = new UnityEvent();

	public void resetLevel(){
		reset.Invoke();
	}
}
