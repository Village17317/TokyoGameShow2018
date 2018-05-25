/*
 *	作成者     :村上和樹
 *	機能説明   :敵の生成
 * 	初回作成日 :2018/05/23
 *	最終更新日 :2018/05/23
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village {

    public class EnemySpawn : MonoBehaviour {
        [SerializeField] private GameObject enemyPrefab;

        /// <summary>
        /// 敵オブジェクトの生成
        /// </summary>
        public Inheritor Spawn() {
            GameObject ene = Instantiate(enemyPrefab) as GameObject;
            ene.transform.position = transform.position;
            return ene.GetComponent<INI.EnemyController>();
        }
    }

}