/*
 *	作成者     :村上和樹
 *	機能説明   :デモアニメ
 * 	初回作成日 :2018/06/22
 *	最終更新日 :2018/06/22
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village {

    public class DemoAnimation : MonoBehaviour {

        [System.Serializable]
        private class AnimAsset {
            public GameObject obj;
            public Animator animator;
            public float endTime = 0;
        }

        private enum AnimationStep {
            step_0,
            step_1,
            step_2,
            step_3,
            step_4,
            step_5,
            step_6,
            END,
        }

        [SerializeField] private AnimAsset[] asset;
        [Space(16)]
        [SerializeField] private AnimationStep activeStep = AnimationStep.step_0;
        [SerializeField] private float time = 0;
        [SerializeField] private Vector3 afterCameraPos;
        [SerializeField] private float afterCameraArrivalSpeed = 1;

        private float allTime = 0;

        private void Start() {
            asset[0].obj.GetComponent<Light>().range = 0;
        }

        private void Update() {
            if(activeStep >= AnimationStep.END) return;
            time += Time.deltaTime;
            allTime += Time.deltaTime;
            if(time >= asset[(int) activeStep].endTime) {
                time = 0;
                activeStep++;
            }

            switch(activeStep) {
                case AnimationStep.step_0: Step_0();      break;
                case AnimationStep.step_1: Step_1();      break;
                case AnimationStep.step_2: Step_2();      break;
                case AnimationStep.step_3: Step_3();      break;
                case AnimationStep.step_4: Step_4();      break;
                case AnimationStep.step_5: Step_5();      break;
                case AnimationStep.step_6: Step_6();      break;
                default:    Debug.Log(allTime);                              break;
            }
        }

        //pointLightのレンジを広げる
        private void Step_0() {
            var range = asset[0].obj.GetComponent<Light>().range;
            range += range >= 500 ? 0 : 10f;
            asset[0].obj.GetComponent<Light>().range = range;
        }

        //姫の疲れているAnimation
        private void Step_1() {
            asset[1].animator.Play("Wait");
        }

        //姫が何か思いつく
        private void Step_2() {
            asset[2].animator.Play("Clear");
        }

        //姫右に歩く
        private void Step_3() {
            asset[3].animator.Play("Walk");
            asset[3].obj.transform.Translate(0.5f,0,0);
        }

        //兵士右に歩く
        private void Step_4() {
            Step_3();
            asset[4].animator.Play("Enemy_walk");
            asset[4].obj.transform.Translate(0.6f,0,0);
        }

        //カメラ引く
        private void Step_5() {
            Step_4();
            asset[5].obj.transform.position = Vector3.Lerp(asset[5].obj.transform.position,afterCameraPos,Time.deltaTime * afterCameraArrivalSpeed);
        }

        //ランプが右に行く
        private void Step_6() {
            Step_5();
            asset[6].obj.transform.eulerAngles = new Vector3(0,90,0);
            asset[6].obj.transform.Translate(0,0,0.5f);
            asset[6].animator.Play("Walk");
        }
    }

}