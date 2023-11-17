using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Interactor : MonoBehaviour
{
    private IInteractable _objectToInteract;
    private IInteractable _lastObjectToInteract;
    
    private float _interactablesFound;
    public bool interactionEnabled = true;
    private bool _canInteract;
    [SerializeField] Camera _cam;
    [SerializeField] private float interactRange;
    [SerializeField] private float interactRadius;
    [SerializeField] private Transform interactPoint;
    [SerializeField] private LayerMask interactableMask;
    
    [SerializeField] private Image crosshair;
    [SerializeField] private Sprite interactOnSprite;
    [SerializeField] private Sprite interactOffSprite;
    
    private PlayerInventory _inventory;
    private bool fpsInteractionMode = true;
    Collider[] colliders = new Collider[1];
    void Start()
    {
        _cam = Camera.main;
        _inventory = GetComponent<PlayerInventory>();
    }

    void Update()
    {
        // CHANGE LATER SO IT DOESNT CHANGE EVERY FRAME WHEN LOCKED
        if (fpsInteractionMode)
        {
            crosshair.rectTransform.anchoredPosition = Vector3.zero;
        }
        else
        {
            crosshair.transform.position = Input.mousePosition;
            
        }

        crosshair.sprite = _canInteract ? interactOnSprite : interactOffSprite;
        
        if(!interactionEnabled) return;
        
        if (Input.GetKeyDown(KeyCode.E) && fpsInteractionMode ||
            Input.GetMouseButtonDown(0) && !fpsInteractionMode)
        {
            
            
            if (_canInteract)
            {
                // Return if interaction was successful
                if(_objectToInteract.Interact()) return;
            }
            
            // Drop item if on movement mode, and didn't interact
            if (_inventory.HasHandItem && GameManager.instance.gameState == GameState.Moving)
            {
                _inventory.DropHandItem();
            }

            
        }

    }

    private void FixedUpdate()
    {
        if(!interactionEnabled) return;
        
        if (fpsInteractionMode)
        {
            if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out RaycastHit hit, interactRange,
                    interactableMask,
                    QueryTriggerInteraction.Collide))
            {
                _objectToInteract = hit.collider.gameObject.GetComponent<IInteractable>();

                if (_objectToInteract != _lastObjectToInteract) _lastObjectToInteract?.StopHighlight();

                if (_objectToInteract != null)
                {
                    if (_objectToInteract.CanInteract())
                    {
                        _lastObjectToInteract = _objectToInteract;
                        _objectToInteract.Highlight();
                        _canInteract = true;
                    }
                    else
                    {
                        _canInteract = false;
                        _objectToInteract.StopHighlight();
                    }
                }
                else _canInteract = false;

            }
            else
            {
                _lastObjectToInteract?.StopHighlight();
                _lastObjectToInteract = null;
                _canInteract = false;
            }
        }
        else
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            //Vector3 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, interactRange,
                    interactableMask,
                    QueryTriggerInteraction.Collide))
            {
                _objectToInteract = hit.collider.gameObject.GetComponent<IInteractable>();

                if (_objectToInteract != _lastObjectToInteract) _lastObjectToInteract?.StopHighlight();

                if (_objectToInteract != null)
                {
                    if (_objectToInteract.CanInteract())
                    {
                        _lastObjectToInteract = _objectToInteract;
                        _objectToInteract.Highlight();
                        _canInteract = true;
                    }
                    else
                    {
                        _canInteract = false;
                        _objectToInteract.StopHighlight();
                    }
                }
                else _canInteract = false;

            }
            else
            {
                _lastObjectToInteract?.StopHighlight();
                _lastObjectToInteract = null;
                _canInteract = false;
            }
        }
    }

    public IInteractable GetInteractable()
    {
        return _objectToInteract;
    }

    public void SetInteractionEnabled(bool isEnabled)
    {
        interactionEnabled = isEnabled;
        if (isEnabled == false) _canInteract = false;
    }

    public void SetFpsInteractionMode(bool fpsModeEnabled)
    {
        fpsInteractionMode = fpsModeEnabled;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(_cam.transform.position, interactPoint.position);
    }
}
