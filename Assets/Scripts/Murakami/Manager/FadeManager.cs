/*
 *	作成者     :村上和樹
 *	機能説明   :フェードイン、フェードアウト
 * 	初回作成日 :2018/06/06
 *	最終更新日 :2018/06/06
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Village {

    public class FadeManager : MonoBehaviour {

        [Header("メインとなるキャンバス"),SerializeField] private Canvas fadeCanvas;
        [Header("暗幕"),SerializeField] private Image curtain;
        [Header("ロード中の画像"),SerializeField] private SpriteRenderer loadingImage;
        [Header("ロードのテキスト"),SerializeField] private Text loadingText;


        private static FadeManager instance;
        public static FadeManager getInstance {
            get {
                if(null == instance) {
                    instance = (FadeManager) FindObjectOfType(typeof(FadeManager));
                    if(null == instance) {
                        Debug.Log(" FadeManager Instance Error ");
                    }
                }
                return instance;
            }
        }

        public enum FadeState {
            FadeInRun,
            FadeInComp,
            FadeOutRun,
            FadeOutComp,
            Default,
        }

        private FadeState myState = FadeState.Default;


        private void Awake(){
            GameObject[] fadeObj = GameObject.FindGameObjectsWithTag("FadeManager");
            if(1 < fadeObj.Length) {
                Destroy(gameObject);
            }
            else {
                DontDestroyOnLoad(gameObject);//破棄させない

                curtain.material.SetFloat("_Threshold",1);//暗幕無し
                loadingImage.color = new Color(loadingImage.color.r,loadingImage.color.g,loadingImage.color.b,0);//透明度０
                loadingText.color =  new Color(loadingText.color.r ,loadingText.color.g ,loadingText.color.b,0 );//透明度０
            }
        }

        public void SetCanvasCamera(Camera cam) {
            fadeCanvas.worldCamera = cam;
        }

        /// <summary>
        /// 明るくする
        /// </summary>
        public bool FadeIn() {
            if(myState == FadeState.FadeInComp) {
                myState = FadeState.Default;
                return true;
            } 
            if(myState == FadeState.FadeInRun) return false;
            //一回目だけ
            myState = FadeState.FadeInRun;
            StartCoroutine(FadeInCoroutine());
            return false;
        }

        /// <summary>
        /// 暗くする
        /// </summary>
        public bool FadeOut() {
            if(myState == FadeState.FadeOutComp) {
                myState = FadeState.Default;
                return true;
            }
            if(myState == FadeState.FadeOutRun) return false;
            //一回目だけ
            myState = FadeState.FadeOutRun;
            StartCoroutine(FadeOutCoroutine());
            return false;
        }


        private IEnumerator FadeInCoroutine() {
            for(int i = 0;curtain.material.GetFloat("_Threshold") < 1;i++) {
                var value = curtain.material.GetFloat("_Threshold");
                value = Mathf.Min(value + 0.05f,1);
                curtain.material.SetFloat("_Threshold",value);
                loadingImage.color -= new Color(0,0,0,0.1f);
                loadingText.color -=  new Color(0,0,0,0.1f);
                yield return new WaitForEndOfFrame();
            }
            loadingImage.color = new Color(loadingImage.color.r,loadingImage.color.g,loadingImage.color.b,0);//透明度０
            loadingText.color = new Color(loadingText.color.r,loadingText.color.g,loadingText.color.b,0);//透明度０
            yield return new WaitForSeconds(0.5f);
            myState = FadeState.FadeInComp;
        }

        private IEnumerator FadeOutCoroutine() {
            for(int i = 0;curtain.material.GetFloat("_Threshold") > 0;i++) {
                var value = curtain.material.GetFloat("_Threshold");
                value = Mathf.Max(value - 0.05f,0);
                curtain.material.SetFloat("_Threshold",value);
                loadingImage.color += new Color(0,0,0,0.1f);
                loadingText.color += new Color(0,0,0, 0.1f);
                yield return new WaitForEndOfFrame();
            }
            loadingImage.color = new Color(loadingImage.color.r,loadingImage.color.g,loadingImage.color.b,1);//透明度０
            loadingText.color = new Color(loadingText.color.r,loadingText.color.g,loadingText.color.b,1);//透明度０
            yield return new WaitForSeconds(1);
            myState = FadeState.FadeOutComp;
        }


    }

}