using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleCombination : PuzzleParent
{
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
        if (_puzzlePieces.All(p => p.IsCorrect()))
        {
            CompletePuzzle();
        }
    }

    public override void CompletePuzzle()
    {
        base.CompletePuzzle();
        
        // Can change completion event if needed
    }

    

    
}
