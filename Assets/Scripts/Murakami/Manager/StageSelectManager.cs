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

    public class StageSelectManager : MonoBehaviour {
        private enum StageSelectState {
            STEP_1,
            STEP_2,
            STEP_3,
            DEFAULT,
        }
        private enum StageNumber {
            STAGE_1,
            STAGE_2,
            STAGE_3,
            DEFAULT,
        }


        private bool isStick = false;
        private bool isTurned = false;
        private StageSelectState state = StageSelectState.STEP_1;
        private StageNumber number = StageNumber.STAGE_1;


        [SerializeField] private Transform tableTf;
        [SerializeField] private float turnAngle = 90;
        [SerializeField] private string[] scenePasses = new string[4];

        private void Awake(){
            
		}

        private void Start(){
            
        }

        private void Update() {
            switch(state) {
                case StageSelectState.STEP_1:
                    Select();
                    break;
                case StageSelectState.STEP_2:
                    SceneChenge();
                    break;
                case StageSelectState.STEP_3:
                break;
                case StageSelectState.DEFAULT:
                break;
                default:
                break;
            }
        }

        /// <summary>
        /// ステージを選ぶ
        /// </summary>
        private void Select() {
            if(Input.GetAxis("Horizontal") > 0 && !isStick) {
                isStick = true;
                isTurned = true;
                StartCoroutine(TableTurn(-turnAngle));
            }
            else if(Input.GetAxis("Horizontal") < 0 && !isStick) {
                isStick = true;
                isTurned = true;
                StartCoroutine(TableTurn(turnAngle));
            }
            else if(Input.GetAxis("Horizontal") == 0) {
                isStick = false;
            }

            if(Input.GetButtonDown("Button_A") && !isTurned) {
                state = StageSelectState.STEP_2;
            }
        }

        /// <summary>
        /// sceneChenge
        /// </summary>
        private void SceneChenge() {
            SceneManager.LoadScene(scenePasses[(int) number]);
        }


        /// <summary>
        /// テーブルを回す
        /// </summary>
        IEnumerator TableTurn(float angle) {
            if(0 < angle) {
                for(float i = 0;i < angle / 2;i++) {
                    tableTf.localEulerAngles += new Vector3(0,2,0);
                    yield return null;
                }
                number--;
                if(number < StageNumber.STAGE_1) {
                    number = StageNumber.DEFAULT;
                }
            }
            else if(0 > angle) {
                for(float i = angle / 2;i < 0;i++) {
                    tableTf.localEulerAngles -= new Vector3(0,2,0);
                    yield return null;
                }
                number++;
                if(number > StageNumber.DEFAULT) {
                    number = StageNumber.STAGE_1;
                }
            }
            isTurned = false;



        }

    }

}