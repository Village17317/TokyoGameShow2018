/*
 *	作成者     :村上和樹
 *	機能説明   :オブジェクトの情報
 * 	初回作成日 :2018/04/15
 *	最終更新日 :2018/04/15
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village {

    public class ObjectInfo : MonoBehaviour {
        [SerializeField] private GameObject stage2D;
        [SerializeField] private GameObject[] object2D = new GameObject[4];//ここを配列化

        [Range(0,6), SerializeField] private int pos_Y = 0;
        [Range(0,6), SerializeField] private int pos_X = 0;

        private Vector3 object2D_Scale;
        private GameObject shadow;
        private SpriteRenderer shadowRenderer;

        private void Awake(){
            object2D_Scale = object2D[0].transform.localScale;
            shadow = Instantiate(object2D[0]) as GameObject;
            shadow.transform.parent = stage2D.transform;
            shadowRenderer = shadow.GetComponent<SpriteRenderer>();

 
        }


        /// <summary>
        /// 位置とサイズの設定
        /// </summary>
        public void SetPos(int y,int x) {
            pos_Y = y;
            pos_X = x;

            shadow.transform.localPosition = new Vector3(transform.localPosition.x,shadow.transform.localPosition.y,0);

            if(pos_Y == 0) {
                shadow.transform.localScale = object2D_Scale * 0.25f;
                ShadowDarkness(0.4f);
            }
            else if(pos_Y == 1) {
                shadow.transform.localScale = object2D_Scale * 0.5f;
                ShadowDarkness(0.5f);
            }
            else if(pos_Y == 2) {
                shadow.transform.localScale = object2D_Scale * 0.75f;
                ShadowDarkness(0.6f);
            }
            else if(pos_Y == 3) {
                shadow.transform.localScale = object2D_Scale;
                ShadowDarkness(0.7f);
            }
            else if(pos_Y == 4) {
                shadow.transform.localScale = object2D_Scale * 1.25f;
                ShadowDarkness(0.8f);
            }
            else if(pos_Y == 5) {
                shadow.transform.localScale = object2D_Scale * 1.5f;
                ShadowDarkness(0.9f);
            }
            else if(pos_Y == 6) {
                shadow.transform.localScale = object2D_Scale * 1.75f;
                shadowRenderer.color = Color.black;
            }
        }

        /// <summary>
        /// 回転の設定
        /// </summary>
        public void SetRotate(int rotate_Y) {
            Destroy(shadow);
            object2D_Scale = object2D[rotate_Y].transform.localScale;
            shadow = Instantiate(object2D[rotate_Y]) as GameObject;
            shadow.transform.parent = stage2D.transform;
            shadowRenderer = shadow.GetComponent<SpriteRenderer>();
            SetPos(pos_Y,pos_X);
        }

        /// <summary>
        /// 影の濃さの設定 1.0f ~ 0f;
        /// </summary>
        public void ShadowDarkness(float alpha) {
            if(alpha >= 1) {
                alpha = 1;
            }
            else if(alpha <= 0){
                alpha = 0;
            }
            shadowRenderer.color = Color.black - new Color(0,0,0,1 - alpha);
        }
    }

}