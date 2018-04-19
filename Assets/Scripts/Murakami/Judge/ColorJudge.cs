/*
 *	作成者     :
 *	機能説明   :
 * 	初回作成日 :
 *	最終更新日 :
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Village {

    public class ColorJudge : MonoBehaviour {
        [SerializeField]  private Transform judgeTf;
        [SerializeField]  private TestGravity tg;
        [SerializeField]  private Color judgePosColor;
        [SerializeField]  private Camera cam;
        [Range(0.0f,1.0f),SerializeField]  private float grayScale = 0.8f;
        private Texture2D tex = null;

        private void Start(){
            tex = new Texture2D(1,1,TextureFormat.RGB24,false);
        }

        private void Update() {
            StartCoroutine(Judge());
        }

        public bool IsGroundShadow() {
            if(judgePosColor.r <= grayScale
            || judgePosColor.g <= grayScale
            || judgePosColor.b <= grayScale) {
                return true;
            }
            else {
                return false;
            }
        }

        private IEnumerator Judge() {
            Vector3 pos = cam.WorldToScreenPoint(judgeTf.position);
            yield return new WaitForEndOfFrame();
            tex.ReadPixels(new Rect(pos.x,pos.y,1,1),0,0);
            judgePosColor = tex.GetPixel(0,0);
            if(judgePosColor.r <= grayScale
            && judgePosColor.g <= grayScale
            && judgePosColor.b <= grayScale) {
                tg.SetGravity(true);
            }
            else {
                tg.SetGravity(false);
            }

            Debug.Log(judgePosColor.r + " : " + IsGroundShadow());
        }
    }

}