/*
*	作成者	：青木仁志
*	機能	：キャラクターの自動操作
*	作成	：2018/05/11
*	更新	：2018/05/31
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace INI
{
    public class ShadowCharController : Village.Inheritor
    {
        public float walkSpeed = 0.1f, fwRayDistance = 10f, dwRayDistance = 10.8f, sfRayDistance;

        public Vector3 jumpForce, fwRayOrigin, sfRayOrigin;

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
        [SerializeField]
        private State state = State.STOP;

        public Animator shadowChar_Animator;

        public override void FixedRun()
        {
            if (Village.GameMaster.getInstance.GetGameMode == Village.GameMaster.GameMode.Game)
            {
                GroundRayCast();
                ForwardRayCast();
                SafetyRayCast();
                
                if (state == State.WALK)
                {
                    Walk();
                }
            }

            if (shadowChar_Animator != null)
            {
                PlayerAnimator();
            }
        }

        //// デバッグ用
        //public override void Run()
        //{
        //    if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Button_X"))
        //    {
        //        Debug.Log("Switch state (state:" + state + ")");
        //        if (state == State.WALK) state = State.STOP;
        //        else if (state == State.STOP) state = State.WALK;
        //    }
        //}

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
                if (hit.collider.tag == "ShadowObj")
                {
                    grounded = true;
                    state = State.WALK;
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
            Ray fwdRay = new Ray(this.transform.position + fwRayOrigin, Vector3.right);

            RaycastHit hit;

            Debug.DrawRay(fwdRay.origin, fwdRay.direction * fwRayDistance, Color.red);

            if (Physics.Raycast(fwdRay, out hit, fwRayDistance))
            {
                if (hit.collider.tag == "ShadowObj")
                {
                    if (grounded　&& state != State.JUMP && state != State.STOP)
                    {
                        state = State.JUMP;
                        Jump();
                    }
                }
            }
        }

        private void SafetyRayCast()
        {
            Ray sfRay = new Ray(this.transform.position + sfRayOrigin, Vector3.down);

            RaycastHit hit;
            
            Debug.DrawRay(sfRay.origin, sfRay.direction * sfRayDistance, Color.blue);

            if (Physics.Raycast(sfRay, out hit, sfRayDistance))
            {
                if (hit.collider.tag == "ShadowObj")
                {
                    if (grounded && state != State.JUMP)
                    {
                        state = State.WALK;
                    }
                }
            }
            else
            {
                if (grounded && state != State.JUMP && state != State.STOP)
                {
                    state = State.STOP;
                    Debug.Log(state);
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
            Debug.Log("Jump called");
        }

        private void PlayerAnimator()
        {
            switch (state)
            {
                case State.STOP:
                    shadowChar_Animator.Play("Wait");
                    break;
                case State.WALK:
                    shadowChar_Animator.Play("Walk");
                    break;
                case State.JUMP:
                    shadowChar_Animator.Play("Jump");
                    break;
                default:
                    break;
            }
        }
    }
}