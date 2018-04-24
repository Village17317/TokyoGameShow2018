/*
 *	作成者     :村上和樹
 *	機能説明   :メインシーンをまとめるクラス
 * 	初回作成日 :2018/04/14
 *	最終更新日 :2018/04/14
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

namespace Village {

    public class GameMaster : Inheritor {

        [System.Serializable]
        private class StageUI {
            public Image startCountDownImage; //最初のカウントダウンの表示用
            public Sprite[] startCountDownSprites; //最初のカウントダウンの数字のスプライト
            public Text timeText;       //制限時間の表示
            public Text deadCountText;  //死んだ回数の表示
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
        private int pauseCursorNumber = 0;
        #endregion

        #region Serialize
        [SerializeField] private StageUI stageUI;
        [SerializeField] private GameMode mode = GameMode.Start;
        [SerializeField] private float time = 300;
        [SerializeField] private Light roomLight;
        [SerializeField] private int deadCountMax = 3;
        [SerializeField] private Canvas pauseMenu_U;
        [SerializeField] private Canvas pauseMenu_D;
        [SerializeField] private Transform pauseCursor;
        [SerializeField] private Transform[] pauseCursors = new Transform[3];
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
            InitializeUI();
            StartCoroutine(WaitTime(3));
            range = roomLight.range;
            roomLight.range = 0;
            pauseMenu_U.gameObject.SetActive(false);
            pauseMenu_D.gameObject.SetActive(false);
            SoundManager.Instance.PlayBGM("TestSound");
        }

        public override void Run() {
            switch(mode) {
                case GameMode.Start:
                    roomLight.range += range * Time.deltaTime * 0.5f;
                    if(roomLight.range >= range) {
                        roomLight.range = range;
                    }
                    break;
                case GameMode.Game:
                    isReStart = false;
                    CountDown();
                    if(Input.GetButtonDown("Button_Start")) {
                        OnPause();
                    }
                    break;
                case GameMode.GameReStart:
                    CountDown();
                    if(Input.GetButtonDown("Button_Start")) {
                        OnPause();
                    }
                    if(!isReStart) {
                        isReStart = true;
                        StartCoroutine(ReStartWaitTime(2));
                    }
                    break;
                case GameMode.GameClear:
                    break;
                case GameMode.GameOver:
                    break;
                case GameMode.Pause:
                    PauseMenu();
                    if(Input.GetButtonDown("Button_Start")) {
                        OffPause();
                    }
                    break;
                default:
                    break;
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
            for(int i = 0;i < second;i++) {
                stageUI.startCountDownImage.sprite = stageUI.startCountDownSprites[(int) (second - i)];
                yield return new WaitForSeconds(1);
            }
            stageUI.startCountDownImage.sprite = stageUI.startCountDownSprites[0];

            Destroy(stageUI.startCountDownImage.gameObject,1);
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
                stageUI.timeText.text = "Time : " + time.ToString("F0");
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
        /// 死んだ回数をカウント
        /// </summary>
        public void DeadCountUp() {
            stageUI.deadCountText.text = "DeadCount : " +  deadCount + " / " + deadCountMax;
            deadCount++;
            if(deadCount >= deadCountMax) {
                mode = GameMode.GameOver;
            }
        }

        /// <summary>
        /// pause画面に切り替える
        /// </summary>
        public void OnPause() {
            mode = GameMode.Pause;
            pauseMenu_U.gameObject.SetActive(true);
            pauseMenu_D.gameObject.SetActive(true);
        }

        /// <summary>
        /// pause画面を終了する
        /// </summary>
        public void OffPause() {
            mode = GameMode.Game;
            pauseMenu_U.gameObject.SetActive(false);
            pauseMenu_D.gameObject.SetActive(false);
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
                pauseCursorNumber--;
                if(pauseCursorNumber < 0) {
                    pauseCursorNumber = 0;
                }
                pauseCursor.position = pauseCursors[pauseCursorNumber].position;
            }
            else if(Input.GetAxis("Vertical") < 0 && !isStick) {
                isStick = true;
                pauseCursorNumber++;
                if(pauseCursorNumber >= pauseCursors.Length) {
                    pauseCursorNumber = pauseCursors.Length -1;
                }
                pauseCursor.position = pauseCursors[pauseCursorNumber].position;
            }
            else if(Input.GetAxis("Vertical") == 0) {
                isStick = false;
            }

            if(Input.GetButtonDown("Button_A")) {
                if(pauseCursorNumber == 0) {
                    OffPause();
                }
                else if(pauseCursorNumber == 1) {
                    SceneChenge(SceneManager.GetActiveScene().name);
                }
                else if(pauseCursorNumber == 2) {
                    SceneChenge("Title");//ステージ選択画面に移行_debug
                }
            }
        }

        /// <summary>
        /// 渡したシーンの名前でシーンを切り替える
        /// </summary>
        private void SceneChenge(string sceneName) {
            SceneManager.LoadScene(sceneName);
        }

        /// <summary>
        /// UIの初期化処理
        /// </summary>
        private void InitializeUI() {
            //stageUI.startCountText.text = "CountDown : ";
            stageUI.timeText.text = "Time : " + time.ToString();
            stageUI.deadCountText.text = "DeadCount : " + deadCount + " / " + deadCountMax;
        }
    }

}