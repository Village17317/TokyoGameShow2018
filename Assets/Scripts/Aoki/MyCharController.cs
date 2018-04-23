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
        private bool cliff = false, slope = false, inShadow = false, wall = false;

        [SerializeField]
        private Rigidbody2D myCharRb;

        [SerializeField]
        private float mvSpd = 1.0f;

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
            this.transform.Translate(mvSpd, mvSpd, 0);
        }

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

        /// <summary>
        /// 目の前が崖だった場合にこのメソッドに真を渡す。
        /// </summary>
        /// <param name="clf"></param>
        public void SetCliffFlag(bool clf)
        {
            cliff = clf;
        }

        /// <summary>
        /// 目の前が下り坂だった場合にこのメソッドに真を渡す。
        /// </summary>
        /// <param name="slp"></param>
        public void SetSlopeFlag(bool slp)
        {
            slope = slp;
        }

        /// <summary>
        /// 目の前が壁だった場合にこのメソッドに真を渡す。
        /// </summary>
        /// <param name="stopJ"></param>
        public void SetStopFlag(bool stopJ)
        {
            wall = stopJ;
        }
    }
}
