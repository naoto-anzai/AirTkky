using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_tenkeycontroller_timedeltatime : MonoBehaviour
{

    const float deltaPosition = 3F;
    void Start()
    {

    }

    void Update()
    {
        // ���Ɉړ�
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = transform.position + new Vector3(-deltaPosition, 0, 0) * Time.deltaTime;
            UnityEngine.Debug.Log("Left key was pressed.");
        }
        // �E�Ɉړ�
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = transform.position + new Vector3(deltaPosition, 0, 0) * Time.deltaTime;
            UnityEngine.Debug.Log("Right key was pressed.");
        }
        // �O�Ɉړ�
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = transform.position + new Vector3(0, 0, deltaPosition) * Time.deltaTime;
            UnityEngine.Debug.Log("Up key was pressed.");
        }
        // ���Ɉړ�
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = transform.position + new Vector3(0, 0, -deltaPosition) * Time.deltaTime;
            UnityEngine.Debug.Log("Down key was pressed.");
        }

    }
}
