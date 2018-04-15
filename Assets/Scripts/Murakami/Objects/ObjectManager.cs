/*
 *	作成者     :村上和樹
 *	機能説明   :3Dオブジェクトを管理するマネージャー
 * 	初回作成日 :2018/04/14
 *	最終更新日 :2018/04/14
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village {

    public class ObjectManager : Inheritor {

        #region class

        #endregion

        #region private
        private static ObjectManager instance;
        private GameObject[,] objects = new GameObject[5,5];
        private Vector3[,] positions = new Vector3[5,5];
        #endregion

        #region Propaty
        public static ObjectManager getInstance {
            get {
                return instance;
            }
        }
        #endregion


                   
        private void Awake(){
            instance = this;
            
        }

        public override void Run() {

        }

        public GameObject GetObject(int y,int x) {
            return objects[y,x];
        }

        public void SetObject(int y,int x,GameObject obj) {
            objects[y,x] = obj;
        }

        public Vector3 GetPos(int y,int x) {
            return positions[y,x];
        }

        public void SetPos(int y,int x,Vector3 pos) {
            positions[y,x] = pos;
        }
    }

}