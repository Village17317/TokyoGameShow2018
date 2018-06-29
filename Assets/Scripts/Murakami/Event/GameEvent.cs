/*
 *	作成者     :村上和樹
 *	機能説明   :Playerが触れたときゲームイベントをだす
 * 	初回作成日 :2018/05/30
 *	最終更新日 :2018/05/30
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Village {

    public class GameEvent : MonoBehaviour {
        [SerializeField] private Image expImage;
        [SerializeField] private Text expText;

        private float imageAlpha = 0;
        private float textAlpha = 0;

        private void Awake(){
            imageAlpha = expImage.color.a;
            textAlpha = expText.color.a;

            expImage.color = new Color(expImage.color.r,expImage.color.g,expImage.color.b,0);
            expText. color = new Color(expText.color.r,expText.color.g,expText.color.b,0);
        }

        private void FixedUpdate() {
            if(GameMaster.getInstance.GetGameMode != GameMaster.GameMode.Game) {
                if(expImage.gameObject.activeInHierarchy)
                    expImage.gameObject.SetActive(false);
                if(expText.gameObject.activeInHierarchy)
                    expText.gameObject.SetActive(false);
            }
            else {
                if(!expImage.gameObject.activeInHierarchy) expImage.gameObject.SetActive(true);
                if(!expText.gameObject.activeInHierarchy)  expText.gameObject.SetActive(true);
            }
        }

        private void OnTriggerEnter(Collider other) {
            if(other.gameObject.tag != "Player") return;
            StartCoroutine(FadeIn(0.01f));
        }

        private void OnTriggerExit(Collider other) {
            if(other.gameObject.tag != "Player") return;
            StartCoroutine(FadeOut(0.05f));
        }

        /// <summary>
        /// フェードイン
        /// </summary>
        private IEnumerator FadeIn(float speed) {
            while(expImage.color.a < imageAlpha || expText.color.a < textAlpha) {
                float addImageAlpha = Mathf.Min(expImage.color.a + speed,imageAlpha);
                float addTextAlpha = Mathf.Min(expText.color.a + speed,textAlpha);

                expImage.color = new Color(expImage.color.r,expImage.color.g,expImage.color.b,addImageAlpha);
                expText.color = new Color(expText.color.r,expText.color.g,expText.color.b,addTextAlpha);
                yield return new WaitForEndOfFrame();
            }
        }

        /// <summary>
        /// フェードアウト
        /// </summary>
        private IEnumerator FadeOut(float speed) {
            while(expImage.color.a > 0 || expText.color.a > 0) {
                float addImageAlpha = Mathf.Max(expImage.color.a - speed,0);
                float addTextAlpha = Mathf.Max(expText.color.a - speed,0);

                expImage.color = new Color(expImage.color.r,expImage.color.g,expImage.color.b,addImageAlpha);
                expText.color = new Color(expText.color.r,expText.color.g,expText.color.b,addTextAlpha);
                yield return new WaitForEndOfFrame();
            }
        }
    }

}