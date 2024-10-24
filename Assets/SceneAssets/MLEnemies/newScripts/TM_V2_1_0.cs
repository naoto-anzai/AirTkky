using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using GameStates;
using System;
using System.Threading;

public class TM_V2_1_0 : Agent
{
    public players mySide;

    public float offset_behind = 0.1f;
    public float Xlifeline = 0.4f;
    public float Outline = -2.3f;
    public float MaxDynaToReward = 1;

    int isSwitch = 0;
    int count = 0;

    public int MaxCount = 0;

    public float time_limit = 0f;
    public float time_limit_behind = 3f;

    public float forceMultiplierTarget = 2f;
    public float forceMultiplierAgent = 10f;

    public Vector3 TargetInitPos, AgentInitPos;

    TargetManagerV2_1_0 TargetManager;
    YoxoAgentV4_0_0 YoxoAgent;

    public Transform Target;
    public Transform Agent;

    void Start()
    {
        TargetManager = FindObjectOfType<TargetManagerV2_1_0>();
        YoxoAgent = FindObjectOfType<YoxoAgentV4_0_0>();

        TargetInitPos = Target.localPosition;
        AgentInitPos = Agent.localPosition;

        count = 0;
    }

    public override void OnEpisodeBegin()
    {
        count++;
        // もろもろの初期化
        time_limit = 0f;

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

    void Update()
    {
        //YoxoAgent.transPosition(Target.localPosition.x - Agent.localPosition.x, 0, 0);
        //
        //
        YoxoAgent.AddForce(new Vector3(Target.localPosition.x - Agent.localPosition.x, 0, 0));
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Actions, size = 2
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0] + 1;
        controlSignal.z = actionBuffers.ContinuousActions[1];

        // 位置の補正（少しパックより後ろにずらす）
        Vector3 fixedTFPaddle =
            new Vector3(Agent.localPosition.x,
            Agent.localPosition.y,
            Agent.localPosition.z + 1.4f + (float)mySide * offset_behind);

        // 時間ごとにrewardを減らす
        AddReward(-0.1f * Time.deltaTime);

        //残り時間を減らす
        time_limit += -1f * Time.deltaTime;

        // 一定時間パックに触らなかったら終了
        if (time_limit < -10)
        {
            Target.localPosition = TargetInitPos;
            TargetManager.SetVelocityZero();
            Agent.localPosition = AgentInitPos;
            YoxoAgent.SetVelocityZero();

            EndEpisode();
        }

        // 残り時間が一定時間以上になると終了
        if (time_limit > 10)
        {
            Target.localPosition = TargetInitPos;
            TargetManager.SetVelocityZero();
            Agent.localPosition = AgentInitPos;
            YoxoAgent.SetVelocityZero();

            EndEpisode();
        }

        // パックが相手陣地へ進むとrewardと時間を増やし続ける
        if (TargetManager.GetVelocityZ() * (float)mySide > 0)
        {
            AddReward(1f * Time.deltaTime);
            time_limit += 1f * Time.deltaTime;
        }

        // パックが動いてる間はrewardを増やし続ける
        float PackVelocity = TargetManager.GetVelocityMag();
        if (PackVelocity <= MaxDynaToReward)
        {
            AddReward(1f * Time.deltaTime);
        }

        /*// パックより少し後ろに時間停止ゾーンを調整
        float distanceToTarget = Vector3.Distance(fixedTFPaddle, Target.localPosition);

        // （すこしパックより後ろ側にいるあいだは、残り時間は減らない）
        if (distanceToTarget < 0.4f)
        {
            // rewardを増やす（一定に保つ）
            AddReward(1f * Time.deltaTime);
            //残り時間を増やす（一定に保つ）
            time_limit += (1f * Time.deltaTime);

            //time_limit_behindを減らす
            time_limit_behind += -1f * Time.deltaTime;

            if (time_limit_behind < 0)// 一定時間を過ぎるとrewardを-1して終了
            {
                AddReward(-1f);
                EndEpisode();
            }
        }

        // パックに触ってないときはtime_limit_behindを増やす
        time_limit_behind += 0.3f;
        */

        /* // 一定の距離中心より離れたら、rewardを0に設定して終了
        if ((float)mySide * Target.localPosition.z < Outline)
        {
            SetReward(0f);

            Target.localPosition = TargetInitPos;
            TargetManager.SetVelocityZero();
            Agent.localPosition = AgentInitPos;
            YoxoAgent.SetVelocityZero();

            EndEpisode();
        }*/

        // エージェントのx座標とターゲットのx座標が近ければrewardを増やし続ける
        float DistanceOfX = (Agent.localPosition.x - Target.localPosition.x)
                            * (Agent.localPosition.x - Target.localPosition.x);
        if (DistanceOfX <= Xlifeline)
        {
            AddReward(1f * Time.deltaTime);

            /*// エージェントが動いてない間はrewardを増やし続ける
            float AgentVelocity = YoxoAgent.GetVelocityX() * YoxoAgent.GetVelocityZ();
            if((AgentVelocity >= 0 - MaxDynaToReward) && (AgentVelocity <= 0 + MaxDynaToReward))
            {
                AddReward(0.1f * Time.deltaTime);
            }*/
            // エージェントがパックより後ろにいるとき、rewardを増加
            if ((float)mySide * Target.localPosition.z * (-1) < (float)mySide * (Agent.localPosition.z + 1.4f) * (-1))
            {
                AddReward(1f * Time.deltaTime);

                // エージェントがパックの方向に動くとrewardを増やす
                if ((float)mySide * YoxoAgent.GetVelocityZ() > 0)
                {
                    AddReward(1f * Time.deltaTime);
                }
            }
            else
            {
                controlSignal.x += -2;
                controlSignal.z += 1;
            }
        }
        else
        {

        }

        YoxoAgent.AddForce(new Vector3(controlSignal.x * forceMultiplierAgent * (Target.localPosition.x - Agent.localPosition.x),
                                       0,
                                       controlSignal.z * forceMultiplierAgent * (Target.localPosition.z - Agent.localPosition.z - 1.4f)));


        // 得点したらrewardを１に設定して終了
        if (TargetManager.score_player == 1)
        {
            if (mySide == players.enemy)
            {
                AddReward(1.0f);
                TargetManager.score_player = 0;
            }
            else
            {
                AddReward(-1.0f);
            }
            EndEpisode();
        }
        if (TargetManager.score_enemy == 1)
        {
            if (mySide == players.player)
            {
                AddReward(1.0f);
                TargetManager.score_enemy = 0;
            }
            else
            {
                AddReward(-1.0f);
            }
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