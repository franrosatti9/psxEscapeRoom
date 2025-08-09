using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class InteractionBase : MonoBehaviour, IInteractable
{
    [Header("Base Interaction")]
    [SerializeField] private InteractionFX[] interactionEffects;

    [SerializeField] bool startAsInteractable = true;

    protected bool _interacted = false;
    protected bool _interactable;
    
    public virtual bool Interacted() => _interacted;


    [FormerlySerializedAs("requiredItem")] [SerializeField] protected ItemDataSO requiredSimpleItem;
    protected virtual void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactable");
        _interactable = startAsInteractable;
        interactionEffects = GetComponents<InteractionFX>();

    }

    public abstract bool Interact();

    public abstract bool CanInteract();

    public virtual bool PlayerHasRequiredItem()
    {
        if (requiredSimpleItem == null) return true;
        
        return GameManager.instance.GetPlayer.Inventory.HasItem(requiredSimpleItem);
    }
    
    public virtual void SetInteractable(bool isEnabled)
    {
        GetComponent<Collider>().enabled = isEnabled;
        _interactable = isEnabled;
    }

    protected virtual void PlayInteractionEffects()
    {
        if (interactionEffects.Length <= 0) return;

        for (int i = 0; i < interactionEffects.Length; i++)
        {
            interactionEffects[0].OnInteract();
        }
    }
}
