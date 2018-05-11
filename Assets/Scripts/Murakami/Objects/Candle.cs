/*
 *	作成者     :村上和樹
 *	機能説明   :ろうそく
 * 	初回作成日 :2018/05/11
 *	最終更新日 :2018/05/11
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village {

    public class Candle : MonoBehaviour {
        [SerializeField] private Transform cylinderTf;
        [SerializeField] private float activeTime = 0.01f;
        [SerializeField]
        private bool isAction = false;

        private float constScale_y;
        
        private void Awake(){}

        private void Start(){
            constScale_y = cylinderTf.localScale.y;
        }

        private void Update() {
            Combustion();
        }

        /// <summary>
        /// ろうそくを燃やす
        /// </summary>
        private void Combustion() {
            if(!isAction) return;

            cylinderTf.localScale -= new Vector3(0,constScale_y * Time.deltaTime * activeTime,0);
            if(cylinderTf.localScale.y < 0) {
                Destroy(cylinderTf.gameObject);
                Destroy(GetComponent<Candle>());
            }
        }

    }

}