using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using GameStates;
using System;

public class TM_V2_0_0 : Agent
{
    public players mySide;

    public float offset_behind = 0.1f;
    public float Xlifeline = 0.4f;
    public float Outline = -2.3f;

    public float time_limit = 0f; 
    public float time_limit_behind = 3f;

    public float forceMultiplierTarget = 2f;
    public float forceMultiplierAgent = 10f;

    public Vector3 TargetInitPos, AgentInitPos;

    TargetManagerV2_0_0　TargetManager;
    YoxoAgentV2_0_0　YoxoAgent;

    public Transform Target;
    public Transform Agent;

    void Start()
    {
        TargetManager = FindObjectOfType<TargetManagerV2_0_0>();
        YoxoAgent = FindObjectOfType< YoxoAgentV2_0_0>();

        TargetInitPos = Target.localPosition;
        AgentInitPos = Agent.localPosition;
    }

    public override void OnEpisodeBegin()
    {
        // もろもろの初期化
        time_limit = 0f;
        PosInitialize();
        TargetManager.SetVelocityZero();
        YoxoAgent.SetVelocityZero();

        // パックの射出
        TargetManager.ShootTarget();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Target and Agent positions
        sensor.AddObservation(Target.localPosition);
        sensor.AddObservation(Agent.localPosition);

        // Target and Agent velocities
        sensor.AddObservation(TargetManager.GetVelocityX());
        sensor.AddObservation(TargetManager.GetVelocityZ());
        sensor.AddObservation(YoxoAgent.GetVelocityX());
        sensor.AddObservation(YoxoAgent.GetVelocityZ());
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Actions, size = 2
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.z = actionBuffers.ContinuousActions[1];
        YoxoAgent.AddForce(controlSignal * forceMultiplierAgent * Time.deltaTime);

        // 位置の補正（少しパックより後ろにずらす）
        Vector3 fixedTFPaddle =
            new Vector3(Agent.localPosition.x,
            Agent.localPosition.y,
            Agent.localPosition.z + 1.4f + offset_behind);

        // 時間ごとにrewardを減らす
        AddReward(-0.1f * Time.deltaTime);

        //残り時間を減らす
        time_limit += -1f * Time.deltaTime;

        // パックより少し後ろに時間停止ゾーンを調整
        float distanceToTarget = Vector3.Distance(fixedTFPaddle, Target.localPosition);

        // 一定時間パックに触らなかったら終了
        if (time_limit < -10)
        {
            EndEpisode();
        }

        // 残り時間が一定時間以上になると終了
        if (time_limit > 10)
        {
            EndEpisode();
        }

        // パックが相手陣地へ進むとrewargを1に設定して終了
        if (TargetManager.GetVelocityZ() * (float)mySide > 0)
        {
            SetReward(1.0f);
            EndEpisode();
        }

        // 一定の距離中心より離れたら、rewardを-1に設定して終了
        if (Target.localPosition.z < (float)mySide * Outline)
        {
            SetReward(-1.0f);
            EndEpisode();
        }

        // エージェントのx座標とターゲットのx座標が近ければ近いほどrewardを増やす
        float DistanceOfX = (Agent.localPosition.x - Target.localPosition.x) 
                            * (Agent.localPosition.x - Target.localPosition.x);
        if ((DistanceOfX <= Xlifeline) && (DistanceOfX >= Xlifeline))
        {
            AddReward(0.2f * Time.deltaTime);
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }

    // BeginEpisode時の位置の初期化
    public void PosInitialize()
    {
        Target.localPosition = TargetInitPos;
        Agent.localPosition = AgentInitPos;
    }
}