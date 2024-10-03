using System.Collections.Specialized;
using UnityEngine;
using GameStates;

namespace PackScoreManager
{
    public class pack_score_manager : MonoBehaviour
    {
        public int score_player = 0; // ���݂̎��g�̓��_��ێ�����ϐ�
        public int score_enemy = 0;  // ���݂̑���̓��_��ێ�����ϐ�

        pack_game_manager PackGameManager;
        handle_game_manager HandleGameManager;

        ScoreUIManager scoreUIManager;

        // �Q�[���J�n���ɌĂ΂�郁�\�b�h
        void Start()
        {
            PackGameManager = FindObjectOfType<pack_game_manager>();
            HandleGameManager = FindObjectOfType<handle_game_manager>();
        }

        // �ǂɓ����������ɌĂ΂�郁�\�b�h
        private void OnTriggerEnter(Collider Wall)
        {
            if (Wall.CompareTag("WallPlayer")) // "WallPlayer" �^�O�����I�u�W�F�N�g�ƏՓ˂�����
            {
                score_player += 1; // ���_�����Z
                scoreUIManager.UpdateScore(score_player, score_enemy);
                Debug.Log("Score: " + score_player); // ���_��\��
                                                     // �K�v�ɉ����ē��_��UI�ɔ��f�����鏈����ǉ�
                PackGameManager.StartRalley(players.player);
                HandleGameManager.StartRalley();
                
            }
            else if (Wall.CompareTag("WallEnemy")) // "WallEnemy" �^�O�����I�u�W�F�N�g�ƏՓ˂�����
            {
                score_enemy += 1; // ���_�����Z
                scoreUIManager.UpdateScore(score_player, score_enemy);
                Debug.Log("Score: " + score_enemy); // ���_��\��
                                                    // �K�v�ɉ����ē��_��UI�ɔ��f�����鏈����ǉ�

                PackGameManager.StartRalley(players.enemy);
            }
        }
    }
}
