using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManagerV2_0_0 : MonoBehaviour
{
    Rigidbody rBody;

    TM_V3_0_0 TrainingManager;

    public Vector3 InitPos;
    
    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody> ();

        TrainingManager = FindObjectOfType<TM_V3_0_0>();

        InitPos = this.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // パックの射出
    public void ShootTarget()
    {
        // パックをランダムな位置に移動(x座標だけランダム、z座標は一定で相手陣地)
        this.transform.localPosition = new Vector3(UnityEngine.Random.value * 2 - 1,
                                           InitPos.y,
                                           InitPos.z 
                                           + (float)TrainingManager.mySide);
        // 相手方向を０として角度を設定
        float angle = UnityEngine.Random.value * (float)Math.PI / 3 - (float)Math.PI / 6;

        rBody.AddForce(new Vector3((float)Math.Sin(angle),
                                         0f,
                                         (float)Math.Cos(angle))
                                         * TrainingManager.forceMultiplierTarget 
                                         * -(float)TrainingManager.mySide);
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
