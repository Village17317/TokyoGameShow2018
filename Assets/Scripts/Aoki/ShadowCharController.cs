/*
*	作成者	：青木仁志
*	機能	：キャラクターの自動操作
*	作成	：2018/05/11
*	更新	：2018/05/11
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace INI
{
    public class ShadowCharController : Village.Inheritor
    {
        public float walkSpeed = 10;
        public Vector3 jumpForce;

        public override void FixedRun()
        {
            if (Village.GameMaster.getInstance.GetGameMode == Village.GameMaster.GameMode.Game)
            {

            }
        }
    }
}