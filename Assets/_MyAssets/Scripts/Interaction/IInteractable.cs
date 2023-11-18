using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    bool Interact();

    bool CanInteract();
    void Highlight();
    void StopHighlight();
    string GetInteractText();
    bool Interacted();
    bool Highlighted();

    public void SetInteractable(bool isEnabled);

}
