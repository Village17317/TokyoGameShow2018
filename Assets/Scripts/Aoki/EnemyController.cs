/*
*	作成者	：青木仁志
*	機能	：ShadowCharControllerを継承したエネミーのスクリプト
*	作成	：2018/05/14
*	更新	：2018/06/19
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace INI {
	
	public class EnemyController : ShadowCharController {

		private void Awake()
		{
			
		}
	
		private void Start ()
		{
			
		}
	
		private void Update ()
		{
			
		}

        /// <summary>
        /// ゲームの状態、及びState型enumの中身に応じてアニメーションを切り替える（エネミー用）
        /// </summary>
        protected override void PlayerAnimator()
        {
            //base.PlayerAnimator();
            switch (state)
            {
                case ShadowCharController.State.STOP:
                    shadowChar_Animator.Play("Enemy_wait");
                    break;
                case ShadowCharController.State.WALK:
                    shadowChar_Animator.Play("Enemy_walk");
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// プレイヤーと衝突時ゲームオーバーにする
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Village.GameMaster.getInstance.SetGameMode(Village.GameMaster.GameMode.GameOver);
            }
        }
    }
}
