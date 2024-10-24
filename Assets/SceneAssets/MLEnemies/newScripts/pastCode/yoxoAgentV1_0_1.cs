using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using GameStates;
using System;

[RequireComponent(typeof(ml_manager))]

public class yoxoAgentV1_0_1 : Agent
{
    Rigidbody rBody;
    public players mySide;
    public float offset_behind = -0.1f;
    public float time_limit_behind = 3f;

    ml_manager MLManager;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        MLManager = GetComponent<ml_manager>();
    }

    public Transform Target;
    public override void OnEpisodeBegin()
    {
        MLManager.setTime(0f);
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

        // 位置の補正（少しパックより後ろにずらす）
        Vector3 fixedTFPaddle = 
            new Vector3(this.transform.localPosition.x,
            this.transform.localPosition.y, 
            this.transform.localPosition.z + 1.4f + offset_behind);

        // 時間ごとにrewardを減らす
        AddReward(-0.1f*Time.deltaTime);

        //残り時間を減らす
        MLManager.AddTime(-1f * Time.deltaTime);


        // パックより少し後ろに時間停止ゾーンを調整
        float distanceToTarget = Vector3.Distance(fixedTFPaddle, Target.localPosition);

        // （すこしパックより後ろ側にいるあいだは、残り時間は減らない）
        if (distanceToTarget < 0.4f)
        {
            // rewardを増やす（一定に保つ）
            AddReward(0.1f * Time.deltaTime);
            //残り時間を増やす（一定に保つ）
            MLManager.AddTime(0.1f * Time.deltaTime);

            //time_limit_behindを減らす
            time_limit_behind += -1f * Time.deltaTime;

            if (time_limit_behind < 0)// 一定時間を過ぎるとrewardを-1にして終了
            {
                SetReward(-1f);
                EndEpisode();
            }
        }

        // パックに触ってないときはtime_limit_behindを増やす
        time_limit_behind += 0.3f;

        if (MLManager.score_player == 1)
        {
            if (mySide == players.player) 
            {
                SetReward(1.0f);
                MLManager.score_player = 0;
            }
            else
            {
                SetReward(-1.0f);
            }
            EndEpisode();
        }
        if (MLManager.score_enemy == 1)
        {
            if (mySide == players.enemy)
            {
                SetReward(1.0f);
                MLManager.score_enemy = 0;
            }
            else
            {
                SetReward(-1.0f);
            }
            EndEpisode();
        }

        // 一定時間パックに触らなかったら終了
        if (MLManager.getTime() < -10)
        {
            MLManager.PosInitialize();
            EndEpisode();
        }

        // 残り時間が一定時間以上になると終了
        if (MLManager.getTime() > 10)
        {
            MLManager.PosInitialize();
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