using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] displayUI;
    public GameObject homeBtn;
    public bool isHome = false;


    // Use this for initialization
    void Start()
    {
        Input.multiTouchEnabled = false;
        // for (int i = 1; i <= 17; i++)
        // {
        //     displayUI[i].SetActive(false);
        // }
        // displayUI[0].SetActive(true);
        // homeBtn.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OpenUI(int i)
    {
        displayUI[i].SetActive(true);
    }
    public void CloseUI(int i)
    {
        displayUI[i].SetActive(false);
    }
    public void OpenUIDelay(int i) //按鈕延遲
    {
        StartCoroutine(functionName(i, true));
    }
    public void CloseUIDelay(int i)
    {
        StartCoroutine(functionName(i, false));
    }
    IEnumerator functionName(int i, bool active)
    {
        yield return new WaitForSeconds(2f);
        if (!isHome) displayUI[i].SetActive(active);
    }
    
    public void backHome()
    {
        // for (int i = 1; i <= 17; i++)
        // {
        //     displayUI[i].SetActive(false);
        // }
        // displayUI[0].SetActive(true);
        isHome = true;
        StartCoroutine(ResetIsHome());
    }
    IEnumerator ResetIsHome()
    {
        yield return new WaitForSeconds(2.1f);
        isHome = false;
    }
}
