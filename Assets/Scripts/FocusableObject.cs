using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusableObject : InteractionBase, IFocusable
{
    [SerializeField] private Transform cameraFocusTransform;
    [SerializeField] private float focusTime;
    [SerializeField] protected InteractionBase[] _subInteractableObjects;
    [SerializeField] private bool startInteractable;
    private bool isFocused = false;


    public bool IsFocused => isFocused;
    public Transform FocusTransform => cameraFocusTransform;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        SetInteractable(startInteractable);
        SetObjectsInteractable(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override bool Interact()
    {
        if(isFocused) return false;
        
        Focus();
        return true;

        //throw new System.NotImplementedException();
    }

    public override bool CanInteract()
    {
        return _interactable;
    }

    public override void Highlight()
    {
        //throw new System.NotImplementedException();
    }

    public override void StopHighlight()
    {
        //throw new System.NotImplementedException();
    }
    


    public void Focus()
    {
        Debug.Log("Interacted");
        GameManager.instance.FocusObject(this);
        isFocused = true;

        
        // Make all pieces in puzzle interactable, unless it's completed already
        //if (completed) return;
        SetInteractable(false);
        if(_subInteractableObjects.Length > 0) SetObjectsInteractable(true);
        
    }

    public void Unfocus()
    {
        isFocused = false;
        
        // Disable interactability in all pieces
        //if (completed) return;
        SetInteractable(true);
        if(_subInteractableObjects.Length > 0) SetObjectsInteractable(true);
        
    }
    
    public void SetObjectsInteractable(bool enable)
    {
        for (int i = 0; i < _subInteractableObjects.Length; i++)
        {
            _subInteractableObjects[i].SetInteractable(enable);
        }
    }
}
