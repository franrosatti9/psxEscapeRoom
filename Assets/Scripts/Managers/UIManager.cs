using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    
    [SerializeField] private Interactor playerInteractor;
    [SerializeField] private GameObject interactionUIObject;
    [SerializeField] private GameObject eventUIObject;
    [SerializeField] private TextMeshProUGUI eventName;
    [SerializeField] private TextMeshProUGUI eventSteps;
    [SerializeField] private TextMeshProUGUI interactionText;
    [SerializeField] private TextMeshProUGUI progressText;

    private bool _isDisplayingEvent;
    //public InteractionUI 
    private void Awake()
    {
        if (!instance) instance = this;
        else Destroy(this);
    }

    private void OnEnable()
    {
    }

    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayCurrentEvent(Event currentEvent)
    {
        SetupEventInfo(currentEvent);
        
        if(!_isDisplayingEvent) eventUIObject.SetActive(true);
        _isDisplayingEvent = false;
    }

    public void HideCurrentEvent()
    {
        eventUIObject.SetActive(false);
        _isDisplayingEvent = false;
    }

    public void ShowInteraction(IInteractable interactable)
    {
        interactionUIObject.SetActive(true);
        interactionText.text = interactable.GetInteractText();
    }

    public void HideInteraction()
    {
        interactionUIObject.SetActive(false);
        
    }

    public void SetupEventInfo(Event currentEvent)
    {
        //eventName.text = currentEvent.eventData.eventName;

        //eventSteps.text = currentEvent.GetStepsUIText();
    }
}
