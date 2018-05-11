/*
*	作成者	：青木仁志
*	機能	：キャラクターの自動操作
*	作成	：2018/05/11
*	更新	：2018/05/11
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace INI
{
    public class ShadowCharController : Village.Inheritor
    {
        public float walkSpeed = 10f, fwRayDistance = 1f, dwRayDistance = 1f;

        public Vector3 jumpForce;

        [SerializeField]
        private Rigidbody shadowCharRb;

        [SerializeField]
        private bool grounded = false;

        private enum State
        {
            STOP,
            WALK,
            JUMP
        }
        private State state = State.STOP;

        public override void FixedRun()
        {
            if (Village.GameMaster.getInstance.GetGameMode == Village.GameMaster.GameMode.Game)
            {
                GroundRayCast();
                ForwardRayCast();
            }
        }

        /// <summary>
        /// 接地判定
        /// </summary>
        private void GroundRayCast()
        {
            Ray gndRay = new Ray(this.transform.position, Vector3.down);

            RaycastHit hit;

            Debug.DrawRay(gndRay.origin, gndRay.direction * dwRayDistance, Color.red);

            if (Physics.Raycast(gndRay, out hit, dwRayDistance))
            {
                if (hit.collider.tag == "ShadowObj" && state != State.JUMP)
                {
                    grounded = true;
                    state = State.WALK;
                    Walk();
                }
                else
                {
                    grounded = false;
                }
            }
        }

        /// <summary>
        /// 登り段差判定用
        /// </summary>
        private void ForwardRayCast()
        {
            Ray fwdRay = new Ray(this.transform.position, Vector3.right);

            RaycastHit hit;

            Debug.DrawRay(fwdRay.origin, fwdRay.direction * fwRayDistance, Color.red);

            if (Physics.Raycast(fwdRay, out hit, fwRayDistance))
            {
                if (hit.collider.tag == "ShadowObj")
                {
                    if (grounded　&& state == State.WALK)
                    {
                        state = State.JUMP;
                        Jump();
                    }
                }
            }
        }

        /// <summary>
        /// 歩行
        /// </summary>
        private void Walk()
        {
            this.transform.Translate(walkSpeed, 0, 0);
        }

        /// <summary>
        /// ジャンプ
        /// </summary>
        private void Jump()
        {
            shadowCharRb.AddForce(jumpForce);
        }

    }
}