﻿/*
 *	作成者     :村上和樹
 *	機能説明   :メインシーンをまとめるクラス
 * 	初回作成日 :2018/04/14
 *	最終更新日 :2018/04/14
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village {

    public class GameMaster : Inheritor {

        #region enum
        public enum GameMode {
            Start,          //ゲーム開始時
            Game,           //ゲーム中
            GameClear,      //ゲームクリア時
            GameOver,       //ゲームオーバー時
            Pause,          //ポーズ中
        }
        #endregion

        #region private
        private static GameMaster instance;
        private bool isCountDown = false;
        private int deadCount = 0;
        #endregion

        #region Serialize
        [SerializeField] private PlayerController2D player2d;
        [SerializeField] private float time = 300;
        [SerializeField] private GameMode mode = GameMode.Start;
        [SerializeField] private int deadCountMax = 3;
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
        }

        public override void Run() {
            switch(mode) {
                case GameMode.Start:
                    mode = GameMode.Game;
                    break;
                case GameMode.Game:
                    CountDown();
                    break;
                case GameMode.GameClear:
                    break;
                case GameMode.GameOver:
                    break;
                case GameMode.Pause:
                    break;
                default:
                    break;
            }
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
            deadCount++;
            if(deadCount >= deadCountMax) {
                mode = GameMode.GameOver;
            }
        }


        public void OnGUI() {
            GUILayout.Label("Time      : " + GetTime);
            GUILayout.Label("GameMode  : " + mode);
            GUILayout.Label("deadCount : " + deadCount + " / " + deadCountMax);
        }
    }

}