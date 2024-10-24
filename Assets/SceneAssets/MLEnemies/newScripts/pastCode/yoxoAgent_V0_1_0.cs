using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class yoxoAgent_V0_1_0 : Agent
{
    Rigidbody rBody;
    public int side;
    public Vector3 NowPosAgent;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        side = 1;

        NowPosAgent = this.transform.localPosition;
    }

    public Transform Target;
    public override void OnEpisodeBegin()
    {
        // ランダムに場所の決定
        if(Random.value < 0.5f)
        {
            side = 1;
        }
        else
        {
            side = -1;
        }

        // Move the target to a new spot
        Target.localPosition = new Vector3(Random.value * 2 - 1,
                                           0.23f,
                                           side * Random.value * (2.6f) );
        // エージェントの位置も動かす

        this.transform.localPosition = new Vector3(side * NowPosAgent.x,
                                           NowPosAgent.y,
                                           side * (NowPosAgent.z + 1.4f) -1.4f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Target and Agent positions
        sensor.AddObservation(Target.localPosition);
        sensor.AddObservation(this.transform.localPosition);

        // Agent velocity
        sensor.AddObservation(rBody.velocity.x);
        sensor.AddObservation(rBody.velocity.z);
    }

    public float forceMultiplier = 10;
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Actions, size = 2
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.z = actionBuffers.ContinuousActions[1];
        rBody.AddForce(controlSignal * forceMultiplier);

        // 現在位置の取得
        NowPosAgent = this.transform.localPosition;
        
        // 原因不明の、sideどおりに移動してくれないバグ対策
        if ((Target.localPosition.z * (NowPosAgent.z +1.4f)) <= 0)
        {
            EndEpisode();
        }
        // 位置の補正
        Vector3 fixedTFPaddle = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z + 1.4f);

        // 時間ごとにrewardを減らす
        AddReward(-0.005f);


        // Rewards
        float distanceToTarget = Vector3.Distance(fixedTFPaddle, Target.localPosition);

        // Reached target
        if (distanceToTarget < 0.5f)
        {
            SetReward(1.0f);
            NowPosAgent = this.transform.localPosition;
            EndEpisode();
        }


    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }

}