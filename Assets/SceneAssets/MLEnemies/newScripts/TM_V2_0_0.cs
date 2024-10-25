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

    TargetManagerV2_0_0�@TargetManager;
    YoxoAgentV2_0_0�@YoxoAgent;

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
        // �������̏�����
        time_limit = 0f;
        PosInitialize();
        TargetManager.SetVelocityZero();
        YoxoAgent.SetVelocityZero();

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
        YoxoAgent.AddForce(controlSignal * forceMultiplierAgent * Time.deltaTime);

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
            SetReward(1.0f);
            EndEpisode();
        }

        // ���̋������S��藣�ꂽ��Areward��-1�ɐݒ肵�ďI��
        if (Target.localPosition.z < (float)mySide * Outline)
        {
            SetReward(-1.0f);
            EndEpisode();
        }

        // �G�[�W�F���g��x���W�ƃ^�[�Q�b�g��x���W���߂���΋߂��ق�reward�𑝂₷
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

    // BeginEpisode���̈ʒu�̏�����
    public void PosInitialize()
    {
        Target.localPosition = TargetInitPos;
        Agent.localPosition = AgentInitPos;
    }
}