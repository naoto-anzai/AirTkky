using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using GameStates;
using System;

[RequireComponent(typeof(ml_manager))]

public class yoxoAgentV1_0_0 : Agent
{
    Rigidbody rBody;
    public players mySide;

    ml_manager MLManager;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        MLManager = GetComponent<ml_manager>();
    }

    public Transform Target;
    public override void OnEpisodeBegin()
    {
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

        // �ʒu�̕␳
        Vector3 fixedTFPaddle = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z + 1.4f);

        // ���Ԃ��Ƃ�reward�����炷
        AddReward(-0.005f);

        // Rewards
        float distanceToTarget = Vector3.Distance(fixedTFPaddle, Target.localPosition);

        // Reached target
        if (distanceToTarget < 0.4f)
        {
            AddReward(0.05f);
            MLManager.AddTime(0.5f * Time.deltaTime); //�c�莞�Ԃ𑝂₷
        }

        MLManager.AddTime(-0.1f * Time.deltaTime); //�p�b�N�ɐG���ĂȂ��Ƃ��͎��Ԃ����炷

        if (MLManager.score_player == 1)
        {
            if (mySide == players.player) 
            {
                SetReward(1.0f);
                MLManager.score_player = 0;
            }
            else
            {
                //SetReward(-1.0f);
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
                //SetReward(-1.0f);
            }
            EndEpisode();
        }

        // ��莞�ԃp�b�N�ɐG��Ȃ�������I��
        if (MLManager.getTime() < 0)
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