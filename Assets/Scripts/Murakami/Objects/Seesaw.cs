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

    public class Seesaw : MonoBehaviour {
        [SerializeField]private Transform bar;
        [SerializeField]private TriggerResever leftCheck;
        [SerializeField]private TriggerResever rightCheck;
        [SerializeField, Range(0,5)] private float speed = 1;
        [SerializeField] private MoveLimit limit = new MoveLimit(-10,10);

        [SerializeField] private float angleValue = 0;

        private void Awake(){
            
		}

        private void Update() {
            if(leftCheck.IsStay) {
                angleValue = Mathf.Min(angleValue + speed,limit.max);
            }
            if(rightCheck.IsStay) {
                angleValue = Mathf.Max(angleValue - speed,limit.min);
            }
            var angle = bar.localEulerAngles;
            angle.z = angleValue;
            bar.localEulerAngles = angle;
        }



    }

}