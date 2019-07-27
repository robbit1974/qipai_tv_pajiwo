using UnityEngine;
using System.Collections;
using System.IO;
using System;

namespace LuaFramework {
    public class ResourceManager : Manager {
        private AssetBundle shared;

        private AssetBundle SoundBundle=null;

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void initialize(Action func) {
            if (AppConst.ExampleMode) {
                //------------------------------------Shared--------------------------------------
                string uri = Util.DataPath + "shared" + AppConst.ExtName;
                //Debug.LogWarning("LoadFile::>> " + uri);

                shared = AssetBundle.LoadFromFile(uri);
#if UNITY_5
                shared.LoadAsset("Dialog", typeof(GameObject));
#else
                shared.Load("Dialog", typeof(GameObject));
#endif
            }
            if (func != null) func();    //��Դ��ʼ����ɣ��ص���Ϸ��������ִ�к������� 
        }

        /// <summary>
        /// �����ز�
        /// </summary>
        public AssetBundle LoadBundle(string name) {
            string uri = Util.DataPath + name.ToLower() + AppConst.ExtName;
            Debug.Log(uri);
            AssetBundle bundle = AssetBundle.LoadFromFile(uri); //�������ݵ��زİ�
            return bundle;
        }
        /// <summary>
        /// ������Դ
        /// </summary>
        void OnDestroy() {
            if (shared != null) shared.Unload(true);
            if (SoundBundle != null) SoundBundle.Unload(true);
            Debug.Log("~ResourceManager was destroy!");
        }
        public void LoadAudioClip(string abName, string assetName, Action<AudioClip> GetAudio) 
        {
            AudioClip a = new AudioClip();
            if (SoundBundle == null)
            {
                string uri = Util.DataPath + abName + AppConst.ExtName;
                Debug.LogWarning("LoadFile::>> " + uri);
                SoundBundle = AssetBundle.LoadFromFile(uri);
            }
            a = SoundBundle.LoadAsset<AudioClip>(assetName);
            GetAudio(a);
        }
    }
}