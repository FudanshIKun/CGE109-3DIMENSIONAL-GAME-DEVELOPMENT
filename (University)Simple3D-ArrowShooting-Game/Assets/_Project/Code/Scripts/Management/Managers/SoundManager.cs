using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Wonderland.Management
{
    public class SoundManager : SerializedMonoBehaviour
    {
        public static SoundManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public AudioSource mainSpeaker;
    }
}
