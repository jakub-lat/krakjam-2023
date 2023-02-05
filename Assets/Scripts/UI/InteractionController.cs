using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public struct InteractionData
    {
        public string text;
        public Action action;
        public int instanceID;
    }
    
    public class InteractionController : MonoSingleton<InteractionController>
    {
        [SerializeField] private Text interactionText;
        
        private CanvasGroup canvasGroup;
        
        private InteractionData? currentInteraction;

        private Dictionary<int, InteractionData> possibleInteractions = new();

        public bool InteractionAvailable => currentInteraction != null;

        protected override void Awake()
        {
            base.Awake();
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
            HideInteraction();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) && currentInteraction != null && !LetterController.Current.Visible)
            {
                currentInteraction.Value.action();
            }
        }

        public void ShowInteraction(int instanceID, string text, Action action)
        {
            canvasGroup.DOFade(1, 0.2f);
            
            var data = new InteractionData
            {
                action = action,
                text = text,
                instanceID = instanceID,
            };
            
            currentInteraction = data;
            interactionText.text = $"[E] {text}";
            if (!possibleInteractions.ContainsKey(instanceID))
            {
                possibleInteractions.Add(instanceID, data);
            }
        }

        public void HideInteraction(int? instanceID = null)
        {
            if (instanceID != null)
            {
                possibleInteractions.Remove(instanceID.Value);

                if (currentInteraction != null && instanceID == currentInteraction.Value.instanceID)
                {
                    currentInteraction = null;
                }
            }

            if (possibleInteractions.Count == 0)
            {
                canvasGroup.DOFade(0, 0.2f);
            }
            else
            {
                var x = possibleInteractions.First().Value;
                ShowInteraction(x.instanceID, x.text, x.action);
            }
        }
    }
}
