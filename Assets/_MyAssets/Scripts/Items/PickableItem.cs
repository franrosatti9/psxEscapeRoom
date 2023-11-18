using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : PickUp
{ 
    protected override void Start()
    {
        base.Start();
    }

    public override void OnPickup()
    {
        GameManager.instance.GetPlayer.Inventory.PickItemUp(this);
        PlayInteractionEffects();
    }

    public override bool CanInteract()
    {
        return _interactable;
    }
}
