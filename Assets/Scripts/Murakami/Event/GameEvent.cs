/*
 *	作成者     :村上和樹
 *	機能説明   :Playerが触れたときゲームイベントをだす
 * 	初回作成日 :2018/05/30
 *	最終更新日 :2018/05/30
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Village {

    public class GameEvent : MonoBehaviour {
        [SerializeField] private Image expImage;

        private void Awake(){
            expImage.gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other) {
            if(other.gameObject.tag != "Player") return;
            expImage.gameObject.SetActive(true);
        }

        private void OnTriggerStay(Collider other) {
            if(other.gameObject.tag != "Player") return;
            bool isActive = GameMaster.getInstance.GetGameMode != GameMaster.GameMode.GameClear && GameMaster.getInstance.GetGameMode != GameMaster.GameMode.GameOver;
            expImage.gameObject.SetActive(isActive);
        }

        private void OnTriggerExit(Collider other) {
            if(other.gameObject.tag != "Player") return;
            expImage.gameObject.SetActive(false);
        }
    }

}