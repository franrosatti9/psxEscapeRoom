using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldableItem : PickUp
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start(); 
    }

    public override void OnPickup()
    {
        PlayInteractionEffects();
        GameManager.instance.GetPlayer.Inventory.TryGrabItem(this);
    }

    public override bool CanInteract()
    {
        return _interactable;
    }
}
