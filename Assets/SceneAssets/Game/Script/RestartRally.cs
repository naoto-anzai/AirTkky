using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RestartRally : MonoBehaviour
{
    [SerializeField] GameObject popUp;
    [SerializeField] TMP_InputField inputField;

    private void Start()
    {
        popUp.SetActive(false);
        inputField = inputField.GetComponent<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inputField.text = "";
            popUp.SetActive(true);
        }
    }
}
