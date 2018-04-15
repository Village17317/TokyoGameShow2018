/*
 *	作成者     :村上和樹
 *	機能説明   :2D世界と3D世界の切り替え
 * 	初回作成日 :2018/04/14
 *	最終更新日 :2018/04/14
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village {

    public class WorldChenge : Inheritor {
        #region enum
        
        public enum WorldType {
            world_2D,
            world_3D,
            MAX,
        }

        #endregion

        #region SerializeField

        [SerializeField] private WorldType type = WorldType.world_2D;
        [SerializeField] private Camera[] cameras;

        #endregion

        #region private

        #endregion

        private void Awake() {
            for(int i = 0;i < cameras.Length;i++) {
                if(i == 0) {
                    cameras[i].gameObject.SetActive(true);
                }
                else {
                    cameras[i].gameObject.SetActive(false);
                }
            }
        }

        public override void Run() {
            if(Input.GetButtonDown("Button_LB") || Input.GetButtonDown("Button_RB")) {
                type++;
                if(type >= WorldType.MAX) {
                    type = 0;
                }
                for(int i = 0;i < cameras.Length;i++) {
                    if(i == (int)type) {
                        cameras[i].gameObject.SetActive(true);
                    }
                    else {
                        cameras[i].gameObject.SetActive(false);
                    }
                }
            }
        }

        public bool IsType(WorldType _type) {
            return type == _type ? true : false;
        }

    }

}