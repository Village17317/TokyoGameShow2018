/*
 *	作成者     :村上和樹
 *	機能説明   :
 * 	初回作成日 :2018/04/14
 *	最終更新日 :2018/04/14
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village {

    public class EditorGizmos : MonoBehaviour {
        [SerializeField] private Transform[] tfs;
        [SerializeField] private Color gizmosColor = Color.green;
        [SerializeField] private bool isActive = true;
        private void OnDrawGizmos() {
            if(isActive) {
                Gizmos.color = gizmosColor;
                Gizmos.DrawSphere(transform.position,0.1f);
                for(int i = 0;i < tfs.Length;i++) {
                    Gizmos.DrawLine(transform.position,tfs[i].position);
                    Gizmos.DrawSphere(tfs[i].position,0.1f);
                }
            }
        }

    }

}