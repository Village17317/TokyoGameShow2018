/*
 *	作成者     :村上和樹
 *	機能説明   :カーソルの判定
 * 	初回作成日 :2018/04/20
 *	最終更新日 :2018/04/20
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village {

    public class Cursor : MonoBehaviour {
        [SerializeField] private Material myMat;
        [SerializeField] private Color normalColor;
        [SerializeField] private Color choicedColor;
        

        private GameObject obj;
        
        public GameObject GetObject {
            get {
                return obj;
            }
        }

        private void Awake() {
            myMat.color = normalColor;
        }

        public void  SetIsChoice(bool flag) {
            if(flag) {
                myMat.color = choicedColor;
            }
            else {
                myMat.color = normalColor;
            }
        }

        private void OnTriggerEnter(Collider other) {
            if(other.tag == "Object3D") {
                obj = other.gameObject;
            }
        }

        private void OnTriggerExit(Collider other) {
            if(other.tag == "Object3D") {
                obj = null;
            }
        }
    }

}