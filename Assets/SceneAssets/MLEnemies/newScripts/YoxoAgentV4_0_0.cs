using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoxoAgentV4_0_0 : MonoBehaviour
{
    Rigidbody rBody;

    public float MaxSpeed = 20f;

    public float offset_paddle = 0.2f;

    float velocityX, velocityZ, velocityMag;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rBody.velocity.magnitude > MaxSpeed)
        {
            // 現在の速度ベクトルの方向を取得
            Vector3 velocityDirection = rBody.velocity.normalized;

            // 固定したい速度に設定
            rBody.velocity = velocityDirection * MaxSpeed;
        }
        velocityX = rBody.velocity.x;
        velocityZ = rBody.velocity.z;
        velocityMag = rBody.velocity.magnitude;
    }

    /*public void transPosition(float x, float y, float z)
    {
        Parent.localPosition = new Vector3(
            Parent.localPosition.x + x,
            Parent.localPosition.y + y,
            Parent.localPosition.z + z);
    }
    */
    public void AddForce(Vector3 add_force)
    {
        rBody.AddForce(add_force);
    }

    public float GetVelocityX()
    {
        return velocityX;
    }

    public float GetVelocityZ()
    {
        return velocityZ;
    }

    public void SetVelocityZero()
    {
        rBody.velocity = Vector3.zero ;
    }
}
