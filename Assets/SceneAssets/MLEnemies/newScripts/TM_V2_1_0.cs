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
        // �������̏�����
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

        // �ʒu�̕␳�i�����p�b�N�����ɂ��炷�j
        Vector3 fixedTFPaddle =
            new Vector3(Agent.localPosition.x,
            Agent.localPosition.y,
            Agent.localPosition.z + 1.4f + (float)mySide * offset_behind);

        // ���Ԃ��Ƃ�reward�����炷
        AddReward(-0.1f * Time.deltaTime);

        //�c�莞�Ԃ����炷
        time_limit += -1f * Time.deltaTime;

        // ��莞�ԃp�b�N�ɐG��Ȃ�������I��
        if (time_limit < -10)
        {
            Target.localPosition = TargetInitPos;
            TargetManager.SetVelocityZero();
            Agent.localPosition = AgentInitPos;
            YoxoAgent.SetVelocityZero();

            EndEpisode();
        }

        // �c�莞�Ԃ���莞�Ԉȏ�ɂȂ�ƏI��
        if (time_limit > 10)
        {
            Target.localPosition = TargetInitPos;
            TargetManager.SetVelocityZero();
            Agent.localPosition = AgentInitPos;
            YoxoAgent.SetVelocityZero();

            EndEpisode();
        }

        // �p�b�N������w�n�֐i�ނ�reward�Ǝ��Ԃ𑝂₵������
        if (TargetManager.GetVelocityZ() * (float)mySide > 0)
        {
            AddReward(1f * Time.deltaTime);
            time_limit += 1f * Time.deltaTime;
        }

        // �p�b�N�������Ă�Ԃ�reward�𑝂₵������
        float PackVelocity = TargetManager.GetVelocityMag();
        if (PackVelocity <= MaxDynaToReward)
        {
            AddReward(1f * Time.deltaTime);
        }

        /*// �p�b�N��菭�����Ɏ��Ԓ�~�]�[���𒲐�
        float distanceToTarget = Vector3.Distance(fixedTFPaddle, Target.localPosition);

        // �i�������p�b�N����둤�ɂ��邠�����́A�c�莞�Ԃ͌���Ȃ��j
        if (distanceToTarget < 0.4f)
        {
            // reward�𑝂₷�i���ɕۂj
            AddReward(1f * Time.deltaTime);
            //�c�莞�Ԃ𑝂₷�i���ɕۂj
            time_limit += (1f * Time.deltaTime);

            //time_limit_behind�����炷
            time_limit_behind += -1f * Time.deltaTime;

            if (time_limit_behind < 0)// ��莞�Ԃ��߂����reward��-1���ďI��
            {
                AddReward(-1f);
                EndEpisode();
            }
        }

        // �p�b�N�ɐG���ĂȂ��Ƃ���time_limit_behind�𑝂₷
        time_limit_behind += 0.3f;
        */

        /* // ���̋������S��藣�ꂽ��Areward��0�ɐݒ肵�ďI��
        if ((float)mySide * Target.localPosition.z < Outline)
        {
            SetReward(0f);

            Target.localPosition = TargetInitPos;
            TargetManager.SetVelocityZero();
            Agent.localPosition = AgentInitPos;
            YoxoAgent.SetVelocityZero();

            EndEpisode();
        }*/

        // �G�[�W�F���g��x���W�ƃ^�[�Q�b�g��x���W���߂����reward�𑝂₵������
        float DistanceOfX = (Agent.localPosition.x - Target.localPosition.x)
                            * (Agent.localPosition.x - Target.localPosition.x);
        if (DistanceOfX <= Xlifeline)
        {
            AddReward(1f * Time.deltaTime);

            /*// �G�[�W�F���g�������ĂȂ��Ԃ�reward�𑝂₵������
            float AgentVelocity = YoxoAgent.GetVelocityX() * YoxoAgent.GetVelocityZ();
            if((AgentVelocity >= 0 - MaxDynaToReward) && (AgentVelocity <= 0 + MaxDynaToReward))
            {
                AddReward(0.1f * Time.deltaTime);
            }*/
            // �G�[�W�F���g���p�b�N�����ɂ���Ƃ��Areward�𑝉�
            if ((float)mySide * Target.localPosition.z * (-1) < (float)mySide * (Agent.localPosition.z + 1.4f) * (-1))
            {
                AddReward(1f * Time.deltaTime);

                // �G�[�W�F���g���p�b�N�̕����ɓ�����reward�𑝂₷
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


        // ���_������reward���P�ɐݒ肵�ďI��
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