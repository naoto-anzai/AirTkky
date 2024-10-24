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

        // �ʒu�̕␳�i�����p�b�N�����ɂ��炷�j
        Vector3 fixedTFPaddle = 
            new Vector3(this.transform.localPosition.x,
            this.transform.localPosition.y, 
            this.transform.localPosition.z + 1.4f + offset_behind);

        // ���Ԃ��Ƃ�reward�����炷
        AddReward(-0.1f*Time.deltaTime);

        //�c�莞�Ԃ����炷
        MLManager.AddTime(-1f * Time.deltaTime);


        // �p�b�N��菭�����Ɏ��Ԓ�~�]�[���𒲐�
        float distanceToTarget = Vector3.Distance(fixedTFPaddle, Target.localPosition);

        // �i�������p�b�N����둤�ɂ��邠�����́A�c�莞�Ԃ͌���Ȃ��j
        if (distanceToTarget < 0.4f)
        {
            // reward�𑝂₷�i���ɕۂj
            AddReward(0.1f * Time.deltaTime);
            //�c�莞�Ԃ𑝂₷�i���ɕۂj
            MLManager.AddTime(0.1f * Time.deltaTime);

            //time_limit_behind�����炷
            time_limit_behind += -1f * Time.deltaTime;

            if (time_limit_behind < 0)// ��莞�Ԃ��߂����reward��-1�ɂ��ďI��
            {
                SetReward(-1f);
                EndEpisode();
            }
        }

        // �p�b�N�ɐG���ĂȂ��Ƃ���time_limit_behind�𑝂₷
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

        // ��莞�ԃp�b�N�ɐG��Ȃ�������I��
        if (MLManager.getTime() < -10)
        {
            MLManager.PosInitialize();
            EndEpisode();
        }

        // �c�莞�Ԃ���莞�Ԉȏ�ɂȂ�ƏI��
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