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
        [SerializeField] private List<EnemySpawn> spawns;
        [SerializeField] private uint spawnMax = 10;
        [SerializeField] private int nowSpawnNumber = 0;

        private void Awake() {
            if(spawns.Count > 0) {
                for(int i=0;i<spawnMax;i++) {
                    Spawn();
                }
            }
		}

        /// <summary>
        /// 敵を生成し、UpdateManagerに登録
        /// </summary>
        private void Spawn() {
            var settingObj = spawns[Mathf.Max(nowSpawnNumber,0)].Spawn();
            updateManager.Add(settingObj);
        }
        

    }

}