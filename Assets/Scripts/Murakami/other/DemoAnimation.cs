/*
 *	作成者     :村上和樹
 *	機能説明   :デモアニメ
 * 	初回作成日 :2018/06/22
 *	最終更新日 :2018/06/22
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Village {

    public class DemoAnimation : MonoBehaviour {

        [System.Serializable]
        private class AnimAsset {
            public GameObject obj;
            public Animator animator;
        }

        private enum AnimationStep {
            step_0,
            step_1,
            step_2,
            step_3,
            step_4,
            step_5,
            step_6,
            step_7,
            step_8,
            step_9,
            END,
        }

        [Header("姫"),SerializeField] private AnimAsset hime;
        [Header("ランプ君"),SerializeField] private AnimAsset lamp;
        [Header("兵士達"),SerializeField] private AnimAsset[] porn;
        [Header("白幕"),SerializeField] private Material feedoutMat;
        [Header("黒幕"), SerializeField] private Image blackCurtain;
        [Header("ライト"),SerializeField] private Light pointLight;
        [Header("ロゴ"),SerializeField] private Image logo;
        [Space(16)]

        [SerializeField] private float[] endTimes;

        [Space(16)]

        [SerializeField] private GameObject animCamera;
        [SerializeField] private Vector3 afterCameraPos;
        [SerializeField] private float afterCameraArrivalSpeed = 1;

        [Space(16)]
        [SerializeField] private AnimationStep activeStep = AnimationStep.step_0;

        private float time = 0;
        private float allTime = 0;
        private float range = 0;
        private bool isJump = false;
        private bool isPlay = false;

        private void Start() {
            Cursor.visible = false;//カーソルの非表示
            range = pointLight.range;
            feedoutMat.SetFloat("_Threshold",1);//幕がかかってない状態
            logo.color = new Color(1,1,1,0);//ロゴが見えていない状態
            blackCurtain.color = Color.black;//黒幕をかける
        }

        private void Update() {

            
            if(Input.GetKeyDown(KeyCode.Space)) {
                isPlay = true;
            }
            if(!isPlay) return;


            if(activeStep >= AnimationStep.END) return;
            time += Time.deltaTime;
            allTime += Time.deltaTime;
            if(time >= endTimes[(int) activeStep]) {
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
                case AnimationStep.step_7: Step_7();      break;
                case AnimationStep.step_8: Step_8();      break;
                case AnimationStep.step_9: Step_9();      break;
                default:                   End();         break;
            }
        }

        /// <summary>
        /// フェードアウト
        /// </summary>
        private void Step_0() {
            FeedOut();
        }

        /// <summary>
        /// フェードイン
        /// </summary>
        private void Step_1() {
            FeedIn();
        }

        /// <summary>
        /// 城から出てくる姫
        /// </summary>
        private void Step_2() {
            PrincessWalk();
        }

        /// <summary>
        /// ヘトヘト
        /// </summary>
        private void Step_3() {
            hime.animator.Play("Wait");
        }

        /// <summary>
        /// 1回後ろを向く
        /// </summary>
        private void Step_4() {
            hime.obj.transform.eulerAngles = new Vector3(0,180,0);
        }

        /// <summary>
        /// びっくりする
        /// </summary>
        private void Step_5() {
            if(!isJump) {
                isJump = true;
                hime.obj.GetComponent<Rigidbody>().AddForce(Vector3.up * 50,ForceMode.Impulse);
            }
        }

        /// <summary>
        /// 右に走りだす
        /// </summary>
        private void Step_6() {
            PrincessWalk();
        }

        /// <summary>
        /// 兵士も一緒に右に走りだす
        /// </summary>
        private void Step_7() {
            PrincessWalk();
            PornWalk();
        }

        /// <summary>
        /// カメラ引く
        /// </summary>
        private void Step_8() {
            PrincessWalk();
            PornWalk();
            animCamera.transform.position = Vector3.Lerp(animCamera.transform.position,afterCameraPos,Time.deltaTime * afterCameraArrivalSpeed);
        }

        /// <summary>
        /// ランプ君右に
        /// </summary>
        private void Step_9() {
            PrincessWalk();
            PornWalk();
            LampWalk();
        }

        private void End() {
            Debug.Log(allTime);
            //SceneManager.LoadScene("Scene_Title");
        }

        private void PrincessWalk() {
            hime.obj.transform.eulerAngles = Vector3.zero;
            hime.animator.Play("Walk");
            hime.obj.transform.Translate(0.5f,0,0);
        }

        private void PornWalk() {
            for(int i = 0;i < porn.Length;i++) {
                porn[i].animator.Play("Enemy_walk");
                porn[i].obj.transform.Translate(Random.Range(0.6f,0.8f),0,0);
            }
        }

        private void LampWalk() {
            lamp.obj.transform.eulerAngles = new Vector3(0,90,0);
            lamp.obj.transform.Translate(0,0,0.5f);
            lamp.animator.Play("Walk");
        }

        /// <summary>
        /// 白くなっていく
        /// </summary>
        private void FeedOut() {
            var value = feedoutMat.GetFloat("_Threshold");
            value = Mathf.Max(value - 0.02f,0);
            feedoutMat.SetFloat("_Threshold",value);

            var color = logo.color;
            color.a = Mathf.Min(color.a + 0.02f,1);
            logo.color = color;
            
            pointLight.range = 0;
        }

        /// <summary>
        /// 白さが消えていく
        /// </summary>
        private void FeedIn() {
            var value = feedoutMat.GetFloat("_Threshold");
            value = Mathf.Min(value + 0.04f,1);
            feedoutMat.SetFloat("_Threshold",value);

            var color = logo.color;
            color.a = Mathf.Max(color.a - 0.04f,0);
            logo.color = color;

            blackCurtain.color -= new Color(0,0,0,0.04f);

            pointLight.range = range;
        }

        private void OnDisable() {
            feedoutMat.SetFloat("_Threshold",1);
        }

    }

}