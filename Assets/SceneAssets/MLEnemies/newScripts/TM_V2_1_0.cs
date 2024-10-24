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

    int isSwitch = 0;
    int count = 0;

    public float time_limit = 0f;
    public float time_limit_behind = 3f;

    public float forceMultiplierTarget = 2f;
    public float TransMultiplierAgent = 10f;

    public Vector3 TargetInitPos, AgentInitPos;

    TargetManagerV2_0_0 TargetManager;
    YoxoAgentV3_0_0 YoxoAgent;

    public Transform Target;
    public Transform Agent;

    void Start()
    {
        TargetManager = FindObjectOfType<TargetManagerV2_0_0>();
        YoxoAgent = FindObjectOfType<YoxoAgentV3_0_0>();

        TargetInitPos = Target.localPosition;
        AgentInitPos = Agent.localPosition;

        count = 0;
    }

    public override void OnEpisodeBegin()
    {
        count += 1;
        // ‚à‚ë‚à‚ë‚Ì‰Šú‰»
        time_limit = 0f;
        Target.localPosition = TargetInitPos;
        TargetManager.SetVelocityZero();

        // count‚ª10‚É‚È‚Á‚½‚çƒG[ƒWƒFƒ“ƒg‚ğ‰Šúó‘Ô‚Ö–ß‚·
        if (count > 10)
        {
            count = 0;
            Agent.localPosition = AgentInitPos;
            YoxoAgent.SetVelocityZero();
        }
        // ƒpƒbƒN‚ÌËo
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
        YoxoAgent.transPosition(controlSignal.x * TransMultiplierAgent * Time.deltaTime,
                                0,
                                controlSignal.z * TransMultiplierAgent * Time.deltaTime);

        // ˆÊ’u‚Ì•â³i­‚µƒpƒbƒN‚æ‚èŒã‚ë‚É‚¸‚ç‚·j
        Vector3 fixedTFPaddle =
            new Vector3(Agent.localPosition.x,
            Agent.localPosition.y,
            Agent.localPosition.z + 1.4f + offset_behind);

        // ŠÔ‚²‚Æ‚Éreward‚ğŒ¸‚ç‚·
        AddReward(-0.1f * Time.deltaTime);

        //c‚èŠÔ‚ğŒ¸‚ç‚·
        time_limit += -1f * Time.deltaTime;

        // ƒpƒbƒN‚æ‚è­‚µŒã‚ë‚ÉŠÔ’â~ƒ][ƒ“‚ğ’²®
        float distanceToTarget = Vector3.Distance(fixedTFPaddle, Target.localPosition);

        // ˆê’èŠÔƒpƒbƒN‚ÉG‚ç‚È‚©‚Á‚½‚çI—¹
        if (time_limit < -10)
        {
            EndEpisode();
        }

        // c‚èŠÔ‚ªˆê’èŠÔˆÈã‚É‚È‚é‚ÆI—¹
        if (time_limit > 10)
        {
            EndEpisode();
        }

        // ƒpƒbƒN‚ª‘Šèw’n‚Öi‚Ş‚Ærewarg‚ğ1‚Éİ’è‚µ‚ÄI—¹
        if (TargetManager.GetVelocityZ() * (float)mySide > 0)
        {
            AddReward(0.1f * Time.deltaTime);
            time_limit += 1f * Time.deltaTime;
        }

        // ˆê’è‚Ì‹——£’†S‚æ‚è—£‚ê‚½‚çAreward‚ğ-1‚Éİ’è‚µ‚ÄI—¹
        if (Target.localPosition.z < (float)mySide * Outline)
        {
            SetReward(-1.0f);

            Target.localPosition = TargetInitPos;
            TargetManager.SetVelocityZero();
            Agent.localPosition = AgentInitPos;
            YoxoAgent.SetVelocityZero();

            EndEpisode();
        }

        // ƒG[ƒWƒFƒ“ƒg‚ÌxÀ•W‚Æƒ^[ƒQƒbƒg‚ÌxÀ•W‚ª‹ß‚¯‚ê‚Î‹ß‚¢‚Ù‚Çreward‚ğ‘‚â‚·
        float DistanceOfX = (Agent.localPosition.x - Target.localPosition.x)
                            * (Agent.localPosition.x - Target.localPosition.x);
        if ((DistanceOfX <= Xlifeline) && (DistanceOfX >= Xlifeline))
        {
            if (TargetManager.GetVelocityZ() * (float)mySide < 0)
            {
                AddReward(0.1f * Time.deltaTime);
            }
            AddReward(0.1f * Time.deltaTime);

            // ã‚¨ãƒ¼ã‚¸ã‚§ãƒ³ãƒˆãŒå‹•ã„ã¦ãªã„é–“ã¯rewardã‚’å¢—ã‚„ã—ç¶šã‘ã‚‹
            float AgentVelocity = YoxoAgent.GetVelocityX() * YoxoAgent.GetVelocityZ();
            if((AgentVelocity >= 0 - MaxDynaToReward) && (AgentVelocity <= 0 + MaxDynaToReward))
            {
                AddReward(0.1f * Time.deltaTime);
            }
        }

        // å¾—ç‚¹ã—ãŸã‚‰rewardã‚’ï¼‘ã«è¨­å®šã—ã¦çµ‚äº†
        if (TargetManager.score_player == 1)
        {
            if (mySide == players.player)
            {
                SetReward(1.0f);
                TargetManager.score_player = 0;
            }
            else
            {
                SetReward(-1.0f);
            }
            EndEpisode();
        }
        if (TargetManager.score_enemy == 1)
        {
            if (mySide == players.enemy)
            {
                SetReward(1.0f);
                TargetManager.score_enemy = 0;
            }
            else
            {
                SetReward(-1.0f);
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