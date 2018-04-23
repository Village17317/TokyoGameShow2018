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
        private bool jmp = false, clm = false, stp = false, isWalk = true, isClimb = false, cliff = false, slope = false, inShadow = false, wall = false;

        [SerializeField]
        private StepDecision jumpCol, climbCol, stopCol;

        [SerializeField]
        private Rigidbody2D myCharRb;

        [SerializeField]
        private float mvSpd = 1.0f, jmpSpd = 1.0f, clmSpd = 1.0f;

        public override void FixedRun()
		{
            if (Village.GameMaster.getInstance.GetGameMode == Village.GameMaster.GameMode.Game)
            {
                if (cliff)
                {
                    if (inShadow)
                    {
                        Up();
                    }
                    else if (wall)
                    {
                        Stop();
                    }
                    else
                    {
                        Walk();
                    }
                }
                else if (slope)
                {
                    Down();
                }



            //    //myCharRb.simulated = true;

            //    //jmp = jumpCol.ovLap;
            //    //clm = climbCol.ovLap;
            //    //stp = stopCol.ovLap;

            //    if (jmp && !clm && !stp)
            //    {
            //        if (!isClimb)
            //        {
            //            Jump();
            //        }
            //    }
            //    else if (jmp && clm && !stp)
            //    {
            //        isWalk = false;
            //        isClimb = true;
            //        Climb();
            //    }
            //    else if (jmp && clm && stp)
            //    {
            //        Stop();
            //    }
            //    else if (!jmp && !clm && !stp)
            //    {
            //        isWalk = true;
            //        isClimb = false;
            //    }

            //    if (isWalk)
            //    {
            //        Walk();
            //    }
            //    else if (!isWalk)
            //    {
            //        //Stop();
            //    }
            }
            //else if (Village.GameMaster.getInstance.GetGameMode == Village.GameMaster.GameMode.Pause)
            //{
            //    myCharRb.simulated = false;
            //}

        }

        private void Walk()
        {
            this.transform.localEulerAngles = new Vector3(0, 0, 0);
            this.transform.Translate(mvSpd, 0, 0);
        }

        private void Down()
        {
            this.transform.localEulerAngles = new Vector3(0, 0, 0);
            this.transform.Translate(mvSpd, -mvSpd, 0);
        }

        private void Up()
        {
            this.transform.localEulerAngles = new Vector3(0, 0, 0);
            this.transform.Translate(mvSpd, mvSpd, 0);
        }

        //private void Jump()
        //{
        //    //if (!myCharRb.simulated) myCharRb.simulated = true;
        //    myCharRb.AddForce(Vector2.up * jmpSpd);
        //}

        //private void Climb()
        //{
        //    //if (!myCharRb.simulated) myCharRb.simulated = true;
        //    myCharRb.AddForce(Vector2.up * clmSpd);
        //}

        private void Stop()
        {
            this.transform.Translate(Vector3.zero);
        }
        
        /// <summary>
        /// 接地判定を受け取るメソッド。真は接地、偽は非接地に対応。
        /// </summary>
        /// <param name="gnd"></param>
        public void IsGround(bool gnd)
        {
            if (gnd)
            {
                //myCharRb.simulated = false;
                myCharRb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                isWalk = true;
            }
            else if (!gnd)
            {
                //myCharRb.simulated = true;
                myCharRb.constraints = RigidbodyConstraints2D.FreezeRotation;
                isWalk = false;
            }
        }

        public void SetInshadowFlag(bool inS)
        {
            inShadow = inS;
        }

        public void SetCliffFlag(bool clf)
        {
            cliff = clf;
        }

        public void SetSlopeFlag(bool slp)
        {
            slope = slp;
        }

        ///// <summary>
        ///// ジャンプの可否判定を引数で受け取り、変数にセットする。真は可、偽は不可に対応する。
        ///// </summary>
        ///// <param name="jumpJ"></param>
        //public void SetJumpFlag(bool jumpJ)
        //{
        //    jmp = jumpJ;
        //}

        ///// <summary>
        ///// クライムの可否判定を引数で受け取り、変数にセットする。真は可、偽は不可に対応する。
        ///// </summary>
        ///// <param name="climbJ"></param>
        //public void SetClimbFlag(bool climbJ)
        //{
        //    clm = climbJ;
        //}

        /// <summary>
        /// ストップする場合に引数で判定を受け取り、変数にセットする。
        /// </summary>
        /// <param name="stopJ"></param>
        public void SetStopFlag(bool stopJ)
        {
            stp = stopJ;
        }
    }
}
