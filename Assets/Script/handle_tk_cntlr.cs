using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class handle_tk : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
    }
        
    // Update is called once per frame
    void Update()
    {// 左に移動
        Rigidbody rb = this.GetComponent<Rigidbody>();

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce (new Vector3(-5f, 0.0f, 0.0f) * Time.deltaTime);
        }
        // 右に移動
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce (new Vector3(5f, 0.0f, 0.0f) * Time.deltaTime);
        }
        // 前に移動
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce (new Vector3(0.0f, 0.0f, 5f) * Time.deltaTime);
        }
        // 後ろに移動
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce (new Vector3(0.0f, 0.0f, -5f) * Time.deltaTime);
        }

    }
}
