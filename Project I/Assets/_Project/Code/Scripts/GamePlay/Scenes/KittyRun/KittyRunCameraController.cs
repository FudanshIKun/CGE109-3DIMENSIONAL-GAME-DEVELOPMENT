using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wonderland.GamePlay.KittyRun
{
    public class KittyRunCameraController : MonoBehaviour
    {
        private Player targetPlayer;
        private KittyRunCat targetCat;
        private Transform targetCameraPosition;

        private void Awake()
        {
            targetPlayer = GameObject.FindWithTag("Player").GetComponent<Player>();
            targetCat = targetPlayer.gameObject.GetComponent<KittyRunCat>();
            targetCameraPosition = targetPlayer.gameObject.transform.GetChild(0).GetComponent<Transform>();
        }

        void Update()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, targetCameraPosition.position.z);
        }
    }
}
