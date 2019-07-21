using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBannerWord : MonoBehaviour {

	public void SwitchWord(int j)
    {
        for (int i = 0; i <= 5; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
        this.transform.GetChild(j).gameObject.SetActive(true);
    }
	public void SwitchWordDelay(int j){
		StartCoroutine(functionName(j));
	}
	IEnumerator functionName(int j)
	{
		yield return new WaitForSeconds(2f);
		for (int i = 0; i <= 5; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
        this.transform.GetChild(j).gameObject.SetActive(true);
	}
}
