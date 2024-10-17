using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameStates;
using System.Collections.Specialized;
using System.Security.Cryptography;


public class pack_game_manager : MonoBehaviour
{
    public float offset = 1f;
    public float minSpeed = 0.1f;
    public float speedDecrease = 1f;
    public Vector3 InitializedPos;
    Rigidbody myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        InitializedPos = transform.position;
        myRigidbody = GetComponent<Rigidbody>();
    }
    //得点後にパックの位置を初期化
    public void StartRalley(players turn)
    {
        transform.position = InitializedPos - new Vector3(0, 0,(float)turn * offset);
        myRigidbody.velocity = myRigidbody.velocity.normalized * 0f;
    }
    // Update is called once per frame
    void Update()
    {  
        myRigidbody.velocity *= 1 - (speedDecrease * Time.deltaTime);
        if(myRigidbody.velocity.magnitude < minSpeed)
        {
            myRigidbody.velocity = Vector3.zero;
        }
    }
}
