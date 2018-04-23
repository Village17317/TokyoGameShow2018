/*
 *	作成者     :
 *	機能説明   :
 * 	初回作成日 :
 *	最終更新日 :
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Village {

    public class ColorJudge : MonoBehaviour {
        [System.Serializable]
        private class BoolCallBack: UnityEngine.Events.UnityEvent<bool> { }
        [System.Serializable]
        private class JudgePosClass {
            public Transform judgeTf;//判定する場所
            public Color judgePosColor;//判定された色
            public BoolCallBack ev = new BoolCallBack();//判定を送る先の関数
        }

        [SerializeField, Space(8)]  private JudgePosClass[] judge;
        [Space(8)][SerializeField]　private Camera cam;
        [Range(0.0f,1.0f), SerializeField] private float grayScale = 0.8f;

        private Texture2D tex = null;

        private void Start() {
            tex = new Texture2D(1,1,TextureFormat.RGB24,false);
        }

        private void Update() {
            StartCoroutine(JudgeColor());
        }

        private IEnumerator JudgeColor() {
            for(int i = 0;i < judge.Length;i++) {
                Vector3 pos = cam.WorldToScreenPoint(judge[i].judgeTf.position);
                yield return new WaitForEndOfFrame();
                tex.ReadPixels(new Rect(pos.x,pos.y,1,1),0,0);
                judge[i].judgePosColor = tex.GetPixel(0,0);
                if(judge[i].judgePosColor.r <= grayScale
                && judge[i].judgePosColor.g <= grayScale
                && judge[i].judgePosColor.b <= grayScale) {
                    judge[i].ev.Invoke(true);
                }
                else {
                    judge[i].ev.Invoke(false);
                }
            }
        }

    }

}

#region prev_2018_04_22
//[System.Serializable]
//private class JudgePosClass {
//    public Transform judgeTf;//判定する場所
//    public Color judgePosColor;//判定された色
//}

//[SerializeField, Space(8)]
//private JudgePosClass judge_Ground;
//[SerializeField, Space(8)]
//private JudgePosClass judge_Jump;
//[SerializeField, Space(8)]
//private JudgePosClass judge_Climb;
//[SerializeField, Space(8)]
//private JudgePosClass judge_Stop;
//[Space(8)]
//[SerializeField]
//private INI.MyCharController player2d;
//[SerializeField]
//private Camera cam;
//[Range(0.0f,1.0f), SerializeField]
//private float grayScale = 0.8f;
//private Texture2D tex = null;

//private void Start() {
//    tex = new Texture2D(1,1,TextureFormat.RGB24,false);
//}

//private void Update() {
//    StartCoroutine(Judge_Ground());
//    StartCoroutine(Judge_Jump());
//    StartCoroutine(Judge_Climb());
//    StartCoroutine(Judge_Stop());
//}

//private IEnumerator Judge_Ground() {
//    Vector3 pos = cam.WorldToScreenPoint(judge_Ground.judgeTf.position);
//    yield return new WaitForEndOfFrame();
//    tex.ReadPixels(new Rect(pos.x,pos.y,1,1),0,0);
//    judge_Ground.judgePosColor = tex.GetPixel(0,0);
//    if(judge_Ground.judgePosColor.r <= grayScale
//    && judge_Ground.judgePosColor.g <= grayScale
//    && judge_Ground.judgePosColor.b <= grayScale) {
//        player2d.IsGround(true);
//    }
//    else {
//        player2d.IsGround(false);
//    }
//    // Debug.Log(judgePosColor.r + " : " + IsGroundShadow());
//}

//private IEnumerator Judge_Jump() {
//    Vector3 pos = cam.WorldToScreenPoint(judge_Jump.judgeTf.position);
//    yield return new WaitForEndOfFrame();
//    tex.ReadPixels(new Rect(pos.x,pos.y,1,1),0,0);
//    judge_Jump.judgePosColor = tex.GetPixel(0,0);
//    if(judge_Jump.judgePosColor.r <= grayScale
//    && judge_Jump.judgePosColor.g <= grayScale
//    && judge_Jump.judgePosColor.b <= grayScale) {
//        player2d.SetJumpFlag(true);
//    }
//    else {
//        player2d.SetJumpFlag(false);
//    }
//}

//private IEnumerator Judge_Climb() {
//    Vector3 pos = cam.WorldToScreenPoint(judge_Climb.judgeTf.position);
//    yield return new WaitForEndOfFrame();
//    tex.ReadPixels(new Rect(pos.x,pos.y,1,1),0,0);
//    judge_Climb.judgePosColor = tex.GetPixel(0,0);
//    if(judge_Climb.judgePosColor.r <= grayScale
//    && judge_Climb.judgePosColor.g <= grayScale
//    && judge_Climb.judgePosColor.b <= grayScale) {
//        player2d.SetClimbFlag(true);
//    }
//    else {
//        player2d.SetClimbFlag(false);
//    }
//}

//private IEnumerator Judge_Stop() {
//    Vector3 pos = cam.WorldToScreenPoint(judge_Stop.judgeTf.position);
//    yield return new WaitForEndOfFrame();
//    tex.ReadPixels(new Rect(pos.x,pos.y,1,1),0,0);
//    judge_Stop.judgePosColor = tex.GetPixel(0,0);
//    if(judge_Stop.judgePosColor.r <= grayScale
//    && judge_Stop.judgePosColor.g <= grayScale
//    && judge_Stop.judgePosColor.b <= grayScale) {
//        player2d.SetStopFlag(true);
//    }
//    else {
//        player2d.SetStopFlag(false);
//    }
//}
#endregion