/*
 *	作成者     :村上和樹
 *	機能説明   :3dキャラのコントローラー
 * 	初回作成日 :2018/04/15
 *	最終更新日 :2018/04/15
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village {

    public class PlayerController3D: Inheritor {

        [System.Serializable]
        private class MoveLimit {
            public float min = 0;
            public float max = 0;
        }

        [SerializeField] private float speed = 0.1f;
        [SerializeField] private Transform catchingTf = null;
        [SerializeField] private MoveLimit horizontal;
        [SerializeField] private MoveLimit vertical;

        private bool isCatch = false;
        private bool isRotateWait = false;
        private float constSpeed;

        private void Awake() {
            constSpeed = speed;
        }

        private void Start() {

        }

        public override void Run() {
            if(GameMaster.getInstance.GetGameMode == GameMaster.GameMode.Start
            || GameMaster.getInstance.GetGameMode == GameMaster.GameMode.Game
            || GameMaster.getInstance.GetGameMode == GameMaster.GameMode.GameReStart) {
                //メイン処理
                Move();
                ObjectMove();
                ObjectRotate();
            }
        }

        /// <summary>
        /// 移動
        /// </summary>
        private void Move() {
            //一旦格納
            Vector3 pos = transform.position;
            //速度制限
            speed = isCatch ? constSpeed * 0.5f : constSpeed;
            //位置制限
            pos += new Vector3(speed * Input.GetAxisRaw("Horizontal"),0,speed * Input.GetAxisRaw("Vertical"));
            pos = new Vector3(Mathf.Clamp(pos.x,horizontal.min,horizontal.max),
                              pos.y,
                              Mathf.Clamp(pos.z,vertical.min,vertical.max));
            //再格納
            transform.position = pos;
        }

        /// <summary>
        /// 掴みながらオブジェクトを動かす
        /// </summary>
        private void ObjectMove() {
            isCatch = catchingTf != null && Input.GetButton("Button_B");
            if(isCatch) {
                //一旦格納
                Vector3 pos = catchingTf.position;
                //速度制限
                speed = constSpeed * 0.5f;
                //位置制限
                pos += new Vector3(speed * Input.GetAxisRaw("Horizontal"),0,speed * Input.GetAxisRaw("Vertical"));
                pos = new Vector3(Mathf.Clamp(pos.x,horizontal.min,horizontal.max),
                                  pos.y,
                                  Mathf.Clamp(pos.z,vertical.min,vertical.max));
                //再格納
                catchingTf.position = pos;
            }               
        }

        /// <summary>
        /// 掴みながら回転させる
        /// </summary>
        private void ObjectRotate() {
            if(catchingTf == null || !Input.GetButton("Button_B")) return;
            if(Input.GetButtonDown("Button_LB") && !isRotateWait)
                StartCoroutine(AsyncRotate(1));
            else if(Input.GetButtonDown("Button_RB") && !isRotateWait)
                StartCoroutine(AsyncRotate(-1));
        }

        private IEnumerator AsyncRotate(float r) {
            for(float i = 0;i < 90;i++) {
                isRotateWait = true;
                catchingTf.Rotate(0,r,0);
                yield return new WaitForEndOfFrame();
            }
            isRotateWait = false;
        }


        private void OnCollisionStay(Collision collision) {

            if(collision.gameObject.tag != "Object3D") return;
            if(catchingTf == null) catchingTf = collision.transform;   
        }

        private void OnCollisionExit(Collision collision) {
            if(collision.gameObject.tag != "Object3D") return;
            if(catchingTf != null) catchingTf = null;
        }
    }
}
