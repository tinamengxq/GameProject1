using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance {get; private set;}

    private readonly Queue<string> lines = new Queue<string>();
    private Sprite currentSpeaker;

    private bool active;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }
    private void Update()
    {
        if (!active) return;

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Next();
        }
    }

    public void StartDialogue(Sprite speaker, IEnumerable<string> dialogueLines)
    {
        lines.Clear();
        foreach (var l in dialogueLines) lines.Enqueue(l);

        currentSpeaker = speaker;
        active = true;
        UIController.Instance.ShowDialogue(currentSpeaker, lines.Peek());
    }

    private void Next()
    {
        if (lines.Count == 0)
        {
            End();
            return;
        }

        lines.Dequeue();
        if (lines.Count == 0)
        {
            End();
            return;
        }

        UIController.Instance.ShowDialogue(currentSpeaker, lines.Peek());
    }

    private void End()
    {
        active = false;
        UIController.Instance.HideDialogue();
    }


}
