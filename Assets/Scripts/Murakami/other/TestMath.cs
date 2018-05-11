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

    public class TestMath : MonoBehaviour {

        [SerializeField] private Transform candlePos;//ろうそくの位置
        [SerializeField] private Transform objectPos;//オブジェクトの位置
        private Transform shadowPos;                 //影の位置

        private Vector3 candle_To_Object_vector;    //ろうそくからオブジェクトまでのベクトル
        private Vector3 Object_To_Shadow_vector;    //オブジェクトから影までのベクトル







        private void Awake(){
            Vector3 center = new Vector3(candlePos.position.x,
                                         objectPos.position.y,
                                         objectPos.position.z);

		}

        private void Start(){
            
        }

        private void Update() {
            
        }

    }

}