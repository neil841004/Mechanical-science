using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answer : MonoBehaviour
{
    public GameObject resultO;
    public GameObject resultX;
    public GameObject again;
    public GameObject enter;
    public GameObject A1, A2, A3, A4;
    public bool answer = false;
    public int q = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
    public void Judge() //判斷答案
    {
        if (!resultO.activeInHierarchy || !resultX.activeInHierarchy)
        {
            if (answer)
            {
                resultO.SetActive(true);
                GameObject.FindWithTag("GM").SendMessage("CloseUIDelay", 10 + q);
                GameObject.FindWithTag("GM").SendMessage("OpenUIDelay", 11 + q);
                if (q < 2)
                {
                    GameObject.FindWithTag("BG").SendMessage("SwitchBGDelay", 10 + q);
                }
                if (q == 2)
                {
                    GameObject.FindWithTag("BG").SendMessage("SwitchBGDelay", 1);
                }
                //GameObject.FindWithTag("SE").SendMessage("PlaySE",1);
            }
            if (!answer)
            {
                resultX.SetActive(true);
                again.SetActive(true);
                enter.gameObject.SetActive(true);
                //GameObject.FindWithTag("SE").SendMessage("PlaySE",2);
            }
        }
    }
    public void CurrectAnswer()
    {
        answer = true;
    }
    public void ErrorAnswer()
    {
        answer = false;
    }
    public void ResetLevel1_2()
    {
        resultO.SetActive(false);
        resultX.SetActive(false);
        again.SetActive(false);
        enter.SetActive(false);
        answer = false;
        A1.transform.GetChild(0).gameObject.SetActive(false);
        A2.transform.GetChild(0).gameObject.SetActive(false);
        A3.transform.GetChild(0).gameObject.SetActive(false);
        A4.transform.GetChild(0).gameObject.SetActive(false);
    }
    public void ButtonDot(int i)
    {
        if ((!resultO.activeInHierarchy && !resultX.activeInHierarchy))
        {
            A1.transform.GetChild(0).gameObject.SetActive(false);
            A2.transform.GetChild(0).gameObject.SetActive(false);
            A3.transform.GetChild(0).gameObject.SetActive(false);
            A4.transform.GetChild(0).gameObject.SetActive(false);
            if (i == 1)
            {
                A1.transform.GetChild(0).gameObject.SetActive(true);
            }
            if (i == 2)
            {
                A2.transform.GetChild(0).gameObject.SetActive(true);
            }
            if (i == 3)
            {
                A3.transform.GetChild(0).gameObject.SetActive(true);
            }
            if (i == 4)
            {
                A4.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
