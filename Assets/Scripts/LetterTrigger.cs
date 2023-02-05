using System;
using UI;
using UnityEngine;

public class LetterTrigger : MonoBehaviour
{
    [SerializeField, TextArea] private string content;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;
        
        InteractionController.Current.ShowInteraction(GetInstanceID(), "Letter", () =>
        {
            LetterController.Current.Show(content);
        });
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        InteractionController.Current.HideInteraction(GetInstanceID());
    }
}
