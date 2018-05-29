/*
 *	作成者     :村上和樹
 *	機能説明   :敵出現位置の視覚化
 * 	初回作成日 :2018/05/29
 *	最終更新日 :2018/05/29
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village {

    public class EnemyGenerationGate : MonoBehaviour {
        [SerializeField] private Vector3 rotate;


        private void Update() {
            if(EnemySpawn.ActiveNumber != -1) {
                transform.position = EnemySpawn.GenerationPoint;
                transform.Rotate(rotate);
            }

        }

    }

}