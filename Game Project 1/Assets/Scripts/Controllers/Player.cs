using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]private float playerSpeed = 5f;
    private Vector3 moveInput;
    private Vector2 lastMoveDir = Vector2.right;
    public Vector2 LastMoveDirection => lastMoveDir;
    private bool facingLeft = false;

    [Header("Assets")]
    [SerializeField]private Animator _animator;
    [SerializeField]private Rigidbody2D _rigidbody;
    [SerializeField]private SpriteRenderer _spriteRenderer;

    [Header("Inventory")]
    public Inventory inventory = new Inventory();

    [Header("Interaction")]
    public KeyCode interactKey = KeyCode.F;
    private IInteractable currentInteractable;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = 0f;
        _rigidbody.freezeRotation = true;
    }
    private void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        if(moveInput != new Vector3(0,0,0))
        {
            _animator.SetBool("Walking", true);
        }
        else
        {
            _animator.SetBool("Walking", false);
        }

        if(Input.GetKeyDown(interactKey) && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
        if (moveInput.sqrMagnitude > 0.01f)
        {
            moveInput = moveInput.normalized;
            lastMoveDir = moveInput;
        }

        if(Input.GetAxisRaw("Horizontal") < 0 && !facingLeft)
        {
            _spriteRenderer.flipX = true;
            facingLeft = true;
        }
        else if(Input.GetAxisRaw("Horizontal") > 0 && facingLeft)
        {
           _spriteRenderer.flipX = false;
            facingLeft = false;
        }
    }

    private void FixedUpdate()
    {
        transform.position += moveInput * playerSpeed * Time.deltaTime;
    }

}
