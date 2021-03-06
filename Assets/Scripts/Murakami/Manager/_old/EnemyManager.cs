﻿/*
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

        private uint enemyCount = 0;

        private void Update() {
            if(spawns.Count > 0 && enemyCount < spawnMax) {
                StartCoroutine(Spawn(1));
            }
        }

        /// <summary>
        /// 生成位置の設定
        /// </summary>
        /// <param name="number"></param>
        public void SetNowSpawnNumber(int number) {
            nowSpawnNumber = number;
        }

        /// <summary>
        /// spawnsにEnemySpawnを登録
        /// </summary>
        public int SetSpawnsList(EnemySpawn obj) {
            spawns.Add(obj);
            return spawns.Count - 1;
        }

        /// <summary>
        /// 敵を生成し、UpdateManagerに登録
        /// </summary>
        private IEnumerator Spawn(float time) {
            enemyCount++;
            yield return new WaitForSeconds(time);
            //var settingObj = spawns[Mathf.Max(nowSpawnNumber,0)].Spawn();
            //updateManager.Add(settingObj);
        }
        

    }

}