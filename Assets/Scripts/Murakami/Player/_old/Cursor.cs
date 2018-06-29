/*
 *	作成者     :村上和樹
 *	機能説明   :カーソルの判定
 * 	初回作成日 :2018/04/20
 *	最終更新日 :2018/04/20
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village_old {

    public class Cursor : MonoBehaviour {
        [SerializeField] private ParticleSystem choiceEffect;
        [SerializeField] private ParticleSystem movingEffect;

        private GameObject obj;
        private bool isChoice = false;

        public GameObject GetObject {
            get {
                return obj;
            }
        }

        private void Awake() {
            choiceEffect.gameObject.SetActive(true);
            movingEffect.gameObject.SetActive(false);
        }

        public void  SetIsChoice(bool flag) {
            if(flag) {
                if(choiceEffect.gameObject.activeInHierarchy)  choiceEffect.gameObject.SetActive(false);
                if(!movingEffect.gameObject.activeInHierarchy) movingEffect.gameObject.SetActive(true);                                                      
            }                               
            else {
                if(!choiceEffect.gameObject.activeInHierarchy)  choiceEffect.gameObject.SetActive(true);
                if(movingEffect.gameObject.activeInHierarchy) movingEffect.gameObject.SetActive(false);
            }
            isChoice = flag;
        }

        private void OnTriggerEnter(Collider other) {
            if(other.tag == "Object3D" && !isChoice) {
                obj = other.gameObject;
            }
        }

        private void OnTriggerStay(Collider other) {
            if(other.tag == "Object3D" && !isChoice) {
                obj = other.gameObject;
            }
        }

        private void OnTriggerExit(Collider other) {
            if(other.tag == "Object3D" && !isChoice) {
                obj = null;
            }
        }
    }

}