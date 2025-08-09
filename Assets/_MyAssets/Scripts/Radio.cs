using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : InteractionBase
{
    [SerializeField] protected float interactCooldown = 1f;
    private AudioSource _audioSource;
    private bool _isOn;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    protected override void Start()
    {
        base.Start();
    }

    public override bool Interact()
    {
        if (!_interactable) return false;

        StartCoroutine(InteractionCooldown());
        PlayInteractionEffects();
        SwitchState();
        
        return true;
    }

    public override bool CanInteract()
    {
        return _interactable;
    }

    public void SwitchState()
    {
        _isOn = !_isOn;
        
        EnableMusic(_isOn);
    }

    void EnableMusic(bool isEnabled)
    {
        if(isEnabled) _audioSource.PlayDelayed(.5f);
        else _audioSource.Stop();
    }

    public void SetAudioClip(AudioClip clip)
    {
        _audioSource.clip = clip;
    }
    
    IEnumerator InteractionCooldown()
    {
        _interactable = false;
        yield return new WaitForSeconds(interactCooldown);
        _interactable = true;
    }
}
