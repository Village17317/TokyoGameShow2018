/*
 *	作成者     :村上和樹
 *	機能説明   :カメラの挙動
 * 	初回作成日 :2018/05/08
 *	最終更新日 :2018/05/08
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village {

    public class CameraController : Inheritor {
        [SerializeField] private Transform playerTf;
        
        public override void Run() {
            float x = playerTf.position.x;
            float y = transform.position.y;
            float z = transform.position.z;
            transform.position = new Vector3(x,y,z);
        }


    }

}