/*
 *	作成者     :村上和樹
 *	機能説明   :ゴール判定
 * 	初回作成日 :2018/04/15
 *	最終更新日 :2018/04/16
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village {

    public class GoalFlagment : MonoBehaviour {
        private void OnTriggerEnter(Collider other) {
            if(other.tag == "Player") {
                GameMaster.getInstance.SetGameMode(GameMaster.GameMode.GameClear); //MainManagerでGameModeをEndに切り替える
            }
        }
        private void OnTriggerEnter2D(Collider2D collision) {

        }
    }
}