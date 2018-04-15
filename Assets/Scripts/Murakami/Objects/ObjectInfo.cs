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
        [SerializeField] private GameObject object2D;

        [Range(0,4), SerializeField] private int pos_Y = 0;
        [Range(0,4), SerializeField] private int pos_X = 0;

        private Vector3 object2D_Scale;
        private GameObject shadow;

        private void Awake(){
            object2D_Scale = object2D.transform.localScale;
            shadow = Instantiate(object2D) as GameObject;
            shadow.transform.parent = stage2D.transform;
		}

        private void Start(){
            
        }

        private void Update() {
            
        }

        public void SetPos(int y,int x) {
            pos_Y = y;
            pos_X = x;

            shadow.transform.localPosition = new Vector3(transform.localPosition.x,shadow.transform.localPosition.y,0);

            if(pos_Y == 0) {
                shadow.transform.localScale = object2D_Scale * 0.3f;
            }
            else if(pos_Y == 1) {
                shadow.transform.localScale = object2D_Scale * 0.6f;
            }
            else if(pos_Y == 2) {
                shadow.transform.localScale = object2D_Scale;
            }
            else if(pos_Y == 3) {
                shadow.transform.localScale = object2D_Scale * 1.6f;
            }
            else if(pos_Y == 4) {
                shadow.transform.localScale = object2D_Scale * 1.9f;
            }
        }
    }

}