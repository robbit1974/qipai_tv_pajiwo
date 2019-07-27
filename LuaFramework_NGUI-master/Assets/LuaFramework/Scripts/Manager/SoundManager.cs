//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//public enum SoundType
//{
//    Bg = 0,
//    Effect = 1,
//    MyHuman = 2,
//    HumanTop = 3,
//    MyHumanRight = 4,
//    MyHumanLeft = 5
//}
//namespace LuaFramework {
//    public class SoundManager : Manager {
//        private AudioSource audio;//bg,effect,human

//        private Hashtable sounds = new Hashtable();


//        void Start() {
//            audio = GetComponent<AudioSource>();
//        }

//        /// <summary>
//        /// 添加一个声音
//        /// </summary>
//        void Add(string key, AudioClip value)
//        {
//            if (sounds[key] != null || value == null) return;
//            sounds.Add(key, value);
//        }

//        /// <summary>
//        /// 获取一个声音
//        /// </summary>
//        AudioClip Get(string key)
//        {
//            if (sounds[key] == null) return null;
//            return sounds[key] as AudioClip;
//        }

//        /// <summary>
//        /// 载入一个音频
//        /// </summary>
//        public AudioClip LoadAudioClip(string path)
//        {
//            AudioClip ac = Get(path);
//            if (ac == null)
//            {
//                ac = (AudioClip)Resources.Load(path, typeof(AudioClip));
//                Add(path, ac);
//            }
//            return ac;
//        }

//        /// <summary>
//        /// 是否播放背景音乐，默认是1：播放
//        /// </summary>
//        /// <returns></returns>
//        public bool CanPlayBackSound()
//        {
//            string key = AppConst.AppPrefix + "BackSound";
//            int i = PlayerPrefs.GetInt(key, 1);
//            return i == 1;
//        }

//        /// <summary>
//        /// 播放背景音乐
//        /// </summary>
//        /// <param name="canPlay"></param>
//        public void PlayBacksound(string name, bool canPlay)
//        {
//            if (audio.clip != null)
//            {
//                if (name.IndexOf(audio.clip.name) > -1)
//                {
//                    if (!canPlay)
//                    {
//                        audio.Stop();
//                        audio.clip = null;
//                        Util.ClearMemory();
//                    }
//                    return;
//                }
//            }
//            if (canPlay)
//            {
//                audio.loop = true;
//                audio.clip = LoadAudioClip(name);
//                audio.Play();
//            }
//            else
//            {
//                audio.Stop();
//                audio.clip = null;
//                Util.ClearMemory();
//            }
//        }

//        /// <summary>
//        /// 是否播放音效,默认是1：播放
//        /// </summary>
//        /// <returns></returns>
//        public bool CanPlaySoundEffect()
//        {
//            string key = AppConst.AppPrefix + "SoundEffect";
//            int i = PlayerPrefs.GetInt(key, 1);
//            return i == 1;
//        }

//        /// <summary>
//        /// 播放音频剪辑
//        /// </summary>
//        /// <param name="clip"></param>
//        /// <param name="position"></param>
//        public void Play(AudioClip clip, Vector3 position)
//        {
//            if (!CanPlaySoundEffect()) return;
//            AudioSource.PlayClipAtPoint(clip, position);
//        }
//    }
//}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LuaFramework
{
    public class SoundManager : Manager
    {
        private  AudioSource audio;
        private Hashtable sounds = new Hashtable();
        string backSoundKey = "";

        public float BgVolum { get; private set; }

        public float EffectVolum { get; private set; }

        void Start()
        {
            audio = GetComponent<AudioSource>();
            if (audio == null)
               audio=gameObject.AddComponent<AudioSource>();
            BgVolum = PlayerPrefs.GetFloat("Bg",1f);
            EffectVolum = PlayerPrefs.GetFloat("Effect", 1f);
            audio.volume = BgVolum;
        }

        //回调函数原型
        private delegate void GetBack(AudioClip clip, string key);

        //获取声音资源
        private void Get(string abName, string assetName, GetBack cb)
        {
            string key = abName + "." + assetName;
            if (sounds[key] == null)
            {
                ResManager.LoadAudioClip(abName, assetName, (obj) =>
                {
                    if (obj == null)
                    {
                        Debug.Log("PlayBackSound fail");

                        cb(null, key);
                        return;
                    }
                    else
                    {
                        sounds.Add(key, obj);
                        cb(obj as AudioClip, key);
                        return;
                    }
                });
            }
            else
            {
                cb(sounds[key] as AudioClip, key);
                return;
            }
        }


        //播放背景音乐
        public void PlayBackSound(string abName, string assetName)
        {
            backSoundKey = abName + "." + assetName;
            Get(abName, assetName, (clip, key) =>
            {
                if (clip == null)
                    return;
                if (key != backSoundKey)
                    return;

                audio.loop = true;
                audio.clip = clip;
                audio.Play();
            });
        }
        //停止背景音乐
        public void StopBackSound()
        {
            backSoundKey = "";
            audio.Stop();
        }
        //播放音效
        public void PlaySound(string abName, string assetName)
        {
            Get(abName, assetName, (clip, key) =>
            {

                if (clip == null)
                    return;
                if (Camera.main == null)
                    return;
                AudioSource.PlayClipAtPoint(clip,
                            Camera.main.transform.position,EffectVolum);
            });
        }
        public void SetBgVolum()
        {
            float volum=PlayerPrefs.GetFloat("Bg")>=1?0:1;
            if (audio == null) return;
            audio.volume = volum;
            PlayerPrefs.SetFloat("Bg",volum);
        }

        public void SetEffectVolum()
        {
            float volum = PlayerPrefs.GetFloat("Effect") >= 1 ? 0 : 1;
            EffectVolum = volum;
            PlayerPrefs.SetFloat("Effect", volum);
        }

        public float GetBgVolum()
        {
            return PlayerPrefs.GetFloat("Bg", 1);
        }
        public float GetEffectVolum()
        {
            return PlayerPrefs.GetFloat("Effect", 1);
        }
    }
}