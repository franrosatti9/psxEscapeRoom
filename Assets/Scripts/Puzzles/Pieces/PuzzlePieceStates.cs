using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePieceStates : PuzzlePiece
{
    [SerializeField] private int correctState;
    [SerializeField] private int initialState = 0;
    [SerializeField] private int maxStates;
    
    [SerializeField] int _currentState;
    private Animator anim;
    protected override void Start()
    {
        anim = GetComponent<Animator>();
        SetInitialState();
    }

    public override bool CanInteract()
    {
        return _interactable;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnInteract()
    {
        ChangeState();
    }

    void ChangeState()
    {
        _currentState++;
        if (_currentState > maxStates - 1)
        {
            _currentState = 0;
        }
        // Check completion after delay
        Invoke(nameof(CheckPuzzleCompletion), 1f); 
        
        // ver como mover depende de tipo de pieza 
        anim.SetInteger("State", _currentState);
    }

    void SetInitialState()
    {
        _currentState = initialState;
        if (_currentState > maxStates - 1)
        {
            _currentState = 0;
        }
        
        anim.SetInteger("State", _currentState);
    }
    
    
    
    public override bool IsCorrect()
    {
        return _currentState == correctState;
    }

    protected override void CheckPuzzleCompletion()
    {
        CancelInvoke();
        puzzleParent.CheckCompletion();
    }
}
