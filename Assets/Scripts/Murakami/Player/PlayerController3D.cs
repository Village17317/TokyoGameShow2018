/*
 *	作成者     :村上和樹
 *	機能説明   :3dキャラのコントローラー
 * 	初回作成日 :2018/04/15
 *	最終更新日 :2018/06/11
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village {

    public class PlayerController3D: Inheritor {
        [System.Serializable]
        private class Animations {
            public Animator anim;
            public string stayAnim = "";
            public string walkAnim = "";
            public string jumpAnim = "";
            public string dashAnim = "";
        }

        private enum MyState {
            Stay,
            Walk,
            Jump,
            Dash,
        }


        [SerializeField] private float speed = 0.1f;
        [SerializeField] private float jumpForce = 1000;
        [SerializeField] private float checkGroundRayLength = 10;
        [SerializeField] private float checkWallRayLength = 10;
        [SerializeField] private MoveLimit horizontal;
        [SerializeField] private MoveLimit vertical;
        [SerializeField] private Animations playerAnimations;


        private LayerMask mask = 1 << 0;
        private MyState myState = MyState.Stay;
        private float walkSeCount = 0.5f;
        private float accel = 0.1f;

        public MoveLimit GetHorizontalLimit {
            get {
                return horizontal;
            }
        }

        public override void Run() {
            if(GameMaster.getInstance.GetGameMode == GameMaster.GameMode.Game
            || GameMaster.getInstance.GetGameMode == GameMaster.GameMode.GameReStart) {
                //メイン処理
                Turn();
                Move();
                Jump();
                PlayAnimation();
            }
        }

        /// <summary>
        /// 移動
        /// </summary>
        private void Move() {
            if(CheckWall()) return;
            bool isGround = CheckGround();
            if(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0) {
                if(isGround) myState = MyState.Stay;
                walkSeCount = 0.5f;
                accel = 0.1f;
                return;
            }

            //一旦格納
            Vector3 pos = transform.position;
            float dash = Input.GetButton("Button_A") ? 1.5f : 1;
            accel = Mathf.Min(accel + 0.01f * dash,speed * dash);
            //位置制限
            pos += new Vector3(accel * Input.GetAxisRaw("Horizontal"), 0, accel * Input.GetAxisRaw("Vertical"));
            pos = new Vector3(Mathf.Clamp(pos.x,horizontal.min,horizontal.max),
                              pos.y,
                              Mathf.Clamp(pos.z,vertical.min,vertical.max));
            //再格納
            transform.position = pos;

            if(isGround) {
                walkSeCount += Time.deltaTime;
                if(Input.GetButton("Button_A")) {
                    if(walkSeCount >= 0.3f) {
                        walkSeCount = 0;
                        SoundManager.Instance.PlaySE("walk",transform);
                    }
                    myState = MyState.Dash;
                }
                else {
                    if(walkSeCount >= 0.5f) {
                        walkSeCount = 0;
                        SoundManager.Instance.PlaySE("walk",transform);
                    }
                    myState = MyState.Walk;
                }


            }
            else {
                walkSeCount = 0;
            }
        }

        /// <summary>
        /// ジャンプ
        /// </summary>
        private void Jump() {
            if(Input.GetButtonDown("Button_B") && CheckGround()) {
                myState = MyState.Jump;
                SoundManager.Instance.PlaySE("jump",transform);
                GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce);
            }
        }

        /// <summary>
        /// 地面の接触判定
        /// </summary>
        private bool CheckGround() {
            Ray ray = new Ray(transform.position,-Vector3.up);
            Debug.DrawRay(transform.position,-Vector3.up * checkGroundRayLength,Color.red);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit,checkGroundRayLength,mask)){
                if(hit.collider.gameObject.tag == "Desk"
                || hit.collider.gameObject.tag == "Object3D") {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 進行方向を向く
        /// </summary>
        private void Turn() {
            if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0) return;

            float x = Input.GetAxis("Horizontal") * 100;
            float z = Input.GetAxis("Vertical") * 100;

            Vector3 origin = transform.position;
            Vector3 dir = origin + new Vector3(x,0,z);

            transform.LookAt(dir);
        }

        /// <summary>
        /// 壁の接触判定
        /// </summary>
        private bool CheckWall() {
            Ray ray = new Ray(transform.position,transform.forward);
            Debug.DrawRay(transform.position,transform.forward * checkWallRayLength,Color.red);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit,checkWallRayLength)) {
                if(hit.collider.gameObject.tag == "Object3D")
                return true;
            }
            return false;
        }

        /// <summary>
        /// 状態に合わせてAnimationを再生
        /// </summary>
        private void PlayAnimation() {
            switch(myState) {
                case MyState.Stay: playerAnimations.anim.Play(playerAnimations.stayAnim); break;
                case MyState.Walk: playerAnimations.anim.Play(playerAnimations.walkAnim); break;
                case MyState.Jump: playerAnimations.anim.Play(playerAnimations.jumpAnim); break;
                case MyState.Dash: playerAnimations.anim.Play(playerAnimations.dashAnim); break;
                default: break;
            }
        }
    }
}
