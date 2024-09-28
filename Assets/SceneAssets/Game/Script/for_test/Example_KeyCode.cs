using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example_KeyCode : MonoBehaviour
{
    void Start()
    {
        UnityEngine.Debug.Log("Start!");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UnityEngine.Debug.Log("Space key was pressed.");
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            UnityEngine.Debug.Log("Space key was released.");
        }
    }
}
