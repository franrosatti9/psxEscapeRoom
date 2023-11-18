using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePieceSequence : PuzzlePiece
{
    [SerializeField] private int id;
    
    private Animator anim;
    protected override void Start()
    {
        base.Start();
    }

    protected override void OnInteract()
    {
        PuzzleSequence parent = (PuzzleSequence)puzzleParent;
        
        parent.AddToSequence(id);
        
        // TODO: Maybe change anim to other class that handles generic animations
        anim.SetBool("Interacted", true);
        
    }

    public override bool IsCorrect()
    {
        //TODO: Clean code so classes don't have functions they don't use
        return true;
    }

    protected override void CheckPuzzleCompletion()
    {
        //TODO: Clean code so classes don't have functions they don't use
        return;
    }

    public void Reset()
    {
        anim.SetBool("Interacted", false);
        _interacted = false;
    }

    public override bool CanInteract()
    {
        return _interactable;
    }
}
