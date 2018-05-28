/*
 *	作成者     :村上和樹
 *	機能説明   :オブジェクトの情報
 * 	初回作成日 :2018/04/15
 *	最終更新日 :2018/05/07
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village {

    public class ObjectInfo : MonoBehaviour {
        [SerializeField] private GameObject shadowObj;      //影にするオブジェクト
        [SerializeField] private Material shadowMatOrigin;  //影のマテリアルの元
        [SerializeField] private Vector3 offset;
        private LayerMask mask = 1 << 8; //WallLayer
        
        private Transform lightTf;//光源（Player）の位置
        private Transform rayTf;//Rayを出す位置
        private Transform shadowTf;//影の位置

        private Material shadowMat; //割り当てる影
        private Vector3 constScale; //影の最初の大きさ
        private Collider[] colliders;//

        private void Awake() {
            lightTf = GameObject.FindGameObjectWithTag("Light").transform;

            GameObject shadow = Instantiate(shadowObj) as GameObject;
            shadowTf = shadow.transform;

            constScale = shadowTf.localScale;

            GameObject rayObj = new GameObject();
            rayObj.transform.position = lightTf.position;
            rayTf = rayObj.transform;
            rayTf.parent = lightTf;

            shadowMat = new Material(shadowMatOrigin);
            MeshRenderer[] meshes =  shadow.GetComponentsInChildren<MeshRenderer>();
            foreach(MeshRenderer m in meshes) {
                m.material = shadowMat;
            }

            colliders = shadow.GetComponentsInChildren<Collider>();

            Vector3 target = transform.position + offset;
            target.z = Mathf.Abs(target.z);
            rayTf.LookAt(target);

            RayHit();
            SetActive(true);
            ShadowTransParent();
            ShadowSizeChenge();
            //ActiveCollider();
            ConnectRotation();
        }

        private void Update() {
            Vector3 target = transform.position + offset;
            //target.z = Mathf.Abs(target.z);
            rayTf.LookAt(target);

            RayHit();
            if(CheckScreenOut(shadowTf.position)) {
                SetActive(false);
            }
            else {
                SetActive(true);
                ShadowTransParent();
                ShadowSizeChenge();
                //ActiveCollider();
                ConnectRotation();
            }
        }

        /// <summary>
        /// カメラ外かどうかの判定
        /// </summary>
        private bool CheckScreenOut(Vector3 origin) {
            Vector3 view_pos = Camera.main.WorldToViewportPoint(origin);
            if(view_pos.x < -0.0f ||
               view_pos.x > 1.0f ||
               view_pos.y < -0.0f ||
               view_pos.y > 1.0f) {
                // 範囲外 
                return true;
            }
            else {
                // 範囲内 
                return false;
            }
        }

        /// <summary>
        /// 影と自分の回転は同じにする
        /// </summary>
        private void ConnectRotation() {
            shadowTf.rotation = transform.rotation;
        }

        /// <summary>
        /// 距離に応じて色の薄さを変える
        /// </summary>
        private void ShadowTransParent() {
            float prop = GetLength(lightTf.position,shadowTf.position,transform.position);

            shadowMat.color = new Color(0,0,0,1.3f - prop);
        }

        /// <summary>
        /// 距離に応じて大きさを変える
        /// </summary>
        private void ShadowSizeChenge() {
            float prop = GetLength(lightTf.position,shadowTf.position,transform.position);
            shadowTf.localScale = constScale * (1.3f - prop);
        }

        /// <summary>
        /// 距離に応じてコライダーの有無を切り替える
        /// </summary>
        private void ActiveCollider() {
            float prop = GetLength(lightTf.position,shadowTf.position,transform.position);
            bool active = prop < 0.6f;

            foreach(Collider c in colliders) {
                c.enabled = active;
            }
        }

        /// <summary>
        /// rayを出し、影を出す位置を取得
        /// </summary>
        private void RayHit() {
            Ray ray = new Ray(rayTf.position,rayTf.forward);
            Debug.DrawRay(rayTf.position,rayTf.forward * 100000);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit,Mathf.Infinity,mask)) {
                if(hit.collider.gameObject.tag == "Wall") {
                    shadowTf.position = hit.point;
                }
            }
        }

        /// <summary>
        /// （光源から自分）/（光源から影）
        /// </summary>
        /// <returns>割合を返す０～１</returns>
        private float GetLength(Vector3 p1,Vector3 p2,Vector3 p3) {
            float maxLength = (p2 - p1).magnitude;//ろうそくから影までの長さ
            float length = (p3 - p1).magnitude;   //ろうそくからオブジェクトまでの長さ
            float prop = length / maxLength;      //割合
            prop = prop >= 1 ? 1 : prop;

            return prop;      
        }

        /// <summary>
        /// Colliderの有無の切り替え
        /// </summary>
        private void SetActive(bool flag) {
            foreach(Collider c in colliders) {
                c.enabled = flag;
            }
        }
    }


}