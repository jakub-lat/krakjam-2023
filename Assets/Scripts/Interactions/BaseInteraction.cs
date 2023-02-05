using System;
using Player;
using UI;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class BaseInteraction : MonoBehaviour
    {
        protected abstract string InteractionText { get; }
        protected abstract void Interact();

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.CompareTag("Player")) return;
        
            InteractionController.Current.ShowInteraction(GetInstanceID(), InteractionText, Interact);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            InteractionController.Current.HideInteraction(GetInstanceID());
        }

        private void OnDestroy()
        {
            InteractionController.Current.HideInteraction(GetInstanceID());
        }
    }
}
