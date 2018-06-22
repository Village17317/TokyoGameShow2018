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

        [SerializeField] private float minVelocity_x = -0.5f;
        [SerializeField] private float maxVelocity_x = 0;
        [SerializeField] private float maxVelocity_y = 0;
        private Rigidbody myRigid;

        private void Start() {
            myRigid = GetComponent<Rigidbody>();
        }

        private void FixedUpdate() {
            VelocityLimiter();
        }

        private void VelocityLimiter() {
            Vector3 velocity = myRigid.velocity;

            if(velocity.x <= minVelocity_x) velocity.x = minVelocity_x;
            else                velocity.x = Mathf.Min(velocity.x,maxVelocity_x);

            velocity.y = Mathf.Min(velocity.y,maxVelocity_y);

            velocity.z = 0;

            myRigid.velocity = velocity;
        }
    }

}