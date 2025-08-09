using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    bool Interact();

    bool CanInteract();
    public bool PlayerHasRequiredItem();
    bool Interacted();

    public void SetInteractable(bool isEnabled);

}
