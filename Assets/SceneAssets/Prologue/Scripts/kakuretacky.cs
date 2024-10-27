using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kakuretacky : MonoBehaviour
{
    [SerializeField] GameObject KakureTacky;
    [SerializeField] GameObject Bikkuri;
    [SerializeField] GameObject popUp;

    int count;

    void Start()
    {
        KakureTacky.SetActive(true);
        Bikkuri.SetActive(false);
        popUp.SetActive(false); 

        count = 0;
    }

    public void OnClickTacky()
    {

        if (count == 0)
        {
            KakureTacky.SetActive(false);
            Bikkuri.SetActive(true);

            Debug.Log("count == 0");
            count++;
        }
        else if (count == 1) 
        {
            Bikkuri.SetActive(false);
            popUp.SetActive(true);
            Debug.Log("count == 1");

            count++;
        }
        else
        {
            Debug.Log("count is not 0 or 1");
        }
    }
}
