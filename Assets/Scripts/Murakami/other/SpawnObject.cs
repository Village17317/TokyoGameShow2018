/*
 *	作成者     :
 *	機能説明   :
 * 	初回作成日 :
 *	最終更新日 :
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Village {
    public class SpawnObject: MonoBehaviour {
        [Header("複数生成するオブジェクト")]
        public GameObject spawnGameObject;
        [Header("複製する個数")]
        public int qty = 0;
        [Header("ずらす幅")]
        public Vector3 addWidth;

        [Header("親オブジェクト")]
        public GameObject ParentObj;
        #region ExpansionClass
#if UNITY_EDITOR
        [CustomEditor(typeof(SpawnObject))]
        public class SpawnObjectEditor: Editor {
            public override void OnInspectorGUI() {
                DrawDefaultInspector();
                SpawnObject _target = target as SpawnObject;
                if(GUILayout.Button("Spawn")) {
                    if(_target.qty == 1) {
                        GameObject prefab = Instantiate(_target.spawnGameObject) as GameObject;
                        if(_target.ParentObj != null) {
                            prefab.transform.parent = _target.ParentObj.transform;
                        }
                        prefab.name = _target.spawnGameObject.name;
                        prefab.transform.position = _target.spawnGameObject.transform.position;
                        prefab.transform.rotation = _target.spawnGameObject.transform.rotation;
                        prefab.transform.localScale = _target.spawnGameObject.transform.localScale;

                    }
                    else {
                        for(int i = 1;i <= _target.qty;i++) {
                            GameObject prefab = Instantiate(_target.spawnGameObject) as GameObject;
                            if(_target.ParentObj != null) {
                                prefab.transform.parent = _target.ParentObj.transform;
                            }
                            prefab.name = _target.spawnGameObject.name;
                            Vector3 posOrigin = _target.spawnGameObject.transform.position;
                            prefab.transform.position = new Vector3(posOrigin.x + i * _target.addWidth.x,
                                                                    posOrigin.y + i * _target.addWidth.y,
                                                                    posOrigin.z + i * _target.addWidth.z);

                        }
                    }


                }
            }
        }
#endif
        #endregion
    }
}

