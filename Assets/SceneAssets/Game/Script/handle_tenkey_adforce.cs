using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class handle_tenkey_adforce : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody myRigidbody;

    bool isKeypressed;

    public float myForce = 20f;
    public float maxSpeed = 20f;
    public float speedDecrease = 0.1f;
    public float minSpeed = 0.01f;

    void Start()
    {
        //Fetch the Rigidbody from the GameObject with this script attached
        myRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        isKeypressed = false;
        // ���Ɉړ�
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            isKeypressed = true;
            myRigidbody.AddForce(new Vector3(-myForce, 0, 0) * Time.deltaTime);
            //UnityEngine.Debug.Log("Left key was pressed.");
        }
        // �E�Ɉړ�
        if (Input.GetKey(KeyCode.RightArrow))
        {
            isKeypressed = true;
            myRigidbody.AddForce(new Vector3(myForce, 0, 0) * Time.deltaTime);
            //UnityEngine.Debug.Log("Right key was pressed.");
        }
        // �O�Ɉړ�
        if (Input.GetKey(KeyCode.UpArrow))
        {
            isKeypressed = true;
            myRigidbody.AddForce(new Vector3(0, 0, myForce) * Time.deltaTime);
            //UnityEngine.Debug.Log("Up key was pressed.");
        }
        // ���Ɉړ�
        if (Input.GetKey(KeyCode.DownArrow))
        {
            isKeypressed = true;
            myRigidbody.AddForce(new Vector3(0, 0, -myForce) * Time.deltaTime);
            //UnityEngine.Debug.Log("Down key was pressed.");
        }
        // �ő呬�x�̐���
        if (myRigidbody.velocity.magnitude > maxSpeed)
        {
            myRigidbody.velocity = myRigidbody.velocity.normalized * maxSpeed ;
        }
        if (!isKeypressed)
        {
            myRigidbody.velocity = myRigidbody.velocity.normalized * speedDecrease * Time.deltaTime;

            if (myRigidbody.velocity.magnitude < minSpeed)
            {
                myRigidbody.velocity = myRigidbody.velocity.normalized * 0f;
            }
        }
    }
}
