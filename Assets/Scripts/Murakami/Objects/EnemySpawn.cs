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
        [SerializeField] private GameObject generationEffectPrefab;
        [SerializeField] private Vector3 offset;
        [SerializeField] private int myNumber = 0;

        private UpdateManager manager;
        private bool isSpawn = false;

        private static Vector3 generationPoint = Vector3.zero;
        private static float time = 0;
        private static float spawnTime = 1;//〇秒置き
        private static int spawnCount = 0;
        private static int maxSpawnCount = 10;
        private static int activeNumber = -1;
        private static int numberGeneration = -1;

        public static Vector3 GenerationPoint {
            get {
                return generationPoint;
            }
        }

        public static int ActiveNumber {
            get {
                return activeNumber;
            }
        }

        private void Start() {
            manager = FindObjectOfType<UpdateManager>();
            myNumber = NumberGeneration();
        }

        private void Update() {
            if(GameMaster.getInstance.GetGameMode == GameMaster.GameMode.Game) {
                if(activeNumber == myNumber) {
                    time += Time.deltaTime;
                    if(time >= spawnTime) {
                        time = 0;
                        if(spawnCount < maxSpawnCount) {
                            Spawn();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 情報をリセット
        /// </summary>
        private void InfoReset(Vector3 point) {
            activeNumber = myNumber;
            time = 0;
            spawnCount = 0;
            generationPoint = point + offset;
        }

        /// <summary>
        /// 初期生成時に自分のmyNumberを設定する
        /// </summary>
        private int NumberGeneration() {
            numberGeneration++;
            return numberGeneration;
        }

        /// <summary>
        /// 敵オブジェクトの生成
        /// </summary>
        public void Spawn() {
            GameObject ene = Instantiate(enemyPrefab) as GameObject;
            ene.transform.position = generationPoint;
            GameObject effect = Instantiate(generationEffectPrefab) as GameObject;
            effect.transform.position = generationPoint;
            Destroy(effect,3);
            manager.Add(ene.GetComponent<INI.EnemyController>());
            spawnCount++;
        }

        private void OnTriggerEnter(Collider other) {
            if(other.tag != "Player") return;
            InfoReset(transform.position);
        }

    }

}