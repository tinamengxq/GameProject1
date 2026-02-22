using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]private float playerSpeed = 5f;
    [SerializeField]private Animator _animator;
    //[SerializeField]private Rigidbody2D _rigidbody;
    private Vector3 movement;

    void Start()
    {
        
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        transform.position += movement * playerSpeed * Time.deltaTime;
    }
}
