using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RockEvent : MonoBehaviour {
	public UnityEvent inSky = new UnityEvent();
	public UnityEvent inGround = new UnityEvent();
	public UnityEvent isTrue = new UnityEvent();
	public UnityEvent isFalse = new UnityEvent();
	public UnityEvent resetPig = new UnityEvent();
	public UnityEvent backHome = new UnityEvent();
	public GameObject pig;
	public Sprite pigInjured, pigOrigin;
	public int pigNumber;
	public int pivotNumber;
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
	public void ThrowRock(){
		this.GetComponent<Animator>().SetInteger("rockWay",pivotNumber);
	}
	public void ShowAnswer(){
		if(pigNumber == pivotNumber){
			pig.GetComponent<SpriteRenderer>().sprite = pigInjured;
			StartCoroutine(ShowAnswerDelay(true));
		}else StartCoroutine(ShowAnswerDelay(false));
	}
	IEnumerator ShowAnswerDelay(bool answer)
	{
		yield return new WaitForSeconds(1f);
		if(answer == true) isTrue.Invoke();
		if(answer == false) isFalse.Invoke();
	}

	public void ResetPigLevel(){
		this.GetComponent<Animator>().Play("Nothing");
		this.GetComponent<Animator>().SetInteger("rockWay",0);
		pivotNumber = 2;
		resetPig.Invoke();
	}
	public void BackHome(){
		this.GetComponent<Animator>().Play("Nothing");
		this.GetComponent<Animator>().SetInteger("rockWay",0);
		pivotNumber = 2;
		pig.GetComponent<SpriteRenderer>().sprite = pigOrigin;
		backHome.Invoke();
	}
}
