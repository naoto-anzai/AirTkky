using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class handle_tenkey : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
    }
        
    // Update is called once per frame
    void Update()
    {// ���Ɉړ�
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.position += new Vector3(-5f, 0.0f, 0.0f) * Time.deltaTime;
        }
        // �E�Ɉړ�
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.position += new Vector3(5f, 0.0f, 0.0f) * Time.deltaTime;
        }
        // �O�Ɉړ�
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.position += new Vector3(0.0f, 0.0f, 5f) * Time.deltaTime;
        }
        // ���Ɉړ�
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.position += new Vector3(0.0f, 0.0f, -5f) * Time.deltaTime;
        }
    }
}
