using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PromptUI : MonoBehaviour
{
    public Player player;
    public TMP_Text promptText;

    private IInteractable last;

    private void Update()
    {
        Collider2D hit = Physics2D.OverlapCircle(player.transform.position, 0.6f);
        if(hit == null)
        {
            promptText.text = "";
            return;
        }

        var interactable = hit.GetComponent<IInteractable>();
        if(interactable == null)
        {
            promptText.text = "";
            return;
        }

        promptText.text = interactable.GetPrompt(player);
    }
}
