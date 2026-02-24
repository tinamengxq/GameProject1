using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public interface IInteractable
{
    void Interact(Player player);
    string GetPrompt(Player player);
}
