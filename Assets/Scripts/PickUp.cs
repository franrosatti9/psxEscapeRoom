using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUp : InteractionBase, IPickupeable
{
    private SphereCollider _collider;
    [SerializeField] private ItemDataSO data;

    public ItemDataSO Data => data;
    
    protected override void Start()
    {
        base.Start();
        SetInteractable(true);
    }

    public abstract void OnPickup();
    protected void OnTriggerEnter(Collider other)
    {
        /*
        var inventory = other.transform.GetComponent<PlayerInventory>();

        if (inventory)
        {
            OnPickup();
        }
        */
        
  
    }

    public override bool Interact()
    {
        if (!_interactable) return false;
        OnPickup();
        return true;
        //GameManager.instance.GetPlayer.Inventory.TryGrabItem(this);
    }

    
    public override void SetInteractable(bool isEnabled)
    {
        GetComponent<Collider>().enabled = isEnabled;
        _interactable = isEnabled;
    }
}
