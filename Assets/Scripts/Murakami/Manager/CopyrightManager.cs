/*
 *	作成者     :
 *	機能説明   :
 * 	初回作成日 :
 *	最終更新日 :
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Village {

    public class CopyrightManager: MonoBehaviour {

        [SerializeField] private string titleSceneName;
        [SerializeField] private float maxTime = 2.0f;

        private float time = 0;
        private bool isOnce = false;

        private void Update() {
            time += Time.deltaTime;
            if(time >= maxTime && !isOnce) {
                isOnce = true;
                SceneManager.LoadScene(titleSceneName);
            }
        }

    }
}