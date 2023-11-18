using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    HashSet<ItemDataSO> _invItems = new HashSet<ItemDataSO>();

    public HashSet<ItemDataSO> InvItems => _invItems;

    [SerializeField] Transform pickUpParent;
    private PickUp inHandItem;

    public bool HasHandItem => inHandItem != null;
    public PickUp InHandItem => inHandItem;


    void Start()
    {
        Debug.Log(_invItems.Count);
    }
    
    void Update()
    {
        
    }

    public bool HasItem(ItemDataSO simpleItem)
    {
        // Si tiene el item o no se pide ninguno, se devuelve true
        return _invItems.Contains(simpleItem) || simpleItem == null;
        
    }

    public void AddItem(ItemDataSO simpleItem)
    {
        if (_invItems.Contains(simpleItem)) return;
        _invItems.Add(simpleItem);
        Debug.Log("Grabbed " + simpleItem);
    }

    public void PickItemUp(PickableItem item)
    {
        // Add inventory items like keys, that don't need to be in hand
        AddItem(item.Data);
        Destroy(item.gameObject);
    }
    public bool TryGrabItem(HoldableItem item)
    {
        // Already has item
        if(inHandItem != null) return false;
        
        item.gameObject.TryGetComponent(out Rigidbody rb);
        
        inHandItem = item;
        inHandItem.transform.SetParent(pickUpParent.transform, false);

        inHandItem.transform.localPosition = Vector3.zero;
        inHandItem.transform.localRotation = Quaternion.identity;

        if (rb != null)
        {
            rb.isKinematic = true;
        }
        inHandItem.SetInteractable(false);
        return true;
    }

    public HoldableItem SwapItems(HoldableItem newItem)
    {
        var handItem = (HoldableItem) inHandItem;
        
        inHandItem = newItem;
        inHandItem.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        inHandItem.transform.SetParent(pickUpParent.transform, false);
        
        inHandItem.transform.localPosition = Vector3.zero;
        inHandItem.transform.localRotation = Quaternion.identity;

        return handItem;
    }

    public PickUp GetAndPlaceItem()
    {
        var item = inHandItem;
        inHandItem = null;
        
        return item;
    }

    public void DropHandItem()
    {
        Debug.Log("Drop");
        inHandItem.transform.SetParent(null);
        
        inHandItem.gameObject.TryGetComponent(out Rigidbody rb);

        if (rb != null)
        {
            rb.isKinematic = false;
        }
        inHandItem.SetInteractable(true);
        inHandItem = null;
        
    }
}

public enum SimpleItemsEnum
{
    None,
    Axe,
    Shovel,
    Pickaxe,
    Flashlight
}
