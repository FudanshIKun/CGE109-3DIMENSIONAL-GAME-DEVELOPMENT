using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Wonderland.Scene.MainWorld
{
    internal class InteractionSystem : System
    {
        [Header("UI")]
        [SerializeField] private Transform interactableContainer;
        [SerializeField] private GameObject interactableUiPrefab;
        [Space(5)]
        public readonly Dictionary<int, GameObject> _uiObjects = new();
        
        public void GenerateInteractionUI()
        {
            var objectsToRemove = new List<int>();

            foreach (var interactableId in _uiObjects.Keys.Where(interactableId => !DetectionSystem.InteractableObjects.ContainsKey(interactableId)))
            {
                Destroy(_uiObjects[interactableId]);
                objectsToRemove.Add(interactableId);
            }

            foreach (var interactableId in objectsToRemove)
            {
                _uiObjects?.Remove(interactableId);
            }

            foreach (var interactable in DetectionSystem.InteractableObjects.Values)
            {
                if (_uiObjects == null || _uiObjects.ContainsKey(interactable.id)) continue;
                var uiObject = Instantiate(interactableUiPrefab, interactableContainer);
                var uiText = uiObject.GetComponentInChildren<TextMeshProUGUI>();
                uiText.text = interactable.name;
                var uiButton = uiObject.GetComponent<Button>();
                uiButton.onClick.AddListener(interactable.Interaction);
                _uiObjects?.Add(interactable.id, uiObject);
            }
        }
    }
}
