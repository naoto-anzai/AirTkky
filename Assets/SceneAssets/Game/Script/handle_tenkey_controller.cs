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
        // ¶‚ÉˆÚ“®
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            UnityEngine.Debug.Log("Left key was pressed.");
        }
        // ‰E‚ÉˆÚ“®
        if (Input.GetKey(KeyCode.RightArrow))
        {
            UnityEngine.Debug.Log("Right key was pressed.");
        }
        // ‘O‚ÉˆÚ“®
        if (Input.GetKey(KeyCode.UpArrow))
        {
            UnityEngine.Debug.Log("Up key was pressed.");
        }
        // Œã‚ë‚ÉˆÚ“®
        if (Input.GetKey(KeyCode.DownArrow))
        {
            UnityEngine.Debug.Log("Down key was pressed.");
        }

    }
}
