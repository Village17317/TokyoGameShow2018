/*
 *	作成者     :村上和樹
 *	機能説明   :指定位置にオブジェクトが存在するかどうかの検知
 * 	初回作成日 :2018/04/14
 *	最終更新日 :2018/04/14
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village {

    public class ObjectTrigger : MonoBehaviour {
        [Range(0,4), SerializeField] private int pos_Y = 0;
        [Range(0,4), SerializeField] private int pos_X = 0;

        private void Awake(){

        }

        private void Start(){
            ObjectManager.getInstance.SetPos(pos_Y,pos_X,transform.position);
            Destroy(gameObject.GetComponent<BoxCollider>());
            Destroy(gameObject.GetComponent<Rigidbody>());
        }

        private void OnTriggerEnter(Collider other) {
            if(other.tag == "Object3D") {
                ObjectManager.getInstance.SetObject(pos_Y,pos_X,other.gameObject);
                other.GetComponent<ObjectInfo>().SetPos(pos_Y,pos_X);
            }
        }
    }

}