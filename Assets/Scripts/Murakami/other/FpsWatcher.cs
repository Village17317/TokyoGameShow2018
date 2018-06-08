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

    public class FpsWatcher : MonoBehaviour {

        int frameCount;
        float prevTime;

        string str = "";

        void Start() {
            frameCount = 0;
            prevTime = 0.0f;
        }

        void Update() {

            ++frameCount;
            float time = Time.realtimeSinceStartup - prevTime;

            if(time >= 0.5f) {
                //Debug.LogFormat("{0}fps",frameCount / time);
                str = "fps : " + frameCount / time;
                frameCount = 0;
                prevTime = Time.realtimeSinceStartup;
            }
        }

        private void OnGUI() {
            GUILayout.Label(str);
        }
    }

}