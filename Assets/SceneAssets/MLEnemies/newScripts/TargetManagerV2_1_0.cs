using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameStates;

public class TargetManagerV2_1_0 : MonoBehaviour
{
    Rigidbody rBody;

    TM_V2_1_0 TrainingManager;
    pack_game_manager PackGameManager;
    handle_game_manager HandleGameManager;

    public Vector3 InitPos;
    public int score_player;
    public int score_enemy;
   
    
    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody> ();

        TrainingManager = FindObjectOfType<TM_V2_1_0>();
        PackGameManager = FindObjectOfType<pack_game_manager>();
        HandleGameManager = FindObjectOfType<handle_game_manager>();

        InitPos = this.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider Wall)
    {
        if (Wall.CompareTag("WallPlayer")) // "WallPlayer" �^�O�����I�u�W�F�N�g�ƏՓ˂�����
        {
            score_player = 1;
            PackGameManager.StartRalley(players.player);
            HandleGameManager.StartRalley();

        }
        else if (Wall.CompareTag("WallEnemy")) // "WallEnemy" �^�O�����I�u�W�F�N�g�ƏՓ˂�����
        {
            score_enemy = 1;
            PackGameManager.StartRalley(players.enemy);
            HandleGameManager.StartRalley();
        }
    }

    public float GetVelocityX()
    {
        return rBody.velocity.x;
    }

    public float GetVelocityZ()
    {
        return rBody.velocity.z;
    }

    public void SetVelocityZero()
    {
        rBody.velocity = Vector3.zero ;
    }
}
