using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Wonderland.Client.MainWorld
{
    public class TutorialSystem : MonoBehaviour
    {
        [ReadOnly] public bool hasTutorialMoveAndTurn;
        [ReadOnly] public bool hasTutorialAim;

        public void StartTutorial()
        {
            DialogueSystem.CreateDialogue("Try To Move With Left Joystick On Left Side of the screen.");
            DialogueSystem.CreateDialogue("Try To Look Around by swipe on the screen.");
            DialogueSystem.CreateDialogue("When You Look At the target, the precision indicator will show up on the screen, Try move close to those turret.");
            DialogueSystem.CreateDialogue("Press on right joystick to aim at a target, Release the right joystick at the perfect timing!.");
            DialogueSystem.CreateDialogue("Watch out! Those Turret can shoot at  you as well. Try moving around to dodge or stay in the cover, But dont worry! The god mode is open.");
            DialogueSystem.CreateDialogue("Can you see any yellow box?, Those are EnergySource!, Try move closer to it.");
            DialogueSystem.CreateDialogue("When you move close enough to any interactable objects, the interact button will show up, Try clicking it!");
            DialogueSystem.CreateDialogue("Each interactable has it own reaction when interacted, as you can see at the bottom of the screen, The energySource provide you it's energy to you!");
            DialogueSystem.CreateDialogue("Can you see any blue boxes on the ground? Those are items! You can try to interact with it, There is no inventory tho");
            DialogueSystem.CreateDialogue("Now, Destroy all the turret.");
            GameplayHandler.Instance.StartDialogue();
        }
    }
}
