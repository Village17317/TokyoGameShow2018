/*
 *	作成者     :村上和樹
 *	機能説明   :
 * 	初回作成日 :2018/02/01
 *	最終更新日 :2018/02/01
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Village {

    public class SoundManager: MonoBehaviour {

        private static SoundManager instance;
        /***********************************************/
        [System.Serializable]
        class BgmStandardClass {
            public string soundKey = null;
            public AudioClip soundClip = null;
            [Range(0,1)] public float volume = 1;
            [Range(-3,3)] public float pitch = 1;
            public bool isLoop = false;
        }
        [System.Serializable]
        class SeStandardClass {
            public string soundKey = null;
            public AudioClip soundClip = null;
            [Range(0,1)]  public float volume = 1;
            [Range(-3,3)] public float pitch = 1;
            public float deleteTime = 0;
        }
        /***********************************************/
        [SerializeField] private BgmStandardClass[] bgmClass = new BgmStandardClass[0];
        [SerializeField] private SeStandardClass[] seClass = new SeStandardClass[0];
        public Dictionary<string,AudioClip> soundData = new Dictionary<string,AudioClip>();
        private AudioClip nullBgmClip = null;
        private AudioClip nullSeClip = null;
        [SerializeField] private AudioSource bgmAudio;
        [SerializeField] private GameObject seAudio;
        
        public static SoundManager Instance {
            get {
                if(null == instance) {
                    instance = (SoundManager) FindObjectOfType(typeof(SoundManager));
                    if(null == instance) {
                        Debug.Log(" SoundManager Instance Error ");
                    }
                }
                return instance;
            }
        }

        void Awake() {
            GameObject[] soundObj = GameObject.FindGameObjectsWithTag("SoundManager");
            if(1 < soundObj.Length) {// 既に存在しているなら削除
                Destroy(gameObject);
            }
            else {// シーン遷移では破棄させない
                DontDestroyOnLoad(gameObject);
            }
            for(int i = 0;i < bgmClass.Length;i++) {
                soundData.Add(bgmClass[i].soundKey,bgmClass[i].soundClip);

            }
            for(int j = 0;j < seClass.Length;j++) {
                soundData.Add(seClass[j].soundKey,seClass[j].soundClip);
            }
        }

        public void PlayBGM(string soundKey) {
            AudioClip tmp = nullBgmClip;
            if(soundData.TryGetValue(soundKey,out nullBgmClip)) {
                if(tmp != nullBgmClip || !bgmAudio.isPlaying) {
                    bgmAudio.clip = nullBgmClip;
                    bgmAudio.volume = GetBgmVolume(soundKey);
                    bgmAudio.pitch = GetBgmPitch(soundKey);
                    bgmAudio.loop = GetIsLoop(soundKey);
                    bgmAudio.Play();
                }
            }
            else {
                Debug.Log(soundKey + "に対応したBGMはありません");
            }
        }
        public void StopBGM() {
            bgmAudio.Stop();
        }

        public void PlaySE(string soundKey,Transform tf) {
            if(soundData.TryGetValue(soundKey,out nullSeClip)) {
                GameObject obj = Instantiate(seAudio,tf) as GameObject;
                obj.GetComponent<AudioSource>().clip = nullSeClip;
                obj.GetComponent<AudioSource>().volume = GetSeVolume(soundKey);
                obj.GetComponent<AudioSource>().pitch = GetSePitch(soundKey);
                obj.GetComponent<AudioSource>().Play();
                Destroy(obj,GetSeDeleteTime(soundKey));
            }
            else {
                Debug.Log(soundKey + "に対応したBGMはありません");
            }
        }

        private float GetBgmVolume(string soundKey) {
            float returnQty = 0;

            for(int i = 0;i < bgmClass.Length;i++) {
                if(soundKey == bgmClass[i].soundKey) {
                    returnQty = bgmClass[i].volume;
                }
            }
            return returnQty;
        }
        private float GetBgmPitch(string soundKey) {
            float returnQty = 0;

            for(int i = 0;i < bgmClass.Length;i++) {
                if(soundKey == bgmClass[i].soundKey) {
                    returnQty = bgmClass[i].pitch;
                }
            }
            return returnQty;
        }

        private bool GetIsLoop(string soundKey) {
            bool isloop = false;
            for(int i = 0;i < bgmClass.Length;i++) {
                if(soundKey == bgmClass[i].soundKey) {
                    isloop = bgmClass[i].isLoop;
                }
            }
            return isloop;
        }

        private float GetSeVolume(string soundKey) {
            float returnQty = 0;
            for(int i = 0;i < seClass.Length;i++) {
                if(soundKey == seClass[i].soundKey) {
                    returnQty = seClass[i].volume;
                }
            }
            return returnQty;
        }

        private float GetSePitch(string soundKey) {
            float returnQty = 0;
            for(int i = 0;i < seClass.Length;i++) {
                if(soundKey == seClass[i].soundKey) {
                    returnQty = seClass[i].pitch;
                }
            }
            return returnQty;
        }

        private float GetSeDeleteTime(string soundKey) {
            float returnQty = 0;
            for(int i = 0;i < seClass.Length;i++) {
                if(soundKey == seClass[i].soundKey) {
                    returnQty = seClass[i].deleteTime;
                }
            }
            return returnQty;
        }
        public int GetBGMLength() {
            return bgmClass.Length;
        }

        public int GetSELength() {
            return seClass.Length;
        }
    }

}