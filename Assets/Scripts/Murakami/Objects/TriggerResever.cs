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

    public class TriggerResever : MonoBehaviour {

        private bool isEnter = false;
        private bool isStay = false;

        public bool IsEnter { get { return isEnter ? true : false; } }
        public bool IsStay  { get { return isStay  ? true : false; } }
        public bool IsExit  { get { return !isEnter && !isStay ? true : false; } }

        private void OnTriggerEnter(Collider other) {
            if(other.tag != "Player") return;
            isEnter = true;
        }

        private void OnTriggerStay(Collider other) {
            if(other.tag != "Player") return;
            isStay = true;
        }

        private void OnTriggerExit(Collider other) {
            if(other.tag != "Player") return;
            isEnter = false;
            isStay = false;
        }
    }

}