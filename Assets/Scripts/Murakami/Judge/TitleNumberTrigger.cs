/*
 *	作成者     :村上和樹
 *	機能説明   :タイトル画面のステージを決める数字
 * 	初回作成日 :2018/05/21
 *	最終更新日 :2018/05/21
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village {

    public class TitleNumberTrigger : MonoBehaviour {
        [SerializeField] private TitlePlayer player;
        [SerializeField] private string nextSceneName = "";
        private void OnTriggerStay(Collider other) {
            if(other.tag != "Player") return;
            player.SetIsCollider(true);
            player.SetNextScene(nextSceneName);
        }

        private void OnTriggerExit(Collider other) {
            if(other.tag != "Player") return;
            player.SetIsCollider(false);
            player.SetNextScene("");
        }
    }

}