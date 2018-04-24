/*
 *	作成者     :村上和樹
 *	機能説明   :遅延
 * 	初回作成日 :
 *	最終更新日 :
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village {

    public class Delay : MonoBehaviour {

        public static IEnumerator DelayMethod(int delayFrameCount,System.Action action) {
            for(var i = 0;i < delayFrameCount;i++) {
                yield return null;
            }
            action();
        }

    }

}