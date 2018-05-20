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

    public class TitleManager : Inheritor {
        [System.Serializable]
        private class NextTransform {
            public Vector3 pos;
            public Vector3 rot;
        }

        private enum TitleStep {
            STEP_1,
            STEP_2,
            STEP_3,
            STEP_4,
            DEFAULT,
        }

        [SerializeField] private Camera titleCamera;
        [SerializeField] private NextTransform cameraNtf;
        [SerializeField] private float animationSpeed = 1;
        [SerializeField] private Image logo;
        [SerializeField] private Image cartain;
        [SerializeField] private TitlePlayer player;
        private TitleStep step = TitleStep.STEP_1;

        private void Awake() {
            cartain.color = new Color(0,0,0,0);
        }

        public override void Run() {
            switch(step) {
                case TitleStep.STEP_1: Step_1(); break;
                case TitleStep.STEP_2: Step_2(); break;
                case TitleStep.STEP_3: Step_3(); break;
                case TitleStep.STEP_4: Step_4(); break;
                default:                         break;
            }
        }

        /// <summary>
        /// タイトルロゴの表示、決定ボタン入力後Step２に切り替え
        /// </summary>
        private void Step_1() {
            if(Input.GetButtonDown("Button_Start")) {       
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
        /// プレイヤーを数字の前まで動かし、決定ボタン入力後Step4に切り替え
        /// </summary>
        private void Step_3() {
            if(player.PlayerUpdate()) {
                step = TitleStep.STEP_4;
            }
        }


        private void Step_4() {
            StartCoroutine(LoadScene(player.NextSceneName));
            step = TitleStep.DEFAULT;
        }

        private IEnumerator LoadScene(string sceneName) {
            AsyncOperation load = SceneManager.LoadSceneAsync(sceneName);
            while(!load.isDone) {
                Debug.Log(load.progress);
                cartain.color = new Color(0,0,0,load.progress);
                yield return null;
            }
        }
    }

}
#region
//private enum TitleStep {
//    STEP_1,
//    STEP_2,
//    STEP_3,
//    DEFAULT,
//}
//[SerializeField]
//private ParticleSystem fireEffect;
//[SerializeField]
//private Light pointLight;
//[SerializeField]
//private Light roomLight;
//[SerializeField]
//private string nextSceneName = "";
//[SerializeField]
//private TitleStep step = TitleStep.STEP_1;
//private float spotRange = 0;
//private bool flag = false;

//private void Awake() {
//    spotRange = roomLight.range;
//    roomLight.range = 0;
//    pointLight.enabled = false;
//}
//private void Start() {

//}
//private void Update() {
//    switch(step) {
//        case TitleStep.STEP_1:
//        Step_1();
//        break;
//        case TitleStep.STEP_2:
//        Step_2();
//        break;
//        case TitleStep.STEP_3:
//        Step_3();
//        break;
//        default:
//        break;
//    }
//}
//private IEnumerator StartCol() {
//    yield return new WaitForSeconds(2);
//    fireEffect.Play();
//    pointLight.enabled = true;
//    yield return new WaitForSeconds(0.5f);
//    step = TitleStep.STEP_2;
//}
//private void SpotLightAnimation() {
//    if(flag) {
//        roomLight.range -= Time.deltaTime * spotRange;
//        if(roomLight.range <= spotRange * 0.98f) {
//            roomLight.range = spotRange * 0.98f;
//            flag = false;
//        }
//    }
//    else {
//        roomLight.range += Time.deltaTime * spotRange;
//        if(roomLight.range >= spotRange) {
//            roomLight.range = spotRange;
//            flag = true;
//        }
//    }
//}
//private IEnumerator LoadScene(string sceneName) {
//    AsyncOperation load = SceneManager.LoadSceneAsync(sceneName);
//    while(!load.isDone) {
//        Debug.Log(load.progress);
//        yield return null;
//    }
//}
//private void Step_1() {
//    StartCoroutine(StartCol());
//    step = TitleStep.DEFAULT;
//}
//private void Step_2() {
//    SpotLightAnimation();
//    if(Input.GetButtonDown("Button_Start")) {
//        StartCoroutine(LoadScene(nextSceneName));
//    }
//}
//private void Step_3() {

//}
#endregion