/*
 *	作成者     :村上和樹
 *	機能説明   :中間ポイント
 * 	初回作成日 :2018/05/13
 *	最終更新日 :2018/05/13
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village {

    public class CheckPoint : MonoBehaviour {
        [SerializeField] private DeathArea deathArea;

        private void OnTriggerEnter(Collider other) {
            if(other.tag != "Player") return;

            deathArea.SetReStartPoint(transform.position);
        }


    }

}