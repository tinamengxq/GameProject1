using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public interface IInteractable
{
    void Interact(PlayerController2D player);
    string GetPrompt(PlayerController2D player);
}
