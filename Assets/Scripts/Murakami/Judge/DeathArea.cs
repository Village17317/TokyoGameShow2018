﻿/*
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

        [SerializeField] private StartFlagment startFlagment;
        [SerializeField] private Transform player2dTf;
        [SerializeField] private Rigidbody playerRigid;

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
            Debug.Log(view_pos.y);
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
            playerRigid.velocity = Vector3.zero;
            startFlagment.ReSpawn();
            GameMaster.getInstance.SetGameMode(GameMaster.GameMode.GameReStart);
            GameMaster.getInstance.DeadCountUp();//MainManagerで死んだ回数をカウントする
        }
    }

}