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
        // �������̏�����
        time_limit = 0f;
        Target.localPosition = TargetInitPos;
        TargetManager.SetVelocityZero();

        // count��10�ɂȂ�����G�[�W�F���g��������Ԃ֖߂�
        if (count > 10)
        {
            count = 0;
            Agent.localPosition = AgentInitPos;
            YoxoAgent.SetVelocityZero();
        }
        // �p�b�N�̎ˏo
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

        // �ʒu�̕␳�i�����p�b�N�����ɂ��炷�j
        Vector3 fixedTFPaddle =
            new Vector3(Agent.localPosition.x,
            Agent.localPosition.y,
            Agent.localPosition.z + 1.4f + offset_behind);

        // ���Ԃ��Ƃ�reward�����炷
        AddReward(-0.1f * Time.deltaTime);

        //�c�莞�Ԃ����炷
        time_limit += -1f * Time.deltaTime;

        // �p�b�N��菭�����Ɏ��Ԓ�~�]�[���𒲐�
        float distanceToTarget = Vector3.Distance(fixedTFPaddle, Target.localPosition);

        // ��莞�ԃp�b�N�ɐG��Ȃ�������I��
        if (time_limit < -10)
        {
            EndEpisode();
        }

        // �c�莞�Ԃ���莞�Ԉȏ�ɂȂ�ƏI��
        if (time_limit > 10)
        {
            EndEpisode();
        }

        // �p�b�N������w�n�֐i�ނ�rewarg��1�ɐݒ肵�ďI��
        if (TargetManager.GetVelocityZ() * (float)mySide > 0)
        {
            AddReward(0.1f * Time.deltaTime);
            time_limit += 1f * Time.deltaTime;
        }

        // ���̋������S��藣�ꂽ��Areward��-1�ɐݒ肵�ďI��
        if (Target.localPosition.z < (float)mySide * Outline)
        {
            SetReward(-1.0f);

            Target.localPosition = TargetInitPos;
            TargetManager.SetVelocityZero();
            Agent.localPosition = AgentInitPos;
            YoxoAgent.SetVelocityZero();

            EndEpisode();
        }

        // �G�[�W�F���g��x���W�ƃ^�[�Q�b�g��x���W���߂���΋߂��ق�reward�𑝂₷
        float DistanceOfX = (Agent.localPosition.x - Target.localPosition.x)
                            * (Agent.localPosition.x - Target.localPosition.x);
        if ((DistanceOfX <= Xlifeline) && (DistanceOfX >= Xlifeline))
        {
            if (TargetManager.GetVelocityZ() * (float)mySide < 0)
            {
                AddReward(0.1f * Time.deltaTime);
            }
            AddReward(0.1f * Time.deltaTime);

            // エージェントが動いてない間はrewardを増やし続ける
            float AgentVelocity = YoxoAgent.GetVelocityX() * YoxoAgent.GetVelocityZ();
            if((AgentVelocity >= 0 - MaxDynaToReward) && (AgentVelocity <= 0 + MaxDynaToReward))
            {
                AddReward(0.1f * Time.deltaTime);
            }
        }

        // 得点したらrewardを１に設定して終了
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