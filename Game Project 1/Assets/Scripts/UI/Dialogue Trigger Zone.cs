using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerZone : MonoBehaviour
{
    [Header("Dialogue Content")]
    [SerializeField] private Sprite speaker;
    [TextArea(2, 4)]
    [SerializeField] private string[] lines;

    [Header("Settings")]
    [SerializeField] private bool triggerOnce = true;

    private bool used;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (used && triggerOnce) return;
        if (!other.CompareTag("Player")) return;

        used = true;
        DialogueManager.Instance.StartDialogue(speaker, lines);
    }
}
