/*
 *	作成者     :村上和樹
 *	機能説明   :3dキャラのコントローラー
 * 	初回作成日 :2018/04/15
 *	最終更新日 :2018/04/15
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village {

    public class PlayerController3D: Inheritor {

        [SerializeField] private float speed = 0.1f;
        [SerializeField] private float jumpForce = 1000;
        [SerializeField] private float checkGroundRayLength = 10;
        [SerializeField] private float checkWallRayLength = 10;
        [SerializeField] private MoveLimit horizontal;
        [SerializeField] private MoveLimit vertical;

        private LayerMask mask = 1 << 0;

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
            }
        }

        /// <summary>
        /// 移動
        /// </summary>
        private void Move() {
            if(CheckWall()) return; 

            //一旦格納
            Vector3 pos = transform.position;
            //位置制限
            pos += new Vector3(speed * Input.GetAxisRaw("Horizontal"),0,speed * Input.GetAxisRaw("Vertical"));
            pos = new Vector3(Mathf.Clamp(pos.x,horizontal.min,horizontal.max),
                              pos.y,
                              Mathf.Clamp(pos.z,vertical.min,vertical.max));
            //再格納
            transform.position = pos;
        }

        /// <summary>
        /// ジャンプ
        /// </summary>
        private void Jump() {
            if(Input.GetButtonDown("Button_B") && CheckGround()) {
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

        private void Turn() {
            if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0) return;

            float x = Input.GetAxis("Horizontal") * 100;
            float z = Input.GetAxis("Vertical") * 100;

            Vector3 origin = transform.position;
            Vector3 dir = origin + new Vector3(x,0,z);

            transform.LookAt(dir);
        }

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
    }
}
