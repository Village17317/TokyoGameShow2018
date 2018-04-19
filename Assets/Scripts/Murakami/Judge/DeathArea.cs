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

    public class DeathArea : MonoBehaviour {

        [SerializeField] private StartFlagment startFlagment;
        [SerializeField] private Rigidbody2D player2DRigid;
        private void OnTriggerEnter2D(Collider2D collision) {
            if(collision.tag == "Player") {
                player2DRigid.velocity = Vector3.zero;
                startFlagment.ReSpawn();
                GameMaster.getInstance.SetGameMode(GameMaster.GameMode.GameReStart);
                GameMaster.getInstance.DeadCountUp();//MainManagerで死んだ回数をカウントする
            }
        }

    }

}