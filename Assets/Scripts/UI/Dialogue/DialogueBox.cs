using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    public List<DialogueElement> dialogues;
    private bool used = false;
    public bool force = false;
    public bool disableScreams = true;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !used)
        {
            used = true;
            //show dialog
            if(force) DialoguesController.Current.ClearDialogues();

            if (disableScreams)
            {
                dialogues.First().screamsEnabled = false;
                dialogues.Last().screamsEnabled = true;
            }

            foreach (var d in dialogues)
            {
                DialoguesController.Current.AddDialogue(d);
            }
        }
    }
}
