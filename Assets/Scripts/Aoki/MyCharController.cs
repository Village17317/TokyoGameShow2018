/*
*	作成者	：青木仁志
*	機能	：キャラクターの自動操作
*	作成	：2018/04/17
*	更新	：2018/04/18
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace INI {
	
	public class MyCharController : Village.Inheritor {

        [SerializeField]
        private bool jmp = false, clm = false, stp = false, isWalk = true;

        [SerializeField]
        private StepDecision jumpCol, climbCol, stopCol;

        [SerializeField]
        private Rigidbody2D myCharRb;

        [SerializeField]
        private float mvSpd = 1.0f, jmpSpd = 1.0f;

		private void Awake()
		{

		}
	
		private void Start ()
		{
			
		}

        public override void Run()
		{
            jmp = jumpCol.ovLap;
            clm = climbCol.ovLap;
            stp = stopCol.ovLap;
            
            if (jmp && !clm && !stp)
            {
                Jump();
            }
            else if (jmp && clm && !stp)
            {
                Climb();
            }
            else if (jmp && clm && stp)
            {
                Stop();
            }

            if (Input.GetButtonDown("Button_LB"))
            {
                isWalk = !isWalk;
            }
            if (isWalk) Walk();
            else Stop();
        }

        private void Walk()
        {
            this.transform.localEulerAngles = new Vector3(0, 0, 0);
            this.transform.Translate(mvSpd, 0, 0);
        }

        private void Jump()
        {
            myCharRb.AddForce(Vector2.right * jmpSpd);
        }

        private void Climb()
        {
        }

        private void Stop()
        {
            this.transform.Translate(0, 0, 0);
        }
    }
}
