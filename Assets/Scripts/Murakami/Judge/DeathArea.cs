/*
 *	作成者     :村上和樹
 *	機能説明   :プレイヤー2Dが入ったらスタート地点に戻す
 * 	初回作成日 :2018/04/15
 *	最終更新日 :2018/04/16
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village {

    public class DeathArea : Inheritor {
        [SerializeField] private Transform player2dTf;
        [SerializeField] private Rigidbody playerRigid;

        [SerializeField] private GameObject deadEffect;

        public override void Run() {
            if(CheckScreenOut(player2dTf.position)) {
                BackToStartPoint();
            }
        }

        /// <summary>
        /// カメラ外かどうかの判定
        /// </summary>
        private bool CheckScreenOut(Vector3 _pos) {
            Vector3 view_pos = Camera.main.WorldToViewportPoint(_pos);
            if(view_pos.y < -0.0f) {
                // 範囲外 
                return true;
            }
            else {
                // 範囲内 
                return false;
            }
        }

        /// <summary>
        /// Start地点に戻す
        /// </summary>
        private void BackToStartPoint() {
            if(GameMaster.getInstance.GetGameMode == GameMaster.GameMode.GameOver) return;

            PlayEffect();
            GameMaster.getInstance.SetGameMode(GameMaster.GameMode.GameOver);
        }

        /// <summary>
        /// 死亡エフェクトの生成
        /// </summary>
        private void PlayEffect() {
            GameObject effect = Instantiate(deadEffect) as GameObject;

            float x = player2dTf.position.x;
            float y = effect.transform.position.y;
            float z = effect.transform.position.z;

            effect.transform.position = new Vector3(x,y,z);
        }

    }

}