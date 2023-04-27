using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wonderland.GamePlay.BeatRunner
{
    public class PlayerHandler : Management.PlayerHandler
    {
        [Header("Gameplay")]
        [SerializeField] private Player player;

        private void Start()
        {
            GameplayHandler.Instance.Player = player;
        }
    }
}
