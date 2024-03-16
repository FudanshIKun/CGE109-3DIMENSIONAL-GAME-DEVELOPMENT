using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Wonderland.Client.MainWorld
{
    internal class DialogueSystem : System
    {
        [SerializeField] private RectTransform dialogueRect;
        [SerializeField] private TextMeshProUGUI dialogueText;

        private static readonly List<string> DialogueQueue = new();
        public static bool isDisplayingDialogue;
        public static bool isTyping;
        

        public static void CreateDialogue(string text)
        {
            DialogueQueue.Add(text);
        }

        public void StartDialogue()
        {
            if (!isDisplayingDialogue && DialogueQueue.Count > 0)
            {
                CustomLog.GamePlaySystem.Log("StartDialogue");
                isDisplayingDialogue = true;
                dialogueRect.gameObject.SetActive(true);
                dialogueText.text = DialogueQueue[0];
            }
        }

        public void NextDialogue()
        {
            if (DialogueQueue.Count > 0)
            {
                DialogueQueue.RemoveAt(0);
                if (DialogueQueue.Count > 0)
                {
                    dialogueText.text = DialogueQueue[0];
                }
                else
                {
                    isDisplayingDialogue = false;
                    dialogueRect.gameObject.SetActive(false);
                }
            }
        }
    }
}
