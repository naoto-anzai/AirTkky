using System.Collections.Specialized;
using UnityEngine;
using GameStates;

namespace PackScoreManager
{
    public class pack_score_manager : MonoBehaviour
    {
        public int score_player = 0; // 現在の自身の得点を保持する変数
        public int score_enemy = 0;  // 現在の相手の得点を保持する変数

        pack_game_manager PackGameManager;
        handle_game_manager HandleGameManager;


        // ゲーム開始時に呼ばれるメソッド
        void Start()
        {
            PackGameManager = FindObjectOfType<pack_game_manager>();
            HandleGameManager = FindObjectOfType<handle_game_manager>();
        }

        // 壁に当たった時に呼ばれるメソッド
        private void OnTriggerEnter(Collider Wall)
        {
            if (Wall.CompareTag("WallPlayer")) // "WallPlayer" タグを持つオブジェクトと衝突した時
            {
                score_player += 1; // 得点を加算
                Debug.Log("Score: " + score_player); // 得点を表示
                                                     // 必要に応じて得点をUIに反映させる処理を追加
                PackGameManager.StartRalley(players.player);
                HandleGameManager.StartRalley();
                
            }
            else if (Wall.CompareTag("WallEnemy")) // "WallEnemy" タグを持つオブジェクトと衝突した時
            {
                score_enemy += 1; // 得点を加算
                Debug.Log("Score: " + score_enemy); // 得点を表示
                                                    // 必要に応じて得点をUIに反映させる処理を追加

                PackGameManager.StartRalley(players.enemy);
            }
        }
    }
}
