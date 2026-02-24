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

    [Header("Inventory")]
    public Inventory inventory = new Inventory();

    [Header("Interaction")]
    public KeyCode interactKey = KeyCode.F;
    



    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        transform.position += movement * playerSpeed * Time.deltaTime;
    }
}
