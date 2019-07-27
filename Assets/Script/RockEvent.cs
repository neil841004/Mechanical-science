using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RockEvent : MonoBehaviour {
	public UnityEvent inSky = new UnityEvent();
	public UnityEvent inGround = new UnityEvent();
	public GameObject pig;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void RockinSky(){
		inSky.Invoke();
	}
	void RockinGround(){
		inGround.Invoke();
	}
	void ShowAnswer(){
		
	}
}
