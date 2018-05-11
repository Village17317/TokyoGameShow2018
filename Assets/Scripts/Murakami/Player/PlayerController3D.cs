/*
 *	作成者     :村上和樹
 *	機能説明   :3dキャラのコントローラー
 * 	初回作成日 :2018/04/15
 *	最終更新日 :2018/04/15
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village {

    public class PlayerController3D : Inheritor {

        [SerializeField] private float speed = 0.1f;

        [SerializeField]
        private Transform catchingTf = null;


        private void Awake() {

        }

        private void Start() {

        }

        public override void Run() {
            if(GameMaster.getInstance.GetGameMode == GameMaster.GameMode.Start
            || GameMaster.getInstance.GetGameMode == GameMaster.GameMode.Game
            || GameMaster.getInstance.GetGameMode == GameMaster.GameMode.GameReStart) {
                //メイン処理
                Move();
            }
        }

        /// <summary>
        /// 移動
        /// </summary>
        private void Move() {
            transform.position += new Vector3(speed * Input.GetAxisRaw("Horizontal"),0,speed * Input.GetAxisRaw("Vertical"));
            if(catchingTf != null) catchingTf.position += new Vector3(speed * Input.GetAxisRaw("Horizontal"),0,speed * Input.GetAxisRaw("Vertical"));
        }

        private void OnCollisionStay(Collision collision) {

            if(collision.gameObject.tag != "Object3D") return;
            if(Input.GetButton("Button_B")) {//Bボタンでつかむ
                if(catchingTf == null) catchingTf = collision.transform;
            }
            else {
                if(catchingTf != null)catchingTf = null;
            }
        }
    }
}

#region prev
/*
namespace Village {

    public class PlayerController3D: Inheritor {

        [SerializeField]
        private GameObject cursor;
        [Range(0,6), SerializeField]
        private int pos_Y = 0;
        [Range(0,6), SerializeField]
        private int pos_X = 0;

        private bool isStickMove_Y = false;
        private bool isStickMove_X = false;

        private bool isChoice = false;
        private GameObject choiceObj;
        private float objectPos_Y = 0;
        private int objectRotate_Y = 0;//0,1,2,3

        private void Awake() {

        }

        private void Start() {
            cursor.transform.position = ObjectManager.getInstance.GetPos(pos_Y,pos_X);
        }

        public override void Run() {
            if(GameMaster.getInstance.GetGameMode == GameMaster.GameMode.Start
            || GameMaster.getInstance.GetGameMode == GameMaster.GameMode.Game
            || GameMaster.getInstance.GetGameMode == GameMaster.GameMode.GameReStart) {
                if(!cursor.activeInHierarchy) {
                    cursor.SetActive(true);
                }
                CursorMove();
                Choice();
                ObjectSpin();
            }
            else {
                if(cursor.activeInHierarchy) {
                    cursor.SetActive(false);
                }
            }
        }

        /// <summary>
        /// 3Dオブジェクトの選択
        /// </summary>
        private void Choice() {
            if(Input.GetButtonDown("Button_A")) {
                if(!isChoice) {
                    if(ObjectManager.getInstance.GetObject(pos_Y,pos_X) != null) {
                        choiceObj = ObjectManager.getInstance.GetObject(pos_Y,pos_X);
                        if(choiceObj.GetComponent<ObjectInfo>().isStatic) {
                            choiceObj = null;
                            isChoice = false;
                        }
                        else {
                            ObjectManager.getInstance.SetObject(pos_Y,pos_X,null);
                            objectPos_Y = choiceObj.transform.position.y;
                            isChoice = true;
                        }
                    }
                }
                else {
                    if(ObjectManager.getInstance.GetObject(pos_Y,pos_X) == null) {
                        ObjectManager.getInstance.SetObject(pos_Y,pos_X,choiceObj);
                        choiceObj = null;
                        objectPos_Y = 0;
                        isChoice = false;
                    }
                }
            }
        }

        /// <summary>
        /// カーソルの移動
        /// </summary>
        private void CursorMove() {
            if(Input.GetAxis("Horizontal") > 0 && !isStickMove_X) {
                pos_X++;
                if(pos_X > 6) {
                    pos_X = 6;
                }
                cursor.transform.position = ObjectManager.getInstance.GetPos(pos_Y,pos_X);
                ObjectMove();
                isStickMove_X = true;
            }
            else if(Input.GetAxis("Horizontal") < 0 && !isStickMove_X) {
                pos_X--;
                if(pos_X < 0) {
                    pos_X = 0;
                }
                cursor.transform.position = ObjectManager.getInstance.GetPos(pos_Y,pos_X);
                ObjectMove();
                isStickMove_X = true;
            }
            else if(Input.GetAxis("Horizontal") == 0) {
                isStickMove_X = false;
            }

            if(Input.GetAxis("Vertical") > 0 && !isStickMove_Y) {
                pos_Y--;
                if(pos_Y < 0) {
                    pos_Y = 0;
                }
                cursor.transform.position = ObjectManager.getInstance.GetPos(pos_Y,pos_X);
                ObjectMove();
                isStickMove_Y = true;
            }
            else if(Input.GetAxis("Vertical") < 0 && !isStickMove_Y) {
                pos_Y++;
                if(pos_Y > 6) {
                    pos_Y = 6;
                }
                cursor.transform.position = ObjectManager.getInstance.GetPos(pos_Y,pos_X);
                ObjectMove();
                isStickMove_Y = true;
            }
            else if(Input.GetAxis("Vertical") == 0) {
                isStickMove_Y = false;
            }
        }

        /// <summary>
        /// 3Dオブジェクトの移動
        /// </summary>
        private void ObjectMove() {
            if(isChoice) {
                float x = ObjectManager.getInstance.GetPos(pos_Y,pos_X).x;
                float z = ObjectManager.getInstance.GetPos(pos_Y,pos_X).z;
                Vector3 newPos = new Vector3(x,objectPos_Y,z);
                choiceObj.transform.position = newPos;
                choiceObj.GetComponent<ObjectInfo>().SetPos(pos_Y,pos_X);
            }
        }

        /// <summary>
        /// 3Dオブジェクトの回転
        /// </summary>
        private void ObjectSpin() {
            if(isChoice) {
                if(Input.GetButtonDown("Button_RB")) {
                    choiceObj.transform.Rotate(0,90,0);
                    objectRotate_Y++;
                    if(objectRotate_Y >= 4) {
                        objectRotate_Y = 0;
                    }
                }

                if(Input.GetButtonDown("Button_LB")) {
                    choiceObj.transform.Rotate(0,-90,0);
                    objectRotate_Y--;
                    if(objectRotate_Y < 0) {
                        objectRotate_Y = 3;
                    }
                }

            }
        }
    }
}
*/
#endregion

#region prev
//[SerializeField]
//private Cursor cursor;
//[SerializeField]
//private float cursorSpeed = 0.1f;

//private bool isChoice = false;
//private GameObject choiceObj;
//private float choicePosY = 0;
//private void Awake() {

//}

//private void Start() {

//}

//public override void Run() {
//    if(GameMaster.getInstance.GetGameMode == GameMaster.GameMode.Start
//    || GameMaster.getInstance.GetGameMode == GameMaster.GameMode.Game
//    || GameMaster.getInstance.GetGameMode == GameMaster.GameMode.GameReStart) {
//        if(!cursor.gameObject.activeInHierarchy) {
//            cursor.gameObject.SetActive(true);
//        }
//        CursorMove();
//        Choice();
//        ObjectSpin();
//    }
//    else {
//        if(cursor.gameObject.activeInHierarchy) {
//            cursor.gameObject.SetActive(false);
//        }
//    }
//}

///// <summary>
///// 3Dオブジェクトの選択
///// </summary>
//private void Choice() {
//    if(Input.GetButtonDown("Button_A")) {
//        if(!isChoice) {//掴むとき
//            if(cursor.GetObject != null) {
//                choiceObj = cursor.GetObject;
//                if(choiceObj.GetComponent<ObjectInfo>().isStatic) {
//                    choiceObj = null;
//                    isChoice = false;
//                }
//                else {
//                    isChoice = true;
//                    choiceObj.GetComponent<ObjectInfo>().isChoice = true;
//                    choicePosY = choiceObj.transform.position.y;
//                    cursor.SetIsChoice(true);
//                }
//            }
//        }
//        else {//離すとき
//            choiceObj.transform.position = new Vector3(choiceObj.transform.position.x,
//                                                       choicePosY,
//                                                       choiceObj.transform.position.z);
//            choicePosY = 0;
//            choiceObj.GetComponent<ObjectInfo>().isChoice = false;
//            choiceObj = null;
//            isChoice = false;
//            cursor.SetIsChoice(false);
//        }
//    }
//}

///// <summary>
///// カーソルの移動
///// </summary>
//private void CursorMove() {
//    if(Input.GetAxis("Horizontal") > 0) {
//        cursor.transform.position += new Vector3(cursorSpeed,0,0);
//        if(cursor.transform.position.x > 45) {
//            cursor.transform.position = new Vector3(45,cursor.transform.position.y,cursor.transform.position.z);
//        }
//        ObjectMove();
//    }
//    else if(Input.GetAxis("Horizontal") < 0) {
//        cursor.transform.position -= new Vector3(cursorSpeed,0,0);
//        if(cursor.transform.position.x < -50) {
//            cursor.transform.position = new Vector3(-50,cursor.transform.position.y,cursor.transform.position.z);
//        }
//        ObjectMove();
//    }

//    if(Input.GetAxis("Vertical") > 0) {
//        cursor.transform.position += new Vector3(0,0,cursorSpeed);
//        if(cursor.transform.position.z > 25) {
//            cursor.transform.position = new Vector3(cursor.transform.position.x,cursor.transform.position.y,25);
//        }
//        ObjectMove();
//    }
//    else if(Input.GetAxis("Vertical") < 0) {
//        cursor.transform.position -= new Vector3(0,0,cursorSpeed);
//        if(cursor.transform.position.z < -25) {
//            cursor.transform.position = new Vector3(cursor.transform.position.x,cursor.transform.position.y,-25);
//        }
//        ObjectMove();
//    }
//}

///// <summary>
///// 3Dオブジェクトの移動
///// </summary>
//private void ObjectMove() {
//    if(isChoice) {
//        float y = choicePosY;// + 1;
//        Vector3 newPos = new Vector3(cursor.transform.position.x,y,cursor.transform.position.z);
//        choiceObj.transform.position = newPos;
//    }
//}

///// <summary>
///// 3Dオブジェクトの回転
///// </summary>
//private void ObjectSpin() {
//    if(isChoice) {
//        if(Input.GetButton("Button_RB")) {
//            choiceObj.transform.Rotate(0,1,0);
//        }

//        if(Input.GetButton("Button_LB")) {
//            choiceObj.transform.Rotate(0,-1,0);
//        }

//    }
//}
#endregion