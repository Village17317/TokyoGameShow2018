/*
 *	作成者     :村上和樹
 *	機能説明   :Updateの代わりにRunを回す
 * 	初回作成日 :2018/03/05
 *	最終更新日 :2018/03/05
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

namespace Village {
	public class UpdateManager : MonoBehaviour
	{
        [Header("Inheritorクラスを継承したクラスを格納する"),SerializeField]
        private List<Inheritor> updateList = new List<Inheritor>();

		private void Start(){
            int i = 0;
            while(i < updateList.Count) {
                if(updateList[i] == null) {
                    Delete(i);
                }
                else {
                    updateList[i].Init();
                    i++;
                }

            }
        }


	    private void Update() {
            int i = 0;
            while(i < updateList.Count) {
                if(updateList[i] == null) {
                    Delete(i);
                }
                else {
                    if(updateList[i].gameObject.activeInHierarchy) {
                        updateList[i].Run();
                    }
                    i++;
                }

            }
	    }

        public void Delete(int deleteNumber) {
            updateList.RemoveAt(deleteNumber);
        }

        #region ExpansionClass
        public void SetObject() {
            updateList.Clear();
            foreach(Inheritor obj in FindObjectsOfType(typeof(Inheritor))) {
                updateList.Add(obj.GetComponent<Inheritor>());
            }
        }

	#if UNITY_EDITOR
	    [CustomEditor(typeof(UpdateManager))]
	    public class UpdateManagerEditor : Editor
	    {
            ReorderableList m_list;

            private void OnEnable() {
                var prop = serializedObject.FindProperty("updateList");

                m_list = new ReorderableList(serializedObject,prop);
                m_list.elementHeight = 25;
                m_list.drawElementCallback = (rect,index,isActive,isFocused) => {
                    var element = prop.GetArrayElementAtIndex(index);
                    rect.height -= 4;
                    rect.y += 2;
                    EditorGUI.PropertyField(rect,element);
                };
                m_list.drawHeaderCallback = (rect) => { EditorGUI.LabelField(rect,"一覧"); };
                m_list.drawElementBackgroundCallback = (rect,index,isActive,isFocused) => {
                    GUI.backgroundColor = Color.cyan;
                };
            }

            public override void OnInspectorGUI()
	        {
	        	DrawDefaultInspector();
	            UpdateManager _target = target as UpdateManager;
                serializedObject.Update();
                m_list.DoLayoutList();//見れるようになる
                serializedObject.ApplyModifiedProperties();

                if(GUILayout.Button("SetButton")) {
                    _target.SetObject();
                }
            }
	    }
	#endif
	#endregion
	}
}
