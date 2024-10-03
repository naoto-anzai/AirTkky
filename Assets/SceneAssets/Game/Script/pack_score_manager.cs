using System.Collections.Specialized;
using UnityEngine;
using GameStates;
using PackGameManager;

namespace PackScoreManager
{
    public class pack_score_manager : MonoBehaviour
    {
        public int score_player = 0; // 現在の自身の得点を保持する変数
        public int score_enemy = 0;  // 現在の相手の得点を保持する変数

        pack_game_manager MyGameManager;
        ScoreUIManager scoreUIManager;

        // ゲーム開始時に呼ばれるメソッド
        void Start()
        {
            MyGameManager = FindObjectOfType<pack_game_manager>();
            scoreUIManager = FindObjectOfType<ScoreUIManager>();
        }

        // 壁に当たった時に呼ばれるメソッド
        private void OnTriggerEnter(Collider Wall)
        {
            if (Wall.CompareTag("WallPlayer")) // "WallPlayer" タグを持つオブジェクトと衝突した時
            {
                score_player += 1; // 得点を加算
                scoreUIManager.UpdateScore(score_player, score_enemy);
                Debug.Log("Score: " + score_player); // 得点を表示
                                                     // 必要に応じて得点をUIに反映させる処理を追加
                MyGameManager.StartRalley(players.enemy);
                
            }
            else if (Wall.CompareTag("WallEnemy")) // "WallEnemy" タグを持つオブジェクトと衝突した時
            {
                score_enemy += 1; // 得点を加算
                scoreUIManager.UpdateScore(score_player, score_enemy);
                Debug.Log("Score: " + score_enemy); // 得点を表示
                                                    // 必要に応じて得点をUIに反映させる処理を追加

                MyGameManager.StartRalley(players.player);
            }
        }
    }
}
