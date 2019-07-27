using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGswitch : MonoBehaviour
{


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
        if(!GameObject.FindWithTag("GM").GetComponent<GameManager>().isHome){
		for (int i = 0; i <= 12; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
        this.transform.GetChild(j).gameObject.SetActive(true);
        }
	}
}
