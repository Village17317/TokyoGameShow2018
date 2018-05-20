/*
 *	作成者     :村上和樹
 *	機能説明   :タイトル画面のプレイヤー
 * 	初回作成日 :2018/05/21
 *	最終更新日 :2018/05/21
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village {

    public class TitlePlayer : MonoBehaviour {
        [SerializeField] private MoveLimit horizontal;
        [SerializeField] private MoveLimit vertical;
        [SerializeField] private float speed = 0.1f;

        [SerializeField]
        private string nextSceneName;
        private bool isCollide = false;

        public string NextSceneName {
            get {
                return nextSceneName;
            }
        }

        public bool PlayerUpdate() {
            Move();
            return isCollide && Input.GetButtonDown("Button_Start");
        }

        public void SetIsCollider(bool flag) {
            isCollide = flag;
        }

        public void SetNextScene(string sceneName) {
            nextSceneName = sceneName;
        }

        private void Move() {
            //一旦格納
            Vector3 pos = transform.position;

            //位置制限
            pos += new Vector3(speed * Input.GetAxisRaw("Horizontal"),0,speed * Input.GetAxisRaw("Vertical"));
            pos = new Vector3(Mathf.Clamp(pos.x,horizontal.min,horizontal.max),
                              pos.y,
                              Mathf.Clamp(pos.z,vertical.min,vertical.max));
            //再格納
            transform.position = pos;
        }
    }

}