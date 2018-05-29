/*
 *	作成者     :村上和樹
 *	機能説明   :メインシーンをまとめるクラス
 * 	初回作成日 :2018/04/14
 *	最終更新日 :2018/05/01
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Village {

    public class GameMaster : Inheritor {

        [System.Serializable]
        private class StageUI {
            public Image startCountDownImage; //最初のカウントダウンの表示用
        }

        [System.Serializable]
        private class OtherCanvas {
            [System.NonSerialized] public int cursorNumber = 0;
            public Canvas canvas;
            public Transform cursorTf;
            public Transform[] cursorPositions;
        }

        #region enum
        public enum GameMode {
            Start,          //ゲーム開始時
            Game,           //ゲーム中
            GameReStart,    //
            GameClear,      //ゲームクリア時
            GameOver,       //ゲームオーバー時
            Pause,          //ポーズ中
        }
        #endregion

        #region private
        private static GameMaster instance;
        private bool isCountDown = false;
        private int deadCount = 0;
        private bool isReStart = false;
        private float range = 0;
        private bool isStick = false;
        private float time = 0;
        #endregion

        #region Serialize
        [Space(8)]
        [SerializeField] private StageUI stageUI;
        [Space(8)]
        [SerializeField] private GameMode mode = GameMode.Start;
        [SerializeField] private float    maxTime = 300;//最大時間
        [SerializeField] private Light    roomLight;
        [SerializeField] private int      deadCountMax = 3;

        [Space(16)]
        [SerializeField] private OtherCanvas pauseMenuCanvas;
        [Space(16)]
        [SerializeField] private OtherCanvas gameClearCanvas;
        [Space(16)]
        [SerializeField] private OtherCanvas gameOverCanvas;
        [Space(16)]
        [SerializeField] private Image       blackCurtain;

        [SerializeField] private string nextScene = "";
        [SerializeField] private string titleScene = "Scene_Title";
        #endregion

        #region Propaty
        public static GameMaster getInstance {
            get {
                return instance;
            }
        }
        public float GetTime {
            get {
                return time;
            }
        }
        public GameMode GetGameMode {
            get {
                return mode;
            }
        }
        #endregion

        private void Awake() {
            instance = this;

            time = maxTime;
            InitializeUI();
            StartCoroutine(WaitTime(-1));
            range = roomLight.range;
            roomLight.range = 0;

            pauseMenuCanvas.canvas.gameObject.SetActive(false);
            gameClearCanvas.canvas.gameObject.SetActive(false);
            gameOverCanvas.canvas.gameObject.SetActive(false);

            PlayBGM(SceneManager.GetActiveScene().name);
        }

        public override void Run() {
            roomLight.intensity = Random.Range(1,1.08f);
            switch(mode) {
                case GameMode.Start:        OnStart();      break;
                case GameMode.Game:         OnGame();       break;
                case GameMode.GameReStart:  OnReStart();    break;
                case GameMode.GameClear:    OnGameClear();  break;
                case GameMode.GameOver:     OnGameOver();   break;
                case GameMode.Pause:        PauseMenu();    break;
                default:                                    break;
            }
        }

        private IEnumerator ReStartWaitTime(float second) {
            yield return new WaitForSeconds(second);
            mode = GameMode.Game;
        }

        /// <summary>
        /// 指定した時間後にGameModeをGameにする。
        /// </summary>
        private IEnumerator WaitTime(float second) {
            yield return new WaitForSeconds(0.5f);

            for(int i = 0;i < 30;i++) {
                var value = stageUI.startCountDownImage.material.GetFloat("_Threshold");
                value = Mathf.Min(value + 0.05f,1);
                stageUI.startCountDownImage.material.SetFloat("_Threshold",value);
                yield return new WaitForEndOfFrame();
            }
            stageUI.startCountDownImage.gameObject.SetActive(false);
            StartCountDown();
            mode = GameMode.Game;
            

        }

        /// <summary>
        /// 時間の計測
        /// </summary>
        private void CountDown() {
            if(isCountDown) {
                time -= Time.deltaTime;
                if(time <= 0) {
                    mode = GameMode.GameOver;
                }
                //stageUI.timeImage.fillAmount = time / maxTime;
            }
        }

        /// <summary>
        /// 時間の計測の開始、再開
        /// </summary>
        private void StartCountDown() {
            isCountDown = true;
        }

        /// <summary>
        /// 時間の計測の停止
        /// </summary>
        private void StopCountDown() {
            isCountDown = false;
        }

        /// <summary>
        /// 外部からのゲームモードの設定
        /// </summary>
        public void SetGameMode(GameMode nextMode) {
            mode = nextMode;
        }

        /// <summary>
        /// 死んだ回数をカウント 使わないかも
        /// </summary>
        public void DeadCountUp() {
            deadCount++;
            if(deadCount >= deadCountMax) {
                mode = GameMode.GameOver;
            }
        }

        /// <summary>
        /// スタート時の処理
        /// </summary>
        private void OnStart() {
            roomLight.range += range * Time.deltaTime * 0.5f;
            if(roomLight.range >= range) {
                roomLight.range = range;
            }
        }

        /// <summary>
        /// ゲーム中の処理
        /// </summary>
        private void OnGame() {
            isReStart = false;
            //CountDown();
            if(Input.GetButtonDown("Button_Start")) {
                OnPause();
            }
        }

        /// <summary>
        /// リスタート時
        /// </summary>
        private void OnReStart() {
            //CountDown();
            if(Input.GetButtonDown("Button_Start")) {
                OnPause();
            }
            if(!isReStart) {
                isReStart = true;
                StartCoroutine(ReStartWaitTime(2));
            }
        }

        /// <summary>
        /// pause画面に切り替える
        /// </summary>
        public void OnPause() {
            mode = GameMode.Pause;
            pauseMenuCanvas.canvas.gameObject.SetActive(true);
        }

        /// <summary>
        /// pause画面を終了する
        /// </summary>
        public void OffPause() {
            mode = GameMode.Game;
            pauseMenuCanvas.canvas.gameObject.SetActive(false);
        }

        /// <summary>
        /// ポーズ画面中の処理
        /// ステージをやりなおす
        /// ステージ選択に戻る
        /// ゲームに戻る
        /// </summary>
        public void PauseMenu() {
            if(Input.GetAxis("Vertical") > 0 && !isStick) {
                isStick = true;
                pauseMenuCanvas.cursorNumber--;
                if(pauseMenuCanvas.cursorNumber < 0) {
                    pauseMenuCanvas.cursorNumber = 0;
                }
                pauseMenuCanvas.cursorTf.position = pauseMenuCanvas.cursorPositions[pauseMenuCanvas.cursorNumber].position;
            }
            else if(Input.GetAxis("Vertical") < 0 && !isStick) {
                isStick = true;
                pauseMenuCanvas.cursorNumber++;
                if(pauseMenuCanvas.cursorNumber >= pauseMenuCanvas.cursorPositions.Length) {
                    pauseMenuCanvas.cursorNumber = pauseMenuCanvas.cursorPositions.Length - 1;
                }
                pauseMenuCanvas.cursorTf.position = pauseMenuCanvas.cursorPositions[pauseMenuCanvas.cursorNumber].position;
            }
            else if(Input.GetAxis("Vertical") == 0) {
                isStick = false;
            }

            if(Input.GetButtonDown("Button_A")) {
                if(pauseMenuCanvas.cursorNumber == 0) {
                    OffPause();
                }
                else if(pauseMenuCanvas.cursorNumber == 1) {
                    StartCoroutine(SceneChenge(SceneManager.GetActiveScene().name));
                }
                else if(pauseMenuCanvas.cursorNumber == 2) {
                    StartCoroutine(SceneChenge(titleScene));//ステージ選択画面に移行_debug
                }
            }
            if(Input.GetButtonDown("Button_Start")) {
                OffPause();
            }

        }

        /// <summary>
        /// ゲームクリア時
        /// </summary>
        public void OnGameClear() {
            gameClearCanvas.canvas.gameObject.SetActive(true);

            if(Input.GetAxis("Vertical") > 0 && !isStick) {
                isStick = true;
                gameClearCanvas.cursorNumber--;
                if(gameClearCanvas.cursorNumber < 0) {
                    gameClearCanvas.cursorNumber = 0;
                }
                gameClearCanvas.cursorTf.position = gameClearCanvas.cursorPositions[gameClearCanvas.cursorNumber].position;
            }
            else if(Input.GetAxis("Vertical") < 0 && !isStick) {
                isStick = true;
                gameClearCanvas.cursorNumber++;
                if(gameClearCanvas.cursorNumber >= gameClearCanvas.cursorPositions.Length) {
                    gameClearCanvas.cursorNumber = gameClearCanvas.cursorPositions.Length - 1;
                }
                gameClearCanvas.cursorTf.position = gameClearCanvas.cursorPositions[gameClearCanvas.cursorNumber].position;
            }
            else if(Input.GetAxis("Vertical") == 0) {
                isStick = false;
            }

            if(Input.GetButtonDown("Button_A")) {
                if(gameClearCanvas.cursorNumber == 0) {//次のステージシーンへ
                    StartCoroutine(SceneChenge(nextScene));
                }
                else if(gameClearCanvas.cursorNumber == 1) {//もう一度やりましょう
                    StartCoroutine(SceneChenge(SceneManager.GetActiveScene().name));
                }
                else if(gameClearCanvas.cursorNumber == 2) {
                    StartCoroutine(SceneChenge(titleScene));//ステージ選択画面に移行
                }
            }
        }

        /// <summary>
        /// ゲームオーバー時の処理
        /// </summary>
        private void OnGameOver() {
            gameOverCanvas.canvas.gameObject.SetActive(true);

            if(Input.GetAxis("Vertical") > 0 && !isStick) {
                isStick = true;
                gameOverCanvas.cursorNumber--;
                if(gameOverCanvas.cursorNumber < 0) {
                    gameOverCanvas.cursorNumber = 0;
                }
                gameOverCanvas.cursorTf.position = gameOverCanvas.cursorPositions[gameOverCanvas.cursorNumber].position;
            }
            else if(Input.GetAxis("Vertical") < 0 && !isStick) {
                isStick = true;
                gameOverCanvas.cursorNumber++;
                if(gameOverCanvas.cursorNumber >= gameOverCanvas.cursorPositions.Length) {
                    gameOverCanvas.cursorNumber = gameOverCanvas.cursorPositions.Length - 1;
                }
                gameOverCanvas.cursorTf.position = gameOverCanvas.cursorPositions[gameOverCanvas.cursorNumber].position;
            }
            else if(Input.GetAxis("Vertical") == 0) {
                isStick = false;
            }

            if(Input.GetButtonDown("Button_A")) {
                if(gameOverCanvas.cursorNumber == 0) {//次のステージシーンへ
                    StartCoroutine(SceneChenge(SceneManager.GetActiveScene().name));
                }
                else if(gameOverCanvas.cursorNumber == 1) {//もう一度やりましょう
                    StartCoroutine(SceneChenge(titleScene));//ステージ選択画面に移行
                }
            }
        }

        /// <summary>
        /// 渡したシーンの名前でシーンを切り替える
        /// </summary>
        private IEnumerator SceneChenge(string sceneName) {
            for(int i = 0;blackCurtain.color.a <= 1;i++) {
                blackCurtain.color += new Color(0,0,0,0.01f);
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(sceneName);
        }

        /// <summary>
        /// UIの初期化処理
        /// </summary>
        private void InitializeUI() {

            //stageUI.timeImage.fillAmount = time / maxTime;
        }

        /// <summary>
        /// 指定したBGMを再生
        /// </summary>
        private void PlayBGM(string bgmName) {
            SoundManager.Instance.PlayBGM(bgmName);
        }

        private void OnDisable() {
            stageUI.startCountDownImage.material.SetFloat("_Threshold",0);
        }
    }

}