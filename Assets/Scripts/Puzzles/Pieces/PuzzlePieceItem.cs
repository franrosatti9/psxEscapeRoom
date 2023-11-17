using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzlePieceItem : PuzzlePiece
{
    [SerializeField] private Transform desiredItemTransform;
    [SerializeField] private ItemDataSO _requiredItem;
    [SerializeField] private ItemDataSO[] compatibleItems;
    private PickUp _currentItem;
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool Interact()
    {
        if (!_interactable) return false;

        StartCoroutine(InteractionCooldown());
        OnInteract();
        return true;
    }

    public override bool CanInteract()
    {
        return _interactable && (PlayerHasCompatibleItem() || _currentItem != null);
    }

    protected override void OnInteract()
    {
        if (_currentItem == null && 
            GameManager.instance.GetPlayer.Inventory.HasHandItem)
        {
            PlaceItem(GameManager.instance.GetPlayer.Inventory.GetAndPlaceItem());
            // Check completion after 1 second
            Invoke(nameof(CheckPuzzleCompletion), 1f);
        }
        else if (_currentItem != null)
        {
            TakeOrSwapItem();
        }
    }

    void PlaceItem(PickUp placedItem)
    {
        _currentItem = placedItem;
        _currentItem.SetInteractable(false);
        placedItem.transform.SetParent(this.transform, false);
        placedItem.transform.SetPositionAndRotation(desiredItemTransform.position, desiredItemTransform.rotation);
        
        
    }

    void TakeOrSwapItem()
    {
        /*if (GameManager.instance.GetPlayer.Inventory.TryGrabItem((HoldableItem)_currentItem))
        {
            _currentItem = null;
        }*/

        if (GameManager.instance.GetPlayer.Inventory.HasHandItem)
        {
            // TODO: CHECK IF IN HAND ITEM IS COMPATIBLE OR DONT SWAP 
            PlaceItem(GameManager.instance.GetPlayer.Inventory.SwapItems((HoldableItem) _currentItem));
        }
        else
        {
            GameManager.instance.GetPlayer.Inventory.TryGrabItem((HoldableItem)_currentItem);
            _currentItem = null;
        }
    }

    bool PlayerHasCompatibleItem()
    {
        var playerItem = GameManager.instance.GetPlayer.Inventory.InHandItem;

        return playerItem != null && compatibleItems.Any(data => data == playerItem.Data);
    }
    public override bool IsCorrect()
    {
        if (_currentItem == null) return false;
        
        return _currentItem.Data == _requiredItem;
    }

    protected override void CheckPuzzleCompletion()
    {
        CancelInvoke();
        puzzleParent.CheckCompletion();
    }
}
