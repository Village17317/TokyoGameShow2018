/*
 *	作成者     :村上和樹
 *	機能説明   :2dキャラのコントローラー
 * 	初回作成日 :2018/04/14
 *	最終更新日 :2018/04/14
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village {

    public class PlayerController2D : Inheritor {
        [SerializeField] private Rigidbody2D myRigid;
        [SerializeField] private float speed = 1;
        [SerializeField] private float jumpSpeed = 1000;

        public override void Run() {
            //if(GameMaster.getInstance.GetGameMode == GameMaster.GameMode.AutoMove) {
            //    Move();
            //    Jump();
            //}
            transform.Translate(speed,0,0);
        }

        private void Move() {
            if(Input.GetAxis("Horizontal") > 0) {
                transform.localEulerAngles = new Vector3(0,0,0);
                transform.Translate(speed,0,0);
            }
            else if(Input.GetAxis("Horizontal") < 0) {
                transform.localEulerAngles = new Vector3(0,180,0);
                transform.Translate(speed,0,0);
            }
        }

        private void Jump() {
            Vector3 origin = transform.position - Vector3.up * 0.76f;
            Vector3 dir = -Vector2.up * 0.1f;
            RaycastHit2D hit = Physics2D.Raycast(origin,dir,0.1f);

            if(hit.collider != null && hit.collider.tag == "Ground") {
                if(Input.GetButtonDown("Button_A")) {
                    myRigid.AddForce(transform.up * jumpSpeed);
                }
            }
            Debug.DrawRay(origin,dir,Color.blue);


        }
    }

}