using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]private float playerSpeed = 2f;
    [SerializeField]private Animator _animator;
    [SerializeField]private Rigidbody2D _rigidbody;
    private Vector2 movement;

    void Start()
    {
        
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        _rigidbody.velocity = playerSpeed * movement * Time.deltaTime;
    }
}
