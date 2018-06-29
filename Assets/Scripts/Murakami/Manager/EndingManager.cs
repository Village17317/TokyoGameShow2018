/*
 *	作成者     :村上和樹
 *	機能説明   :エンディングのシーン管理
 * 	初回作成日 :2018/06/11
 *	最終更新日 :2018/06/11
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Village {

    public class EndingManager : MonoBehaviour {
        private enum EndingStep {
            STEP_0,
            STEP_1,
            DEFAULT,
        }
        private EndingStep step = EndingStep.STEP_0;

        [SerializeField] private string titleScene = "Scene_Title";

        private void Awake(){
            Cursor.visible = false;
            FadeManager.getInstance.SetCanvasCamera(Camera.main);
            SoundManager.Instance.PlayBGM(SceneManager.GetActiveScene().name);
		}

        private void Update() {
            switch(step) {
                case EndingStep.STEP_0: WhiteIn();     break; 
                case EndingStep.STEP_1: BackToTitle(); break;
                default: break;
            }
        }

        /// <summary>
        /// ホワイトイン
        /// </summary>
        private void WhiteIn() {
            if(FadeManager.getInstance.WhiteIn()) step = EndingStep.STEP_1;
        }

        /// <summary>
        /// タイトルに戻る
        /// </summary>
        private void BackToTitle() {
            if(Input.GetButtonDown("Button_Start") || Input.GetButtonDown("Button_A")) {
                SoundManager.Instance.PlaySE("select",transform);
                StartCoroutine(SceneChenge(titleScene));
                step = EndingStep.DEFAULT;
            }
        }

        /// <summary>
        /// シーンを変える
        /// </summary>
        private IEnumerator SceneChenge(string sceneName) {
            while(!FadeManager.getInstance.FadeOut()) {
                yield return null;
            }
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(sceneName);
        }
    }

}