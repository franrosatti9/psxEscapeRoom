using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public abstract class PuzzleParent : InteractionBase, IFocusable
{
    [SerializeField] private Transform cameraFocusTransform;
    [SerializeField] private float focusTime;
    [SerializeField] protected PuzzlePiece[] _puzzlePieces;
    [SerializeField] private bool isButtonChecked;
    private bool isFocused = false;

    protected bool completed = false;
    public UnityEvent OnCompletePuzzle;

    public bool IsFocused => isFocused;


    public Transform FocusTransform => cameraFocusTransform;
    
    protected override void Start()
    {
        // If it needs focus to interact, disable pieces at the beginning
        // and set this as their puzzle parent
        SetInteractable(true);
        InitializePieces();
    }

    public abstract void CheckCompletion();

    public virtual void OnButtonPressed()
    {
        if(!isButtonChecked) return;
        CheckCompletion();
    }
    public virtual void CompletePuzzle()
    {
        // In case it checks twice
        if(completed) return;
        
        // Invoke event and disable pieces
        completed = true;
        OnCompletePuzzle.Invoke();
        SetPiecesInteractable(false);
    }

    public override bool Interact()
    {
        if(isFocused || !_interactable) return false;
        
        Focus();
        return true;

        //throw new System.NotImplementedException();
    }
    

    public void Focus()
    {
        Debug.Log("Interacted");
        GameManager.instance.FocusObject(this);
        isFocused = true;

        
        // Make all pieces in puzzle interactable, unless it's completed already
        if (completed) return;

        SetInteractable(false);
        SetPiecesInteractable(true);
    }

    public void Unfocus()
    {
        isFocused = false;
        
        // Disable interactability in all pieces
        if (completed) return;
        
        SetInteractable(true);
        SetPiecesInteractable(false);
        
    }

    void InitializePieces()
    {
        for (int i = 0; i < _puzzlePieces.Length; i++)
        {
            _puzzlePieces[i].SetParent(this);
        }
        SetPiecesInteractable(false);
    }

    public void SetPiecesInteractable(bool enable)
    {
        for (int i = 0; i < _puzzlePieces.Length; i++)
        {
            _puzzlePieces[i].SetInteractable(enable);
        }
    }
    
    public override void SetInteractable(bool isEnabled)
    {
        GetComponent<Collider>().enabled = isEnabled;
        _interactable = isEnabled;
    }
    

    
}
