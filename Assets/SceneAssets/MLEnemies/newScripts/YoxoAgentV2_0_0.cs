using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoxoAgentV2_0_0 : MonoBehaviour
{
    Rigidbody rBody;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddForce(Vector3 add_force)
    {
        rBody.AddForce(add_force);
    }

    public float GetVelocityX()
    {
        return rBody.velocity.x;
    }

    public float GetVelocityZ()
    {
        return rBody.velocity.z;
    }

    public void SetVelocityZero()
    {
        rBody.velocity = Vector3.zero ;
    }
}
