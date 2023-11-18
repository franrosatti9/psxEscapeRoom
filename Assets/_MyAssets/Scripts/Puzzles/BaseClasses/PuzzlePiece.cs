using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PuzzlePiece : InteractionBase
{
    
    [SerializeField] protected bool interactOnce = false;
    [SerializeField] protected float interactCooldown = 1f;
    [SerializeField] protected PuzzleParent puzzleParent;
    
    protected override void Start()
    {
        //puzzleParent = GetComponentInParent<PuzzleParent>();
    }

    public override bool Interact()
    {
        if (!_interactable) return false;
        
        if (interactOnce) _interacted = true;

        if(puzzleParent.CooldownAllPieces) puzzleParent.StartPiecesCooldown();
        else StartCooldown();
        
        OnInteract();
        return true;


    }

    protected abstract void OnInteract();

    public abstract bool IsCorrect();

    protected abstract void CheckPuzzleCompletion();

    public void StartCooldown()
    {
        StartCoroutine(InteractionCooldown());
    }
    protected IEnumerator InteractionCooldown()
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
    }

    public void SetParent(PuzzleParent parent)
    {
        puzzleParent = parent;
    }
}