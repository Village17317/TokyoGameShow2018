/*
 *	作成者     :村上和樹
 *	機能説明   :velocityの上限の設定
 * 	初回作成日 :2018/05/25
 *	最終更新日 :2018/05/25
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village {

    public class PlayerVelocityController : MonoBehaviour {

        [SerializeField] private float maxVelocity_y;
        private Rigidbody myRigid;

        private void Start() {
            myRigid = GetComponent<Rigidbody>();
        }

        private void FixedUpdate() {

            Vector3 velocity = myRigid.velocity;

            velocity.y = Mathf.Min(velocity.y,maxVelocity_y);

            myRigid.velocity = velocity;
            Debug.Log("velocity : " + myRigid.velocity);
        }

    }

}