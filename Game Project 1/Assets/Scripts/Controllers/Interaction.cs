using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField]private Transform interactPoint;
    [SerializeField]private float interactRadius = 0.6f;
    [SerializeField]private LayerMask interactLayer;

    private IInteractable current;

    private void Update()
    {
        FindInteractable();

        if (current != null)
        {
            UIController.Instance.ShowInteractPrompt(current.GetPrompt());

            if (Input.GetKeyDown(KeyCode.F))
            {
                current.Interact();
            }
        }
        else
        {
            UIController.Instance.HideInteractPrompt();
        }
    }

    private void FindInteractable()
    {
        Collider2D hit = Physics2D.OverlapCircle(interactPoint.position, interactRadius, interactLayer);
        current = hit != null ? hit.GetComponent<IInteractable>() : null;
    }

    private void OnDrawGizmosSelected()
    {
        if (interactPoint == null) return;
        Gizmos.DrawWireSphere(interactPoint.position, interactRadius);
    }

}
