using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PuzzleSequence : PuzzleParent
{
    [SerializeField] private int[] expectedSequence;
    [SerializeField] private bool checkWhenFinished;
    private int[] currentSequence;
    
    protected override void Start()
    {
        base.Start();
    }

    public override bool CanInteract()
    {
        return _interactable;
    }

    public override void CheckCompletion()
    {
        for (int i = 0; i < currentSequence.Length; i++)
        {
            // Continue iteration if input is correct
            if (currentSequence[i] == expectedSequence[i]) continue;
            
            ResetAttempt();
            FailAnimation();
            
            return;
        }
        
        if(currentSequence.Length == expectedSequence.Length) CompletePuzzle();
    }

    public void ClearSequence()
    {
        Array.Clear(expectedSequence, 0, expectedSequence.Length);
    }

    public void AddToSequence(int id)
    {
        currentSequence = currentSequence.Append(id).ToArray();
        
        // Check if correct every time player inputs or only when all inputs are done
        if(!checkWhenFinished) CheckCompletion();
        else if(currentSequence.Length == expectedSequence.Length) CheckCompletion();
        
    }

    void ResetAttempt()
    {
        ClearSequence();
        for (int i = 0; i < _puzzlePieces.Length; i++)
        {
            PuzzlePieceSequence piece = (PuzzlePieceSequence) _puzzlePieces[i];
            piece.Reset();
        }
    }

    void FailAnimation()
    {
        
    }

    public override void CompletePuzzle()
    {
        base.CompletePuzzle();
        
        // Can change completion event if needed
    }
}
