using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    public List<DialogueElement> dialogues;
    private bool used = false;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !used)
        {
            used = true;
            //show dialog
            foreach (var d in dialogues)
            {
                DialoguesController.Current.AddDialogue(d);
            }
        }
    }
}
