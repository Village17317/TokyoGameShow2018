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
            public Material imageMat;
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
        [Header("文字"), SerializeField]              private Image logoTextImage;
        [Header("ステージセレクトロゴ"),SerializeField] private Image selectLogo;
        [Header("左矢印"), SerializeField]            private SpriteRenderer leftArrow;
        [Header("右矢印"), SerializeField]            private SpriteRenderer rightArrow;
        [Header(""), SerializeField]                  private float addAlphaSpeed = 0.05f;
        [Header("ステージの内容"),SerializeField]      private List<NextSceneInfo> stageNumberList;

        [Header("環境光"), SerializeField] private Light directionalLight;
        [Header("ポイントライト"), SerializeField] private Light pointLight;

        [Header("ステージセレクト時に見えるオブジェクト"), SerializeField] private GameObject[] objects;

        private float range = 0;

        private TitleStep step = TitleStep.FadeIn;
        private bool isGetAxis = false;
        private int stageNumber = 0;
        private int addAlpha = -1;

        private void Awake() {
            FadeManager.getInstance.SetCanvasCamera(Camera.main);
            ActiveObjectVisible(-1);
            ActiveShadowObjectVisible(-1);
            ActiveImageObject(-1);
            ActiveOtherObjects(false);
            range = pointLight.range;
            pointLight.range = 0;
            selectLogo.color = new Color(1,1,1,0);
            Cursor.visible = false;
        }

        private void Start() {
            SoundManager.Instance.PlayBGM("Title");
        }

        public override void Run() {
            ExitGame();

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
            LightAnimation();
            LogoTextAnimation();
            if(Input.GetButtonDown("Button_Start") || Input.GetButtonDown("Button_A")) {
                SoundManager.Instance.PlaySE("select",transform);
                ActiveObjectVisible(0);
                ActiveShadowObjectVisible(0);
                ActiveImageObject(0);
                ActiveOtherObjects(true);
                step = TitleStep.STEP_2;
            }
        }

        /// <summary>
        /// カメラを引く。いい位置まで来たらStep3に切り替え
        /// </summary>
        private void Step_2() {
            LightAnimation();
            logo.color -= new Color(0,0,0,Time.deltaTime * animationSpeed);
            logoTextImage.color -= new Color(0,0,0,Time.deltaTime * animationSpeed);
            selectLogo.color += new Color(0,0,0,Time.deltaTime * animationSpeed);
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
            LightAnimation();
            if(Input.GetAxisRaw("Horizontal") > 0 && !isGetAxis) {
                stageNumber = Wrap(stageNumber + 1,0,stageNumberList.Count);
                ActiveObjectVisible(stageNumber);
                ActiveShadowObjectVisible(stageNumber);
                ActiveImageObject(stageNumber);
                SoundManager.Instance.PlaySE("choice",transform);
                StartCoroutine(ArrowAnimation(rightArrow));
                isGetAxis = true;
            }
            if(Input.GetAxisRaw("Horizontal") < 0 && !isGetAxis) {
                stageNumber = Wrap(stageNumber - 1,0,stageNumberList.Count);
                ActiveObjectVisible(stageNumber);
                ActiveShadowObjectVisible(stageNumber);
                ActiveImageObject(stageNumber);
                SoundManager.Instance.PlaySE("choice",transform);
                StartCoroutine(ArrowAnimation(leftArrow));
                isGetAxis = true;
            }
            if(Input.GetAxisRaw("Horizontal") == 0 && isGetAxis) {
                isGetAxis = false;
            }
            if(Input.GetButtonDown("Button_Start") || Input.GetButtonDown("Button_A")) {
                directionalLight.gameObject.SetActive(false);
                pointLight.gameObject.SetActive(false);

                SoundManager.Instance.PlaySE("select",transform);

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

        /// <summary>
        /// フェードイン
        /// </summary>
        private void FadeIn() {
            if(FadeManager.getInstance.FadeIn()) {
                step = TitleStep.STEP_1;
            }
        }

        /// <summary>
        /// フェードアウト
        /// </summary>
        private void FadeOut() {
            if(FadeManager.getInstance.FadeOut()) {
                step = TitleStep.STEP_4;
            }
        }

        /// <summary>
        /// 指定した名前のシーンをロード
        /// </summary>
        private IEnumerator LoadScene(string sceneName) {
            AsyncOperation load = SceneManager.LoadSceneAsync(sceneName);
            while(!load.isDone) {
                yield return null;
            }
        }

        /// <summary>
        /// ポイントライトをチカチカさせる
        /// </summary>
        private void LightAnimation() {
            pointLight.range += Mathf.Max(range * Time.deltaTime * 5,0);
            if(pointLight.range >= range) {
                pointLight.intensity = Random.Range(1,1.03f);
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
        /// 指定した番号のステージイメージだけアクティブにする
        /// </summary>
        /// <param name="active"></param>
        private void ActiveImageObject(int active) {
            for(int i = 0;i < stageNumberList.Count;i++) {
                StartCoroutine(StageImageAlphaChenge(i,i == active));
            }
        }

        /// <summary>
        /// ステージセレクト時に見えるオブジェクト
        /// </summary>
        private void ActiveOtherObjects(bool isActive) {
            for(int i = 0;i < objects.Length;i++) {
                objects[i].SetActive(isActive);
            }
        }

        /// <summary>
        /// Press Start Buttonの点滅アニメーション
        /// </summary>
        private void LogoTextAnimation() {
            var color = logoTextImage.color;
            color.a += addAlpha * addAlphaSpeed;
            if(color.a < 0) {
                addAlpha *= -1;
                color.a = 0;
            }
            else if(1 < color.a) {
                addAlpha *= -1;
                color.a = 1;
            }
            logoTextImage.color = color;
        }

        /// <summary>
        /// 矢印のアニメーション
        /// </summary>
        private IEnumerator ArrowAnimation(SpriteRenderer arrow) {
            arrow.color = new Color(1,1,1,1);
            for(int i = 0;arrow.color.a > 0;i++) {
                arrow.color -= new Color(0,0,0,0.2f);
                yield return new WaitForEndOfFrame();
            }
            for(int i = 0;arrow.color.a <= 1;i++) {
                arrow.color += new Color(0,0,0,0.2f);
                yield return new WaitForEndOfFrame();
            }
            arrow.color = new Color(1,1,1,1);
        }

        /// <summary>
        /// ステージイメージの透明度の変更
        /// </summary>
        private IEnumerator StageImageAlphaChenge(int num,bool isFlag) {
            var color = stageNumberList[num].imageMat.color;
            if(isFlag) {
                while(color.a <= 1) {
                    color.a = Mathf.Min(color.a + 0.05f,1);
                    stageNumberList[num].imageMat.color = color;
                    yield return new WaitForEndOfFrame();
                }
            }
            else {
                while(color.a >= 0) {
                    color.a = Mathf.Max(color.a - 0.05f,0);
                    stageNumberList[num].imageMat.color = color;
                    yield return new WaitForEndOfFrame();
                }
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

        /// <summary>
        /// Escapeキーでゲーム終了
        /// </summary>
        private void ExitGame() {
            if(Input.GetKeyDown(KeyCode.Escape)) {
                Application.Quit();
            }
        }
    }

}