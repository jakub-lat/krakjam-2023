using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UI.Dialogue;
using UnityEngine;
using UnityEngine.Serialization;
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
    public int displayerIndex;
}


public class DialoguesController : MonoSingleton<DialoguesController>
{
    public Queue<DialogueElement> dialogues;
    public List<DialogueDisplayer> dialogueDisplayers;

    private DialogueElement current;
    private float hideTimer = 0;
    private bool hidden = true;

    private void Start()
    {
        dialogues = new Queue<DialogueElement>();
        Next();
    }

    public void Show(int displayerIndex, bool show = true)
    {
        foreach (var d in dialogueDisplayers)
        {
            d.gameObject.SetActive(false);
        }

        if (show)
        {
            var displayer = dialogueDisplayers[displayerIndex];
            displayer.gameObject.SetActive(true);

            displayer.header.text = current.personName;
            displayer.content.text = current.contentText;

            hideTimer = current.timeout;
            hidden = false;
        }
        else
        {
            hidden = true;
        }
    }

    private void Update()
    {
        hideTimer -= Time.deltaTime;
        if (!hidden && hideTimer <= 0)
        {
            Next();
        }

        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.E)) Next();
    }

    public void Next()
    {
        if (dialogues.Count <= 0)
        {
            Show(-1, false);
            return;
        }

        current = dialogues.First();
        dialogues.Dequeue();
        Show(current.displayerIndex);
    }

    public void AddDialogue(string t, string p = "", float tt = 1.5f)
    {
        dialogues.Enqueue(new DialogueElement(p, t, tt));
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
