/*
 *	作成者     :
 *	機能説明   :
 * 	初回作成日 :
 *	最終更新日 :
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village {

    public class BookTween : MonoBehaviour {

        [SerializeField] private Transform bookTf;
        [SerializeField] private float angle = 60;
        private bool isPlay = false;

        private void OnCollisionEnter(Collision collision) {
            if(collision.gameObject.tag != "Player") return;
            isPlay = true;
            StartCoroutine(PlayTween(angle));
        }

        private  IEnumerator PlayTween(float rotation) {
            for(int i = 0;i<90;i++) {
                Vector3 tmp = bookTf.localEulerAngles;
                tmp.z = Mathf.Max(tmp.z - 1,rotation);
                bookTf.localEulerAngles = tmp;
                yield return null;
            }
            Destroy(this);
        }

    }

}