/*
 *	作成者     :
 *	機能説明   :
 * 	初回作成日 :
 *	最終更新日 :
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village {

    public class FireAnimation : MonoBehaviour {

        [SerializeField] private Transform fireTf;
        [SerializeField] private float rotSpeed = 10;
        [SerializeField] private float speed = 0;

        private void Awake(){
            
		}

        private void Start(){
            
        }

        private void Update() {
            transform.Rotate(0,rotSpeed,0);
            fireTf.Translate(0,0,fireTf.localPosition.z > 0 ? speed : 0);
        }

    }

}