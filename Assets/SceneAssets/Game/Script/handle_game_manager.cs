using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handle_game_manager : MonoBehaviour
{
    public Vector3 InitializedPos;
    Rigidbody myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        InitializedPos = transform.position;
        myRigidbody = GetComponent<Rigidbody>();
    }
    //���_��Ƀn���h���̈ʒu��������
    public void StartRalley()
    {
        transform.position = InitializedPos;
        myRigidbody.velocity = myRigidbody.velocity.normalized * 0f;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
