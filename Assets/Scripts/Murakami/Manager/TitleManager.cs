/*
 *	作成者     :村上和樹
 *	機能説明   :タイトル画面のマネージャー
 * 	初回作成日 :2018/05/20
 *	最終更新日 :2018/05/20
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Village {

    public class TitleManager: Inheritor {
        [System.Serializable]
        private class NextTransform {
            public Vector3 pos;
            public Vector3 rot;
        }

        [System.Serializable]
        private class NextSceneInfo {
            public string path;
            public GameObject numberObject;
            public GameObject shadowObject;
        }

        private enum TitleStep {
            STEP_1,
            STEP_2,
            STEP_3,
            STEP_4,
            FadeIn,
            FadeOut,
            DEFAULT,
        }

        [Header("タイトルで使うカメラ"),SerializeField] private Camera titleCamera;
        [Header("カメラの次の位置"),SerializeField]    private NextTransform cameraNtf;
        [Header("動くスピード"),SerializeField]        private float animationSpeed = 1;
        [Header("タイトルロゴ"),SerializeField]        private Image logo;
        [Header("ステージの内容"),SerializeField]      private List<NextSceneInfo> stageNumberList;

        [Header("環境光"), SerializeField] private Light directionalLight;
        [Header("ポイントライト"), SerializeField] private Light pointLight;

        private TitleStep step = TitleStep.FadeIn;
        private bool isGetAxis = false;
        private int stageNumber = 0;

        private void Awake() {
            ActiveObjectVisible(0);
            ActiveShadowObjectVisible(-1);
        }

        private void Start() {
            SoundManager.Instance.PlayBGM("Title");
        }

        public override void Run() {
            switch(step) {
                case TitleStep.STEP_1:  Step_1();  break;
                case TitleStep.STEP_2:  Step_2();  break;
                case TitleStep.STEP_3:  Step_3();  break;
                case TitleStep.STEP_4:  Step_4();  break;
                case TitleStep.FadeIn:  FadeIn();  break;
                case TitleStep.FadeOut: FadeOut(); break;
                default:                           break;
            }
        }

        /// <summary>
        /// タイトルロゴの表示、決定ボタン入力後Step２に切り替え
        /// </summary>
        private void Step_1() {
            if(Input.GetButtonDown("Button_Start") || Input.GetButtonDown("Button_A")) {
                ActiveShadowObjectVisible(0);
                step = TitleStep.STEP_2;
            }
        }

        /// <summary>
        /// カメラを引く。いい位置まで来たらStep3に切り替え
        /// </summary>
        private void Step_2() {
            logo.color -= new Color(0,0,0,Time.deltaTime * animationSpeed);
            titleCamera.transform.position =         Vector3.LerpUnclamped(titleCamera.transform.position,cameraNtf.pos,Time.deltaTime * animationSpeed);
            titleCamera.transform.localEulerAngles = Vector3.LerpUnclamped(titleCamera.transform.localEulerAngles,cameraNtf.rot,Time.deltaTime * animationSpeed);
            if(titleCamera.transform.position.z <= cameraNtf.pos.z + 5) {
                step = TitleStep.STEP_3;
            }
        }

        /// <summary>
        /// 左右キーで数字を切り替え、決定ボタン入力後Step4に切り替え
        /// </summary>
        private void Step_3() {
            if(Input.GetAxisRaw("Horizontal") > 0 && !isGetAxis) {
                stageNumber = Wrap(stageNumber + 1,0,stageNumberList.Count);
                ActiveObjectVisible(stageNumber);
                ActiveShadowObjectVisible(stageNumber);
                isGetAxis = true;
            }
            if(Input.GetAxisRaw("Horizontal") < 0 && !isGetAxis) {
                stageNumber = Wrap(stageNumber - 1,0,stageNumberList.Count);
                ActiveObjectVisible(stageNumber);
                ActiveShadowObjectVisible(stageNumber);
                isGetAxis = true;
            }
            if(Input.GetAxisRaw("Horizontal") == 0 && isGetAxis) {
                isGetAxis = false;
            }
            if(Input.GetButtonDown("Button_Start") || Input.GetButtonDown("Button_A")) {
                //StartCoroutine(BlackOut());
                //step = TitleStep.DEFAULT;

                step = TitleStep.FadeOut;
            }
        }

        /// <summary>
        /// シーン読み込み
        /// </summary>
        private void Step_4() {
            StartCoroutine(LoadScene(stageNumberList[stageNumber].path));
            step = TitleStep.DEFAULT;
        }

        private void FadeIn() {
            if(FadeManager.getInstance.FadeIn()) {
                step = TitleStep.STEP_1;
            }
        }

        private void FadeOut() {
            if(FadeManager.getInstance.FadeOut()) {
                step = TitleStep.STEP_4;
            }
        }

        //private IEnumerator BlackOut() {
        //    for(int i = 0;curtain.material.GetFloat("_Threshold") > 0;i++) {
        //        var value = curtain.material.GetFloat("_Threshold");
        //        value = Mathf.Max(value - 0.05f,0);
        //        curtain.material.SetFloat("_Threshold",value);
        //        loadingImage.color += new Color(0,0,0,value);
        //        loadingText.color += new Color(0,0,0,value);
        //        yield return new WaitForEndOfFrame();
        //    }
        //    yield return new WaitForSeconds(1);
        //    step = TitleStep.STEP_4;
        //}

        private IEnumerator LoadScene(string sceneName) {
            AsyncOperation load = SceneManager.LoadSceneAsync(sceneName);
            while(!load.isDone) {
                yield return null;
            }
        }

        /// <summary>
        /// 指定した番号だけアクティブにする
        /// </summary>
        private void ActiveObjectVisible(int active) {
            for(int i = 0;i < stageNumberList.Count;i++) {
                stageNumberList[i].numberObject.SetActive(i == active);
            }
        }
        /// <summary>
        /// 指定した番号の影だけアクティブにする
        /// </summary>
        private void ActiveShadowObjectVisible(int active) {
            for(int i = 0;i < stageNumberList.Count;i++) {
                stageNumberList[i].shadowObject.SetActive(i == active);
            }
        }

        /// <summary>
        /// 上限まで行ったら下限に、下限まで行ったら上限に
        /// </summary>
        private int Wrap(int x,int min,int max) {
            if(min >= max) return x;
            int n = (x - min) % (max - min);
            return (n >= 0) ? (n + min) : (n + max);
        }

    }

}