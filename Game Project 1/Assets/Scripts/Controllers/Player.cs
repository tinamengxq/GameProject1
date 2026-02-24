using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]private float playerSpeed = 5f;
    private Vector3 movement;

    [Header("Assets")]
    [SerializeField]private Animator _animator;
    [SerializeField]private Rigidbody2D _rigidbody;

    [Header("Inventory")]
    public Inventory inventory = new Inventory();

    [Header("Interaction")]
    public KeyCode interactKey = KeyCode.F;
    private IInteractable currentInteractable;

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(interactKey) && currentInteractable != null)
        {
            currentInteractable.Interact(this);
        }        
    }

    private void FixedUpdate()
    {
        transform.position += movement * playerSpeed * Time.deltaTime;
    }

    public void SetInteractable(IInteractable interactable)
    {
        currentInteractable = interactable;
    }

    public void ClearInteractable(IInteractable interactable)
    {
        if(currentInteractable == interactable)
        {
            currentInteractable = null;
        }
    }

    private void OggerEnter2D(Collider2D collision)
    {
        var interactable = collision.GetComponent<IInteractable>();
        if(interactable != null)
        {
            SetInteractable(interactable);
        }
    }

    private void OnD(Collider2D collision)
    {
        var interactable = collision.GetComponent<IInteractable>();
        if (interactable != null)
        {
            ClearInteractable(interactable);
        }
    }
}
