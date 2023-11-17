using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractAnim : InteractionBase, IInteractable
{
    [SerializeField] protected bool interactOnce = false;
    [SerializeField] protected float interactCooldown = 1f;
    
    [SerializeField] private int initialState = 0;
    [SerializeField] private int maxStates;
    
    [SerializeField] int _currentState;
    
    [SerializeField] Animator anim;
    
    protected override void Start()
    {

    }

    public override bool Interact()
    {
        if (!_interactable || (_interacted && interactOnce)) return false;

        if (interactOnce)
        {
            _interacted = true;
            _interactable = false;
        }

        StartCoroutine(InteractionCooldown());
        
        ChangeAnimState();
        return true;
    }
    
    void ChangeAnimState()
    {
        _currentState++;
        
        if (_currentState > maxStates - 1)
        {
            _currentState = 0;
        }

        // ver como mover depende de tipo de pieza 
        anim.SetInteger("State", _currentState);
    }

    IEnumerator InteractionCooldown()
    {
        _interactable = false;
        yield return new WaitForSeconds(interactCooldown);
        _interactable = true;
    }

    public override void SetInteractable(bool isEnabled)
    {
        // Force Interactable set, regardless of the cooldown
        StopAllCoroutines();
        GetComponent<Collider>().enabled = isEnabled;
        _interactable = isEnabled;
        Debug.Log("Set interactable");
    }

    public override bool CanInteract()
    {
        return _interactable;
    }
}
