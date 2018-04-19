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
        public Rigidbody2D rigid;
        public ColorJudge judge;

        public void SetGravity(bool f) {
            if(f) {
                //rigid.gravityScale = 0;
                rigid.simulated = false;
            }
            else {
                rigid.simulated = true;
                //rigid.gravityScale = 3;
            }
        }
    }

}