using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] se;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PressSE()
    {
        this.GetComponent<AudioSource>().volume = 1f;
        this.GetComponent<AudioSource>().PlayOneShot(se[0]);
    }
    public void CorrectSE()
    {
        this.GetComponent<AudioSource>().volume = 1f;
        this.GetComponent<AudioSource>().PlayOneShot(se[1]);
    }
    public void ErrorSE()
    {
        this.GetComponent<AudioSource>().volume = 1f;
        this.GetComponent<AudioSource>().PlayOneShot(se[2]);
    }
    public void NextSE()
    {
        this.GetComponent<AudioSource>().volume = 0.9f;
        this.GetComponent<AudioSource>().PlayOneShot(se[3]);
    }
	public void BackSE()
    {
        this.GetComponent<AudioSource>().volume = 0.8f;
        this.GetComponent<AudioSource>().PlayOneShot(se[4]);
    }
    public void PigHitSE(){
        this.GetComponent<AudioSource>().volume = 1f;
        this.GetComponent<AudioSource>().PlayOneShot(se[5]);
    }
}
