/*
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
        [SerializeField] private GameObject player;
        [SerializeField] private DeathArea deathArea;
        private void Awake(){
            player.transform.position = transform.position;
		}
    }

}