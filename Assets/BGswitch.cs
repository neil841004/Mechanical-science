using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGswitch : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SwitchBackground(int j)
    {
        for (int i = 0; i <= 12; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
        this.transform.GetChild(j).gameObject.SetActive(true);
    }
	public void SwitchBackgroundWidhBalck(int h)
    {
        for (int i = 0; i <= 11; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
        this.transform.GetChild(h).gameObject.SetActive(true);
		this.transform.GetChild(12).gameObject.SetActive(true);
    }
	public void SwitchBGDelay(int j){
		StartCoroutine(functionName(j));
	}
	IEnumerator functionName(int j)
	{
		yield return new WaitForSeconds(2f);
		for (int i = 0; i <= 12; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
        this.transform.GetChild(j).gameObject.SetActive(true);
	}
}
