/*
 *	作成者     :村上和樹
 *	機能説明   :シーソー
 * 	初回作成日 :2018/06/20
 *	最終更新日 :2018/06/20
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