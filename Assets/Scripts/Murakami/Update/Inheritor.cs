/*
 *	作成者     :村上和樹
 *	機能説明   :MonoBehaviourの代わり
 * 	初回作成日 :2018/03/05
 *	最終更新日 :2018/03/05
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village {

    public class Inheritor : MonoBehaviour {
        /// <summary>
        /// Startの代わり
        /// </summary>
        public virtual void Init() {

        }

        /// <summary>
        /// Updateの代わり
        /// </summary>
        public virtual void Run() {

        }

        /// <summary>
        /// FixedUpdateの代わり
        /// </summary>
        public virtual void FixedRun() {

        }

    }

}