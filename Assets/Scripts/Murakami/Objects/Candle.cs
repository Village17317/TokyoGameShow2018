/*
 *	作成者     :村上和樹
 *	機能説明   :3DPlayerの初期位置
 * 	初回作成日 :2018/05/11
 *	最終更新日 :2018/05/11
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village {

    public class Candle : Inheritor {
        [SerializeField] private Transform playerTf;
        [SerializeField] private float rotateSpeed = 1;
        [SerializeField] private float scaleUpSpeed = 1;
        [SerializeField] private Vector3 shotForce;

        private Rigidbody playerRigid;
        private Vector3 lastScale;
        private float rotateTmp = 0;

        private void Start() {
            lastScale = playerTf.localScale;
            playerTf.position = transform.position;
            playerTf.localScale = Vector3.zero;

            playerRigid = playerTf.GetComponent<Rigidbody>();
            playerRigid.AddForce(shotForce);
        }

        public override void Run() {
            if(GameMaster.getInstance.GetGameMode == GameMaster.GameMode.Start) {
                Rotate();
                ScaleUp();
            }
            else {
                Destroy(this);
            }

        }

        /// <summary>
        /// サイズを大きくする
        /// </summary>
        private void ScaleUp() {
            float x = playerTf.localScale.x >= lastScale.x ? 0 : scaleUpSpeed;
            float y = playerTf.localScale.y >= lastScale.y ? 0 : scaleUpSpeed;
            float z = playerTf.localScale.z >= lastScale.z ? 0 : scaleUpSpeed;
            playerTf.localScale += new Vector3(x,y,z);
        }

        private void Rotate() {
            if(rotateTmp >= 450) return;
            playerTf.localEulerAngles += new Vector3(0,rotateSpeed,0);
            rotateTmp += rotateSpeed;
        }
    }

}