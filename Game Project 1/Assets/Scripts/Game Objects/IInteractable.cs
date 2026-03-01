using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public interface IInteractable
{
    void Interact();
    string GetPrompt();
}
