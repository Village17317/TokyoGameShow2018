﻿/*
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

        [SerializeField] private float offset;

        public bool isStatic = false;
        public bool isChoice = false;

        private Transform candleTf;//ろうそくの位置
        private Transform rayTf;//Rayを出す位置
        private Transform shadowTf;//影の位置

        private Material shadowMat; //割り当てる影
        private Vector3 constScale; //影の最初の大きさ
        private Collider[] colliders;//

        private void Awake() {
            candleTf = GameObject.FindGameObjectWithTag("Light").transform;

            GameObject shadow = Instantiate(shadowObj) as GameObject;
            shadowTf = shadow.transform;

            constScale = shadowTf.localScale;

            GameObject rayObj = new GameObject();
            rayObj.transform.position = candleTf.position;
            rayTf = rayObj.transform;
            rayTf.parent = candleTf;

            shadowMat = new Material(shadowMatOrigin);
            MeshRenderer[] meshes =  shadow.GetComponentsInChildren<MeshRenderer>();
            foreach(MeshRenderer m in meshes) {
                m.material = shadowMat;
            }

            colliders = shadow.GetComponentsInChildren<Collider>();
        }

        private void Update() {
            float aim = GetAim(candleTf.position,transform.position);
            rayTf.localEulerAngles = new Vector3(0,aim,0);
            RayHit();
            ShadowTransParent();
            ShadowSizeChenge();
            ActiveCollider();
        }

        /// <summary>
        /// 距離に応じて色の薄さを変える
        /// </summary>
        private void ShadowTransParent() {
            float prop = GetLength(candleTf.position,shadowTf.position,transform.position);

            shadowMat.color = new Color(0,0,0,1 - prop);
        }

        /// <summary>
        /// 距離に応じて大きさを変える
        /// </summary>
        private void ShadowSizeChenge() {
            float prop = GetLength(candleTf.position,shadowTf.position,transform.position);
            shadowTf.localScale = constScale * (1 - prop) * 2;
        }

        /// <summary>
        /// 距離に応じてコライダーの有無を切り替える
        /// </summary>
        private void ActiveCollider() {
            float prop = GetLength(candleTf.position,shadowTf.position,transform.position);
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
            if(Physics.Raycast(ray,out hit,Mathf.Infinity)) {
                if(hit.collider.gameObject.tag == "Wall") {
                    shadowTf.position = hit.point;
                }
                if(hit.collider.gameObject.tag == "ShadowObj") {
                    shadowTf.position = hit.point + rayTf.forward * offset;
                }
            }
        }

        /// <summary>
        /// rayを出す角度を取得
        /// </summary>
        private float GetAim(Vector3 p1,Vector3 p2) {
            float dx = p2.x - p1.x;
            float dz = p2.z - p1.z;
            float rad = Mathf.Atan2(dx,Mathf.Abs(dz));
            return rad * Mathf.Rad2Deg;
        }

        /// <summary>
        /// （光源から自分）/（光源から影）
        /// </summary>
        private float GetLength(Vector3 p1,Vector3 p2,Vector3 p3) {
            float maxLength = (p2 - p1).magnitude;//ろうそくから影までの長さ
            float length = (p3 - p1).magnitude;   //ろうそくからオブジェクトまでの長さ
            float prop = length / maxLength;      //割合
            prop = prop >= 1 ? 1 : prop;

            return prop;      
        }
    }


}