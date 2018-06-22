/*
 *	作成者     :村上和樹
 *	機能説明   :PostProseccing
 * 	初回作成日 :2018/06/19
 *	最終更新日 :2018/06/19
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

namespace Village {

    public class PostProcessingManager : MonoBehaviour {
        [SerializeField] private PostProcessingProfile profile;
        [Space(8)]
        [SerializeField]private VignetteModel.Settings v_settings;
        [SerializeField, Range(0,1)] private float smoothness = 0;

        private void Update() {
            VignetteSmoothness(smoothness);
        }

        private void OnDisable() {
            VignetteInit();
        }

        /// <summary>
        /// Vignetteの初期化
        /// </summary>
        private void VignetteInit() {
            profile.vignette.settings = v_settings;
        }

        /// <summary>
        /// Vignette(ぼかしの処理)
        /// </summary>
        /// <param name="value"></param>
        private void VignetteSmoothness(float value) {
            var vignette = profile.vignette.settings;
            vignette.smoothness = value;
            profile.vignette.settings = vignette;
        }

    }

}