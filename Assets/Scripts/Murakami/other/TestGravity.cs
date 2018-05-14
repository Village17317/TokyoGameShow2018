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

    public class TestGravity : MonoBehaviour {
        public Vector3 fixedRigid;
        private void FixedUpdate() {
            GetComponent<Rigidbody>().AddForce(fixedRigid);
        }
    }

}