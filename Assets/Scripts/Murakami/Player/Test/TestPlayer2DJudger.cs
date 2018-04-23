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

    public class TestPlayer2DJudger : MonoBehaviour {
        private Rigidbody2D myRigid;
        public bool isGround= false;
        public bool isInShadow = false;
        public bool isHole = false;
        public bool isSlope = false;
        public float speed = 0.1f;

        private void Awake(){
            
		}

        private void Start(){
            myRigid = GetComponent<Rigidbody2D>();
        }

        private void Update() {
            if(GameMaster.getInstance.GetGameMode == GameMaster.GameMode.Game) {

                if(isHole) {
                    transform.Translate(speed,0,0);
                }
                else if(isSlope) {
                    transform.Translate(speed,-speed,0);
                }

                if(isInShadow) {
                    transform.Translate(0,speed,0);
                }

                Debug.Log("ground : " + isGround + " inShadow : " + isInShadow + " ishole : " + isHole + " isslope : " + isSlope);
            }
        }

        public void setIsGround(bool f) {
            isGround = f;
            if(isGround) {
                myRigid.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            }
            else {
                myRigid.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }

        public void setIsHole(bool f) {
            isHole = f;
        }

        public void SetIsInShadow(bool f) {
            isInShadow = f;
        }

        public void SetIsSlope(bool f) {
            isSlope = f;
        }
    }

}