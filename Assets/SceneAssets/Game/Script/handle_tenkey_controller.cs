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
        // 左に移動
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            UnityEngine.Debug.Log("Left key was pressed.");
        }
        // 右に移動
        if (Input.GetKey(KeyCode.RightArrow))
        {
            UnityEngine.Debug.Log("Right key was pressed.");
        }
        // 前に移動
        if (Input.GetKey(KeyCode.UpArrow))
        {
            UnityEngine.Debug.Log("Up key was pressed.");
        }
        // 後ろに移動
        if (Input.GetKey(KeyCode.DownArrow))
        {
            UnityEngine.Debug.Log("Down key was pressed.");
        }

    }
}
