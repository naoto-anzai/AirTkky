using System.Collections.Specialized;
using UnityEngine;
using GameStates;

public class ml_manager : MonoBehaviour
{
    public float time_limit;
    public float score_player;
    public float score_enemy;

    public Vector3 PuckInitPos;

    pack_game_manager PackGameManager;
    handle_game_manager HandleGameManager;

    // �Q�[���J�n���ɌĂ΂�郁�\�b�h
    void Start()
    {
        PackGameManager = FindObjectOfType<pack_game_manager>();
        HandleGameManager = FindObjectOfType<handle_game_manager>();

        time_limit = 0;
        PuckInitPos = this.transform.localPosition;
    }

    // �ǂɓ����������ɌĂ΂�郁�\�b�h
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

    public void AddTime(float add_time)
    {
        time_limit += add_time; 
    }

    public float getTime() 
    {
        return time_limit;
    }

    public void setTime(float set_time)
    {
        time_limit = set_time;
    }

    public void PosInitialize()
    {
        this.transform.localPosition = PuckInitPos;
        HandleGameManager.StartRalley();
    }
}
