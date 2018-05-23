/*
 *	作成者     :村上和樹
 *	機能説明   :敵を生成する指示を出す
 * 	初回作成日 :2018/05/23
 *	最終更新日 :2018/05/23
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village {

    public class EnemyManager : MonoBehaviour {

        [SerializeField] private UpdateManager updateManager;
        private List<EnemySpawn> spawns;

        private void Awake(){
            if(spawns.Count > 0) {
                spawns[0].Spawn();
            }
		}

        private void Start(){
            
        }

        private void Update() {
            
        }

    }

}