/*
 *	作成者     :
 *	機能説明   :
 * 	初回作成日 :
 *	最終更新日 :
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village {

    public class TestColorJudge : MonoBehaviour {
        [System.Serializable]
        private class BoolCallBack: UnityEngine.Events.UnityEvent<bool> { }
        [System.Serializable]
        private class JudgePosClass {
            public Transform judgeTf;//判定する場所
            public Color judgePosColor;//判定された色
            public BoolCallBack ev = new BoolCallBack();
        }

        [SerializeField, Space(8)] private JudgePosClass[] judge;
        [Space(8)]
        [SerializeField]        private Camera cam;
        [Range(0.0f,1.0f), SerializeField]        private float grayScale = 0.8f;

        private Texture2D tex = null;

        private void Start() {
            tex = new Texture2D(1,1,TextureFormat.RGB24,false);
        }

        private void Update() {
            StartCoroutine(Judge_Ground());
        }

        private IEnumerator Judge_Ground() {
            for(int i = 0;i < judge.Length;i++) {
                Vector3 pos = cam.WorldToScreenPoint(judge[i].judgeTf.position);
                yield return new WaitForEndOfFrame();
                tex.ReadPixels(new Rect(pos.x,pos.y,1,1),0,0);
                judge[i].judgePosColor = tex.GetPixel(0,0);
                if(judge[i].judgePosColor.r <= grayScale
                && judge[i].judgePosColor.g <= grayScale
                && judge[i].judgePosColor.b <= grayScale) {
                    judge[i].ev.Invoke(true);
                }
                else {
                    judge[i].ev.Invoke(false);
                }
            }
        }
    }

}