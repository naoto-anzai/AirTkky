using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handle_tenkey_controller: MonoBehaviour
{
    
    void Start() 
    {

    }
        
    void Update()
    {
        // ���Ɉړ�
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            UnityEngine.Debug.Log("Left key was pressed.");
        }
        // �E�Ɉړ�
        if (Input.GetKey(KeyCode.RightArrow))
        {
            UnityEngine.Debug.Log("Right key was pressed.");
        }
        // �O�Ɉړ�
        if (Input.GetKey(KeyCode.UpArrow))
        {
            UnityEngine.Debug.Log("Up key was pressed.");
        }
        // ���Ɉړ�
        if (Input.GetKey(KeyCode.DownArrow))
        {
            UnityEngine.Debug.Log("Down key was pressed.");
        }

    }
}
