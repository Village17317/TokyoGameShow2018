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

    public class TitleManager : MonoBehaviour {
        private enum TitleStep {
            STEP_1,
            STEP_2,
            STEP_3,
            DEFAULT,
        }

        [SerializeField] private ParticleSystem fireEffect;
        [SerializeField] private Light pointLight;
        [SerializeField] private Light roomLight;
        [SerializeField] private string nextSceneName = "";
        [SerializeField] private TitleStep step = TitleStep.STEP_1;
        private float spotRange = 0;
        private bool flag = false;

        private void Awake(){
            spotRange = roomLight.range;
            roomLight.range = 0;
            pointLight.enabled = false;
		}

        private void Start(){
            
        }

        private void Update() {
            switch(step) {
                case TitleStep.STEP_1:
                    Step_1();
                    break;
                case TitleStep.STEP_2:
                    Step_2();
                    break;
                case TitleStep.STEP_3:
                    Step_3();
                    break;
                default:
                break;
            }
        }

        private IEnumerator StartCol() {
            yield return new WaitForSeconds(2);
            fireEffect.Play();
            pointLight.enabled = true;
            yield return new WaitForSeconds(0.5f);
            step = TitleStep.STEP_2;
        }

        private void SpotLightAnimation() {
            if(flag) {
                roomLight.range -= Time.deltaTime * spotRange;
                if(roomLight.range <= spotRange * 0.98f) {
                    roomLight.range = spotRange * 0.98f;
                    flag = false;
                }
            }
            else {
                roomLight.range += Time.deltaTime * spotRange;
                if(roomLight.range >= spotRange) {
                    roomLight.range = spotRange;
                    flag = true;
                }
            }
        }
        
        private IEnumerator LoadScene(string sceneName) {
            AsyncOperation load = SceneManager.LoadSceneAsync(sceneName);
            while(!load.isDone) {
                Debug.Log(load.progress);
                yield return null;
            }
        }

        private void Step_1() {
            StartCoroutine(StartCol());
            step = TitleStep.DEFAULT;
        }

        private void Step_2() {
            SpotLightAnimation();
            if(Input.GetButtonDown("Button_Start")) {
                StartCoroutine(LoadScene(nextSceneName));
            }
        }

        private void Step_3() {

        }
    }

}