using System.Collections;
using System.Collections.Specialized;
using UnityEngine;
using GameStates;
using System.ComponentModel.Design.Serialization;

namespace PackScoreManager
{
    public class pack_score_manager_bug : MonoBehaviour
    {
        [SerializeField] GameAudioManager audioManager;

        public int score_player = 0; 
        public int score_enemy = 0; 

        pack_game_manager PackGameManager;
        handle_game_manager HandleGameManager;

        [SerializeField] ScoreUIManagerBug scoreUIManager;

        void Start()
        {
            PackGameManager = FindObjectOfType<pack_game_manager>();
            HandleGameManager = FindObjectOfType<handle_game_manager>();
        }

        private void OnTriggerEnter(Collider Wall)
        {
            StartCoroutine(checkScore(Wall));
        }

        private IEnumerator checkScore(Collider Wall)
        {
            if (Wall.CompareTag("WallPlayer")) 
            {
                SendToWorldEnd();
                score_player += 1;
                audioManager.PlayWhenScored();

                yield return StartCoroutine(scoreUIManager.UpdateScore(score_player, score_enemy));
                if (score_player == 7) yield break;
                PackGameManager.StartRalley(players.player);
                HandleGameManager.StartRalley();
                
            }
            else if (Wall.CompareTag("WallEnemy")) 
            {
                SendToWorldEnd();
                score_enemy += 1;
                audioManager.PlayWhenLost();

                yield return StartCoroutine(scoreUIManager.UpdateScore(score_player, score_enemy));
                if (score_enemy == 7) yield break;
                PackGameManager.StartRalley(players.enemy);
                HandleGameManager.StartRalley();
            }
        }

        private void SendToWorldEnd()
        {
            transform.position = new Vector3(7777, 7777, 7777);
        }
    }
}
