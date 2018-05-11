/*
*	作成者	：青木仁志
*	機能	：キャラクターの自動操作
*	作成	：2018/04/17
*	更新	：2018/04/24
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace INI {
	
	public class MyCharController : Village.Inheritor {

        [SerializeField]
        private bool reverse = false, inShadow = false, isStop = false, isGround = false, nearJudge = false, farJudge = false, jump = false;

        [SerializeField]
        private Rigidbody2D myCharRb;

        [SerializeField]
        private float mvSpd = 1.0f;

        [SerializeField]
        private Vector2 jmpSpd = Vector2.zero;

        private enum direction
        {
            RIGHT,
            LEFT
        }
        private direction dir = direction.RIGHT;

        public override void FixedRun()
		{
            if (Village.GameMaster.getInstance.GetGameMode == Village.GameMaster.GameMode.Game)
            {
                    if (inShadow)
                    {
                        Up();
                    }
                    else
                    {
                        Walk();

                        if ((!nearJudge && farJudge) || jump)
                        {
                            myCharRb.constraints = RigidbodyConstraints2D.FreezeRotation;
                            if (dir == direction.RIGHT)
                            {
                                myCharRb.AddForce(jmpSpd);
                            }
                            else if (dir == direction.LEFT)
                            {
                                jmpSpd = new Vector2(jmpSpd.x * -1, jmpSpd.y);
                            }
                        }
                    }
            }
            else if (Village.GameMaster.getInstance.GetGameMode == Village.GameMaster.GameMode.Pause)
            {
                myCharRb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            }

        }

        private void Walk()
        {
            //this.transform.localEulerAngles = new Vector3(0, 0, 0);
            this.transform.Translate(mvSpd, 0, 0);
        }

        private void Up()
        {
            //this.transform.localEulerAngles = new Vector3(0, 0, 0);
            this.transform.Translate(0, mvSpd, 0);
        }
        
        /// <summary>
        /// 接地判定を受け取るメソッド。真は接地、偽は非接地に対応。
        /// </summary>
        /// <param name="gnd"></param>
        public void IsGround(bool gnd)
        {
            isGround = gnd;

            if (gnd)
            {
                myCharRb.velocity = Vector2.zero;
                myCharRb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

            }
            else if (!gnd)
            {
                //myCharRb.simulated = true;
                myCharRb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }

        /// <summary>
        /// 反転判定を受け取るメソッド。真の時に反転する。
        /// </summary>
        /// <param name="rvs"></param>
        public void Reverse(bool rvs)
        {
            if (Village.GameMaster.getInstance.GetGameMode == Village.GameMaster.GameMode.Game)
            {
                if (inShadow) return;
                
                reverse = rvs;

                if (rvs)
                {
                    if (dir == direction.RIGHT)
                    {
                        dir = direction.LEFT;
                        this.transform.localEulerAngles = new Vector3(0, 180, 0);
                    }
                    else if (dir == direction.LEFT)
                    {
                        dir = direction.RIGHT;
                        this.transform.localEulerAngles = new Vector3(0, 0, 0);
                    }
                }
            }
        }

        /// <summary>
        /// 目の前の段差の判定を受け取りローカル変数にセットするクラス。
        /// </summary>
        /// <param name="jmp"></param>
        public void SetJump(bool jmp)
        {
            jump = jmp;
        }

        /// <summary>
        /// 近距離の足場の判定を受け取りローカル変数にセットするクラス。
        /// </summary>
        /// <param name="near"></param>
        public void SetNear(bool near)
        {
            nearJudge = near;
        }

        /// <summary>
        /// 遠距離の足場の判定を受け取りローカル変数にセットするクラス。
        /// </summary>
        /// <param name="far"></param>
        public void SetFar(bool far)
        {
            farJudge = far;
        }

        /// <summary>
        /// 影の中に居る場合にこのメソッドに真を渡す。
        /// </summary>
        /// <param name="inS"></param>
        public void SetInshadowFlag(bool inS)
        {
            inShadow = inS;
        }
    }
}
