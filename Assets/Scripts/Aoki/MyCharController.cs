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
        private bool reverse = false, inShadow = false, isStop = false, isGround = false;

        [SerializeField]
        private Rigidbody2D myCharRb;

        [SerializeField]
        private float mvSpd = 1.0f;

        public override void FixedRun()
		{
            if (Village.GameMaster.getInstance.GetGameMode == Village.GameMaster.GameMode.Game)
            {
                //if (Input.GetButtonDown("Button_B"))
                //{
                //    isStop = !isStop;
                //}

                if (!isStop)
                {

                    if (inShadow)
                    {
                        Up();
                    }
                    else
                    {
                        Walk();
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
            this.transform.Translate(0, mvSpd, 0);
        }

        private void Stop()
        {
            this.transform.Translate(Vector3.zero);
        }
        
        /// <summary>
        /// 接地判定を受け取るメソッド。真は接地、偽は非接地に対応。
        /// </summary>
        /// <param name="gnd"></param>
        public void IsGround(bool gnd, bool rvs)
        {
            isGround = gnd;
            reverse = rvs;

            if (gnd)
            {
                //myCharRb.simulated = false;
                myCharRb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

                if (rvs)
                {
                    this.transform.localEulerAngles += new Vector3(0, 180, 0);

                    if (this.transform.localEulerAngles.y >= 360)
                    {
                        this.transform.localEulerAngles = new Vector3(0, 0, 0);
                    }
                }
            }
            else if (!gnd)
            {
                //myCharRb.simulated = true;
                myCharRb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
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
