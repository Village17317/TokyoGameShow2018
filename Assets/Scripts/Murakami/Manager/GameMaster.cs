/*
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
        #region private
        private static GameMaster instance;
        #endregion

        #region Serialize
        [SerializeField] private PlayerController2D player2d;
        [SerializeField] private WorldChenge world;
        #endregion

        #region Propaty
        public static GameMaster getInstance {
            get {
                return instance;
            }
        }
        public WorldChenge World {
            get {
                return world;
            }
        }
        #endregion

        private void Awake() {
            instance = this;
        }

    }

}