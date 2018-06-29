/*
 *	作成者     :村上和樹
 *	機能説明   :影のオブジェクト
 * 	初回作成日 :2018/05/11
 *	最終更新日 :2018/05/11
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village {

    public class ShadowObjectInfo : MonoBehaviour {

        [SerializeField]private Collider myColl;

        private void Start(){
            myColl = GetComponent<Collider>();
		}


        private void OnCollisionStay(Collision collision) {
            if(collision.gameObject.tag != "ShadowObj")return;
            myColl.enabled = false;
        }

        private void OnCollisionExit(Collision collision) {
            if(collision.gameObject.tag != "ShadowObj")return;
            myColl.enabled = true;
        }

    }

}