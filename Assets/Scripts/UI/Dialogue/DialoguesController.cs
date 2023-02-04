using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Utils;

[Serializable]
public class DialogueElement
{
    public DialogueElement(string p, string t, float tt)
    {
        personName = p;
        contentText = t;
        timeout = tt;
    }
    public string personName;
    public string contentText;
    public float timeout = 1.5f;
}



public class DialoguesController : MonoSingleton<DialoguesController>
{
    public Queue<DialogueElement> dialogues;
    public GameObject dialogueBox;

    public Text header;
    public Text content;
    
    private DialogueElement current;
    private float hideTimer = 0;
    private bool hidden = true;

    private void Start()
    {
        dialogues = new Queue<DialogueElement>();
        Next();
    }

    public void Show(bool show = true)
    {
        dialogueBox.SetActive(show);
        if (show)
        {
            header.text = current.personName;
            content.text = current.contentText;
            
            hideTimer = current.timeout;
            hidden = false;
        }
        else hidden = true;
    }

    private void Update()
    {
        hideTimer -= Time.deltaTime;
        if (!hidden && hideTimer <= 0)
        {
            Show(false);
        }
        
        if(Input.GetKeyDown(KeyCode.Space)) Next();
    }

    public void Next()
    {
        if (dialogues.Count <= 0)
        {
            Show(false);
            return;
        }
        current = dialogues.First();
        dialogues.Dequeue();
        Show();
    }

    public void AddDialogue(string t, string p="", float tt=1.5f)
    {
        dialogues.Enqueue(new DialogueElement(p,t,tt));
        if (hidden)
        {
            Next();
        }
    }
    
    public void AddDialogue(DialogueElement d)
    {
        dialogues.Enqueue(d);
        if (hidden)
        {
            Next();
        }
    }

    public void ClearDialogues()
    {
        dialogues.Clear();
    }
    
}
