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

    // �p�b�N�̎ˏo
    public void ShootTarget()
    {
        // �p�b�N�������_���Ȉʒu�Ɉړ�(x���W���������_���Az���W�͈��ő���w�n)
        this.transform.localPosition = new Vector3(UnityEngine.Random.value * 2 - 1,
                                           InitPos.y,
                                           InitPos.z 
                                           + (float)TrainingManager.mySide);
        // ����������O�Ƃ��Ċp�x��ݒ�
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
