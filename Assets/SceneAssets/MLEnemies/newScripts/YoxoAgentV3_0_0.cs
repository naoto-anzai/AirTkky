using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoxoAgentV3_0_0 : MonoBehaviour
{
    Rigidbody rBody;

    TM_V3_0_0 TrainingManager;

    public Transform Parent;

    public float offset_paddle = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();

        TrainingManager = FindObjectOfType<TM_V3_0_0>();
    }

    // Update is called once per frame
    void Update()
    {
        // 相手陣地へ行ってしまったとき
        if((float)TrainingManager.mySide * Parent.localPosition.z 
           > 0f - (float)TrainingManager.mySide * offset_paddle)
        {
            Parent.transform.localPosition = new Vector3(
            Parent.localPosition.x,
            Parent.localPosition.y,
            0 - (float)TrainingManager.mySide * offset_paddle);
        }

        // 外へ行ってしまったとき(xが-)
        if ((float)TrainingManager.mySide * Parent.localPosition.x
           < -1.5f + (float)TrainingManager.mySide * offset_paddle)
        {
            Parent.transform.localPosition = new Vector3(
            -1.5f + (float)TrainingManager.mySide * offset_paddle,
            Parent.localPosition.y,
            Parent.localPosition.z);
        }

        // 外へ行ってしまったとき(xが+)
        if ((float)TrainingManager.mySide * Parent.localPosition.x
           > 1.5f - (float)TrainingManager.mySide * offset_paddle)
        {
            Parent.transform.localPosition = new Vector3(
            1.5f - (float)TrainingManager.mySide * offset_paddle,
            Parent.localPosition.y,
            Parent.localPosition.z);
        }

        // 外へ行ってしまったとき(zがmySide側)
        if ((float)TrainingManager.mySide * Parent.localPosition.z
           < -2.8f + (float)TrainingManager.mySide * offset_paddle)
        {
            Parent.transform.localPosition = new Vector3(
            Parent.localPosition.x,
            Parent.localPosition.y,
            -2.8f + (float)TrainingManager.mySide * offset_paddle);
        }

    }

    public void transPosition(float x, float y, float z)
    {
        Parent.transform.localPosition = new Vector3(
            Parent.localPosition.x + x,
            Parent.localPosition.y + y,
            Parent.localPosition.z + z);
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
