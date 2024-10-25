using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameStates;

public class TargetManagerV2_1_0 : MonoBehaviour
{
    Rigidbody rBody;

    public Vector3 InitPos;
    public int score_player;
    public int score_enemy;

    float velocityX, velocityZ, velocityMag;
    
    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody> ();
        InitPos = this.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        velocityX = rBody.velocity.x;
        velocityZ = rBody.velocity.z;
        velocityMag = rBody.velocity.magnitude;
    }

    public float GetVelocityX()
    {
        return velocityX;
    }

    public float GetVelocityZ()
    {
        return velocityZ;
    }

    public float GetVelocityMag()
    {
        return velocityMag;
    }

    public void SetVelocityZero()
    {
        rBody.velocity = Vector3.zero ;
    }
}
