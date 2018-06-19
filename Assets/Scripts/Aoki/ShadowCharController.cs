/*
*	作成者	：青木仁志
*	機能	：キャラクターの自動操作
*	作成	：2018/05/11
*	更新	：2018/06/19
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace INI
{
    public class ShadowCharController : Village.Inheritor
    {
        // 移動速度、正面に飛ばすRayの距離、足元に飛ばすRayの距離、穴・段差判定用に飛ばすRayの距離
        public float walkSpeed = 0.1f, fwRayDistance = 10f, dwRayDistance = 10.8f, sfRayDistance;

        // ジャンプ時の力、正面に飛ばすRayの原点、穴・段差判定用に飛ばすRayの原点
        public Vector3 jumpForce, fwRayOrigin, sfRayOrigin;

        // 影のキャラのRigidbody
        [SerializeField]
        private Rigidbody shadowCharRb;

        // 接地フラグ
        [SerializeField]
        private bool grounded = false;

        // アニメーションの状態を示すenum
        protected enum State
        {
            STOP,
            WALK,
            JUMP
        }

        // State型のenumの宣言
        [SerializeField]
        protected State state = State.STOP;

        // 影のキャラのアニメーターを持つ
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

            // デバッグ用にRayを可視化
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
        /// 段差判定（横）
        /// </summary>
        private void ForwardRayCast()
        {
            Ray fwdRay = new Ray(this.transform.position + fwRayOrigin, Vector3.right);

            RaycastHit hit;

            // デバッグ用にRayを可視化
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

        /// <summary>
        /// 穴・段差判定（前方下）
        /// </summary>
        private void SafetyRayCast()
        {
            Ray sfRay = new Ray(this.transform.position + sfRayOrigin, Vector3.down);

            RaycastHit hit;

            // デバッグ用にRayを可視化
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

        /// <summary>
        /// ゲームの状態、及びState型enumの中身に応じてアニメーションを切り替える（プレイヤー用）
        /// </summary>
        protected virtual void PlayerAnimator()
        {
            if (Village.GameMaster.getInstance.GetGameMode == Village.GameMaster.GameMode.GameClear)
            {
                shadowChar_Animator.Play("Clear");
                return;
            }

            if (Village.GameMaster.getInstance.GetGameMode == Village.GameMaster.GameMode.GameOver)
            {
                shadowChar_Animator.Play("Down");
                return;
            }

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