using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    public List<DialogueElement> dialogues;
    private bool used = false;
    public bool force = false;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !used)
        {
            used = true;
            //show dialog
            if(force) DialoguesController.Current.ClearDialogues();
            foreach (var d in dialogues)
            {
                DialoguesController.Current.AddDialogue(d);
            }
        }
    }
}
