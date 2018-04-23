﻿/*
 *	作成者     :村上和樹
 *	機能説明   :プレイヤーの初期位置、リスポーン位置
 * 	初回作成日 :2018/04/15
 *	最終更新日 :2018/04/15
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village {

    public class StartFlagment : MonoBehaviour {
        private GameObject player;

        private void Awake(){
            player = GameObject.FindWithTag("Player");
            player.transform.position = transform.position;
		}

        /// <summary>
        /// プレイヤーの位置を再設定
        /// </summary>
        public void ReSpawn() {
            player.transform.position = transform.position;
        }
    }

}