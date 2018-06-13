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
        [SerializeField] private SpriteRenderer sprite;

        private int nowActiveNumber = -1;

        private void Update() {
            if(EnemySpawn.ActiveNumber != -1) {
                if(!sprite.enabled) sprite.enabled = true;
                transform.position = EnemySpawn.GenerationPoint;
                transform.Rotate(rotate);
                if(nowActiveNumber != EnemySpawn.ActiveNumber) {
                    nowActiveNumber = EnemySpawn.ActiveNumber;
                    transform.localScale = new Vector3(0,0,1);
                    StartCoroutine(ScaleUp());
                }
            }
            else {
                if(sprite.enabled) sprite.enabled = false;
            }
        }

        private IEnumerator ScaleUp() {
            for(int i = 0;i < 60;i++) {
                transform.localScale += new Vector3(0.1f,0.1f,0);
                yield return new WaitForEndOfFrame();
            }
        }
    }

}