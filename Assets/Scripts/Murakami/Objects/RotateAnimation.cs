/*
 *	作成者     :村上和樹
 *	機能説明   :まわるだけ
 * 	初回作成日 :2018/05/28
 *	最終更新日 :2018/05/28
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village {

    public class RotateAnimation : MonoBehaviour {

        [SerializeField] private Vector3 rotate;

        private void Update() {
            transform.Rotate(rotate);    
        }

    }

}